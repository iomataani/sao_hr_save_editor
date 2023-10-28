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
    public struct Data0x56A88
    {        
        public const int SIZE = 80;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public byte[] data;
    }
}
