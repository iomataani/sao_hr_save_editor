using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;

namespace SaveEditor.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 2)]
    public struct ItemStat
    {
        public ushort Data;

        public ItemStatType Type //0 - 31, 24-31 = unused
        {
            get => (ItemStatType)(Data & 0x1F);
            set => Data = (ushort)((Data & 0xFFE0) | ((int)value & 0x1F));
        }
        
        public int Value // -1024 ~ +1023
        {
            get
            {
                var v = (Data >> 5) & 0x3FF;
                if ((Data & 0x8000) > 0) 
                    v = v - 1024;

                return v;
            }

            set
            {   
                var v = value;
                bool IsSigned = v < 0;
                if(v < 0) v = v + 1024;

                Data = (ushort)((Data & 0x1F) | ((v & 0x3FF) << 5) | (IsSigned ? 0x8000 : 0));
            }
        }

        public override string ToString()
        {
            if (Type == ItemStatType.None) return "";

            int value = Value;
            if (Type == ItemStatType.HP) value *= 100;

            if(value < 0)
                return $"{Database.GetItemStatName(Type)} {value}";
            else
                return $"{Database.GetItemStatName(Type)} +{value}";
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SIZE)]
    public struct Item
    {
        public const int SIZE = 0x18;

        public ushort Id;
        public ItemType IType;
        public byte Upgrade;
        public ItemStat Stat1, Stat2, Stat3;
        public ushort Slot1, Slot2, Effect, Oracle;
        public byte Awakening, unk1;
        public ushort unk2, unk3;

        public int UpgradeValue
        {
            get => Upgrade & 0x3F;
            set => Upgrade = (byte)((Upgrade & 0xC0) | (value & 0x3F));
        }
                 
        public bool UpgradeBit6 //adds an hidden extra effect
        {
            get => (Upgrade & 0x40) > 0;
            set => Upgrade = (byte)((Upgrade & 0xBF) | (value ? 0x40 : 0));
        }
         
        public bool UpgradeBit7 //adds an hidden extra effect
        {
            get => (Upgrade & 0x80) > 0;
            set => Upgrade = (byte)((Upgrade & 0x7F) | (value ? 0x80 : 0));
        }

        public bool IsStackable => Database.GetItemIsStackable(IType);

        public string GetName()
        {
            return Database.GetItemName(IType, Id);
        }

        public override string ToString()
        {
            string itemName = GetName();

            if (IType == ItemType.None) return Database.GetString(501819);


            //if (IsStackable) return $"[{IType} - {Id:D4} - {itemName}] x{Stat1.Data}"; //debug
            if (IsStackable) return $"{Id:D4} - {itemName} x{Stat1.Data}";

            var ValueList = new List<string>();

            if (UpgradeValue > 0) itemName += $" +{UpgradeValue}";

            //ValueList.Add($"[{IType} - {Id:D4} - {itemName}]"); //debug
            ValueList.Add($"{Id:D4} - {itemName}");

            if(Stat1.Type != ItemStatType.None) ValueList.Add(Stat1.ToString());
            if(Stat2.Type != ItemStatType.None) ValueList.Add(Stat2.ToString());
            if(Stat3.Type != ItemStatType.None) ValueList.Add(Stat3.ToString());
            if(Slot1 > 0) ValueList.Add($"Slot1:{Slot1}");
            if(Slot2 > 0) ValueList.Add($"Slot2:{Slot2}");
            if(Effect > 0) ValueList.Add($"Effect:{Effect}");
            if(Oracle > 0) ValueList.Add($"Oracle+{Oracle}");

            return ValueList.Aggregate((a, b) => a + ", " + b);

        }
    }

    public class ItemCompare : IComparer<Item>
    {
        public int Compare(Item i1, Item i2)
        {
            int result;  

            if (i1.IType == ItemType.None)  
            {  
                result = 1;  
            }  
            else if (i2.IType == ItemType.None)  
            {  
                result = -1;  
            }  
            else
            {
                if (!i1.IsStackable && i2.IsStackable) //stackable items first
                    result = 1;
                else if (i1.IsStackable && !i2.IsStackable) //then non-stackable
                    result = -1;
                else
                {
                    result = NumberCompare((int)i1.IType, (int)i2.IType);
                    if (result == 0)
                    {
                        result = NumberCompare(i1.Id, i2.Id);
                    }
                }
            }  

            return result; 
        }

        int NumberCompare(int number1, int number2)  
        {  
            int result;  
            if (number1 > number2)  
            {  
                result = 1;  
            }  
            else if (number1 < number2)  
            {  
                result = -1;  
            }  
            else  
            {  
                result = 0;  
            }  
            return result;  
        }




    }

}
