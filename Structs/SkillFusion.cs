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
    public struct SkillFusion
    {
        public const int SIZE = 0x34;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x24)]
        public byte[] data1;

        public uint QuestPlayer, QuestPartner;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x8)]
        public byte[] data2;
    }
}
