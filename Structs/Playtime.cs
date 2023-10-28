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
    public struct Playtime
    {
        public const int SIZE = 0x4;

        public int time;

        public int Seconds => time % 60;
        public int Minutes => (time / 60) % 60;
        public int Hours => time / 3600;

        public Playtime(int h, int m, int s)
        {
            //if (h > 999) h = 999;
            if (m > 60) m = 60;
            if (s > 60) s = 60;

            time = 0;
            time += h * 3600;
            time += m * 60;
            time += s;
        }

        public override string ToString()
        {
            return $"{Hours:D3}:{Minutes:D2}:{Seconds:D2}";
        }
    }
}
