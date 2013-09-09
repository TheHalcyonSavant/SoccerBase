namespace SoccerBase
{
    partial class NewMatchesForm
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
            this.btnNewMatches = new System.Windows.Forms.Button();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.tbBigBet = new System.Windows.Forms.TextBox();
            this.tbMediumBet = new System.Windows.Forms.TextBox();
            this.tbSmallBet = new System.Windows.Forms.TextBox();
            this.cbBigBet = new System.Windows.Forms.CheckBox();
            this.cbMediumBet = new System.Windows.Forms.CheckBox();
            this.cbSmallBet = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnNewMatches
            // 
            this.btnNewMatches.Location = new System.Drawing.Point(12, 12);
            this.btnNewMatches.Name = "btnNewMatches";
            this.btnNewMatches.Size = new System.Drawing.Size(92, 23);
            this.btnNewMatches.TabIndex = 46;
            this.btnNewMatches.Tag = "";
            this.btnNewMatches.Text = "New Matches";
            this.btnNewMatches.UseVisualStyleBackColor = true;
            this.btnNewMatches.Click += new System.EventHandler(this.btnNewMatches_Click);
            // 
            // browser
            // 
            this.browser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.browser.Location = new System.Drawing.Point(13, 96);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(1264, 463);
            this.browser.TabIndex = 47;
            // 
            // tbBigBet
            // 
            this.tbBigBet.Enabled = false;
            this.tbBigBet.Location = new System.Drawing.Point(346, 40);
            this.tbBigBet.Name = "tbBigBet";
            this.tbBigBet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbBigBet.Size = new System.Drawing.Size(73, 20);
            this.tbBigBet.TabIndex = 57;
            this.tbBigBet.Text = "1.5";
            // 
            // tbMediumBet
            // 
            this.tbMediumBet.Location = new System.Drawing.Point(239, 40);
            this.tbMediumBet.Name = "tbMediumBet";
            this.tbMediumBet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbMediumBet.Size = new System.Drawing.Size(101, 20);
            this.tbMediumBet.TabIndex = 56;
            this.tbMediumBet.Text = "1";
            // 
            // tbSmallBet
            // 
            this.tbSmallBet.Location = new System.Drawing.Point(146, 40);
            this.tbSmallBet.Name = "tbSmallBet";
            this.tbSmallBet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbSmallBet.Size = new System.Drawing.Size(87, 20);
            this.tbSmallBet.TabIndex = 55;
            this.tbSmallBet.Text = "0.5";
            // 
            // cbBigBet
            // 
            this.cbBigBet.AccessibleName = "tbBigBet";
            this.cbBigBet.AutoSize = true;
            this.cbBigBet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBigBet.Location = new System.Drawing.Point(346, 14);
            this.cbBigBet.Name = "cbBigBet";
            this.cbBigBet.Size = new System.Drawing.Size(73, 20);
            this.cbBigBet.TabIndex = 64;
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
            this.cbMediumBet.Location = new System.Drawing.Point(239, 14);
            this.cbMediumBet.Name = "cbMediumBet";
            this.cbMediumBet.Size = new System.Drawing.Size(101, 20);
            this.cbMediumBet.TabIndex = 63;
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
            this.cbSmallBet.Location = new System.Drawing.Point(146, 14);
            this.cbSmallBet.Name = "cbSmallBet";
            this.cbSmallBet.Size = new System.Drawing.Size(87, 20);
            this.cbSmallBet.TabIndex = 62;
            this.cbSmallBet.Tag = "";
            this.cbSmallBet.Text = "Small Bet:";
            this.cbSmallBet.UseVisualStyleBackColor = true;
            // 
            // NewMatchesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1285, 571);
            this.Controls.Add(this.cbBigBet);
            this.Controls.Add(this.cbMediumBet);
            this.Controls.Add(this.cbSmallBet);
            this.Controls.Add(this.tbBigBet);
            this.Controls.Add(this.tbMediumBet);
            this.Controls.Add(this.tbSmallBet);
            this.Controls.Add(this.browser);
            this.Controls.Add(this.btnNewMatches);
            this.Name = "NewMatchesForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewMatchesForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewMatches;
        public System.Windows.Forms.WebBrowser browser;
        public System.Windows.Forms.TextBox tbBigBet;
        public System.Windows.Forms.TextBox tbMediumBet;
        public System.Windows.Forms.TextBox tbSmallBet;
        public System.Windows.Forms.CheckBox cbBigBet;
        public System.Windows.Forms.CheckBox cbMediumBet;
        public System.Windows.Forms.CheckBox cbSmallBet;
    }
}