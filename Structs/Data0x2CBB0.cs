using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SaveEditor.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct Data0x2CBB0
    {
        public const int SIZE = 0xDF54;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x2C)]
        public byte[] data0x0;

        //0x2C
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public Data0x2CBB0_Sub1[] data0x2C;

        //0x502C
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public Data0x2CBB0_Sub2[] data0x502C;

        //0x662C
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 384)]
        public Data0x2CBB0_Sub3[] data0x662C;

        //0x7E2C
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1000)] //there is no quest count in the code, but only ~900 quest slots are used, so no big problem
        public Quest[] Quests; 

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x3248)]
        public byte[] unk1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct Data0x2CBB0_Sub1
    {
        public const int SIZE = 20;

        public uint idx, id, unk3, unk4, unk5;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct Data0x2CBB0_Sub2
    {
        public const int SIZE = 44;

        public uint idx, id;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
        public byte[] data;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct Data0x2CBB0_Sub3
    {
        public const int SIZE = 16;

        public uint unk1;
        public float unk2;
        public uint unk3, unk4;
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct Quest
    {
        public const int SIZE = 12;

        public ushort Id;
        public QuestState State;
        public byte Progress;
        public byte unk1, unk2, unk3, unk4;
        public float time; //default value is 0F or 999999F, timelimit?

        
        public override string ToString()
        {
            if (Id == 0) return Database.GetString(501819);

            string result;

            var questData = Database.GetQuest(Id);

            switch (questData.RequestType)
            {
                //kill monster quest
                case QuestType.KillMonster:
                case QuestType.KillNightmare:
                    var enemyName = Database.GetEnemyName(questData.EnemyId);
                    result = $"{State} - {Database.GetString(questData.StringId2)}, Enemy: {enemyName} - {Progress}/{questData.MaxProgress}"; break;

                //collect item quest
                case QuestType.CollectItem1:
                case QuestType.CollectItem2:
                case QuestType.CollectItem3:
                    var itemName = Database.GetItemName(questData.ReqItemType, questData.ReqItemId);
                    result = $"{State} - {Database.GetString(questData.StringId2)}, Item: {itemName} x{questData.ReqItemAmount}"; break;

                case QuestType.None:
                    result = "No Quest"; break;

                default:
                    result = $"{State} - {Database.GetString(questData.StringId2)}, Progress: {Progress}/{questData.MaxProgress}"; break;
            }

            return result;
        }
    }
}
