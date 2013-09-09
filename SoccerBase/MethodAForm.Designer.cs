namespace SoccerBase
{
    partial class MethodAForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.btnMethodA = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.lbTrace = new System.Windows.Forms.ListBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this.lbLeagues = new System.Windows.Forms.ListBox();
            this.cmsLBLeagues = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeselectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInvertSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFilter = new System.Windows.Forms.Label();
            this.cbOrderBy = new System.Windows.Forms.ComboBox();
            this.cbOrderDir = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabHTML = new System.Windows.Forms.TabPage();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.tabGraph = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbPanel = new System.Windows.Forms.Panel();
            this.cb2 = new System.Windows.Forms.CheckBox();
            this.cbX = new System.Windows.Forms.CheckBox();
            this.cb1 = new System.Windows.Forms.CheckBox();
            this.cmsLBLeagues.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabHTML.SuspendLayout();
            this.tabGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.cbPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMethodA
            // 
            this.btnMethodA.Location = new System.Drawing.Point(12, 12);
            this.btnMethodA.Name = "btnMethodA";
            this.btnMethodA.Size = new System.Drawing.Size(92, 23);
            this.btnMethodA.TabIndex = 0;
            this.btnMethodA.Text = "Method A";
            this.btnMethodA.UseVisualStyleBackColor = true;
            this.btnMethodA.Click += new System.EventHandler(this.btnMethodA_Click);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(1172, 12);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 20;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lbTrace
            // 
            this.lbTrace.BackColor = System.Drawing.SystemColors.Control;
            this.lbTrace.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbTrace.FormattingEnabled = true;
            this.lbTrace.Location = new System.Drawing.Point(160, 41);
            this.lbTrace.Name = "lbTrace";
            this.lbTrace.Size = new System.Drawing.Size(330, 121);
            this.lbTrace.TabIndex = 21;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this._backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this._backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this._backgroundWorker_RunWorkerCompleted);
            // 
            // _progressBar
            // 
            this._progressBar.BackColor = System.Drawing.SystemColors.Control;
            this._progressBar.Location = new System.Drawing.Point(160, 12);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(330, 23);
            this._progressBar.TabIndex = 22;
            // 
            // lbLeagues
            // 
            this.lbLeagues.ContextMenuStrip = this.cmsLBLeagues;
            this.lbLeagues.FormattingEnabled = true;
            this.lbLeagues.Location = new System.Drawing.Point(496, 41);
            this.lbLeagues.Name = "lbLeagues";
            this.lbLeagues.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbLeagues.Size = new System.Drawing.Size(270, 121);
            this.lbLeagues.TabIndex = 64;
            // 
            // cmsLBLeagues
            // 
            this.cmsLBLeagues.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelectAll,
            this.tsmiDeselectAll,
            this.tsmiInvertSel,
            this.tsmiSaveSelection});
            this.cmsLBLeagues.Name = "cmsLBLeagues";
            this.cmsLBLeagues.Size = new System.Drawing.Size(156, 114);
            // 
            // tsmiSelectAll
            // 
            this.tsmiSelectAll.Name = "tsmiSelectAll";
            this.tsmiSelectAll.Size = new System.Drawing.Size(155, 22);
            this.tsmiSelectAll.Text = "Select All";
            this.tsmiSelectAll.Click += new System.EventHandler(this.tsmiSelectAll_Click_1);
            // 
            // tsmiDeselectAll
            // 
            this.tsmiDeselectAll.Name = "tsmiDeselectAll";
            this.tsmiDeselectAll.Size = new System.Drawing.Size(155, 22);
            this.tsmiDeselectAll.Text = "Deselect All";
            this.tsmiDeselectAll.Click += new System.EventHandler(this.tsmiDeselectAll_Click);
            // 
            // tsmiInvertSel
            // 
            this.tsmiInvertSel.Name = "tsmiInvertSel";
            this.tsmiInvertSel.Size = new System.Drawing.Size(155, 22);
            this.tsmiInvertSel.Text = "Invert Selection";
            this.tsmiInvertSel.Click += new System.EventHandler(this.tsmiInvertSel_Click);
            // 
            // tsmiSaveSelection
            // 
            this.tsmiSaveSelection.Name = "tsmiSaveSelection";
            this.tsmiSaveSelection.Size = new System.Drawing.Size(155, 22);
            this.tsmiSaveSelection.Text = "Save Selection";
            this.tsmiSaveSelection.Click += new System.EventHandler(this.tsmiSaveSelection_Click);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(496, 17);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(43, 13);
            this.lblFilter.TabIndex = 65;
            this.lblFilter.Text = "Sort by:";
            // 
            // cbOrderBy
            // 
            this.cbOrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderBy.FormattingEnabled = true;
            this.cbOrderBy.Location = new System.Drawing.Point(578, 14);
            this.cbOrderBy.Name = "cbOrderBy";
            this.cbOrderBy.Size = new System.Drawing.Size(121, 21);
            this.cbOrderBy.TabIndex = 66;
            this.cbOrderBy.SelectedIndexChanged += new System.EventHandler(this.cbOrder_SelectedIndexChanged);
            // 
            // cbOrderDir
            // 
            this.cbOrderDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderDir.FormattingEnabled = true;
            this.cbOrderDir.Items.AddRange(new object[] {
            "DESC",
            "ASC"});
            this.cbOrderDir.Location = new System.Drawing.Point(705, 14);
            this.cbOrderDir.Name = "cbOrderDir";
            this.cbOrderDir.Size = new System.Drawing.Size(61, 21);
            this.cbOrderDir.TabIndex = 67;
            this.cbOrderDir.SelectedIndexChanged += new System.EventHandler(this.cbOrder_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabHTML);
            this.tabControl1.Controls.Add(this.tabGraph);
            this.tabControl1.Location = new System.Drawing.Point(12, 168);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1235, 415);
            this.tabControl1.TabIndex = 68;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabHTML
            // 
            this.tabHTML.BackColor = System.Drawing.Color.PeachPuff;
            this.tabHTML.Controls.Add(this.browser);
            this.tabHTML.Location = new System.Drawing.Point(4, 22);
            this.tabHTML.Name = "tabHTML";
            this.tabHTML.Padding = new System.Windows.Forms.Padding(3);
            this.tabHTML.Size = new System.Drawing.Size(1227, 389);
            this.tabHTML.TabIndex = 0;
            this.tabHTML.Text = "HTML";
            // 
            // browser
            // 
            this.browser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browser.Location = new System.Drawing.Point(-4, 0);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(1231, 393);
            this.browser.TabIndex = 20;
            // 
            // tabGraph
            // 
            this.tabGraph.AutoScroll = true;
            this.tabGraph.Controls.Add(this.chart1);
            this.tabGraph.Location = new System.Drawing.Point(4, 22);
            this.tabGraph.Name = "tabGraph";
            this.tabGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tabGraph.Size = new System.Drawing.Size(1227, 389);
            this.tabGraph.TabIndex = 1;
            this.tabGraph.Text = "Graph";
            this.tabGraph.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisY.Interval = 5D;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.Maximum = 100D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.DockedToChartArea = "ChartArea1";
            legend1.IsDockedInsideChartArea = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 3);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(1227, 386);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseClick);
            // 
            // cbPanel
            // 
            this.cbPanel.Controls.Add(this.cb2);
            this.cbPanel.Controls.Add(this.cbX);
            this.cbPanel.Controls.Add(this.cb1);
            this.cbPanel.Location = new System.Drawing.Point(12, 41);
            this.cbPanel.Name = "cbPanel";
            this.cbPanel.Size = new System.Drawing.Size(92, 68);
            this.cbPanel.TabIndex = 69;
            // 
            // cb2
            // 
            this.cb2.AutoSize = true;
            this.cb2.Location = new System.Drawing.Point(4, 49);
            this.cb2.Name = "cb2";
            this.cb2.Size = new System.Drawing.Size(32, 17);
            this.cb2.TabIndex = 2;
            this.cb2.Text = "2";
            this.cb2.UseVisualStyleBackColor = true;
            this.cb2.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cbX
            // 
            this.cbX.AutoSize = true;
            this.cbX.Checked = true;
            this.cbX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbX.Location = new System.Drawing.Point(4, 26);
            this.cbX.Name = "cbX";
            this.cbX.Size = new System.Drawing.Size(33, 17);
            this.cbX.TabIndex = 1;
            this.cbX.Text = "X";
            this.cbX.UseVisualStyleBackColor = true;
            this.cbX.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cb1
            // 
            this.cb1.AutoSize = true;
            this.cb1.Location = new System.Drawing.Point(4, 3);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(32, 17);
            this.cb1.TabIndex = 0;
            this.cb1.Text = "1";
            this.cb1.UseVisualStyleBackColor = true;
            this.cb1.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // MethodAForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 595);
            this.Controls.Add(this.cbPanel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cbOrderDir);
            this.Controls.Add(this.cbOrderBy);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lbLeagues);
            this.Controls.Add(this._progressBar);
            this.Controls.Add(this.lbTrace);
            this.Controls.Add(this.btnMethodA);
            this.Controls.Add(this.btnTest);
            this.Name = "MethodAForm";
            this.Text = "MethodAForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MethodAForm_FormClosing);
            this.cmsLBLeagues.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabHTML.ResumeLayout(false);
            this.tabGraph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.cbPanel.ResumeLayout(false);
            this.cbPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMethodA;
        private System.Windows.Forms.Button btnTest;
        public System.Windows.Forms.ListBox lbTrace;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ProgressBar _progressBar;
        private System.Windows.Forms.ListBox lbLeagues;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ComboBox cbOrderBy;
        private System.Windows.Forms.ComboBox cbOrderDir;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabHTML;
        public System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.TabPage tabGraph;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ContextMenuStrip cmsLBLeagues;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeselectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveSelection;
        private System.Windows.Forms.ToolStripMenuItem tsmiInvertSel;
        private System.Windows.Forms.Panel cbPanel;
        private System.Windows.Forms.CheckBox cb2;
        private System.Windows.Forms.CheckBox cbX;
        private System.Windows.Forms.CheckBox cb1;
    }
}