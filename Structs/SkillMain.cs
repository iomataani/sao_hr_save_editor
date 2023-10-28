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
    public struct SkillMain
    {
        public const int SIZE = 0xC;

        public byte Id;
        public SkillState State;
        public ushort SkillLevel;
        public uint SkillPoints;
        public uint Value; //only used for unlocking

        public override string ToString()
        {
            if (Id == 0 && State == 0) return Database.GetString(501819);

            return $"{Database.GetSkillTypeName(Id)} - [Lv:{SkillLevel}, SP:{SkillPoints}, State:{State}]";
        }
    }
}
