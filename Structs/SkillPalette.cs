using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Remoting;

namespace SaveEditor.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct SkillPalette
    {
        public const int SIZE = 0x2E4;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] //only 10 of 16 are used
        public SkillPaletteEntry[] Row1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] //only 10 of 16 are used
        public SkillPaletteEntry[] Row2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] //only 10 of 16 are used
        public SkillPaletteEntry[] Row3;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] //only 10 of 16 are used
        public SkillPaletteEntry[] Row4;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] //only 10 of 16 are used
        public SkillPaletteEntry[] Row5;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] //only 10 of 16 are used
        public SkillPaletteEntry[] Row6;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] //unused
        public SkillPaletteEntry[] Row7;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] //unused
        public SkillPaletteEntry[] Row8;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] //unused
        public SkillPaletteEntry[] Row9;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] //unused
        public SkillPaletteEntry[] Row10;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] //only 8 of 16 are used
        public SkillPaletteEntry[] SubPalette;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public SkillPaletteEntry[] CommandPalette;

        public SkillPaletteEntry DefaultSkill; //used as combo finisher, unchangeable?
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct SkillPaletteEntry
    {
        public const int SIZE = 0x4;

        public ushort Id; //SkillId or ItemId
        public byte EntryType; //SkillType or ItemType
        public byte PaletteType; //0 = Skill, 1 = Item
    }
}
