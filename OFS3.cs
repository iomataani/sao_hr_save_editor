using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaveEditor
{
    public class OFS3
    {
        public List<long> Offset;
        public List<int> Size;
        public List<int> ID;
        public List<long> OffsetName;
        public List<string> FileName;

        byte[] magic;
        public uint HeaderSize { get; set; }
        public uint EntryCount { get; set; }
        public byte[] HeaderFlags { get; set; }
        public uint FileNameTablePointer { get; set; }

        public OFS3()
        {
            Offset = new List<long>();
            Size = new List<int>();
            ID = new List<int>();
            OffsetName = new List<long>();
            FileName = new List<string>();
            magic = GetMagic();
            HeaderSize = 0;
            HeaderFlags = new byte[4];
            FileNameTablePointer = 0;
            EntryCount = 0;
        }
        
        #region Helper functions

        public byte[] GetMagic() => new byte[] { 0x4F, 0x46, 0x53, 0x33 };
        public bool HasNames => HeaderFlags[0] == 2;
        public byte Alignment => HeaderFlags[2];
        public bool IsIdTable => HeaderFlags[3] == 1;

        #endregion
        
        public void Load(string sPath)
        {
            if (File.Exists(sPath))
                Load(File.ReadAllBytes(sPath));
        }

        public void Load(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                magic = br.ReadBytes(4);

                if (!Util.CompareByteArrays(magic, GetMagic()))
                    return;

                HeaderSize = br.ReadUInt32();
                HeaderFlags = br.ReadBytes(4);
                FileNameTablePointer = br.ReadUInt32();
                EntryCount = br.ReadUInt32();

                //note: if there are no names, then ofsNameTbl points to the end of file
                Offset = new List<long>();
                Size = new List<int>();
                ID = new List<int>();
                OffsetName = new List<long>();
                FileName = new List<string>();

                for (int i = 0; i < EntryCount; i++)
                {
                    var uiTemp = br.ReadUInt32();

                    if (uiTemp == 0)
                        Offset.Add(uiTemp);
                    else
                        Offset.Add(HeaderSize + uiTemp);

                    if (IsIdTable)
                    {
                        Size.Add(0);
                        ID.Add(br.ReadInt32());
                    }
                    else
                    {
                        Size.Add(br.ReadInt32());
                        ID.Add(-1);
                    }

                    if (HasNames)
                    {
                        uiTemp = br.ReadUInt32();

                        if (uiTemp == 0)
                            OffsetName.Add(uiTemp);
                        else
                            OffsetName.Add(HeaderSize + uiTemp);
                    } 
                }

                if (IsIdTable)
                {
                    for (int i = 0; i < EntryCount; i++)
                    {
                        if (i == EntryCount - 1)
                            Size[i] = (int)(br.BaseStream.Length - Offset[i]);
                        else
                            Size[i] = (int)(Offset[i + 1] - Offset[i]);
                    }
                }

                if (HasNames)
                {
                    for (int i = 0; i < EntryCount; i++)
                    {
                        if (OffsetName[i] == 0)
                        {
                            FileName.Add("dummy_file_" + i.ToString("D4") + ".dat");
                            continue;
                        }

                        br.BaseStream.Seek(OffsetName[i], SeekOrigin.Begin);
                        FileName.Add(Util.ReadCString(br));
                    }
                }
            }
        }

        public void CreateFileSystem(string sPath)
        {
            OFS3 temp = new OFS3();

            byte[] result = null;

            string outName = "", sTemp = "";
            string WorkDir = Path.GetDirectoryName(sPath) + "\\";

            long Position = 0;
            int size = 0;

            using (var sr = new StreamReader(sPath))
            {
                outName = sr.ReadLine().Replace("file:", "");
                sr.ReadLine(); //flags:

                for (int i = 0; i < 4; i++)
                {
                    temp.HeaderFlags[i] = (byte)int.Parse(sr.ReadLine());
                }

                temp.EntryCount = uint.Parse(sr.ReadLine().Replace("count:", ""));

                sTemp = sr.ReadLine();

                if (sTemp == "idlist:")
                {
                    for (int i = 0; i < temp.EntryCount; i++)
                    {
                        temp.ID.Add(int.Parse(sr.ReadLine()));
                    }
                    sTemp = sr.ReadLine(); //filelist:
                }

                for (int i = 0; i < temp.EntryCount; i++)
                {
                    temp.FileName.Add(sr.ReadLine());
                }
            }

            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(this.GetMagic());
                    bw.Write((int)0x10);
                    bw.Write(temp.HeaderFlags);
                    bw.Write((int)0x0);
                    bw.Write(temp.EntryCount);

                    Position = 0x14;

                    if (temp.HasNames)
                        Position += temp.EntryCount * 12;
                    else
                        Position += temp.EntryCount * 8;

                    Position = Util.Align(Position, temp.Alignment);

                    //write filetable
                    for (int i = 0; i < temp.EntryCount; i++)
                    {
                        temp.Offset.Add(Position);
                        bw.Write((uint)(Position - 0x10));

                        size = (int)Util.GetFileSize(WorkDir + temp.FileName[i]);
                        temp.Size.Add(size);

                        if (temp.IsIdTable)
                            bw.Write(temp.ID[i]);
                        else
                            bw.Write(size);

                        if (temp.HasNames)
                            bw.Write((int)0);

                        Position += Util.Align(size, temp.Alignment);
                    }

                    Util.WritePadding(bw, Util.Align(Position, temp.Alignment) - Position);
                    temp.FileNameTablePointer = (uint)Position;


                    //update header with nametable position
                    bw.BaseStream.Seek(0xC, SeekOrigin.Begin);
                    bw.Write((uint)(temp.FileNameTablePointer - 0x10));

                    //build & write nametable offsets
                    if (temp.HasNames)
                    {
                        Position = temp.FileNameTablePointer;

                        for (int i = 0; i < temp.EntryCount; i++)
                        {
                            temp.OffsetName.Add(Position);
                            Position += Encoding.ASCII.GetByteCount(temp.FileName[i]) + 1;
                        }

                        Position = 0x1C;
                        for (int i = 0; i < temp.EntryCount; i++)
                        {
                            bw.BaseStream.Seek(Position, SeekOrigin.Begin);
                            bw.Write((uint)(temp.OffsetName[i] - 0x10));
                            Position += 0xC;
                        }
                    }

                    //write files to offset
                    for (int i = 0; i < temp.EntryCount; i++)
                    {
                        bw.BaseStream.Seek(temp.Offset[i], SeekOrigin.Begin);
                        bw.Write(File.ReadAllBytes(WorkDir + temp.FileName[i]));
                        Util.WritePadding(bw, Util.Align(bw.BaseStream.Position, temp.Alignment) - bw.BaseStream.Position);
                    }

                    //write names to offset
                    if (temp.HasNames)
                    {
                        for (int i = 0; i < temp.EntryCount; i++)
                        {
                            bw.BaseStream.Seek(temp.OffsetName[i], SeekOrigin.Begin);
                            bw.Write(Encoding.ASCII.GetBytes(temp.FileName[i]));
                            bw.Write((byte)0);
                        }
                        Util.WritePadding(bw, Util.Align(bw.BaseStream.Position, temp.Alignment) - bw.BaseStream.Position);
                    }
                }

                result = ms.ToArray();
            }

            Util.DeleteFile(WorkDir + outName + ".new");
            File.WriteAllBytes(WorkDir + outName + ".new", result);

            result = null;
        }

        public void CreateStringSystem(string sPath)
        {
            OFS3 temp = new OFS3();
            byte[] result = null;
            string outName = Path.GetFileName(sPath), WorkDir = Path.GetDirectoryName(sPath) + "\\";
            Encoding enc;

            using (var sr = new StreamReader(sPath, Encoding.UTF8))
            {
                //flags
                var sArrTemp = sr.ReadLine().Split(';');
                for (int i = 0; i < 4; i++)
                {
                    temp.HeaderFlags[i] = (byte)int.Parse(sArrTemp[i+1]);
                }

                //count
                sArrTemp = sr.ReadLine().Split(';');
                temp.EntryCount = uint.Parse(sArrTemp[1]);

                //encoding
                sArrTemp = sr.ReadLine().Split(';');
                if(sArrTemp[1] == "utf8")
                    enc = Encoding.UTF8;
                else if(sArrTemp[1] == "utf16le")
                    enc = Encoding.Unicode;
                else
                    enc = Encoding.ASCII;

                //strings
                for (int i = 0; i < temp.EntryCount; i++)
                {
                    sArrTemp = sr.ReadLine().Split(';');
                    temp.ID.Add(int.Parse(sArrTemp[1]));
                    temp.FileName.Add(ProcessTextCommands(sArrTemp[2]));
                }
            }

            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(this.GetMagic());
                    bw.Write((int)0x10);
                    bw.Write(temp.HeaderFlags);
                    bw.Write((int)0x0);
                    bw.Write(temp.EntryCount);

                    //ignore "has names" flag, id tables doesn't have filenames
                    long Position = 0x14;
                    Position += temp.EntryCount * 8;
                    Position = Util.Align(Position, temp.Alignment);

                    //write filetable
                    for (int i = 0; i < temp.EntryCount; i++)
                    {
                        temp.Offset.Add(Position);
                        bw.Write((uint)(Position - 0x10));

                        var size = (int)Util.Align(enc.GetByteCount(temp.FileName[i]), temp.Alignment, false);

                        temp.Size.Add(size);

                        if (temp.IsIdTable)
                            bw.Write(temp.ID[i]);
                        else
                            bw.Write(size);

                        Position += Util.Align(size, temp.Alignment);
                    }

                    Util.WritePadding(bw, Util.Align(Position, temp.Alignment) - Position);
                    temp.FileNameTablePointer = (uint)Position;

                    //update header with nametable position
                    bw.BaseStream.Seek(0xC, SeekOrigin.Begin);
                    bw.Write((uint)(temp.FileNameTablePointer - 0x10));

                    //write strings to offset
                    for (int i = 0; i < temp.EntryCount; i++)
                    {
                        bw.BaseStream.Seek(temp.Offset[i], SeekOrigin.Begin);
                        bw.Write(enc.GetBytes(temp.FileName[i]));
                        Util.WritePadding(bw, Util.Align(bw.BaseStream.Position, temp.Alignment, false) - bw.BaseStream.Position);
                    }

                }
                result = ms.ToArray();
            }

            Util.DeleteFile(WorkDir + outName + ".new");
            File.WriteAllBytes(WorkDir + outName + ".new", result);

            result = null;
        }

        private string ProcessTextCommands(string input)
        {
            string result = "";
            //.Replace("\\r", "\r").Replace("\\n", "\n")

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\\')
                {
                    i++;

                    if (input[i] == 'n')
                        result += "\n";
                    else if (input[i] == 'r')
                        result += "\r";
                    else if (input[i] == 'x')
                    {
                        var sTemp = ""; i++;
                        sTemp += input[i]; i++;
                        sTemp += input[i];
                        result += (char)(Convert.ToByte(sTemp, 16));
                    }
                    else 
                        result += "\\";
                }
                else
                    result += input[i];
            }


            return result;
        }

    }
}
