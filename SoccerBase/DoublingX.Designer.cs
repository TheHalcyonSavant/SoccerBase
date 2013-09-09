namespace SoccerBase
{
    partial class DoublingXForm
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
            this.btnCheckWorst = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.lbTrace = new System.Windows.Forms.ListBox();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnBet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCheckWorst
            // 
            this.btnCheckWorst.Location = new System.Drawing.Point(12, 12);
            this.btnCheckWorst.Name = "btnCheckWorst";
            this.btnCheckWorst.Size = new System.Drawing.Size(125, 23);
            this.btnCheckWorst.TabIndex = 0;
            this.btnCheckWorst.Tag = "WorstSituation";
            this.btnCheckWorst.Text = "Worst Situation";
            this.btnCheckWorst.UseVisualStyleBackColor = true;
            this.btnCheckWorst.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this._backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this._backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this._backgroundWorker_RunWorkerCompleted);
            // 
            // lbTrace
            // 
            this.lbTrace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTrace.BackColor = System.Drawing.SystemColors.Control;
            this.lbTrace.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbTrace.FormattingEnabled = true;
            this.lbTrace.Location = new System.Drawing.Point(12, 73);
            this.lbTrace.Name = "lbTrace";
            this.lbTrace.Size = new System.Drawing.Size(647, 277);
            this.lbTrace.TabIndex = 22;
            // 
            // _progressBar
            // 
            this._progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._progressBar.BackColor = System.Drawing.SystemColors.Control;
            this._progressBar.Location = new System.Drawing.Point(12, 44);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(647, 23);
            this._progressBar.TabIndex = 23;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(596, 12);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(63, 23);
            this.btnTest.TabIndex = 24;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnBet
            // 
            this.btnBet.Location = new System.Drawing.Point(143, 12);
            this.btnBet.Name = "btnBet";
            this.btnBet.Size = new System.Drawing.Size(112, 23);
            this.btnBet.TabIndex = 25;
            this.btnBet.Tag = "Bet";
            this.btnBet.Text = "Bet Some Money";
            this.btnBet.UseVisualStyleBackColor = true;
            this.btnBet.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // DoublingXForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 362);
            this.Controls.Add(this.btnBet);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this._progressBar);
            this.Controls.Add(this.lbTrace);
            this.Controls.Add(this.btnCheckWorst);
            this.Name = "DoublingXForm";
            this.Text = "DoublingX";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DoublingXForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCheckWorst;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        public System.Windows.Forms.ListBox lbTrace;
        private System.Windows.Forms.ProgressBar _progressBar;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnBet;
    }
}