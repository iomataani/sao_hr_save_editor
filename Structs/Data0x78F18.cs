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
    public struct Data0x78F18
    {  
        public const int SIZE = 24;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public ushort[] tbl1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public ushort[] tbl2;

        public uint value;

        public void Init()
        {
            tbl1 = new ushort[5];
            tbl2 = new ushort[5];

            for (int i = 0; i < 5; i++)
            {
                tbl1[i] = 0;
                tbl2[i] = 0;
            }

            value = 998;
        }
    }
}
