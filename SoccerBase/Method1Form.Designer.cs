namespace SoccerBase
{
    partial class Method1Form
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
            this.browser = new System.Windows.Forms.WebBrowser();
            this.numMinMatches = new System.Windows.Forms.NumericUpDown();
            this.lblMinMatches = new System.Windows.Forms.Label();
            this.cboxTopLeagues = new System.Windows.Forms.ComboBox();
            this.cbBigBet = new System.Windows.Forms.CheckBox();
            this.cbMediumBet = new System.Windows.Forms.CheckBox();
            this.cbSmallBet = new System.Windows.Forms.CheckBox();
            this.cbGroupAll = new System.Windows.Forms.CheckBox();
            this.gbIncrement = new System.Windows.Forms.GroupBox();
            this.tbMaxGroupInvest = new System.Windows.Forms.TextBox();
            this.cbMaxGroupInvest = new System.Windows.Forms.CheckBox();
            this.lblMinInvest = new System.Windows.Forms.Label();
            this.tbMinInvest = new System.Windows.Forms.TextBox();
            this.cbMaxInvest = new System.Windows.Forms.CheckBox();
            this.cbPercentTake = new System.Windows.Forms.CheckBox();
            this.tbMaxInvest = new System.Windows.Forms.TextBox();
            this.tbPercentTake = new System.Windows.Forms.TextBox();
            this.cbIncrement = new System.Windows.Forms.CheckBox();
            this.gpScore = new System.Windows.Forms.GroupBox();
            this.cbScore2 = new System.Windows.Forms.CheckBox();
            this.cbScoreX = new System.Windows.Forms.CheckBox();
            this.cbScore1 = new System.Windows.Forms.CheckBox();
            this.gbTip = new System.Windows.Forms.GroupBox();
            this.cbTip2 = new System.Windows.Forms.CheckBox();
            this.cbTipX = new System.Windows.Forms.CheckBox();
            this.cbTip1 = new System.Windows.Forms.CheckBox();
            this.tbBigBet = new System.Windows.Forms.TextBox();
            this.tbMediumBet = new System.Windows.Forms.TextBox();
            this.tbSmallBet = new System.Windows.Forms.TextBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.cboxGroup = new System.Windows.Forms.ComboBox();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.btnMethod1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numMinMatches)).BeginInit();
            this.gbIncrement.SuspendLayout();
            this.gpScore.SuspendLayout();
            this.gbTip.SuspendLayout();
            this.SuspendLayout();
            // 
            // browser
            // 
            this.browser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browser.Location = new System.Drawing.Point(12, 122);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(1233, 463);
            this.browser.TabIndex = 18;
            // 
            // numMinMatches
            // 
            this.numMinMatches.Location = new System.Drawing.Point(280, 32);
            this.numMinMatches.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMinMatches.Name = "numMinMatches";
            this.numMinMatches.Size = new System.Drawing.Size(83, 20);
            this.numMinMatches.TabIndex = 64;
            this.numMinMatches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMinMatches.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblMinMatches
            // 
            this.lblMinMatches.AutoSize = true;
            this.lblMinMatches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinMatches.Location = new System.Drawing.Point(277, 9);
            this.lblMinMatches.Name = "lblMinMatches";
            this.lblMinMatches.Size = new System.Drawing.Size(86, 16);
            this.lblMinMatches.TabIndex = 63;
            this.lblMinMatches.Text = "Min Matches:";
            // 
            // cboxTopLeagues
            // 
            this.cboxTopLeagues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxTopLeagues.FormattingEnabled = true;
            this.cboxTopLeagues.Location = new System.Drawing.Point(1024, 30);
            this.cboxTopLeagues.Name = "cboxTopLeagues";
            this.cboxTopLeagues.Size = new System.Drawing.Size(80, 21);
            this.cboxTopLeagues.TabIndex = 62;
            this.cboxTopLeagues.Tag = "Method 1";
            // 
            // cbBigBet
            // 
            this.cbBigBet.AccessibleName = "tbBigBet";
            this.cbBigBet.AutoSize = true;
            this.cbBigBet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBigBet.Location = new System.Drawing.Point(870, 8);
            this.cbBigBet.Name = "cbBigBet";
            this.cbBigBet.Size = new System.Drawing.Size(73, 20);
            this.cbBigBet.TabIndex = 61;
            this.cbBigBet.Tag = "";
            this.cbBigBet.Text = "Big Bet:";
            this.cbBigBet.UseVisualStyleBackColor = true;
            // 
            // cbMediumBet
            // 
            this.cbMediumBet.AccessibleName = "tbMediumBet";
            this.cbMediumBet.AutoSize = true;
            this.cbMediumBet.Checked = true;
            this.cbMediumBet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMediumBet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMediumBet.Location = new System.Drawing.Point(763, 8);
            this.cbMediumBet.Name = "cbMediumBet";
            this.cbMediumBet.Size = new System.Drawing.Size(101, 20);
            this.cbMediumBet.TabIndex = 60;
            this.cbMediumBet.Tag = "";
            this.cbMediumBet.Text = "Medium Bet:";
            this.cbMediumBet.UseVisualStyleBackColor = true;
            // 
            // cbSmallBet
            // 
            this.cbSmallBet.AccessibleName = "tbSmallBet";
            this.cbSmallBet.AutoSize = true;
            this.cbSmallBet.Checked = true;
            this.cbSmallBet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSmallBet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSmallBet.Location = new System.Drawing.Point(670, 8);
            this.cbSmallBet.Name = "cbSmallBet";
            this.cbSmallBet.Size = new System.Drawing.Size(87, 20);
            this.cbSmallBet.TabIndex = 44;
            this.cbSmallBet.Tag = "";
            this.cbSmallBet.Text = "Small Bet:";
            this.cbSmallBet.UseVisualStyleBackColor = true;
            // 
            // cbGroupAll
            // 
            this.cbGroupAll.AutoSize = true;
            this.cbGroupAll.Checked = true;
            this.cbGroupAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGroupAll.Location = new System.Drawing.Point(949, 34);
            this.cbGroupAll.Name = "cbGroupAll";
            this.cbGroupAll.Size = new System.Drawing.Size(69, 17);
            this.cbGroupAll.TabIndex = 59;
            this.cbGroupAll.Tag = "Method 1";
            this.cbGroupAll.Text = "Group All";
            this.cbGroupAll.UseVisualStyleBackColor = true;
            // 
            // gbIncrement
            // 
            this.gbIncrement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbIncrement.Controls.Add(this.tbMaxGroupInvest);
            this.gbIncrement.Controls.Add(this.cbMaxGroupInvest);
            this.gbIncrement.Controls.Add(this.lblMinInvest);
            this.gbIncrement.Controls.Add(this.tbMinInvest);
            this.gbIncrement.Controls.Add(this.cbMaxInvest);
            this.gbIncrement.Controls.Add(this.cbPercentTake);
            this.gbIncrement.Controls.Add(this.tbMaxInvest);
            this.gbIncrement.Controls.Add(this.tbPercentTake);
            this.gbIncrement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbIncrement.Location = new System.Drawing.Point(266, 60);
            this.gbIncrement.Name = "gbIncrement";
            this.gbIncrement.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.gbIncrement.Size = new System.Drawing.Size(402, 56);
            this.gbIncrement.TabIndex = 57;
            this.gbIncrement.TabStop = false;
            // 
            // tbMaxGroupInvest
            // 
            this.tbMaxGroupInvest.Location = new System.Drawing.Point(255, 30);
            this.tbMaxGroupInvest.Name = "tbMaxGroupInvest";
            this.tbMaxGroupInvest.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbMaxGroupInvest.Size = new System.Drawing.Size(133, 22);
            this.tbMaxGroupInvest.TabIndex = 46;
            this.tbMaxGroupInvest.Text = "300";
            // 
            // cbMaxGroupInvest
            // 
            this.cbMaxGroupInvest.AccessibleName = "tbMaxGroupInvest";
            this.cbMaxGroupInvest.AutoSize = true;
            this.cbMaxGroupInvest.Checked = true;
            this.cbMaxGroupInvest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMaxGroupInvest.Location = new System.Drawing.Point(255, 11);
            this.cbMaxGroupInvest.Name = "cbMaxGroupInvest";
            this.cbMaxGroupInvest.Size = new System.Drawing.Size(133, 20);
            this.cbMaxGroupInvest.TabIndex = 45;
            this.cbMaxGroupInvest.Tag = "Method 1";
            this.cbMaxGroupInvest.Text = "Max Group Invest:";
            this.cbMaxGroupInvest.UseVisualStyleBackColor = true;
            // 
            // lblMinInvest
            // 
            this.lblMinInvest.AutoSize = true;
            this.lblMinInvest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinInvest.Location = new System.Drawing.Point(6, 12);
            this.lblMinInvest.Name = "lblMinInvest";
            this.lblMinInvest.Size = new System.Drawing.Size(70, 16);
            this.lblMinInvest.TabIndex = 44;
            this.lblMinInvest.Text = "Min Invest:";
            // 
            // tbMinInvest
            // 
            this.tbMinInvest.Location = new System.Drawing.Point(9, 30);
            this.tbMinInvest.Name = "tbMinInvest";
            this.tbMinInvest.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbMinInvest.Size = new System.Drawing.Size(67, 22);
            this.tbMinInvest.TabIndex = 43;
            this.tbMinInvest.Text = "6";
            // 
            // cbMaxInvest
            // 
            this.cbMaxInvest.AccessibleName = "tbMaxInvest";
            this.cbMaxInvest.AutoSize = true;
            this.cbMaxInvest.Enabled = false;
            this.cbMaxInvest.Location = new System.Drawing.Point(162, 11);
            this.cbMaxInvest.Name = "cbMaxInvest";
            this.cbMaxInvest.Size = new System.Drawing.Size(93, 20);
            this.cbMaxInvest.TabIndex = 42;
            this.cbMaxInvest.Tag = "Method 1";
            this.cbMaxInvest.Text = "Max Invest:";
            this.cbMaxInvest.UseVisualStyleBackColor = true;
            // 
            // cbPercentTake
            // 
            this.cbPercentTake.AccessibleName = "tbPercentTake";
            this.cbPercentTake.AutoSize = true;
            this.cbPercentTake.Checked = true;
            this.cbPercentTake.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPercentTake.Location = new System.Drawing.Point(82, 11);
            this.cbPercentTake.Name = "cbPercentTake";
            this.cbPercentTake.Size = new System.Drawing.Size(74, 20);
            this.cbPercentTake.TabIndex = 41;
            this.cbPercentTake.Tag = "Method 1";
            this.cbPercentTake.Text = "%Take:";
            this.cbPercentTake.UseVisualStyleBackColor = true;
            // 
            // tbMaxInvest
            // 
            this.tbMaxInvest.Enabled = false;
            this.tbMaxInvest.Location = new System.Drawing.Point(162, 30);
            this.tbMaxInvest.Name = "tbMaxInvest";
            this.tbMaxInvest.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbMaxInvest.Size = new System.Drawing.Size(87, 22);
            this.tbMaxInvest.TabIndex = 40;
            this.tbMaxInvest.Text = "2000";
            // 
            // tbPercentTake
            // 
            this.tbPercentTake.Location = new System.Drawing.Point(82, 30);
            this.tbPercentTake.Name = "tbPercentTake";
            this.tbPercentTake.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbPercentTake.Size = new System.Drawing.Size(74, 22);
            this.tbPercentTake.TabIndex = 38;
            this.tbPercentTake.Text = "18";
            // 
            // cbIncrement
            // 
            this.cbIncrement.AccessibleName = "gbIncrement";
            this.cbIncrement.AutoSize = true;
            this.cbIncrement.Checked = true;
            this.cbIncrement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncrement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIncrement.Location = new System.Drawing.Point(108, 81);
            this.cbIncrement.Name = "cbIncrement";
            this.cbIncrement.Size = new System.Drawing.Size(152, 20);
            this.cbIncrement.TabIndex = 58;
            this.cbIncrement.Tag = "Method 1";
            this.cbIncrement.Text = "Increment Investment";
            this.cbIncrement.UseVisualStyleBackColor = true;
            // 
            // gpScore
            // 
            this.gpScore.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpScore.Controls.Add(this.cbScore2);
            this.gpScore.Controls.Add(this.cbScoreX);
            this.gpScore.Controls.Add(this.cbScore1);
            this.gpScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpScore.Location = new System.Drawing.Point(545, 9);
            this.gpScore.Name = "gpScore";
            this.gpScore.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.gpScore.Size = new System.Drawing.Size(119, 44);
            this.gpScore.TabIndex = 56;
            this.gpScore.TabStop = false;
            this.gpScore.Text = "Score:";
            // 
            // cbScore2
            // 
            this.cbScore2.AutoSize = true;
            this.cbScore2.Checked = true;
            this.cbScore2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbScore2.Location = new System.Drawing.Point(83, 23);
            this.cbScore2.Name = "cbScore2";
            this.cbScore2.Size = new System.Drawing.Size(34, 20);
            this.cbScore2.TabIndex = 2;
            this.cbScore2.Tag = "Method 1";
            this.cbScore2.Text = "2";
            this.cbScore2.UseVisualStyleBackColor = true;
            // 
            // cbScoreX
            // 
            this.cbScoreX.AutoSize = true;
            this.cbScoreX.Checked = true;
            this.cbScoreX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbScoreX.Location = new System.Drawing.Point(44, 23);
            this.cbScoreX.Name = "cbScoreX";
            this.cbScoreX.Size = new System.Drawing.Size(35, 20);
            this.cbScoreX.TabIndex = 1;
            this.cbScoreX.Tag = "Method 1";
            this.cbScoreX.Text = "X";
            this.cbScoreX.UseVisualStyleBackColor = true;
            // 
            // cbScore1
            // 
            this.cbScore1.AutoSize = true;
            this.cbScore1.Checked = true;
            this.cbScore1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbScore1.Location = new System.Drawing.Point(6, 23);
            this.cbScore1.Name = "cbScore1";
            this.cbScore1.Size = new System.Drawing.Size(34, 20);
            this.cbScore1.TabIndex = 0;
            this.cbScore1.Tag = "Method 1";
            this.cbScore1.Text = "1";
            this.cbScore1.UseVisualStyleBackColor = true;
            // 
            // gbTip
            // 
            this.gbTip.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbTip.Controls.Add(this.cbTip2);
            this.gbTip.Controls.Add(this.cbTipX);
            this.gbTip.Controls.Add(this.cbTip1);
            this.gbTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTip.Location = new System.Drawing.Point(420, 9);
            this.gbTip.Name = "gbTip";
            this.gbTip.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.gbTip.Size = new System.Drawing.Size(119, 44);
            this.gbTip.TabIndex = 55;
            this.gbTip.TabStop = false;
            this.gbTip.Text = "Tips:";
            // 
            // cbTip2
            // 
            this.cbTip2.AutoSize = true;
            this.cbTip2.Checked = true;
            this.cbTip2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTip2.Location = new System.Drawing.Point(83, 23);
            this.cbTip2.Name = "cbTip2";
            this.cbTip2.Size = new System.Drawing.Size(34, 20);
            this.cbTip2.TabIndex = 2;
            this.cbTip2.Tag = "Method 1";
            this.cbTip2.Text = "2";
            this.cbTip2.UseVisualStyleBackColor = true;
            // 
            // cbTipX
            // 
            this.cbTipX.AutoSize = true;
            this.cbTipX.Checked = true;
            this.cbTipX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTipX.Location = new System.Drawing.Point(44, 23);
            this.cbTipX.Name = "cbTipX";
            this.cbTipX.Size = new System.Drawing.Size(35, 20);
            this.cbTipX.TabIndex = 1;
            this.cbTipX.Tag = "Method 1";
            this.cbTipX.Text = "X";
            this.cbTipX.UseVisualStyleBackColor = true;
            // 
            // cbTip1
            // 
            this.cbTip1.AutoSize = true;
            this.cbTip1.Checked = true;
            this.cbTip1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTip1.Location = new System.Drawing.Point(6, 23);
            this.cbTip1.Name = "cbTip1";
            this.cbTip1.Size = new System.Drawing.Size(34, 20);
            this.cbTip1.TabIndex = 0;
            this.cbTip1.Tag = "Method 1";
            this.cbTip1.Text = "1";
            this.cbTip1.UseVisualStyleBackColor = true;
            // 
            // tbBigBet
            // 
            this.tbBigBet.Enabled = false;
            this.tbBigBet.Location = new System.Drawing.Point(870, 32);
            this.tbBigBet.Name = "tbBigBet";
            this.tbBigBet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbBigBet.Size = new System.Drawing.Size(73, 20);
            this.tbBigBet.TabIndex = 54;
            this.tbBigBet.Text = "1.5";
            // 
            // tbMediumBet
            // 
            this.tbMediumBet.Location = new System.Drawing.Point(763, 32);
            this.tbMediumBet.Name = "tbMediumBet";
            this.tbMediumBet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbMediumBet.Size = new System.Drawing.Size(101, 20);
            this.tbMediumBet.TabIndex = 53;
            this.tbMediumBet.Text = "1";
            // 
            // tbSmallBet
            // 
            this.tbSmallBet.Location = new System.Drawing.Point(670, 32);
            this.tbSmallBet.Name = "tbSmallBet";
            this.tbSmallBet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbSmallBet.Size = new System.Drawing.Size(87, 20);
            this.tbSmallBet.TabIndex = 52;
            this.tbSmallBet.Text = "0.5";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroup.Location = new System.Drawing.Point(366, 9);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(48, 16);
            this.lblGroup.TabIndex = 51;
            this.lblGroup.Text = "Group:";
            // 
            // cboxGroup
            // 
            this.cboxGroup.DisplayMember = "2";
            this.cboxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxGroup.FormattingEnabled = true;
            this.cboxGroup.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cboxGroup.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.cboxGroup.Location = new System.Drawing.Point(369, 31);
            this.cboxGroup.Name = "cboxGroup";
            this.cboxGroup.Size = new System.Drawing.Size(45, 21);
            this.cboxGroup.TabIndex = 50;
            this.cboxGroup.Tag = "Method 1";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "yyyy-MM-dd";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(194, 32);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(80, 20);
            this.dtpTo.TabIndex = 49;
            this.dtpTo.Tag = "Method 1";
            this.dtpTo.Value = new System.DateTime(2011, 12, 31, 0, 0, 0, 0);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Location = new System.Drawing.Point(191, 9);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(28, 16);
            this.lblToDate.TabIndex = 48;
            this.lblToDate.Text = "To:";
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Location = new System.Drawing.Point(105, 9);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(42, 16);
            this.lblFromDate.TabIndex = 47;
            this.lblFromDate.Text = "From:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "yyyy-MM-dd";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(108, 32);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(80, 20);
            this.dtpFrom.TabIndex = 46;
            this.dtpFrom.Tag = "Method 1";
            this.dtpFrom.Value = new System.DateTime(2011, 8, 1, 0, 0, 0, 0);
            // 
            // btnMethod1
            // 
            this.btnMethod1.Location = new System.Drawing.Point(12, 12);
            this.btnMethod1.Name = "btnMethod1";
            this.btnMethod1.Size = new System.Drawing.Size(75, 23);
            this.btnMethod1.TabIndex = 45;
            this.btnMethod1.Tag = "Method 1";
            this.btnMethod1.Text = "Method 1";
            this.btnMethod1.UseVisualStyleBackColor = true;
            this.btnMethod1.Click += new System.EventHandler(this.btnMethod1_Click);
            // 
            // Method1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 597);
            this.Controls.Add(this.numMinMatches);
            this.Controls.Add(this.lblMinMatches);
            this.Controls.Add(this.cboxTopLeagues);
            this.Controls.Add(this.cbBigBet);
            this.Controls.Add(this.cbMediumBet);
            this.Controls.Add(this.cbSmallBet);
            this.Controls.Add(this.cbGroupAll);
            this.Controls.Add(this.gbIncrement);
            this.Controls.Add(this.cbIncrement);
            this.Controls.Add(this.gpScore);
            this.Controls.Add(this.gbTip);
            this.Controls.Add(this.tbBigBet);
            this.Controls.Add(this.tbMediumBet);
            this.Controls.Add(this.tbSmallBet);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.cboxGroup);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.lblFromDate);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.btnMethod1);
            this.Controls.Add(this.browser);
            this.Name = "Method1Form";
            this.Text = "Method1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Method1Form_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numMinMatches)).EndInit();
            this.gbIncrement.ResumeLayout(false);
            this.gbIncrement.PerformLayout();
            this.gpScore.ResumeLayout(false);
            this.gpScore.PerformLayout();
            this.gbTip.ResumeLayout(false);
            this.gbTip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.WebBrowser browser;
        public System.Windows.Forms.NumericUpDown numMinMatches;
        private System.Windows.Forms.Label lblMinMatches;
        public System.Windows.Forms.ComboBox cboxTopLeagues;
        public System.Windows.Forms.CheckBox cbBigBet;
        public System.Windows.Forms.CheckBox cbMediumBet;
        public System.Windows.Forms.CheckBox cbSmallBet;
        private System.Windows.Forms.CheckBox cbGroupAll;
        private System.Windows.Forms.GroupBox gbIncrement;
        public System.Windows.Forms.TextBox tbMaxGroupInvest;
        private System.Windows.Forms.CheckBox cbMaxGroupInvest;
        private System.Windows.Forms.Label lblMinInvest;
        public System.Windows.Forms.TextBox tbMinInvest;
        private System.Windows.Forms.CheckBox cbMaxInvest;
        private System.Windows.Forms.CheckBox cbPercentTake;
        public System.Windows.Forms.TextBox tbMaxInvest;
        public System.Windows.Forms.TextBox tbPercentTake;
        public System.Windows.Forms.CheckBox cbIncrement;
        private System.Windows.Forms.GroupBox gpScore;
        public System.Windows.Forms.CheckBox cbScore2;
        public System.Windows.Forms.CheckBox cbScoreX;
        public System.Windows.Forms.CheckBox cbScore1;
        private System.Windows.Forms.GroupBox gbTip;
        public System.Windows.Forms.CheckBox cbTip2;
        public System.Windows.Forms.CheckBox cbTipX;
        public System.Windows.Forms.CheckBox cbTip1;
        public System.Windows.Forms.TextBox tbBigBet;
        public System.Windows.Forms.TextBox tbMediumBet;
        public System.Windows.Forms.TextBox tbSmallBet;
        private System.Windows.Forms.Label lblGroup;
        public System.Windows.Forms.ComboBox cboxGroup;
        public System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblFromDate;
        public System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Button btnMethod1;
    }
}