using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SaveEditor.Structs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = SIZE)]
    public struct SystemSave
    {
        public const int SIZE = 0x124;
    }
}
