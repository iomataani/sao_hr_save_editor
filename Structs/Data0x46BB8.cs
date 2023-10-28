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
    public struct Data0x46BB8
    {
        public const int SIZE = 16;

        public uint unk1; //Id?
        public float unk2; //TimeLimit?
        public uint unk3; //Enabled?
        public uint unk4;
    }
}
