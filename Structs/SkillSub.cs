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
    public struct SkillSub
    {
        public const int SIZE = 0xC;
        
        public ushort Id;
        public ushort SkillType;
        public uint Value; //only used for unlocking
        public ushort Mastery;
        public SkillState State;
        public byte unk1;

        public override string ToString()
        {      
            if (SkillType == 0 && Id == 0) return Database.GetString(501819);

            //return $"[{SkillType};{Id}] - {Database.GetSkillMasteryName(SkillType, Id)} - [{Value}; {Mastery}; {State}]"; //debug

            if (SkillType == 1 && Id < 1000 && Id != 600) //misc skills useable by all main skills
            {
                return $"{Database.GetSubSkillName(SkillType, Id)} - [{Value}; {Mastery}; {State}]";
            }
            
            return $"{Database.GetSkillTypeName(SkillType)} - {Database.GetSubSkillName(SkillType, Id)} - [Lv:{Mastery}, State:{State}]";
        }
    }
}
