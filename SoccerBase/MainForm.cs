using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

using Mannex.Net;
using Fizzler;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Threading;
using SoccerBase.BackgroundWorks;
using System.Globalization;
using System.Security.Permissions;
using System.Net.Mail;
using System.Net.Mime;

namespace SoccerBase
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class MainForm : Form
    {
        public const int XLSXAWAYWINDRAWY = 48;
        public const int XLSXHOMEWINDRAWY = 41;
        public const int XLSXLAST5MATCHESY = 55;
        public const string STRCONN = @"server=DENI\SQLEXPRESS;database=SoccerBase;Trusted_Connection=true;MultipleActiveResultSets=true";
        public Excel.App excel;
        public DoublingXForm frmDoublingX;
        public MethodAForm frmMethodA;
        public Method1Form frmMethod1;
        public NewMatchesForm frmNewMatches;
        public Dictionary<string, string[]> archiveLinks = new Dictionary<string, string[]>()
        {
            {"Argentina,Primera Nacianal B,2009/10", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_48,22_3,5_2322,9_fixtures,231_full,23_1","Primera B Nacional",""
            }},{"Argentina,Primera Nacional B,2010/11", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_48,22_3,5_2923,9_fixtures,231_full,23_1","Primera B Nacional",""
            }},{"Argentina,Primera Nacional B,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_48,22_3,5_3635,9_fixtures,231_full,23_1","Primera B Nacional",""
            }},{"Austria,Bundesliga,2009/10", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_17,22_1,5_2149,25_1,9_fixtures,231_full,23_1","",""
            }},{"Austria,Bundesliga,2010/11", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_17,22_1,5_2773,10_4273,26_5,9_fixtures,231_full,23_1","",""
            }},{"Austria,Bundesliga,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_17,22_1,5_3416,9_fixtures,231_full,23_1","",""
            }},{"Belgium,Pro League,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_33,22_1,5_3414,9_fixtures,231_full,23_1","Pro League",""
            }},{"Denmark,Superligaen,2010/11", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_8,22_1,5_2767,9_fixtures,231_full,23_1","",""
            }},{"Denmark,Superligaen,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_8,22_1,5_3376,9_fixtures,231_full,23_1","",""
            }},{"England,Championship,2008/09", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_1,22_1,5_1545,9_fixtures,231_full,23_1","Championship",""
            }},{"England,Championship,2009/10", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_1,22_1,5_2141,9_fixtures,231_full,23_1","Championship",""
            }},{"England,Championship,2010/11", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_1,22_1,5_2748,9_fixtures,231_full,23_1","Championship",""
            }},{"England,Premier League,2010/11", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_1,22_1,5_2746,9_fixtures,231_full,23_1","",""
            }},{"France,Ligue 1,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_7,22_1,5_3380,9_fixtures,231_full,23_1","",""
            }},{"Germany,Bundesliga,2010/11", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_30,22_1,5_2811,9_fixtures,231_full,23_1","Bundesliga",""
            }},{"Germany,2nd Bundesliga,2010/11", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_30,22_1,5_2812,9_fixtures,231_full,23_1",
                "2nd Bundesliga","2nd Bundesliga Relegation/Promotion"
            }},{"Greece,Super League,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_67,22_1,5_3735,9_fixtures,231_full,23_1","Super League",""
            }},{"Italy,Serie A,2010/11", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_31,22_1,5_2930,9_fixtures,231_full,23_1","Serie A",""
            }},{"Netherlands,Eredivisie,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_35,22_1,5_3432,9_fixtures,231_full,23_1","Eredivisie",""
            }},{"Portugal,Primeira Liga,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_44,22_1,5_3462,9_fixtures,231_full,23_1","",""
            }},{"Romania,First Division,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_77,22_1,5_3539,9_fixtures,231_full,23_1","",""
            }},{"Russia,Premier League,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_21,22_1,5_3288,9_fixtures,231_full,23_1","Premier League",""
            }},{"Scotland,Premier League,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1424&clientid=4&state=2_1,3_22,22_1,5_3392,9_fixtures,231_full,23_1","",""
            }},{"Spain,Primera Division,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_32,22_1,5_3502,9_fixtures,231_full,23_1","Primera Division",""
            }},{"Turkey,Super Lig,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_46,22_1,5_3831,9_fixtures,231_full,23_1","Super Lig",""
            }},{"Ukraine,Premier League,2011/12", new string[] {
                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_86,22_1,5_3390,9_fixtures,231_full,23_1","",""
            }}
        };

        public MainForm()
        {
            InitializeComponent();
            frmDoublingX = new DoublingXForm(this);
            frmMethodA = new MethodAForm(this);
            frmMethod1 = new Method1Form();
            frmMethod1.mainForm = this;
            frmNewMatches = new NewMatchesForm();
            frmNewMatches.mainForm = this;

            //tsmiMethodA_Click(null,null);
        }

#region public methods

        public void closeExcel(BackgroundWorker bgWorker = null)
        {
            if (bgWorker == null) bgWorker = backgroundWorker;
            bgWorker.ReportProgress(95, new BGReport("Closing MS Excel ..."));
            excel.Close();
        }

        public double[] getExcelRates(int range = 5)
        {
            double[] rates = new double[range];
            for (int i = 0; i < range; i++)
                rates[i] = excel.getDouble(((char)('A' + i)).ToString() + (MainForm.XLSXLAST5MATCHESY + 1 + range - 5));
            return rates;
        }

        public void lbTrace_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            e.DrawBackground();
            Brush myBrush = ((BGReport)lb.Items[e.Index]).Color ?? Brushes.Black;
            e.Graphics.DrawString(lb.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        public void openExcel(BackgroundWorker bgWorker = null)
        {
            if (bgWorker == null) bgWorker = backgroundWorker;
            bgWorker.ReportProgress(0, new BGReport("Initializing MS Excel ..."));
            excel = new Excel.App(Directory.GetCurrentDirectory() + @"\..\..\MethodA_Rules.xlsx");
        }

 #endregion

#region private methods

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _progressBar.Value = e.ProgressPercentage;
            if (e.UserState != null)
            {
                lbTrace.Items.Add((BGReport)e.UserState);
                lbTrace.SelectedIndex = lbTrace.Items.Count - 1;
                lbTrace.SelectedIndex = -1;
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int i;
            string operation = "";
            AbstractBase obj = null;

            if (e.Argument != null) operation = e.Argument.ToString();
            backgroundWorker.ReportProgress(0, new BGReport("Connecting to SoccerBase (Operation: " + operation + ") ..."));
            using (SqlConnection connection = new SqlConnection(MainForm.STRCONN))
            {
                connection.Open();
                switch (operation)
                {
                    case "Ivnet":
                        obj = new BackgroundWorks.IvnetNS.Ivnet();
                        obj.Links = new Dictionary<string, string[]>(){
                            {"Belgium,Pro League,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/belgium/belgium.htm",""
                            }},
                            {"Denmark,Superligaen,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/denmark/denmark.htm",""
                            }},
                            {"England,Premier League,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/england/england.htm",""
                            }},
                            {"France,Ligue 1,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/france/france.htm",""
                            }},
                            {"Germany,Bundesliga,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/germany/germany.htm",""
                            }},
                            {"Greece,Super League,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/greece/greece.htm",""
                            }},
                            {"Netherlands,Eredivisie,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/nether/nether.htm",""
                            }},
                            {"Italy,Serie A,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/italy/italy.htm",""
                            }},
                            {"Portugal,Liga Zon Sagres,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/portugal/portugal.htm",""
                            }},
                            {"Romania,First Division,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/romania/romania.htm",""
                            }},
                            {"Russia,Premier League,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/russia/russia.htm",""
                            }},
                            //{"Scotland,Premier League,2011/12", new string[] {"http://invarius.ivnet.ru/soccer/champ/scotland/scotland.htm",""}},
                            {"Spain,Primera Division,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/spain/spain.htm",""
                            }},
                            {"Turkey,Super Lig,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/turkey/turkey.htm",""
                            }},
                            {"Ukraine,Premier League,2011/12", new string[] {
                                "http://invarius.ivnet.ru/soccer/champ/ukraine/ukraine.htm",""
                            }}
                        };
                        obj.Connection = connection;
                        obj.Table = "Ivnet";
                        while (obj.enrLinks.MoveNext())
                        {
                            double percent = (double)(obj.Index + 1) / obj.Links.Count * 100;
                            backgroundWorker.ReportProgress((int)percent, new BGReport(obj.ProcessLink()));
                        }
                        break;
                    case "NewIvnet":
                        BackgroundWorks.IvnetNS.Ivnet newIvnet = new BackgroundWorks.IvnetNS.Ivnet();
                        newIvnet.Links = new Dictionary<string, string[]>()
                        {
                            {"New matches",new string[] {"http://invarius.ivnet.ru/soccer/champ/champ.htm",""}}
                        };
                        newIvnet.Connection = connection;
                        newIvnet.isNew = true;
                        if (newIvnet.enrLinks.MoveNext()) backgroundWorker.ReportProgress(100, new BGReport(newIvnet.ProcessLink()));
                        break;
                    case "SportRadar":
                        obj = new SportRadar();
                        obj.Links = new Dictionary<string,string[]>(){
                            {"Belgium,Pro League,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_33,22_1,5_3414,9_fixtures,231_full,23_1",""
                            }},
                            {"Denmark,Superligaen,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_8,22_1,5_3376,9_fixtures,231_full,23_1",""
                            }},
                            {"England,Premier League,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_1,22_1,5_3391,9_fixtures,231_full,23_1",""
                            }},
                            {"France,Ligue 1,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_7,22_1,5_3380,9_fixtures,231_full,23_1",""
                            }},
                            {"Germany,Bundesliga,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_30,22_1,5_3405,9_fixtures,231_full,23_1",""
                            }},
                            {"Greece,Super League,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_67,22_1,5_3735,9_fixtures,231_full,23_1",""
                            }},
                            {"Italy,Serie A,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_31,22_1,5_3639,9_fixtures,231_full,23_1",""
                            }},
                            {"Netherlands,Eredivisie,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_35,22_1,5_3432,9_fixtures,231_full,23_1",""
                            }},
                            {"Portugal,Liga Zon Sagres,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_44,22_1,5_3462,9_fixtures,231_full,23_1",""
                            }},
                            {"Romania,First Division,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_77,22_1,5_3539,9_fixtures,231_full,23_1",""
                            }},
                            {"Russia,Premier League,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_21,22_1,5_3288,9_fixtures,231_full,23_1",""
                            }},
                            //{"Scotland,Premier League,2011/12", new string[] {
                            //    "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_22,22_1,5_3392,9_fixtures,231_full,23_2",""
                            //}},
                            {"Spain,Primera Division,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_32,22_1,5_3502,9_fixtures,231_full,23_1",""
                            }},
                            {"Turkey,Super Lig,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_46,22_1,5_3831,9_fixtures,231_full,23_1",""
                            }},
                            {"Ukraine,Premier League,2011/12", new string[] {
                                "http://stats.betradar.com/s4/gismo.php?&html=1&id=1827&clientid=4&state=2_1,3_86,22_1,5_3390,9_fixtures,231_full,23_1",""
                            }}
                        };
                        obj.Connection = connection;
                        obj.Table = "SportRadar";
                        while (obj.enrLinks.MoveNext())
                        {
                            double percent = (double)(obj.Index + 1) / obj.Links.Count * 100;
                            backgroundWorker.ReportProgress((int)percent, new BGReport(obj.ProcessLink()));
                        }
                        break;
                    case "Link":
                        BackgroundWorks.IvnetNS.Link link = new BackgroundWorks.IvnetNS.Link();
                        link.Connection = connection;
                        link.process();
                        break;
                    /* ------------------------------------------------------------------------ */
                    case "Truncate":
                        SqlCommand sql = new SqlCommand("TRUNCATE TABLE dbo.archive", connection);
                        backgroundWorker.ReportProgress(10, new BGReport("Truncating archive ..."));
                        sql.ExecuteNonQuery();
                        sql = new SqlCommand("TRUNCATE TABLE dbo.Leagues", connection);
                        backgroundWorker.ReportProgress(40, new BGReport("Truncating Leagues ..."));
                        sql.ExecuteNonQuery();
                        sql = new SqlCommand("TRUNCATE TABLE dbo.history", connection);
                        backgroundWorker.ReportProgress(70, new BGReport("Truncating history ..."));
                        sql.ExecuteNonQuery();
                        break;
                    case "Archive":
                        obj = new SportRadar();
                        obj.Links = archiveLinks;
                        obj.Table = "archive";
                        obj.Connection = connection;
                        i = 1;
                        while (obj.enrLinks.MoveNext())
                        {
                            double percent = (double)(i++) / obj.Links.Count * 100;
                            backgroundWorker.ReportProgress((int)percent, new BGReport("  " + obj.ProcessLink()));
                        }
                        break;
                    case "Leagues":
                        Leagues leagues = new Leagues();
                        leagues.Connection = connection;
                        backgroundWorker.ReportProgress(10, new BGReport("Processing Leagues ..."));
                        leagues.process();
                        break;
                    case "History":
                        History history = new History();
                        History.mainForm = this;
                        history.Connection = connection;
                        openExcel();
                        history.process();
                        closeExcel();
                        break;
                    case "PrevAvgStrength":
                        PrevAvgStrength strength = new PrevAvgStrength();
                        PrevAvgStrength.mainForm = this;
                        strength.Connection = connection;
                        openExcel();
                        strength.process();
                        closeExcel();
                        break;
                }
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _progressBar.Value = 100;
            lbTrace.Items.Add(new BGReport("Processing finished successfuly."));
            lbTrace.SelectedIndex = lbTrace.Items.Count - 1;
            lbTrace.SelectedIndex = -1;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void tsmiMethod1_Click(object sender, EventArgs e)
        {
            frmMethod1.Show(this);
        }

        private void tsmiMethodA_Click(object sender, EventArgs e)
        {
            frmMethodA.Show(this);
        }

        private void tsmiNewIvnetMatches_Click(object sender, EventArgs e)
        {
            frmNewMatches.Show(this);
        }

        private void tsmiDoublingX_Click(object sender, EventArgs e)
        {
            frmDoublingX.Show(this);
        }

        private void allCheckBoxes_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (sender as CheckBox);
            Control[] cs = Controls.Find(cb.AccessibleName, true);
            if (cs.Length < 1) return;
            cs[0].Enabled = cb.Checked;
            btnStart_Click(sender, e);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            lbTrace.Items.Clear();
            backgroundWorker.RunWorkerAsync((sender as Control).Tag);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //Regex _rx = new Regex(@"^(?<Date>.{7})(?<HTeam>.{14,15})(?<ATeam>.{15})(?<FH>\d{2})  (?<FD>\d{2})  (?<FA>\d{2})  \((?<Tip>\d{1}-\d{1})\)(?( \d{1,2}-) (?<Score>\d{1,2}-\d{1,2}))",
            Regex _rx = new Regex(@"^(?<HScore>\d+):(?<AScore>\d+)",
                   RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
            //string str2 = "       Udinese	      Fiorentina     62  24  14  (2-0) 2-0  (+) 3";
            Match m = _rx.Match("Postponed");
            //Console.WriteLine("{0}-{1}-{2}-{3}", m.Groups["HTeam"].Value.Trim(), m.Groups["ATeam"].Value.Trim(), m.Groups["Tip"].Value.Trim(), m.Groups["Score"].Value);
            int round;
            int.TryParse("fff", out round);
            Console.WriteLine("{0}", round);
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(MainForm.STRCONN))
            {
                connection.Open();
                string q = @"SELECT ScoreH FROM dbo.archive WHERE ID=4407";
                SqlCommand selectCmd = new SqlCommand(q, connection);
                using (SqlDataReader reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //DateTime dt1 = new DateTime(2011,5,15), dt2 = reader.GetDateTime(0);
                        //Console.WriteLine("compare({0}): {1} > {2}", dt1 > dt2, dt1.ToString("yyyy-MM-dd"), dt2.ToString("yyyy-MM-dd"));
                        byte? x = reader["ScoreH"] is DBNull ? null : (byte?)reader["ScoreH"];
                        byte? y = (byte?)reader["ScoreH"] ?? null;
                        Console.WriteLine("{0}", x == null);
                    }
                }
            }
        }

#endregion

    }
}
