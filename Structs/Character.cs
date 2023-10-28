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
    public struct CharacterData
    {
        public const int SIZE = 0x12C;

        public ushort Affection, Level;
        public uint Exp;

        public Character_Sub1 pal1;
        public Character_Sub1 pal2;
        public Character_Sub1 pal3;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public ushort[] SkillLevel;

        public uint Unk1;
        public ushort CharacterId;
        public byte unk2;
        public FriendState FriendState; //setting this value from 0 to 1, does not make this character appear in the friend list!

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public byte[] UnkData;

        public override string ToString()
        {
            if (CharacterId == 0) return Database.GetString(501819);

            return $"{FriendState} - {Database.GetNpcName(CharacterId)} - Lv: {Level}, Affection: {Affection}";
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct Character_Sub1
    {
        public const int SIZE = 0x44;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public ushort[] SkillID;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
        public ushort[] SkillSetting;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct CharacterEmotion
    {
        public const int SIZE = 0xC8;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public EmotionSkill[] EmotionSkills;

        public uint unk1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public byte[] padding;
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 16)]
    public struct EmotionSkill
    {
        public float X,Y,Z; //useless to modify, it constantly changes anyway
        public ushort Value; //100 = 1,00, max = 1000
        public byte Id;
        public byte State; //0 - Don't keep, 1 = Great!
    }
}
