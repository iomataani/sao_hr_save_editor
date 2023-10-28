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
    public struct Data0x6240C
    {
        public const int SIZE = 0x108;

        public uint Id;
        public byte IsEnabled; //0 = false, 1 = true

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] padding; //0xFF

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public Data0x6240C_Sub1[] data;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct Data0x6240C_Sub1
    {
        public const int SIZE = 4;

        public byte State; //2 = ???, 0xFF = Disabled
        public byte Index; //0xFF = Disabled
        public ushort Value; //0xFFFF = None
    }

}
