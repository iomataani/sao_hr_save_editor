using System;
using System.IO;
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
    public partial class frmMain : Form
    {       
        private const string SKIDROW_PATH = @"C:\Users\{0}\Documents\{1}\607890\SteamEmu\Saves\"; //{0} = PC User Name, {1} = username, default: "SKIDROW"
        private const string CODEX_PATH = @"C:\Users\Public\Documents\Steam\CODEX\607890\remote\";
        private const string STR_TITLE = @"Save Editor by Falo";

        private SaveFile save;
        private enmLanguage currentLanguage;
        private string curFile;
        private bool IsLoading, IsUpdating;

        public frmMain()
        {
            InitializeComponent();
            Database.Init();
            Database.LoadStringTable(enmLanguage.usa);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {  
            this.Text = STR_TITLE;
            
            cboVersion.Items.Clear();
            cboLanguage.Items.Clear();

            foreach (SaveGameType enm in Enum.GetValues(typeof(SaveGameType)))
            {
                cboVersion.Items.Add(enm.GetDescription());
            }
            
            foreach (enmLanguage enm in Enum.GetValues(typeof(enmLanguage)))
            {
                cboLanguage.Items.Add(enm.GetDescription());
            }

            //curFile = "game00.bin";
            //LoadSave(curFile);
            
            cboLanguage.SelectedIndex = 0;
            cboVersion.SelectedIndex = (int) SaveGameType.Invalid;
        }
        
        private void frmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            curFile = files[0];

            try
            {
                LoadSave(curFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Functions

        private void LoadSave(string sPathGame)
        {
            IsLoading = true;

            save = new SaveFile();
            save.Load(sPathGame);

            if (save.game.Version == 2)
                rdoVersion2.Checked = true;
            if (save.game.Version == 3)
                rdoVersion3.Checked = true;
            
            cboVersion.SelectedIndex = (int)save.game.currentType;

            FillForm();

            IsLoading = false;
        }

        private void ChangeLanguage()
        {
            currentLanguage = (enmLanguage) cboLanguage.SelectedIndex;
            Database.LoadStringTable(currentLanguage);

            this.Text = Database.GetString(509999) + @" - " + STR_TITLE;

            grpMainSkill.Text = Database.GetString(501600);
            grpSubSkill.Text = Database.GetString(501602);
            grpInventory.Text = Database.GetString(501404);
            grpStorage.Text = Database.GetString(501458);
            grpNpc.Text = Database.GetString(501800);
            grpCol.Text = Database.GetString(501316);

            //statistic
            grpSoaringSlash.Text = Database.GetString(5600029);
            grpIllusionThorn.Text = Database.GetString(5600032);
            grpDamnedBlade.Text = Database.GetString(5600030);
            grpAssassinsBlade.Text = Database.GetString(5600033);
            grpCelestialBlades.Text = Database.GetString(5600019);
            grpSacredCross.Text = Database.GetString(5600034);
            grpAttacker.Text = Database.GetString(5600014);
            grpTank.Text = Database.GetString(5600015);
            grpBuffer.Text = Database.GetString(5600017);
            grpHealer.Text = Database.GetString(5600016);
            grpScout.Text = Database.GetString(5600031);
            grpFangedGuardian.Text = Database.GetString(5600024);
            grpWarmonger.Text = Database.GetString(5600035);
            grpRejuvenatingTouch.Text = Database.GetString(5600028);

            btnAddNebulaOnslaught.Text = "Add " + Database.GetString(5013290);

            FillForm();
        }

        private void FillForm()
        {
            if(save == null) return;

            numCol.Value = save.game.Col;
            txtPlayerName.Text = save.game.data0x4DC.GetKiritoName();

            numPlaytimeH.Value = save.game.playtime.Hours;
            numPlaytimeM.Value = save.game.playtime.Minutes;
            numPlaytimeS.Value = save.game.playtime.Seconds;

            LoadMainSkills();
            LoadSubSkills();
            LoadInventory();
            LoadStorage();
            LoadCharacters();
            LoadQuests();
            LoadStatistics();
        }

        private void LoadMainSkills(int index = 0)
        {
            lstMainSkill.Items.Clear();

            for (int i = 0; i < save.game.MainSkills.Count; i++) 
                lstMainSkill.Items.Add($"{i:D2}: {save.game.MainSkills[i]}");

            lstMainSkill.SelectedIndex = index;
        }
   
        private void LoadSubSkills(int index = 0)
        {
            lstSubSkill.Items.Clear();

            for (int i = 0; i < save.game.SubSkills.Count; i++) 
                lstSubSkill.Items.Add($"{i:D4}: {save.game.SubSkills[i]}");

            lstSubSkill.SelectedIndex = index;
        }

        private void LoadInventory(int index = 0)
        {
            lstInventory.Items.Clear();

            for (int i = 0; i < save.game.Inventory.Count; i++) 
                lstInventory.Items.Add($"{i:D3}: {save.game.Inventory[i]}");

            lstInventory.SelectedIndex = index;
        }
 
        private void LoadStorage(int index = 0)
        {
            lstStorage.Items.Clear();

            for (int i = 0; i < save.game.Storage.Count; i++) 
                lstStorage.Items.Add($"{i:D3}: {save.game.Storage[i]}");

            lstStorage.SelectedIndex = index;
        }

        private void LoadCharacters(int index = 0)
        {
            lstCharacter.Items.Clear();

            for (int i = 0; i < Structs.Game.MAX_CHARACTER; i++)
            {
                var chara1 = save.game.CharacterDatas[i];
                var chara2 = save.game.CharacterEmotions[i];

                lstCharacter.Items.Add($"{i:D3}: {chara1}");
            }

            lstCharacter.SelectedIndex = index;
        }

        private void LoadQuests(int index = 0)
        {
            lstQuest.Items.Clear();

            for (int i = 0; i < save.game.Data0x2CBB0.Quests.Length; i++)
            {
                var quest = save.game.Data0x2CBB0.Quests[i];

                lstQuest.Items.Add($"{i:D3}: {quest}");
            }

            lstQuest.SelectedIndex = index;
        }

        private void LoadStatistics()
        {
            if(save == null) return;

            //Main Skill
            numSoaringSlash1.Value = save.game.GetStatistic(true, 30, -1);
            numIllusionThorn1.Value = save.game.GetStatistic(true, 31, -1);
            numDamnedBlade1.Value = save.game.GetStatistic(true, 33, -1);
            numAssassinsBlade1.Value = save.game.GetStatistic(true, 34, -1);
            numCelestialBlades1.Value = save.game.GetStatistic(true, 35, -1);
            numSacredCross1.Value = save.game.GetStatistic(true, 37, -1);
            numFangedGuardian1.Value =  save.game.GetStatistic(true, 77, -1);
            numWarmonger1.Value =  save.game.GetStatistic(true, 78, -1);
            numRejuvenatingTouch1.Value =  save.game.GetStatistic(true, 80, -1);

            //Sub Skill
            numSoaringSlash2.Value = save.game.GetStatistic(false, 30, 5122);
            numSoaringSlash3.Value = save.game.GetStatistic(false, 30, 5123);
            numIllusionThorn2.Value =  save.game.GetStatistic(false, 31, 5131);
            numDamnedBlade2.Value = save.game.GetStatistic(false, 33, 1053);
            numAssassinsBlade2.Value = save.game.GetStatistic(false, 34, 5079);
            numCelestialBlades2.Value = save.game.GetStatistic(false, 35, 5120);
            numSacredCross2.Value = save.game.GetStatistic(false, 37, 1052);
            numAttacker.Value =  save.game.GetStatistic(false, 70, 5118);
            numTank.Value =  save.game.GetStatistic(false, 71, 5132);
            numBuffer.Value =  save.game.GetStatistic(false, 72, 5164);
            numHealer.Value =  save.game.GetStatistic(false, 73, 5142);
            numScout.Value =  save.game.GetStatistic(false, 75, 5126);
            numFangedGuardian2.Value =  save.game.GetStatistic(false, 77, 5136);
            numWarmonger2.Value =  save.game.GetStatistic(false, 78, 5156);
            numRejuvenatingTouch2.Value =  save.game.GetStatistic(false, 80, 5147);
        }

        private void UpdateStatistics(object sender, EventArgs e)
        {
            if(save == null || IsLoading || IsUpdating) return;

            //Main Skill
            save.game.SetStatistic(true, 30, -1, (uint)numSoaringSlash1.Value);	
            save.game.SetStatistic(true, 31, -1, (uint)numIllusionThorn1.Value);	
            save.game.SetStatistic(true, 33, -1, (uint)numDamnedBlade1.Value);	
            save.game.SetStatistic(true, 34, -1, (uint)numAssassinsBlade1.Value);	
            save.game.SetStatistic(true, 35, -1, (uint)numCelestialBlades1.Value);	
            save.game.SetStatistic(true, 37, -1, (uint)numSacredCross1.Value);	
            save.game.SetStatistic(true, 77, -1, (uint)numFangedGuardian1.Value);	
            save.game.SetStatistic(true, 78, -1, (uint)numWarmonger1.Value);	
            save.game.SetStatistic(true, 80, -1, (uint)numRejuvenatingTouch1.Value);	

            //Sub Skill
            save.game.SetStatistic(false, 30, 5122, (uint)numSoaringSlash2.Value);	
            save.game.SetStatistic(false, 30, 5123, (uint)numSoaringSlash3.Value);	
            save.game.SetStatistic(false, 31, 5131, (uint)numIllusionThorn2.Value);	
            save.game.SetStatistic(false, 33, 1053, (uint)numDamnedBlade2.Value);	
            save.game.SetStatistic(false, 34, 5079, (uint)numAssassinsBlade2.Value);	
            save.game.SetStatistic(false, 35, 5120, (uint)numCelestialBlades2.Value);	
            save.game.SetStatistic(false, 37, 1052, (uint)numSacredCross2.Value);	
            save.game.SetStatistic(false, 70, 5118, (uint)numAttacker.Value);	
            save.game.SetStatistic(false, 71, 5132, (uint)numTank.Value);	
            save.game.SetStatistic(false, 72, 5164, (uint)numBuffer.Value);	
            save.game.SetStatistic(false, 73, 5142, (uint)numHealer.Value);	
            save.game.SetStatistic(false, 75, 5126, (uint)numScout.Value);	
            save.game.SetStatistic(false, 77, 5136, (uint)numFangedGuardian2.Value);	
            save.game.SetStatistic(false, 78, 5156, (uint)numWarmonger2.Value);	
            save.game.SetStatistic(false, 80, 5147, (uint)numRejuvenatingTouch2.Value);	

            //LoadStatistics();
        }

        #endregion

        #region Form Events
        
        private void lstInventory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(save == null) return;
            int x = lstInventory.SelectedIndex;
            if(x == -1) return;

            using (var frm = new frmItemEditor())
            {
                frm.item = save.game.Inventory[x];

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    save.game.Inventory[x] = frm.item;

                    LoadInventory(x);
                }
            }
        }

        private void lstStorage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(save == null) return;
            int x = lstStorage.SelectedIndex;
            if(x == -1) return;

            using (var frm = new frmItemEditor())
            {
                frm.item = save.game.Storage[x];

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    save.game.Storage[x] = frm.item;

                    LoadStorage(x);
                }
            }
        }
        
        private void lstSkills_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(save == null) return;
            int x = lstMainSkill.SelectedIndex;
            if(x == -1) return;

            var skill = save.game.MainSkills[x];
            
            switch (skill.State)
            {
                case SkillState.Disabled:
                    skill.State = SkillState.Available;
                    break;
                case SkillState.Available:
                    skill.State = SkillState.Learned;
                    break;
                case SkillState.Learned:
                    skill.State = SkillState.Disabled;
                    break;
            }

            save.game.MainSkills[x] = skill;
            lstMainSkill.Items[x] = $"{x:D2}: {save.game.MainSkills[x]}";
        }

        private void lstSubSkill_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(save == null) return;
            int x = lstSubSkill.SelectedIndex;
            if(x == -1) return;

            var skill = save.game.SubSkills[x];
                       
            switch (skill.State)
            {
                case SkillState.Disabled:
                    skill.State = SkillState.Available;
                    break;
                case SkillState.Available:
                    skill.State = SkillState.Learned;
                    break;
                case SkillState.Learned:
                    skill.State = SkillState.Disabled;
                    break;
            }

            save.game.SubSkills[x] = skill;
            lstSubSkill.Items[x] = $"{x:D4}: {save.game.SubSkills[x]}";
        }
        
        private void lstQuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(save == null) return;
            int x = lstQuest.SelectedIndex;
            if(x == -1) return;

            var quest = save.game.Data0x2CBB0.Quests[x];

            if (quest.Id == 0)
            {
                txtQuestDetails.Text = "No Quest";
            }
            else
            {
                var questData = Database.GetQuest(quest.Id);

                txtQuestDetails.Text = "";
                txtQuestDetails.Text += $"ID: {quest.Id}\r\n";
                txtQuestDetails.Text += $"Type: {(int)questData.RequestType}\r\n";
                txtQuestDetails.Text += $"Required Level: {questData.ReqLevel}\r\n";
                txtQuestDetails.Text += $"Timelimit: {quest.time}\r\n";
                txtQuestDetails.Text += $"Name: {Database.GetString(questData.StringId2)}\r\n";
                txtQuestDetails.Text += $"Description: \r\n\r\n\"{Database.GetString(questData.StringId3).Replace("\n", "\r\n")}\"\r\n\r\n";

                if (questData.RequestType == QuestType.KillMonster || questData.RequestType == QuestType.KillNightmare)
                {
                    txtQuestDetails.Text += $"Enemy: {Database.GetEnemyName(questData.EnemyId)} {quest.Progress}/{questData.MaxProgress}\r\n";
                }
                else //collect quest
                {
                    txtQuestDetails.Text += $"Item: {Database.GetItemName(questData.ReqItemType, questData.ReqItemId)} x{questData.ReqItemAmount}\r\n";
                }

                txtQuestDetails.Text += $"Col: {questData.Col}\r\n";
                txtQuestDetails.Text += $"EP: {questData.EP}\r\n";

                if(questData.MapId != -1)
                    txtQuestDetails.Text += $"Map: {Database.GetMapName(questData.MapId)}\r\n";

                foreach (var reward in questData.ItemRewards)
                {
                    if(reward.State == 0) continue;
                    txtQuestDetails.Text += reward.ToString();
                }
            }
        }
        
        private void lstCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(save == null) return;
            int x = lstCharacter.SelectedIndex;
            if(x == -1) return;

            var data = save.game.CharacterDatas[x];
            var emotion = save.game.CharacterEmotions[x];

            numCharEditLevel.Value = data.Level;
            numCharEditAffection.Value = data.Affection;

        }

        private void cboVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((SaveGameType)cboVersion.SelectedIndex)
            {
                case SaveGameType.Pc:
                case SaveGameType.PSVita:
                case SaveGameType.NintendoSwitch:
                    btnSave.Enabled = true;
                    btnSave.Text = @"Save Changes";
                    break;

                default:
                    btnSave.Enabled = false;
                    btnSave.Text = @"Unsupported Version";
                    break;
            }
        }

        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeLanguage();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(save == null) return;

            save.game.Col = (uint) numCol.Value;
            save.game.data0x4DC.SetKiritoName(txtPlayerName.Text);
            save.game.playtime = new Playtime((int)numPlaytimeH.Value, (int)numPlaytimeM.Value, (int)numPlaytimeS.Value);
       
            int GameVersion = 0;

            if (rdoVersion2.Checked)
                GameVersion = 2;
            if (rdoVersion3.Checked)
                GameVersion = 3;

            var gameType = (SaveGameType) cboVersion.SelectedIndex;

            save.Save(curFile + ".new", GameVersion, gameType);
        }

        private void btnSetStackableItems_Click(object sender, EventArgs e)
        {
            if(save == null) return;

            for (int i = 0; i < save.game.Inventory.Count; i++)
            {
                var item = save.game.Inventory[i];
  
                if(item.IsStackable && item.IType != ItemType.Event) 
                    item.Stat1.Data = (ushort)numStackableItems.Value;

                save.game.Inventory[i] = item;
            }

            for (int i = 0; i < save.game.Storage.Count; i++)
            {
                var item = save.game.Storage[i];
                
                if(item.IsStackable && item.IType != ItemType.Event) 
                    item.Stat1.Data = (ushort)numStackableItems.Value;

                save.game.Storage[i] = item;
            }

            LoadInventory();
            LoadStorage();
        }
        
        private void btnSortItems_Click(object sender, EventArgs e)
        {
            if(save == null) return;

            var comparer = new Structs.ItemCompare();

            //sorting the inventory messes with equipment indexes

            //save.game.Inventory.Sort(comparer.Compare);
            //LoadInventory();

            save.game.Storage.Sort(comparer.Compare);
            LoadStorage();
        }

        private void btnSetAllLevel_Click(object sender, EventArgs e)
        {
            if(save == null) return;

            for (int i = 0; i < Structs.Game.MAX_CHARACTER; i++)
            {
                var chara1 = save.game.CharacterDatas[i];
                if(chara1.CharacterId == 0) continue;

                chara1.Level = (ushort)numAllLevel.Value;
                save.game.CharacterDatas[i] = chara1;
            }

            LoadCharacters();
        }

        private void btnSetAllAffection_Click(object sender, EventArgs e)
        {
            if(save == null) return;

            for (int i = 0; i < Structs.Game.MAX_CHARACTER; i++)
            {
                var chara1 = save.game.CharacterDatas[i];
                if(chara1.CharacterId == 0) continue;

                chara1.Affection = (ushort) numAllAffection.Value;
                save.game.CharacterDatas[i] = chara1;
            }

            LoadCharacters();
        }
           
        private void btnSaveCharacter_Click(object sender, EventArgs e)
        {
            if(save == null) return;
            int x = lstCharacter.SelectedIndex;
            if(x == -1) return;

            var data = save.game.CharacterDatas[x];

            if(data.CharacterId == 0) return;

            data.Level = (ushort)numCharEditLevel.Value;
            data.Affection = (ushort)numCharEditAffection.Value;

            save.game.CharacterDatas[x] = data;

            LoadCharacters(x);
        }

        private void btnMaxMainSkill_Click(object sender, EventArgs e)
        {
            if(save == null) return;

            for (int i = 0; i < save.game.MainSkills.Count; i++)
            {
                var skill = save.game.MainSkills[i];

                if(skill.Id == 0) continue;

                if (chkUnlockMainSkill.Checked) 
                    skill.State = SkillState.Learned;

                if (skill.State == SkillState.Learned)
                {
                    skill.SkillLevel = 50000;
                    if (chkMaxMainSkill.Checked) 
                        skill.SkillPoints = 999;
                }

                save.game.MainSkills[i] = skill;
            }

            LoadMainSkills();
        }

        private void btnMaxSubSkill_Click(object sender, EventArgs e)
        {
            if(save == null) return;
            
            for (int i = 0; i < save.game.SubSkills.Count; i++)
            {
                var skill = save.game.SubSkills[i];

                if(skill.Id == 0) continue;

                skill.Mastery = 50000;

                if (chkUnlockSubSkill.Checked)
                    skill.State = SkillState.Learned;

                save.game.SubSkills[i] = skill;
            }

            LoadSubSkills();
        }
        
        private void btnFinishQuests_Click(object sender, EventArgs e)
        {
            if(save == null) return;
                        
            for (int i = 0; i < save.game.Data0x2CBB0.Quests.Length; i++)
            {
                var quest = save.game.Data0x2CBB0.Quests[i];

                if(quest.Id == 0) continue;

                if (quest.State == QuestState.Active)
                {
                    quest.Progress = (byte)Database.GetQuestProgress(quest.Id);
                }

                save.game.Data0x2CBB0.Quests[i] = quest;
            }

            LoadQuests();
        }
        
        private void btnTakeAllQuests_Click(object sender, EventArgs e)
        {
            if(save == null) return;
                        
            for (int i = 0; i < save.game.Data0x2CBB0.Quests.Length; i++)
            {
                var quest = save.game.Data0x2CBB0.Quests[i];

                if(quest.Id == 0) continue;

                if (quest.State == QuestState.Available)
                {
                    quest.State = QuestState.Active;
                }

                save.game.Data0x2CBB0.Quests[i] = quest;
            }

            LoadQuests();
        }

        private void btnAddNebulaOnslaught_Click(object sender, EventArgs e)
        {
            if(save == null) return;

            AddSubSkill(35, 1010); //Nebula Onslaught, the skill is not learnable without save/memory modding
        }
        
        private void btnMaxStatistic_Click(object sender, EventArgs e)
        {
            if(save == null) return;

            var value = 99999999u;
            
            //Main Skill
            save.game.SetStatistic(true, 30, -1, value);
            save.game.SetStatistic(true, 31, -1, value);	
            save.game.SetStatistic(true, 33, -1, value);	
            save.game.SetStatistic(true, 34, -1, value);	
            save.game.SetStatistic(true, 35, -1, value);	
            save.game.SetStatistic(true, 37, -1, value);	
            save.game.SetStatistic(true, 77, -1, value);	
            save.game.SetStatistic(true, 78, -1, value);	
            save.game.SetStatistic(true, 80, -1, value);	

            //Sub Skill
            save.game.SetStatistic(false, 30, 5122, value);	
            save.game.SetStatistic(false, 30, 5123, value);	
            save.game.SetStatistic(false, 31, 5131, value);	
            save.game.SetStatistic(false, 33, 1053, value);	
            save.game.SetStatistic(false, 34, 5079, value);	
            save.game.SetStatistic(false, 35, 5120, value);	
            save.game.SetStatistic(false, 37, 1052, value);	
            save.game.SetStatistic(false, 70, 5118, value);	
            save.game.SetStatistic(false, 71, 5132, value);	
            save.game.SetStatistic(false, 72, 5164, value);	
            save.game.SetStatistic(false, 73, 5142, value);	
            save.game.SetStatistic(false, 75, 5126, value);	
            save.game.SetStatistic(false, 77, 5136, value);	
            save.game.SetStatistic(false, 78, 5156, value);	
            save.game.SetStatistic(false, 80, 5147, value);

            IsUpdating = true;
            LoadStatistics();
            IsUpdating = false;
        }
     
        #endregion

        private void AddSubSkill(ushort type, ushort Id)
        {
            bool AlreadyLearned = save.game.SubSkills.Where(skill => skill.Id != 0).Any(skill => skill.Id == Id && skill.SkillType == type);

            if (!AlreadyLearned)
            {
                for (int i = 0; i < save.game.SubSkills.Count; i++)
                {
                    var skill = save.game.SubSkills[i];

                    if (skill.Id == 0) //add it to an empty slot
                    {
                        skill = new SkillSub
                        {
                            Id = Id,
                            SkillType = type,
                            Mastery = 0,
                            State = SkillState.Learned,
                            Value = 0,
                            unk1 = 0
                        };

                        save.game.SubSkills[i] = skill;

                        break;
                    }
                }

                LoadSubSkills();
            }
            else
            {
                MessageBox.Show($"You already know {Database.GetSubSkillName(type, Id)}!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
