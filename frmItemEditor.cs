using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SaveEditor.Structs;

namespace SaveEditor
{
    public partial class frmItemEditor : Form
    {
        private readonly Dictionary<ItemType, int> TypeTable;
        private Dictionary<int, FixItem> TempDatabase;

        public Item item;
        private bool IsLoading, IsUpdating;

        public frmItemEditor()
        {
            InitializeComponent();

            //create type table
            int i = 0;
            TypeTable = new Dictionary<ItemType, int>();
            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                TypeTable.Add(type, i++);
            }
        }

        private void frmItemEditor_Load(object sender, EventArgs e)
        {
            IsLoading = true;

            cboItemType.Items.Clear();

            foreach (var v in TypeTable)
            {
                cboItemType.Items.Add($"{(int)v.Key:D3} - {Database.GetItemTypeName(v.Key)}");
            }

            cboItemType.SelectedIndex = 0;

            cboStat1.Items.Clear();
            cboStat2.Items.Clear();
            cboStat3.Items.Clear();

            foreach (ItemStatType stat in Enum.GetValues(typeof(ItemStatType)))
            {
                var id = (int) stat;

                cboStat1.Items.Add($"{id:D2} - {Database.GetItemStatName(stat)}");
                cboStat2.Items.Add($"{id:D2} - {Database.GetItemStatName(stat)}");
                cboStat3.Items.Add($"{id:D2} - {Database.GetItemStatName(stat)}");
            }

            UpdateForm();
            IsLoading = false;
        }
        
        private void cboItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateItemType();
            UpdateItem(sender, e);
        }
        
        private void cboItemId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(IsLoading || IsUpdating) return;
            numItemId.Value = Util.GetSortedKey<FixItem>(cboItemId);
        }

        private void UpdateForm()
        {
            numItemId.Value = item.Id;
            cboItemType.SelectedIndex = TypeTable[item.IType];
            UpdateItemType();

            numUpgrade.Value = item.UpgradeValue;

            chkBit6.Checked = item.UpgradeBit6;
            chkBit7.Checked = item.UpgradeBit7;

            cboStat1.SelectedIndex = (int)item.Stat1.Type;
            cboStat2.SelectedIndex = (int)item.Stat2.Type;
            cboStat3.SelectedIndex = (int)item.Stat3.Type;

            numStat1.Value = item.Stat1.Value;
            numCount.Value = item.Stat1.Data;
            numStat2.Value = item.Stat2.Value;
            numStat3.Value = item.Stat3.Value;

            numSlot1.Value = item.Slot1;
            numSlot2.Value = item.Slot2;

            numEffect.Value = item.Effect;
            numOracle.Value = item.Oracle;

            numAwakening.Value = item.Awakening;

            txtResult.Text = BitConverter.ToString(Util.StructureToByteArray(item)).Replace("-", " ") + "\r\n\r\n";
            txtResult.Text += item.ToString();

            if (TempDatabase.ContainsKey(item.Id))
            {
                Util.SetSortedIndex<FixItem>(cboItemId, item.Id);
            }
        }

        private void UpdateItem(object sender, EventArgs e)
        {
            if(IsLoading) return;
            IsUpdating = true;

            item = new Item
            {
                Id = (ushort) numItemId.Value, 
                IType = (ItemType) KeyByValue(TypeTable, cboItemType.SelectedIndex)
            };

            if (item.IsStackable)
            {
                item.Stat1.Data = (ushort)numCount.Value;
            }
            else
            {
                item.UpgradeValue = (byte)numUpgrade.Value;
                item.UpgradeBit6 = chkBit6.Checked;
                item.UpgradeBit7 = chkBit7.Checked;
                item.Stat1.Type = (ItemStatType)cboStat1.SelectedIndex;
                item.Stat2.Type = (ItemStatType)cboStat2.SelectedIndex;
                item.Stat3.Type = (ItemStatType)cboStat3.SelectedIndex;
                item.Stat1.Value = (int)numStat1.Value;
                item.Stat2.Value = (int)numStat2.Value;
                item.Stat3.Value = (int)numStat3.Value;
                item.Slot1 = (ushort)numSlot1.Value;
                item.Slot2 = (ushort)numSlot2.Value;
                item.Effect = (ushort)numEffect.Value;
                item.Oracle = (ushort)numOracle.Value;
                item.Awakening = (byte) numAwakening.Value;
            }

            UpdateForm();
            IsUpdating = false;
        }
        
        private void UpdateItemType()
        {
            var type = KeyByValue(TypeTable, cboItemType.SelectedIndex);

            TempDatabase = new Dictionary<int, FixItem>();

            if (cboItemType.SelectedIndex == 0)
            {
                TempDatabase.Add(0, new FixItem(){ Id = 0, StringId = 501819});
            }

            foreach (var itm in Database.ItemDatabase)
            {
                if (itm.Value.ItemType == (int)type)
                {
                    TempDatabase.Add(itm.Value.Id, itm.Value);
                }
            }

            Util.SetComboBoxDataSource(cboItemId, TempDatabase);

            grpEquip.Enabled = !Database.GetItemIsStackable(type);
            grpStackable.Enabled = Database.GetItemIsStackable(type);
        }

        private ItemType KeyByValue(Dictionary<ItemType, int> dict, int val)
        {
            ItemType key = ItemType.None;
            foreach (KeyValuePair<ItemType, int> pair in dict)
            {
                if (pair.Value == val)
                { 
                    key = pair.Key; 
                    break; 
                }
            }
            return key;
        }

    }
}
