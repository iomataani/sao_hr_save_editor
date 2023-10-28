using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SaveEditor
{    
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct FixItem
    {
        public const int SIZE = 84;

        public ushort ItemType;
        public ushort Id;
        public int StringId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] data1;

        public uint Price;

        public uint unk0, unk1;

        public ushort ATK, DEF;

        public ushort unk2, unk3;
        public byte unk4;

        public byte STR;

        public ushort unk5;

        public ushort HP, SP;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public ushort[] data2;

        public byte VIT, SPD;

        public byte unk6;

        public byte resist1, resist2, resist3, resist4;

        public byte NumbResist;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public ushort[] data3;
        
        public string GetName()
        {
            return Database.GetItemName(ItemType, Id);
        }

        public override string ToString()
        {
            return $"{Id:D4} - {GetName()}";
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct SkillData
    {
        public const int SIZE = 64;

        public ushort Id, SkillType;
        public int StringId1, StringId2, unk1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct ReqQuest
    {
        public const int SIZE = 240;

        public ushort Id;
        public ushort unk1;
        public byte unk2, ReqLevel;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 42)]
        public byte[] data1;

        public int StringId1,StringId2,StringId3,StringId4;

        public QuestType RequestType;

        public ushort EnemyId, MaxProgress, unk3, ReqItemType, ReqItemId, ReqItemAmount, unk4;
        public uint Col, EP;
        public uint unk5, unk6;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public ReqQuest_RewardItem[] ItemRewards;

        public int MapId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] data2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct ReqQuest_RewardItem{
        public const int SIZE = 16;

        public ushort State, Type, Id, Amount, unk1, unk2, unk3, unk4;

        public override string ToString()
        {
            return $"Item: {Database.GetItemName(Type, Id)}, x{Amount}, {unk1}, {unk2}, {unk3}, {unk4}\r\n";;
        }
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct FixMap
    {
        public const int SIZE = 0x80;

        public int unk1, unk2, StringId, Id, unk4;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        public byte[] Name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public byte[] data;
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct NpcEntry
    {
        public const int SIZE = 0x150;

        public ushort Id;
        public byte unk1, unk2;
        public int StringId1, StringId2;
        public short unk3, unk4, unk5;
        public short FriendIndex; //-1 = not in list
    }
        
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct EnemyEntry
    {
        public const int SIZE = 876;

        public ushort Id;
        public ushort unk1, unk2, unk3;
        public int StringId1, StringId2;
    }

    public static class Database
    {
        public static Dictionary<string, FixItem> ItemDatabase;
        public static Dictionary<string, SkillData> SkillDatabase;
        public static Dictionary<string, SkillData> SkillPvpDatabase;
        public static Dictionary<int, ReqQuest> QuestDatabase;
        public static Dictionary<int, FixMap> MapDatabase;

        public static Dictionary<int, NpcEntry> NpcDatabase;
        public static Dictionary<int, EnemyEntry> EnemyDatabase;

        public static Dictionary<int, string> StringTable;

        //other
        public static Dictionary<int, int> SkillTable;

        public static void Init()
        {
            LoadItemDatabase();
            LoadSkillDatabase();
            LoadSkillPvpDatabase();
            LoadQuestDatabase();
            LoadNpcDatabase();
            LoadMapDatabase();
            LoadEnemyDatabase();

            LoadIndexTable(out SkillTable, "skill");
        }

        private static void LoadItemDatabase()
        {
            var sPath = "Data\\fix_item.dat";

            ItemDatabase = new Dictionary<string, FixItem>();

            using (var br = new BinaryReader(File.OpenRead(sPath)))
            {
                int count = (int)br.BaseStream.Length / FixItem.SIZE;

                for (int i = 0; i < count; i++)
                {
                    var temp = Util.ReadStructure<FixItem>(br.ReadBytes(FixItem.SIZE));
                    ItemDatabase.Add($"{temp.ItemType};{temp.Id}", temp);
                }
            }
        }
        
        private static void LoadSkillDatabase()
        {
            var sPath = "Data\\skill_data.dat";

            SkillDatabase = new Dictionary<string, SkillData>();

            using (var br = new BinaryReader(File.OpenRead(sPath)))
            {
                int count = (int)br.BaseStream.Length / SkillData.SIZE;

                for (int i = 0; i < count; i++)
                {
                    var temp = Util.ReadStructure<SkillData>(br.ReadBytes(SkillData.SIZE));
                    SkillDatabase.Add($"{temp.SkillType};{temp.Id}", temp);
                }
            }
        }
                
        private static void LoadSkillPvpDatabase()
        {
            var sPath = "Data\\skill_data_pvp.dat";

            SkillPvpDatabase = new Dictionary<string, SkillData>();

            using (var br = new BinaryReader(File.OpenRead(sPath)))
            {
                int count = (int)br.BaseStream.Length / SkillData.SIZE;

                for (int i = 0; i < count; i++)
                {
                    var temp = Util.ReadStructure<SkillData>(br.ReadBytes(SkillData.SIZE));
                    SkillPvpDatabase.Add($"{temp.SkillType};{temp.Id}", temp);
                }
            }
        }

        private static void LoadQuestDatabase()
        {
            var sPath = $"Data\\reqquest.dat";

            QuestDatabase = new Dictionary<int, ReqQuest>();

            using (var br = new BinaryReader(File.OpenRead(sPath)))
            {
                int count = (int)br.BaseStream.Length / ReqQuest.SIZE;

                for (int i = 0; i < count; i++)
                {
                    var temp = Util.ReadStructure<ReqQuest>(br.ReadBytes(ReqQuest.SIZE));
                    QuestDatabase.Add(temp.Id, temp);
                }
            }
        }

        private static void LoadMapDatabase()
        {
            string sPath = "Data\\fixmap.dat";

            MapDatabase = new Dictionary<int, FixMap>();

            using (var br = new BinaryReader(File.OpenRead(sPath)))
            {
                int count = (int)br.BaseStream.Length / FixMap.SIZE;

                for (int i = 0; i < count; i++)
                {
                    var temp = Util.ReadStructure<FixMap>(br.ReadBytes(FixMap.SIZE));
                    MapDatabase.Add(temp.Id, temp);
                }
            }
        }
        
        private static void LoadNpcDatabase()
        {
            string sPath = "Data\\npc_character.dat";
            OFS3 npcs = new OFS3();
            npcs.Load(sPath);

            NpcDatabase = new Dictionary<int, NpcEntry>();

            using (var br = new BinaryReader(File.OpenRead(sPath)))
            {
                for (int i = 0; i < npcs.EntryCount; i++)
                {
                    br.SeekTo(npcs.Offset[i]);

                    var entry = Util.ReadStructure<NpcEntry>(br.ReadBytes(NpcEntry.SIZE));

                    NpcDatabase.Add(entry.Id, entry);
                }
            }
        }

        private static void LoadEnemyDatabase()
        {
            string sPath = "Data\\enemy.dat";
            OFS3 enemies = new OFS3();
            enemies.Load(sPath);

            EnemyDatabase = new Dictionary<int, EnemyEntry>();

            using (var br = new BinaryReader(File.OpenRead(sPath)))
            {
                for (int i = 0; i < enemies.EntryCount; i++)
                {
                    br.SeekTo(enemies.Offset[i]);

                    var entry = Util.ReadStructure<EnemyEntry>(br.ReadBytes(EnemyEntry.SIZE));

                    EnemyDatabase.Add(entry.Id, entry);
                }
            }
        }

        public static void LoadStringTable(enmLanguage lang)
        {
            StringTable = new Dictionary<int, string>();

            var sPath = $"Data\\{lang.ToString()}.dat";

            if (!File.Exists(sPath))
            {
                sPath = $"Data\\{enmLanguage.usa.ToString()}.dat";
            }

            if (!File.Exists(sPath))
            {
                return;
            }

            OFS3 strings = new OFS3();
            strings.Load(sPath);

            using (var br = new BinaryReader(File.OpenRead(sPath)))
            {
                for (int i = 0; i < strings.EntryCount; i++)
                {
                    br.SeekTo(strings.Offset[i]);

                    var str = Util.ReadCString(br, -1, -1, Encoding.UTF8);

                    StringTable.Add(strings.ID[i], str);
                }
            }
        }

        private static void LoadIndexTable(out Dictionary<int, int> tbl, string fileName)
        {
            string sPath = $"Data\\{fileName}.csv";
            
            tbl = new Dictionary<int, int>();

            if (File.Exists(sPath))
            {
                using (var sr = new StreamReader(sPath))
                {
                    var header = sr.ReadLine();

                    while (sr.Peek() != -1)
                    {
                        var line = sr.ReadLine();
                        if(string.IsNullOrWhiteSpace(line)) continue;

                        var data = line.Split(';');

                        if(string.IsNullOrEmpty(data[0])) continue;
                        int id1 = int.Parse(data[0]);
                        if(string.IsNullOrEmpty(data[1])) continue;
                        int id2 = int.Parse(data[1]);

                        tbl.Add(id1, id2);
                    }
                }
            }
        }

        public static string GetString(int stringId)
        {
            if (StringTable.ContainsKey(stringId))
            {
                return StringTable[stringId];
            }

            return $"[{stringId}]";
        }

        #region load data from Database

        public static string GetItemName(ItemType type, int Id)
        {
            return GetItemName((int)type, Id);
        }
     
        public static string GetItemName(int type, int Id)
        {
            string result = null;

            if (type == 0)
            {
                return "Invalid";
            }

            var str_id = $"{type};{Id}";

            if (ItemDatabase.ContainsKey(str_id))
            {
                result = GetString(ItemDatabase[str_id].StringId);
            }

            return result ?? $"[{type},{Id}]";
        }

        public static string GetSubSkillName(int SkillType, int Id)
        {
            string result = null;
            var str_id = $"{SkillType};{Id}";

            if (SkillDatabase.ContainsKey(str_id))
            {
                result = GetString(SkillDatabase[str_id].StringId1);
            }

            return result ?? $"[{SkillType},{Id}]";
        }

        public static ReqQuest GetQuest(int Id)
        {
            ReqQuest result = new ReqQuest();

            if (QuestDatabase.ContainsKey(Id))
            {
                result = QuestDatabase[Id];
            }

            return result;
        }

        public static string GetQuestName(int Id)
        {  
            string result = null;

            if (QuestDatabase.ContainsKey(Id))
            {
                result = $"{QuestDatabase[Id].Id} - " + GetString(QuestDatabase[Id].StringId2);
            }

            return result ?? $"[{Id}]";
        }
        
        public static int GetQuestProgress(int Id)
        {
            if (QuestDatabase.ContainsKey(Id))
            {
                return QuestDatabase[Id].MaxProgress;
            }

            return -1;
        }
        
        public static string GetMapName(int Id)
        {  
            string result = null;

            if (MapDatabase.ContainsKey(Id))
            {
                result = $"{MapDatabase[Id].Id} - " + GetString(MapDatabase[Id].StringId);
            }

            return result ?? $"[{Id}]";
        }
        
        public static string GetNpcName(int Id)
        {
            string result = "???";

            if (NpcDatabase.ContainsKey(Id))
            {
                result = $"{NpcDatabase[Id].Id} - " + GetString(NpcDatabase[Id].StringId2);
            }

            //return $"[{id:D3} - {result}]"; //debug
            return result;
        }

        public static string GetEnemyName(int Id)
        {
            string result = "???";

            if (EnemyDatabase.ContainsKey(Id))
            {
                result = GetString(EnemyDatabase[Id].StringId2);
            }

            //return $"[{id:D3} - {result}]"; //debug
            return result;
        }

        #endregion
      
        public static string GetSkillTypeName(int id)
        {
            string result = "???";

            if (SkillTable.ContainsKey(id))
            {
                result = GetString(SkillTable[id]);
            }

            //return $"[{id:D2} - {result}]"; //debug
            return result;
        }

        public static string GetItemTypeName(ItemType type)
        {
            switch (type)
            {
                case ItemType.None: return GetString(510000);
                case ItemType.OneHandSword: return GetString(510001);
                case ItemType.Rapier: return GetString(510002);
                case ItemType.Scimitar: return GetString(510003);
                case ItemType.Dagger: return GetString(510004);
                case ItemType.Club: return GetString(510006); //One-Handed Club
                case ItemType.Katana: return GetString(510008);
                case ItemType.TwoHandSword: return GetString(510020); //Two-Handed Sword
                case ItemType.Axe: return GetString(510021); //Two-Handed Axe
                case ItemType.Spear: return GetString(510022);

                case ItemType.Shield: return GetString(510050);
                case ItemType.Helmet: return GetString(510051); //Head
                case ItemType.Armor: return GetString(510052);
                case ItemType.Boots: return GetString(510053); //Greaves

                case ItemType.Necklace: return GetString(510056); //Neck
                case ItemType.Ring: return GetString(510057); //Finger
                case ItemType.Waist: return GetString(510058); 
                case ItemType.Wrist: return GetString(510059);
                case ItemType.Charm: return GetString(510060);

                case ItemType.Medicine: return GetString(510100);
                case ItemType.Crystal: return GetString(510101);
                case ItemType.Material: return GetString(510103);
                case ItemType.Ore: return GetString(510104);
                case ItemType.Sellable: return GetString(510105);

                case ItemType.Event: return GetString(510113);

                default:  return null;
            }
        }

        public static string GetItemStatName(ItemStatType type)
        {
            switch (type)
            {
                case ItemStatType.None: return "";
                case ItemStatType.HP: return GetString(501623);
                case ItemStatType.AP: return GetString(501624);
                case ItemStatType.ATK: return GetString(501605);
                case ItemStatType.DEF: return GetString(501606);
             
                case ItemStatType.STR: return GetString(501610);
                case ItemStatType.VIT: return GetString(501611);
                case ItemStatType.DEX: return GetString(501612);
                case ItemStatType.AGI: return GetString(501613);
                case ItemStatType.Luck: return GetString(501642);
                case ItemStatType.SlashResist: return GetString(501619);
                case ItemStatType.ThrustResist: return GetString(501620);
                case ItemStatType.BluntResist: return GetString(501621);
             
                case ItemStatType.StunResist: return GetString(501625); 
                case ItemStatType.KnockDownResist: return GetString(501626);
                case ItemStatType.CriticalResist: return GetString(501627);
                case ItemStatType.PoisonResist: return GetString(501628); 
                case ItemStatType.NumbResist: return GetString(501629);
                case ItemStatType.BleedResist: return GetString(501630);
                case ItemStatType.PhysicalResist: return GetString(501631);
                case ItemStatType.SoulResist: return GetString(501632);
                case ItemStatType.ParamDebuffResist: return GetString(501633);

                case ItemStatType.Unk5:
                case ItemStatType.Unk14: //SP BASED ATK UP?
                    return "???";

                default:  return null;
            }
        }

        public static bool GetItemIsStackable(int type)
        {
            return GetItemIsStackable((ItemType)type);
        }

        public static bool GetItemIsStackable(ItemType type)
        {
            switch (type)
            {
                case ItemType.Medicine:
                case ItemType.Crystal:
                case ItemType.Material:
                case ItemType.Ore:
                case ItemType.Sellable:
                case ItemType.Unknown102:
                case ItemType.Event:
                case ItemType.Unknown110:
                case ItemType.Unknown111:
                case ItemType.Unknown120:
                case ItemType.Unknown121:
                return true;
            }

            return false;
        }



    }
}
