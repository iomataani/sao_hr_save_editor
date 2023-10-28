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
    public struct Mail
    {
        public const int SIZE = 16;

        public short Index, MailId, CharacterId, Index2;
        public byte IsRead, IsNew, unk1, MailState;
        public short SendState;
        public byte GiftState1, GiftState2;

        public void Init()
        {
            Index = 9999; //Index in List
            MailId = 9999;
            CharacterId = 998;
            Index2 = 9999; //sort?

            IsRead = 0;
            IsNew = 1;
            unk1 = 0;
            MailState = 0; //0 = Disabled, 1 = ???, 2 = ???
            SendState = -1; //-1 = UnSend, 0 = Send, only applies to request mails
            GiftState1 = 1; //1 = No Gift
            GiftState2 = 1; //1 = No Gift
        }
    }
}
