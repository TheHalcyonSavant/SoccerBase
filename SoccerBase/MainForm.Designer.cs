namespace SoccerBase
{
    partial class MainForm
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
            this.btnIvnet = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnTest2 = new System.Windows.Forms.Button();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this.lbTrace = new System.Windows.Forms.ListBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.btnSportRadar = new System.Windows.Forms.Button();
            this.btnLink = new System.Windows.Forms.Button();
            this.btnNewIvnet = new System.Windows.Forms.Button();
            this.btnArchive = new System.Windows.Forms.Button();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMethods = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMethod1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewIvnetMatches = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMethodA = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDoublingX = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnLeagues = new System.Windows.Forms.Button();
            this.btnTruncate = new System.Windows.Forms.Button();
            this.btnPrevAvgStrength = new System.Windows.Forms.Button();
            this.numericTextBox1 = new SoccerBase.NumericTextBox();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnIvnet
            // 
            this.btnIvnet.AccessibleName = "";
            this.btnIvnet.Location = new System.Drawing.Point(414, 26);
            this.btnIvnet.Name = "btnIvnet";
            this.btnIvnet.Size = new System.Drawing.Size(147, 23);
            this.btnIvnet.TabIndex = 0;
            this.btnIvnet.Tag = "Ivnet";
            this.btnIvnet.Text = "Process ivnet";
            this.btnIvnet.UseVisualStyleBackColor = true;
            this.btnIvnet.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(729, 26);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnTest2
            // 
            this.btnTest2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest2.Location = new System.Drawing.Point(648, 26);
            this.btnTest2.Name = "btnTest2";
            this.btnTest2.Size = new System.Drawing.Size(75, 23);
            this.btnTest2.TabIndex = 2;
            this.btnTest2.Text = "Test 2";
            this.btnTest2.UseVisualStyleBackColor = true;
            this.btnTest2.Click += new System.EventHandler(this.btnTest2_Click);
            // 
            // _progressBar
            // 
            this._progressBar.BackColor = System.Drawing.SystemColors.Control;
            this._progressBar.Location = new System.Drawing.Point(12, 26);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(396, 23);
            this._progressBar.TabIndex = 12;
            // 
            // lbTrace
            // 
            this.lbTrace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTrace.BackColor = System.Drawing.SystemColors.Control;
            this.lbTrace.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbTrace.FormattingEnabled = true;
            this.lbTrace.Location = new System.Drawing.Point(12, 58);
            this.lbTrace.Name = "lbTrace";
            this.lbTrace.Size = new System.Drawing.Size(396, 264);
            this.lbTrace.TabIndex = 13;
            this.lbTrace.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbTrace_DrawItem);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // btnSportRadar
            // 
            this.btnSportRadar.AccessibleName = "";
            this.btnSportRadar.Location = new System.Drawing.Point(414, 84);
            this.btnSportRadar.Name = "btnSportRadar";
            this.btnSportRadar.Size = new System.Drawing.Size(147, 23);
            this.btnSportRadar.TabIndex = 14;
            this.btnSportRadar.Tag = "SportRadar";
            this.btnSportRadar.Text = "Process SportRadar";
            this.btnSportRadar.UseVisualStyleBackColor = true;
            this.btnSportRadar.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnLink
            // 
            this.btnLink.Location = new System.Drawing.Point(414, 113);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(147, 23);
            this.btnLink.TabIndex = 15;
            this.btnLink.Tag = "Link";
            this.btnLink.Text = "Link";
            this.btnLink.UseVisualStyleBackColor = true;
            this.btnLink.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnNewIvnet
            // 
            this.btnNewIvnet.AccessibleName = "";
            this.btnNewIvnet.Location = new System.Drawing.Point(414, 55);
            this.btnNewIvnet.Name = "btnNewIvnet";
            this.btnNewIvnet.Size = new System.Drawing.Size(147, 23);
            this.btnNewIvnet.TabIndex = 41;
            this.btnNewIvnet.Tag = "NewIvnet";
            this.btnNewIvnet.Text = "Process New ivnet";
            this.btnNewIvnet.UseVisualStyleBackColor = true;
            this.btnNewIvnet.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnArchive
            // 
            this.btnArchive.AccessibleName = "";
            this.btnArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnArchive.Location = new System.Drawing.Point(414, 213);
            this.btnArchive.Name = "btnArchive";
            this.btnArchive.Size = new System.Drawing.Size(147, 23);
            this.btnArchive.TabIndex = 44;
            this.btnArchive.Tag = "Archive";
            this.btnArchive.Text = "Process Archive";
            this.btnArchive.UseVisualStyleBackColor = true;
            this.btnArchive.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmMethods});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(816, 24);
            this.menu.TabIndex = 45;
            // 
            // tsmFile
            // 
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(37, 20);
            this.tsmFile.Text = "&File";
            // 
            // tsmMethods
            // 
            this.tsmMethods.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMethod1,
            this.tsmiNewIvnetMatches,
            this.tsmiMethodA,
            this.tsmiDoublingX});
            this.tsmMethods.Name = "tsmMethods";
            this.tsmMethods.Size = new System.Drawing.Size(66, 20);
            this.tsmMethods.Text = "Methods";
            // 
            // tsmiMethod1
            // 
            this.tsmiMethod1.Name = "tsmiMethod1";
            this.tsmiMethod1.Size = new System.Drawing.Size(175, 22);
            this.tsmiMethod1.Text = "Method 1";
            this.tsmiMethod1.Click += new System.EventHandler(this.tsmiMethod1_Click);
            // 
            // tsmiNewIvnetMatches
            // 
            this.tsmiNewIvnetMatches.Name = "tsmiNewIvnetMatches";
            this.tsmiNewIvnetMatches.Size = new System.Drawing.Size(175, 22);
            this.tsmiNewIvnetMatches.Text = "New Ivnet Matches";
            this.tsmiNewIvnetMatches.Click += new System.EventHandler(this.tsmiNewIvnetMatches_Click);
            // 
            // tsmiMethodA
            // 
            this.tsmiMethodA.Name = "tsmiMethodA";
            this.tsmiMethodA.Size = new System.Drawing.Size(175, 22);
            this.tsmiMethodA.Text = "Method A";
            this.tsmiMethodA.Click += new System.EventHandler(this.tsmiMethodA_Click);
            // 
            // tsmiDoublingX
            // 
            this.tsmiDoublingX.Name = "tsmiDoublingX";
            this.tsmiDoublingX.Size = new System.Drawing.Size(175, 22);
            this.tsmiDoublingX.Text = "Doubling X";
            this.tsmiDoublingX.Click += new System.EventHandler(this.tsmiDoublingX_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHistory.Location = new System.Drawing.Point(414, 271);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(147, 23);
            this.btnHistory.TabIndex = 46;
            this.btnHistory.Tag = "History";
            this.btnHistory.Text = "Prepare History";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnLeagues
            // 
            this.btnLeagues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLeagues.Location = new System.Drawing.Point(414, 242);
            this.btnLeagues.Name = "btnLeagues";
            this.btnLeagues.Size = new System.Drawing.Size(147, 23);
            this.btnLeagues.TabIndex = 47;
            this.btnLeagues.Tag = "Leagues";
            this.btnLeagues.Text = "Prepare Leagues";
            this.btnLeagues.UseVisualStyleBackColor = true;
            this.btnLeagues.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnTruncate
            // 
            this.btnTruncate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTruncate.Location = new System.Drawing.Point(414, 184);
            this.btnTruncate.Name = "btnTruncate";
            this.btnTruncate.Size = new System.Drawing.Size(147, 23);
            this.btnTruncate.TabIndex = 48;
            this.btnTruncate.Tag = "Truncate";
            this.btnTruncate.Text = "Truncate All Tables";
            this.btnTruncate.UseVisualStyleBackColor = true;
            this.btnTruncate.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPrevAvgStrength
            // 
            this.btnPrevAvgStrength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrevAvgStrength.Location = new System.Drawing.Point(414, 300);
            this.btnPrevAvgStrength.Name = "btnPrevAvgStrength";
            this.btnPrevAvgStrength.Size = new System.Drawing.Size(147, 23);
            this.btnPrevAvgStrength.TabIndex = 49;
            this.btnPrevAvgStrength.Tag = "PrevAvgStrength";
            this.btnPrevAvgStrength.Text = "Prepare PrevAvgStrength";
            this.btnPrevAvgStrength.UseVisualStyleBackColor = true;
            this.btnPrevAvgStrength.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // numericTextBox1
            // 
            this.numericTextBox1.AllowSpace = false;
            this.numericTextBox1.Location = new System.Drawing.Point(414, 147);
            this.numericTextBox1.Name = "numericTextBox1";
            this.numericTextBox1.Size = new System.Drawing.Size(147, 20);
            this.numericTextBox1.TabIndex = 30;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 339);
            this.Controls.Add(this.btnPrevAvgStrength);
            this.Controls.Add(this.btnTruncate);
            this.Controls.Add(this.btnLeagues);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.btnArchive);
            this.Controls.Add(this.btnNewIvnet);
            this.Controls.Add(this.numericTextBox1);
            this.Controls.Add(this.btnLink);
            this.Controls.Add(this.btnSportRadar);
            this.Controls.Add(this.lbTrace);
            this.Controls.Add(this._progressBar);
            this.Controls.Add(this.btnTest2);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnIvnet);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Soccer Base";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIvnet;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnTest2;
        private System.Windows.Forms.ProgressBar _progressBar;
        private System.Windows.Forms.Button btnSportRadar;
        private System.Windows.Forms.Button btnLink;
        private NumericTextBox numericTextBox1;
        public System.Windows.Forms.ListBox lbTrace;
        private System.Windows.Forms.Button btnNewIvnet;
        private System.Windows.Forms.Button btnArchive;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmMethods;
        private System.Windows.Forms.ToolStripMenuItem tsmiMethod1;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewIvnetMatches;
        private System.Windows.Forms.ToolStripMenuItem tsmiMethodA;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnLeagues;
        public System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button btnTruncate;
        private System.Windows.Forms.ToolStripMenuItem tsmiDoublingX;
        private System.Windows.Forms.Button btnPrevAvgStrength;
    }
}

