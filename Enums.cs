using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveEditor
{
    public enum ItemType : byte
    {
        None = 0,
        OneHandSword = 1,
        Rapier = 2,
        Scimitar = 3,
        Dagger = 4,

        Club = 6,

        Katana = 8,

        TwoHandSword = 20,
        Axe = 21,
        Spear = 22,

        Shield = 50,
        Helmet = 51,
        Armor = 52,
        Boots = 53,

        Necklace = 56,
        Ring = 57,
        Waist = 58,
        Wrist = 59,
        Charm = 60,

        Medicine = 100,
        Crystal = 101,
        Unknown102 = 102,
        Material = 103,
        Ore = 104,
        Sellable = 105,

        Unknown110 = 110,
        Unknown111 = 111,

        Event = 113, //106,

        Unknown120 = 120,
        Unknown121 = 121,
    }

    public enum ItemStatType : byte
    {
        None = 0, 
        HP, AP, ATK, DEF, 
        Unk5, 
        STR, VIT, DEX, AGI, Luck, SlashResist, ThrustResist, BluntResist, 
        Unk14, //SP BASED ATK UP?
        StunResist, KnockDownResist, CriticalResist, PoisonResist, NumbResist,
        BleedResist, PhysicalResist, SoulResist, ParamDebuffResist,
    }
    
    public enum SkillState : byte
    {
        Disabled = 0,
        Available = 1,
        Learned = 2,
    }
       
    public enum QuestState : byte
    {
        Disabled = 0,
        Available = 1,
        Active = 2,
        Finished = 3,
    }
          
    public enum FriendState : byte
    {
        Disabled = 0,
        Friend = 1,
        Available = 2,
    }

    public enum QuestType : ushort
    {
        None = 0,
        KillMonster = 1,
        CollectItem1 = 2,
        CollectItem2 = 3,
        CollectItem3 = 4,
        KillNightmare = 5,
    }

    public enum SaveGameType
    {
        [Description("PC")] Pc = 0,
        [Description("Playstation Vita")] PSVita,
        [Description("Playstation 4")] PS4,
        [Description("Nintendo Switch")] NintendoSwitch,
        [Description("Invalid")] Invalid,
    }

    public enum enmLanguage
    {
        [Description("English")] usa, 
        [Description("Japanese")] jpn,
        [Description("German")] ger,
        [Description("French")] fra,
        [Description("Spanish")] esp,
        [Description("Italian")] ita,
        [Description("Korean")] kor,
        [Description("Chinese")] cht,
        [Description("Neutral Spanish")] nsp, 
        [Description("Brazilian Portuguese")] brp, 
    }

}
