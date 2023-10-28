using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SaveEditor.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct Data0x4DC
    {
        public const int SIZE = 0x8C4;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x7C)]
        public byte[] NameTable; //Name StringTable

        public float Unk0x558, Unk0x55C, Unk0x560, Unk0x564;
        public uint Unk0x568, Unk0x56C;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public ushort[] PartyMember; //0x570, 998 = default value (no member)

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public uint[] Unk0x57C;

        public float Unk0x594, Unk0x598, Unk0x59C;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x800)]
        public byte[] StringTable; //Actions StringTable

        public string GetKiritoName()
        {
            string result = "";

            using (var ms = new MemoryStream(NameTable))
            using (var br = new BinaryReader(ms))
            {
                result = Encoding.GetEncoding("shift-jis").GetString(br.ReadBytes(8));
            }

            return result.Replace("\0", "");
        }

        public void SetKiritoName(string Name)
        {
            byte[] NameBuffer = new byte[8];
            byte[] buffer = new byte[8];

            if(Name.Length >= 1)
                NameBuffer = Encoding.GetEncoding("shift-jis").GetBytes(Name);

            for (int i = 0; i < 8; i++)
            {
                if(i < Name.Length)
                    buffer[i] = NameBuffer[i];
                else
                    buffer[i] = 0;
            }

            using (var ms = new MemoryStream(NameTable))
            using (var bw = new BinaryWriter(ms))
            {
                bw.SeekTo(0); bw.Write(buffer);
                //bw.SeekTo(32); bw.Write(buffer);
                bw.SeekTo(48); bw.Write(buffer);
                //bw.SeekTo(88); bw.Write(buffer);
            }
        }

    }
}
