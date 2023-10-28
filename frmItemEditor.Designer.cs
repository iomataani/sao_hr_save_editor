namespace SaveEditor
{
    partial class frmItemEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboItemType = new System.Windows.Forms.ComboBox();
            this.numItemId = new System.Windows.Forms.NumericUpDown();
            this.cboItemId = new System.Windows.Forms.ComboBox();
            this.numUpgrade = new System.Windows.Forms.NumericUpDown();
            this.grpEquip = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.numSlot2 = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numAwakening = new System.Windows.Forms.NumericUpDown();
            this.numOracle = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.numEffect = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numSlot1 = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numStat3 = new System.Windows.Forms.NumericUpDown();
            this.cboStat3 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numStat2 = new System.Windows.Forms.NumericUpDown();
            this.cboStat2 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numStat1 = new System.Windows.Forms.NumericUpDown();
            this.cboStat1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkBit7 = new System.Windows.Forms.CheckBox();
            this.chkBit6 = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.grpStackable = new System.Windows.Forms.GroupBox();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numItemId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpgrade)).BeginInit();
            this.grpEquip.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSlot2)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAwakening)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOracle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEffect)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSlot1)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStat3)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStat2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStat1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.grpStackable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Id:";
            // 
            // cboItemType
            // 
            this.cboItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItemType.FormattingEnabled = true;
            this.cboItemType.Location = new System.Drawing.Point(192, 8);
            this.cboItemType.Name = "cboItemType";
            this.cboItemType.Size = new System.Drawing.Size(264, 21);
            this.cboItemType.TabIndex = 2;
            this.cboItemType.SelectedIndexChanged += new System.EventHandler(this.cboItemType_SelectedIndexChanged);
            // 
            // numItemId
            // 
            this.numItemId.Location = new System.Drawing.Point(64, 32);
            this.numItemId.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numItemId.Name = "numItemId";
            this.numItemId.Size = new System.Drawing.Size(120, 20);
            this.numItemId.TabIndex = 3;
            this.numItemId.ValueChanged += new System.EventHandler(this.UpdateItem);
            // 
            // cboItemId
            // 
            this.cboItemId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItemId.FormattingEnabled = true;
            this.cboItemId.Location = new System.Drawing.Point(192, 32);
            this.cboItemId.Name = "cboItemId";
            this.cboItemId.Size = new System.Drawing.Size(264, 21);
            this.cboItemId.TabIndex = 4;
            this.cboItemId.SelectedIndexChanged += new System.EventHandler(this.cboItemId_SelectedIndexChanged);
            // 
            // numUpgrade
            // 
            this.numUpgrade.Location = new System.Drawing.Point(8, 16);
            this.numUpgrade.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.numUpgrade.Name = "numUpgrade";
            this.numUpgrade.Size = new System.Drawing.Size(48, 20);
            this.numUpgrade.TabIndex = 6;
            this.numUpgrade.ValueChanged += new System.EventHandler(this.UpdateItem);
            // 
            // grpEquip
            // 
            this.grpEquip.Controls.Add(this.groupBox6);
            this.grpEquip.Controls.Add(this.groupBox7);
            this.grpEquip.Controls.Add(this.groupBox5);
            this.grpEquip.Controls.Add(this.groupBox4);
            this.grpEquip.Controls.Add(this.groupBox3);
            this.grpEquip.Controls.Add(this.groupBox2);
            this.grpEquip.Controls.Add(this.groupBox1);
            this.grpEquip.Location = new System.Drawing.Point(8, 56);
            this.grpEquip.Name = "grpEquip";
            this.grpEquip.Size = new System.Drawing.Size(600, 240);
            this.grpEquip.TabIndex = 7;
            this.grpEquip.TabStop = false;
            this.grpEquip.Text = "Only for Equipment";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.numSlot2);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Location = new System.Drawing.Point(168, 136);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(152, 100);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Slot2";
            // 
            // numSlot2
            // 
            this.numSlot2.Location = new System.Drawing.Point(8, 32);
            this.numSlot2.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numSlot2.Name = "numSlot2";
            this.numSlot2.Size = new System.Drawing.Size(136, 20);
            this.numSlot2.TabIndex = 8;
            this.numSlot2.ValueChanged += new System.EventHandler(this.UpdateItem);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Value:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.numAwakening);
            this.groupBox7.Controls.Add(this.numOracle);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.numEffect);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Location = new System.Drawing.Point(328, 136);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(152, 100);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Other";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Awakening:";
            // 
            // numAwakening
            // 
            this.numAwakening.Location = new System.Drawing.Point(80, 32);
            this.numAwakening.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numAwakening.Name = "numAwakening";
            this.numAwakening.Size = new System.Drawing.Size(64, 20);
            this.numAwakening.TabIndex = 11;
            this.numAwakening.ValueChanged += new System.EventHandler(this.UpdateItem);
            // 
            // numOracle
            // 
            this.numOracle.Location = new System.Drawing.Point(8, 72);
            this.numOracle.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numOracle.Name = "numOracle";
            this.numOracle.Size = new System.Drawing.Size(64, 20);
            this.numOracle.TabIndex = 10;
            this.numOracle.ValueChanged += new System.EventHandler(this.UpdateItem);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Oracle:";
            // 
            // numEffect
            // 
            this.numEffect.Location = new System.Drawing.Point(8, 32);
            this.numEffect.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numEffect.Name = "numEffect";
            this.numEffect.Size = new System.Drawing.Size(64, 20);
            this.numEffect.TabIndex = 8;
            this.numEffect.ValueChanged += new System.EventHandler(this.UpdateItem);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Effect:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.numSlot1);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Location = new System.Drawing.Point(8, 136);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(152, 100);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Slot1";
            // 
            // numSlot1
            // 
            this.numSlot1.Location = new System.Drawing.Point(8, 32);
            this.numSlot1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numSlot1.Name = "numSlot1";
            this.numSlot1.Size = new System.Drawing.Size(136, 20);
            this.numSlot1.TabIndex = 8;
            this.numSlot1.ValueChanged += new System.EventHandler(this.UpdateItem);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Value:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numStat3);
            this.groupBox4.Controls.Add(this.cboStat3);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(328, 24);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(152, 100);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Stat3";
            // 
            // numStat3
            // 
            this.numStat3.Location = new System.Drawing.Point(8, 72);
            this.numStat3.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.numStat3.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            -2147483648});
            this.numStat3.Name = "numStat3";
            this.numStat3.Size = new System.Drawing.Size(136, 20);
            this.numStat3.TabIndex = 6;
            this.numStat3.ValueChanged += new System.EventHandler(this.UpdateItem);
            // 
            // cboStat3
            // 
            this.cboStat3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStat3.FormattingEnabled = true;
            this.cboStat3.Location = new System.Drawing.Point(8, 32);
            this.cboStat3.Name = "cboStat3";
            this.cboStat3.Size = new System.Drawing.Size(136, 21);
            this.cboStat3.TabIndex = 5;
            this.cboStat3.SelectedIndexChanged += new System.EventHandler(this.UpdateItem);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Value:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Type:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numStat2);
            this.groupBox3.Controls.Add(this.cboStat2);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(168, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(152, 100);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Stat2";
            // 
            // numStat2
            // 
            this.numStat2.Location = new System.Drawing.Point(8, 72);
            this.numStat2.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.numStat2.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            -2147483648});
            this.numStat2.Name = "numStat2";
            this.numStat2.Size = new System.Drawing.Size(136, 20);
            this.numStat2.TabIndex = 6;
            this.numStat2.ValueChanged += new System.EventHandler(this.UpdateItem);
            // 
            // cboStat2
            // 
            this.cboStat2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStat2.FormattingEnabled = true;
            this.cboStat2.Location = new System.Drawing.Point(8, 32);
            this.cboStat2.Name = "cboStat2";
            this.cboStat2.Size = new System.Drawing.Size(136, 21);
            this.cboStat2.TabIndex = 5;
            this.cboStat2.SelectedIndexChanged += new System.EventHandler(this.UpdateItem);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Value:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Type:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numStat1);
            this.groupBox2.Controls.Add(this.cboStat1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(8, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(152, 100);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stat1";
            // 
            // numStat1
            // 
            this.numStat1.Location = new System.Drawing.Point(8, 72);
            this.numStat1.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.numStat1.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            -2147483648});
            this.numStat1.Name = "numStat1";
            this.numStat1.Size = new System.Drawing.Size(136, 20);
            this.numStat1.TabIndex = 6;
            this.numStat1.ValueChanged += new System.EventHandler(this.UpdateItem);
            // 
            // cboStat1
            // 
            this.cboStat1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStat1.FormattingEnabled = true;
            this.cboStat1.Location = new System.Drawing.Point(8, 32);
            this.cboStat1.Name = "cboStat1";
            this.cboStat1.Size = new System.Drawing.Size(136, 21);
            this.cboStat1.TabIndex = 5;
            this.cboStat1.SelectedIndexChanged += new System.EventHandler(this.UpdateItem);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Value:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Type:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBit7);
            this.groupBox1.Controls.Add(this.chkBit6);
            this.groupBox1.Controls.Add(this.numUpgrade);
            this.groupBox1.Location = new System.Drawing.Point(488, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(104, 100);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upgrade";
            // 
            // chkBit7
            // 
            this.chkBit7.AutoSize = true;
            this.chkBit7.Location = new System.Drawing.Point(8, 64);
            this.chkBit7.Name = "chkBit7";
            this.chkBit7.Size = new System.Drawing.Size(44, 17);
            this.chkBit7.TabIndex = 1;
            this.chkBit7.Text = "Bit7";
            this.chkBit7.UseVisualStyleBackColor = true;
            this.chkBit7.CheckedChanged += new System.EventHandler(this.UpdateItem);
            // 
            // chkBit6
            // 
            this.chkBit6.AutoSize = true;
            this.chkBit6.Location = new System.Drawing.Point(8, 40);
            this.chkBit6.Name = "chkBit6";
            this.chkBit6.Size = new System.Drawing.Size(44, 17);
            this.chkBit6.TabIndex = 0;
            this.chkBit6.Text = "Bit6";
            this.chkBit6.UseVisualStyleBackColor = true;
            this.chkBit6.CheckedChanged += new System.EventHandler(this.UpdateItem);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtResult);
            this.groupBox8.Location = new System.Drawing.Point(8, 304);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(600, 100);
            this.groupBox8.TabIndex = 8;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Result";
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Location = new System.Drawing.Point(3, 16);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(594, 81);
            this.txtResult.TabIndex = 0;
            // 
            // grpStackable
            // 
            this.grpStackable.Controls.Add(this.numCount);
            this.grpStackable.Location = new System.Drawing.Point(464, 8);
            this.grpStackable.Name = "grpStackable";
            this.grpStackable.Size = new System.Drawing.Size(144, 48);
            this.grpStackable.TabIndex = 9;
            this.grpStackable.TabStop = false;
            this.grpStackable.Text = "Value (Only for stackable)";
            // 
            // numCount
            // 
            this.numCount.Location = new System.Drawing.Point(8, 16);
            this.numCount.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(120, 20);
            this.numCount.TabIndex = 4;
            this.numCount.ValueChanged += new System.EventHandler(this.UpdateItem);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(448, 408);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(528, 408);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmItemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 434);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpStackable);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.grpEquip);
            this.Controls.Add(this.cboItemId);
            this.Controls.Add(this.numItemId);
            this.Controls.Add(this.cboItemType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmItemEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Item Editor";
            this.Load += new System.EventHandler(this.frmItemEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numItemId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpgrade)).EndInit();
            this.grpEquip.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSlot2)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAwakening)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOracle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEffect)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSlot1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStat3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStat2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStat1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.grpStackable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboItemType;
        private System.Windows.Forms.NumericUpDown numItemId;
        private System.Windows.Forms.ComboBox cboItemId;
        private System.Windows.Forms.NumericUpDown numUpgrade;
        private System.Windows.Forms.GroupBox grpEquip;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown numSlot2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown numOracle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numEffect;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown numSlot1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown numStat3;
        private System.Windows.Forms.ComboBox cboStat3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numStat2;
        private System.Windows.Forms.ComboBox cboStat2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numStat1;
        private System.Windows.Forms.ComboBox cboStat1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkBit7;
        private System.Windows.Forms.CheckBox chkBit6;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.GroupBox grpStackable;
        private System.Windows.Forms.NumericUpDown numCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numAwakening;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}