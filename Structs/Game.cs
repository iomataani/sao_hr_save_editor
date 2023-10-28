using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Security.Cryptography;

namespace SaveEditor.Structs
{
    public class Game
    {
        public const ulong PC_INIT = 0xD2355296BBD59289u, PC_MULTI = 0x1B9C3B918F638Fu;
        public const ulong VITA_INIT = 0x6B7A3B48BA2DB978u, VITA_MULTI = 0xD85FCD9BE08D1u;
        public const ulong SWITCH_INIT = 0xA7E58677CC302AF8u, SWITCH_MULTI = 0xD85FCD9BE08D1u; //unused, because switch uses the vita values...

        public const int SIZE = 0x80400;
        public const int MAX_CHARACTER = 330;

        private byte[] SaveData;
        public SaveGameType currentType;

        public byte[] Hash1; //0x0
        public uint unk0x14;
        public uint unk0x18, unk0x1C, unk0x20;
        public Playtime playtime;
        public uint unk0x28;
        public byte[] unk0x2C; //0x2C, 0x4B0
        public Data0x4DC data0x4DC; //0x4DC, 0x8C4
        public List<SkillMain> MainSkills; //0xDA0
        public List<SkillSub> SubSkills; //0xF20
        public byte[] Unk0x3F20; //0x3F20
        public List<CharacterData> CharacterDatas; //0x3F28, 330
        public byte[] Unk0x1C1E0; //0x1C1E0, 0x800
        public List<CharacterEmotion> CharacterEmotions; //0x1C9E0, 330
        public Data0x2CBB0 Data0x2CBB0; //0x2CBB0, 0xDF54, Quests and more
        public List<SkillPalette> SkillPalettes; //0x3AB04, 0x39D0, (20 * 185 * 4)
        public byte[] Unk0x3E4D4; //0x3E4D4, 0x50
        public byte[] Unk0x3E524; //0x3E524, 0x60
        public byte[] Unk0x3E584; //0x3E584, 0x320
        public List<Mail> Mails; //0x3E8A4, 1000
        public byte[] Unk0x42724; //0x42724, 0x6C, Version2 = 0x28
        public byte[] Unk0x42790; //0x42790, 0x550
        public byte[] Unk0x42CE0; //0x42CE0, 0x50
        public byte[] Unk0x42D30; //0x42D30, 0x28
        public byte[] Unk0x42D58; //0x42D58, 0x3DE0
        public byte[] Unk0x46B38; //0x46B38, 0x80, Version2 = 0x84
        public List<Data0x46BB8> data0x46BB8; //0x46BB8, 200
        public byte[] EnigmaOrder; //0x47838, 0x68B4
        public SkillFusion SkillFusion; //0x4E0EC, 0x34
        public byte[] Unk0x4E120; //0x4E120, 0xD0
        public List<Mail> Unk0x4E1F0; //0x4E1F0, 500
        public List<Equipment> Equip; //0x50130
        public List<Item> Inventory; //0x50270, 100
        public List<Item> Storage; //0x50BD0, 1000
        public List<Item> data0x56990; //0x56990, 7, special quest/npc items?
        public uint Col; //0x56A38
        public ulong Unk0x56A3C; //0x56A3C
        public ulong Unk0x56A44; //0x56A44
        public ushort Unk0x56A4C; //0x56A4C
        public byte Unk0x56A4E; //0x56A4E
        public byte[] Unk0x56A4F; //0x56A4F, 0x39, padding?
        public List<Data0x56A88> data0x56A88; //0x56A88, 330, Characters3
        public byte[] Unk0x5D1A8; //0x5D1A8, 0x3358
        public List<Data0x60500> data0x60500; //0x60500, 3
        public byte[] Unk0x607E8; //0x607E8, 0x12C0
        public byte[] Unk0x61AA8; //0x61AA8, 0x960
        public int Version; //0x62408
        public List<Data0x6240C> data0x6240C; //0x6240C, 352, Event/Map Flags?
        public uint Unk0x78F0C; //0x78F0C, padding
        public ulong Hash2; //0x78F10
        public List<Data0x78F18> data0x78F18; //0x78F18, 330, Characters4
        public byte[] End0x7AE08; //0x7AE08, 0x55F8, end of file, structure is unclear...

        //button configuration?
        private readonly byte[] Unk0x42724_Version2_Vita = {
            0x12, 0x10, 0x13, 0x11, 0x15, 0x19, 0x18, 0x16, 0x1C, 0x20, 0x17, 0x1E, 0x14, 0x10, 0x02, 0x03, 
            0x04, 0x12, 0x15, 0x12, 0x10, 0x13, 0x11, 0x15, 0x25, 0x24, 0x24, 0x21, 0x22, 0x25, 0x29, 0x14, 
            0x10, 0x02, 0x03, 0x04, 0x15, 0x12, 0x00, 0x00
        };
        private readonly byte[] Unk0x42724_Version2_Switch = {
            0x12, 0x10, 0x13, 0x11, 0x15, 0x19, 0x18, 0x16, 0x1C, 0x20, 0x17, 0x1E, 0x14, 0x10, 0x02, 0x03, 
            0x04, 0x12, 0x15, 0x12, 0x10, 0x13, 0x11, 0x15, 0x25, 0x24, 0x24, 0x21, 0x22, 0x25, 0x29, 0x14, 
            0x10, 0x02, 0x03, 0x04, 0x12, 0x15, 0x00, 0x00
        };
        private readonly byte[] Unk0x42724_Version3 = {
            0x12, 0x10, 0x13, 0x11, 0x15, 0x19, 0x18, 0x16, 0x1C, 0x20, 0x17, 0x1E, 0x14, 0x10, 0x02, 0x03, 
            0x04, 0x12, 0x15, 0x12, 0x10, 0x13, 0x11, 0x15, 0x25, 0x24, 0x24, 0x21, 0x22, 0x25, 0x29, 0x14, 
            0x10, 0x02, 0x03, 0x04, 0x12, 0x15, 0x12, 0x10, 0x13, 0x11, 0x15, 0x19, 0x18, 0x16, 0x1C, 0x20,
            0x17, 0x1E, 0x14, 0x10, 0x02, 0x03, 0x04, 0x12, 0x15, 0x43, 0x3F, 0x2D, 0x30, 0x35, 0x37, 0x36, 
            0x38, 0x5C, 0x5E, 0x57, 0x3A, 0x47, 0x41, 0x3B, 0x39, 0x34, 0x3D, 0x5B, 0x5C, 0x46, 0x33, 0x31, 
            0x44, 0x53, 0x2F, 0x4C, 0x4E, 0x4D, 0x4F, 0x48, 0x4A, 0x49, 0x4B, 0x34, 0x52, 0x42, 0x53, 0x54,
            0x41, 0x3A, 0x51, 0x5F, 0x60, 0x01, 0x14, 0x15, 0x11, 0x13, 0x5D, 0x22
        };

        private void Init()
        {
            MainSkills = new List<SkillMain>();
            SubSkills = new List<SkillSub>();
            CharacterDatas = new List<CharacterData>();
            CharacterEmotions = new List<CharacterEmotion>();
            SkillPalettes = new List<SkillPalette>();
            data0x46BB8 = new List<Data0x46BB8>();
            Mails = new List<Mail>();
            Unk0x4E1F0 = new List<Mail>();
            Equip = new List<Equipment>();
            Inventory = new List<Item>();
            Storage = new List<Item>();
            data0x56990 = new List<Item>();
            data0x56A88 = new List<Data0x56A88>();
            data0x60500 = new List<Data0x60500>();
            data0x6240C = new List<Data0x6240C>();
            data0x78F18 = new List<Data0x78F18>();
        }

        public void Read(string sPath)
        {
            SaveData = File.ReadAllBytes(sPath);

            if (SaveData.Length != SIZE)
            {
                throw new InvalidDataException("FileSize mismatch!");
            }

            Init();

            using (var ms = new MemoryStream(SaveData))
            using (var br = new BinaryReader(ms))
            {
                br.SeekTo(0x623C8);
                Version = br.ReadInt32();

                if (Version != 2)
                {
                    br.SeekTo(0x62408);
                    Version = br.ReadInt32();

                    if (Version != 3)
                    {
                        throw new InvalidDataException($"Save Version mismatch, expected 2 or 3, got {Version}!");
                    }
                }
                
                GetTypeFromHash(SaveData);

                br.SeekTo(0);

                Hash1 = br.ReadBytes(0x14);
                unk0x14 = br.ReadUInt32();
                unk0x18 = br.ReadUInt32();
                unk0x1C = br.ReadUInt32();
                unk0x20 = br.ReadUInt32();
                playtime = Util.ReadStructure<Playtime>(br.ReadBytes(Playtime.SIZE));
                unk0x28 = br.ReadUInt32();
                unk0x2C = br.ReadBytes(0x4B0);
                data0x4DC = Util.ReadStructure<Data0x4DC>(br.ReadBytes(Data0x4DC.SIZE));
                for (int i = 0; i < 32; i++) MainSkills.Add(Util.ReadStructure<SkillMain>(br.ReadBytes(SkillMain.SIZE)));  //0xDA0
                for (int i = 0; i < 1024; i++) SubSkills.Add(Util.ReadStructure<SkillSub>(br.ReadBytes(SkillSub.SIZE))); //0xF20
                Unk0x3F20 = br.ReadBytes(0x8); //0x3F20
                for (int i = 0; i < MAX_CHARACTER; i++) CharacterDatas.Add(Util.ReadStructure<CharacterData>(br.ReadBytes(CharacterData.SIZE))); //0x3F28
                Unk0x1C1E0 = br.ReadBytes(0x800); //0x1C1E0
                for (int i = 0; i < MAX_CHARACTER; i++) CharacterEmotions.Add(Util.ReadStructure<CharacterEmotion>(br.ReadBytes(CharacterEmotion.SIZE))); //0x1C9E0
                Data0x2CBB0 = Util.ReadStructure<Data0x2CBB0>(br.ReadBytes(Data0x2CBB0.SIZE)); //0x2CBB0
                for (int i = 0; i < 20; i++) SkillPalettes.Add(Util.ReadStructure<SkillPalette>(br.ReadBytes(SkillPalette.SIZE))); //0x3AB04
                Unk0x3E4D4 = br.ReadBytes(0x50); //0x3E4D4
                Unk0x3E524 = br.ReadBytes(0x60); //0x3E524
                Unk0x3E584 = br.ReadBytes(0x320); //0x3E584 
                for (int i = 0; i < 1000; i++) Mails.Add(Util.ReadStructure<Mail>(br.ReadBytes(Mail.SIZE))); //0x3E8A4

                //after this the console version is different

                if (Version == 2)
                {
                    Unk0x42724 = br.ReadBytes(0x28); //0x42724, -44 byte?
                    Unk0x42790 = br.ReadBytes(0x550); //0x4274C
                    Unk0x42CE0 = br.ReadBytes(0x50); //0x42C9C
                    Unk0x42D30 = br.ReadBytes(0x28); //0x42CEC
                    Unk0x42D58 = br.ReadBytes(0x3DE0); //0x42D14
                    Unk0x46B38 = br.ReadBytes(0x80); //0x46AF4, 5 * 0x18 Byte, 4 byte padding
                    br.ReadBytes(4); //1 byte unknown, 3 byte padding
                }
                else
                {
                    Unk0x42724 = br.ReadBytes(0x6C); //0x42724
                    Unk0x42790 = br.ReadBytes(0x550); //0x42790
                    Unk0x42CE0 = br.ReadBytes(0x50); //0x42CE0
                    Unk0x42D30 = br.ReadBytes(0x28); //0x42D30
                    Unk0x42D58 = br.ReadBytes(0x3DE0); //0x42D58
                    Unk0x46B38 = br.ReadBytes(0x80); //0x46B38, 5 * 0x18 Byte, 4 byte padding
                }
    
                //after this, the console version matches again

                for (int i = 0; i < 200; i++) data0x46BB8.Add(Util.ReadStructure<Data0x46BB8>(br.ReadBytes(Data0x46BB8.SIZE))); //0x46BB8
                EnigmaOrder = br.ReadBytes(0x68B4); //0x47838
                SkillFusion = Util.ReadStructure<SkillFusion>(br.ReadBytes(SkillFusion.SIZE));
                Unk0x4E120 = br.ReadBytes(0xD0); //0x4E120
                for (int i = 0; i < 500; i++) Unk0x4E1F0.Add(Util.ReadStructure<Mail>(br.ReadBytes(Mail.SIZE))); //0x4E1F0
                for (int i = 0; i < 10; i++) Equip.Add(Util.ReadStructure<Equipment>(br.ReadBytes(Equipment.SIZE))); //0x50130
                for (int i = 0; i < 100; i++) Inventory.Add(Util.ReadStructure<Item>(br.ReadBytes(Item.SIZE))); //0x50270
                for (int i = 0; i < 1000; i++) Storage.Add(Util.ReadStructure<Item>(br.ReadBytes(Item.SIZE))); //0x50BD0
                for (int i = 0; i < 7; i++) data0x56990.Add(Util.ReadStructure<Item>(br.ReadBytes(Item.SIZE))); //0x56990
                Col = br.ReadUInt32(); //0x56A38
                Unk0x56A3C = br.ReadUInt64();
                Unk0x56A44 = br.ReadUInt64();
                Unk0x56A4C = br.ReadUInt16();
                Unk0x56A4E = br.ReadByte();
                Unk0x56A4F = br.ReadBytes(0x39); //padding?
                for (int i = 0; i < MAX_CHARACTER; i++) data0x56A88.Add(Util.ReadStructure<Data0x56A88>(br.ReadBytes(Data0x56A88.SIZE))); //0x56A88
                Unk0x5D1A8 = br.ReadBytes(0x3358); //0x5D1A8
                for (int i = 0; i < 3; i++) data0x60500.Add(Util.ReadStructure<Data0x60500>(br.ReadBytes(Data0x60500.SIZE))); //0x56A88
                Unk0x607E8 = br.ReadBytes(0x12C0); //0x607E8
                Unk0x61AA8 = br.ReadBytes(0x960); //0x61AA8
                Version = br.ReadInt32(); //0x62408
                for (int i = 0; i < 352; i++) data0x6240C.Add(Util.ReadStructure<Data0x6240C>(br.ReadBytes(Data0x6240C.SIZE))); //0x6240C
                Unk0x78F0C = br.ReadUInt32();
                Hash2 = br.ReadUInt64(); //0x78F10
                for (int i = 0; i < MAX_CHARACTER; i++) data0x78F18.Add(Util.ReadStructure<Data0x78F18>(br.ReadBytes(Data0x78F18.SIZE))); //0x78F18

                End0x7AE08 = br.ReadBytes(0x55F8); //version 2 is 0x40 byte longer...
            }
        }

        public void Write(string sPath, int version, SaveGameType gameType)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Hash1);
                bw.Write(unk0x14);
                bw.Write(unk0x18);
                bw.Write(unk0x1C);
                bw.Write(unk0x20);
                bw.Write(Util.StructureToByteArray(playtime));
                bw.Write(unk0x28);
                bw.Write(unk0x2C);
                bw.Write(Util.StructureToByteArray(data0x4DC));

                for (int i = 0; i < 32; i++) bw.Write(Util.StructureToByteArray(MainSkills[i]));  //0xDA0
                for (int i = 0; i < 1024; i++) bw.Write(Util.StructureToByteArray(SubSkills[i])); //0xF20
                bw.Write(Unk0x3F20); //0x3F20
                for (int i = 0; i < MAX_CHARACTER; i++) bw.Write(Util.StructureToByteArray(CharacterDatas[i])); //0x3F28
                bw.Write(Unk0x1C1E0); //0x1C1E0
                for (int i = 0; i < MAX_CHARACTER; i++) bw.Write(Util.StructureToByteArray(CharacterEmotions[i])); //0x1C9E0
                bw.Write(Util.StructureToByteArray(Data0x2CBB0)); //0x2CBB0
                for (int i = 0; i < 20; i++) bw.Write(Util.StructureToByteArray(SkillPalettes[i])); //0x3AB04
                bw.Write(Unk0x3E4D4); //0x3E4D4
                bw.Write(Unk0x3E524); //0x3E524
                bw.Write(Unk0x3E584); //0x3E584 
                for (int i = 0; i < 1000; i++) bw.Write(Util.StructureToByteArray(Mails[i])); //0x3E8A4

                //after this the console version is different
                if (version == 2)
                {
                    switch (gameType)  //0x42724
                    {
                        case SaveGameType.PSVita:
                            bw.Write(Unk0x42724_Version2_Vita);
                            break;
                        case SaveGameType.NintendoSwitch:
                            bw.Write(Unk0x42724_Version2_Switch);
                            break;
                        default:
                            bw.Write(Unk0x42724_Version2_Vita);
                            break;
                    }

                    bw.Write(Unk0x42790); //0x4274C
                    bw.Write(Unk0x42CE0); //0x42C9C
                    bw.Write(Unk0x42D30); //0x42CEC
                    bw.Write(Unk0x42D58); //0x42D14
                    bw.Write(Unk0x46B38); //0x46AF4, 5 * 0x18 Byte, 4 byte padding
                    bw.Write((int)0); //1 byte unknown, 3 byte padding
                }
                else
                {
                    bw.Write(Unk0x42724_Version3); //0x42724
                    bw.Write(Unk0x42790); //0x42790
                    bw.Write(Unk0x42CE0); //0x42CE0
                    bw.Write(Unk0x42D30); //0x42D30
                    bw.Write(Unk0x42D58); //0x42D58
                    bw.Write(Unk0x46B38); //0x46B38, 5 * 0x18 Byte, 4 byte padding
                }
    
                //after this, the console version matches again
                for (int i = 0; i < 200; i++) bw.Write(Util.StructureToByteArray(data0x46BB8[i])); //0x46BB8
                bw.Write(EnigmaOrder); //0x47838
                bw.Write(Util.StructureToByteArray(SkillFusion));
                bw.Write(Unk0x4E120); //0x4E120
                for (int i = 0; i < 500; i++) bw.Write(Util.StructureToByteArray(Unk0x4E1F0[i])); //0x4E1F0
                for (int i = 0; i < 10; i++) bw.Write(Util.StructureToByteArray(Equip[i])); //0x50130
                for (int i = 0; i < 100; i++) bw.Write(Util.StructureToByteArray(Inventory[i])); //0x50270
                for (int i = 0; i < 1000; i++) bw.Write(Util.StructureToByteArray(Storage[i])); //0x50BD0
                for (int i = 0; i < 7; i++) bw.Write(Util.StructureToByteArray(data0x56990[i])); //0x56990
                bw.Write(Col); //0x56A38
                bw.Write(Unk0x56A3C);
                bw.Write(Unk0x56A44);
                bw.Write(Unk0x56A4C);
                bw.Write(Unk0x56A4E);
                bw.Write(Unk0x56A4F); //padding?
                for (int i = 0; i < MAX_CHARACTER; i++) bw.Write(Util.StructureToByteArray(data0x56A88[i])); //0x56A88
                bw.Write(Unk0x5D1A8); //0x5D1A8
                for (int i = 0; i < 3; i++) bw.Write(Util.StructureToByteArray(data0x60500[i])); //0x56A88
                bw.Write(Unk0x607E8); //0x607E8
                bw.Write(Unk0x61AA8); //0x61AA8
                bw.Write(version); //0x62408
                for (int i = 0; i < 352; i++) bw.Write(Util.StructureToByteArray(data0x6240C[i])); //0x6240C
                bw.Write(Unk0x78F0C);
                bw.Write(Hash2); //0x78F10
                for (int i = 0; i < MAX_CHARACTER; i++) bw.Write(Util.StructureToByteArray(data0x78F18[i])); //0x78F18

                bw.Write(End0x7AE08); //version 2 is 0x40 byte longer...

                if (version == 2)
                {
                    for (int i = 0; i < 0x40; i++)
                    {
                        bw.Write((byte)0);
                    }
                }

                SaveData = ms.ToArray();
            }

            CalculateHash(ref SaveData, version, gameType);
            File.WriteAllBytes(sPath, SaveData);
        }

        private void CalculateHash(ref byte[] data, int version, SaveGameType gameType)
        {
            int Hash1Offset, Hash2Offset;
            int Hash1Size = 0x14, Hash2Size = 0x8;
        
            ulong init, multi;

            if (version == 2)
            {
                Hash1Offset = 0x0;
                Hash2Offset = 0x78ED0;
            }
            else if(version == 3)
            {                
                Hash1Offset = 0x0;
                Hash2Offset = 0x78F10;
            }
            else
            {
                return;
            }

            switch (gameType)
            {
                case SaveGameType.Pc: //Steam
                    init = PC_INIT;
                    multi = PC_MULTI;
                    break;
                case SaveGameType.PSVita: //PCSE00903
                    init = VITA_INIT;
                    multi = VITA_MULTI;
                    break;
                case SaveGameType.NintendoSwitch: //0x0100A1100B70E000
                    //init = SWITCH_INIT;
                    //multi = SWITCH_MULTI;
                    init = VITA_INIT;
                    multi = VITA_MULTI;
                    break;

                default: //error
                    init = 0;
                    multi = 0;
                    break;
            }

            //fill hash1 with zeroes
            for (int i = 0; i < Hash1Size; i++) data[i + Hash1Offset] = 0;

            //fill hash2 with zeroes
            for (int i = 0; i < Hash2Size; i++) data[i + Hash2Offset] = 0;

            //calc hash1
            var hash1 = new SHA1Managed().ComputeHash(data);

            //calc hash2
            ulong hash2 = CalculateHash2(data, init, multi);

            //write hash1
            switch (gameType)
            {
                case SaveGameType.NintendoSwitch: break; //do nothing, hash1 is ignored on switch?
                default:
                    hash1.CopyTo(data, Hash1Offset);
                    break;
            }

            //write hash2
            BitConverter.GetBytes(hash2).CopyTo(data, Hash2Offset);
        }

        private ulong CalculateHash2(byte[] data, ulong init, ulong multiplier)
        {
            var hash = init;

            for (int i = 0; i < data.Length; i++)
            {
                hash = multiplier * (hash ^ data[i]);
            }

            return hash;
        }

        private void GetTypeFromHash(byte[] data)
        {
            int Hash1Offset, Hash2Offset;
            int Hash1Size = 0x14, Hash2Size = 0x8;
            currentType = SaveGameType.Pc;

            if (Version == 2)
            {
                Hash1Offset = 0x0;
                Hash2Offset = 0x78ED0;
            }
            else if(Version == 3)
            {                
                Hash1Offset = 0x0;
                Hash2Offset = 0x78F10;
            }
            else
            {
                return;
            }

            byte[] CurrentHash1 = new byte[Hash1Size];
            Array.Copy(data, CurrentHash1, Hash1Size);
            ulong CurrentHash2 = BitConverter.ToUInt64(data, Hash2Offset);

            //fill hash1 with zeroes
            for (int i = 0; i < Hash1Size; i++) data[i + Hash1Offset] = 0;

            //fill hash2 with zeroes
            for (int i = 0; i < Hash2Size; i++) data[i + Hash2Offset] = 0;

            if(Util.IsArrayZero(CurrentHash1))
            {
                if (CurrentHash2 == CalculateHash2(data, VITA_INIT, VITA_MULTI))
                {
                    currentType = SaveGameType.NintendoSwitch;
                    return;
                }
            }

            if (CurrentHash2 == CalculateHash2(data, PC_INIT, PC_MULTI))
            {
                currentType = SaveGameType.Pc;
                return;
            }
            if (CurrentHash2 == CalculateHash2(data, VITA_INIT, VITA_MULTI))
            {
                currentType = SaveGameType.PSVita;
                return;
            }
            if (CurrentHash2 == CalculateHash2(data, SWITCH_INIT, SWITCH_MULTI))
            {
                currentType = SaveGameType.NintendoSwitch;
                return;
            }

        }

        public uint GetStatistic(bool IsMain, int MainId, int SubId)
        {
            uint value = 0;

            if (IsMain)
            {
                foreach (var skill in MainSkills)
                {
                    if (skill.Id == MainId)
                    {
                        value = skill.Value;
                    }
                }
            }
            else
            {
                foreach (var skill in SubSkills)
                {
                    if (skill.Id == SubId && skill.SkillType == MainId)
                    {
                        value = skill.Value;
                    }
                }
            }

            if (value > 99999999)
                value = 99999999;

            return value;
        }

        public void SetStatistic(bool IsMain, int MainId, int SubId, uint value)
        {
            if (IsMain)
            {
                for(int i = 0;i<MainSkills.Count;i++)
                {
                    var skill = MainSkills[i];

                    if (skill.Id == MainId)
                    {
                        skill.Value = value;
                    }

                    MainSkills[i] = skill;
                }
            }
            else
            {
                for(int i = 0;i<SubSkills.Count;i++)
                {
                    var skill = SubSkills[i];

                    if (skill.Id == SubId && skill.SkillType == MainId)
                    {
                        skill.Value = value;
                    }

                    SubSkills[i] = skill;
                }
            }
        }

    }
}
