using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Win32;
using SoccerBase.BackgroundWorks;

namespace SoccerBase
{

    struct Betting
    {
        public const double MATCHPOT = 1;
        public double pot, won;
        public double profit { get { return won - pot; } }
        public double successPercent { get  { return (double)tipsWon / tipsPlayed * 100; } }
        public int tipsPlayed;
        public int tipsWon;
        public StringBuilder rowsHtml;

        public Betting(bool dummy)
        {
            won = pot = 0.0;
            tipsPlayed = tipsWon = 0;
            rowsHtml = new StringBuilder();
        }

        public override string ToString()
        {
            return String.Format("Played: {0}, Won: {1} ({2:0.00}%), Profit: {3:0.00} ", tipsPlayed, tipsWon, successPercent, profit);
        }
    }

    public struct LeaguesKey
    {
        public object ExtraCol;
        public string Country, League, Season;

        public override string ToString()
        {
            string ret = String.Format("{0}, {1}, {2}", this.Country, this.League, this.Season);
            if (this.ExtraCol != null && this.ExtraCol.ToString() != "") ret += " (" + this.ExtraCol.ToString() + ")";
            return ret;
        }
    }

    struct OrderBy
    {
        private string _output;
        public string OrderCol, SelectCol;

        public OrderBy(string selectCol, string orderCol = "", string output = "")
        {
            SelectCol = selectCol;
            OrderCol = orderCol == "" ? SelectCol : orderCol;
            _output = output == "" ? OrderCol : output;
        }

        public override string ToString()
        {
            return _output;
        }
    }

    public partial class MethodAForm : Form
    {
        public MainForm mainForm;
        public static bool debug;

        private const int FORMGAP = 30;

        public MethodAForm(MainForm main = null)
        {
            InitializeComponent();
            mainForm = main;
            lbTrace.DrawItem += new DrawItemEventHandler(mainForm.lbTrace_DrawItem);

            cbOrderBy.Items.AddRange(new object[]{
                new OrderBy("","Country,League,Season","Default"),
                new OrderBy("","Season"),
                new OrderBy("Teams"),
                new OrderBy("Matches"),
                new OrderBy("HomeWinsPercent"),
                new OrderBy("DrawsPercent"),
                new OrderBy("AwayWinsPercent"),
            });
            cbOrderBy.SelectedIndex = 0;

            cbOrderDir.SelectedIndex = 0;

            if (lbLeagues.Items.Count == 0) return;
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SoccerBase", true))
            {
                if (key == null) return;
                foreach (byte i in (byte[])key.GetValue("lbLeaguesSelectedIndices", null))
                {
                    if (i >= lbLeagues.Items.Count) break;
                    lbLeagues.SetSelected(i, true);
                }
            }
        }

        private void MethodAForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker.ReportProgress(0, new BGReport("Connecting to SoccerBase ..."));
            mainForm.openExcel(backgroundWorker);
            using (SqlConnection connection = new SqlConnection(MainForm.STRCONN))
            {
                bool b1, b2, bX;
                byte aLevel, aScore, hLevel, hScore, round;
                double aOdds, avgPercent = 0, dOdds, hOdds, sumProfit = 0;
                int aStrength, diffLevel, i = 0, hStrength, scoreResult, strengthDiff, strengthResult;
                string bgcolor, aTeam, hTeam, q, qWhere;
                string rowFormat = @"<tr style='background-color:{0};color:{1};'>
                    <td class='League'>{2}</td>
                    <td class='Season'>{3}</td>
                    <td class='Round'>{4}</td>
                    <td class='HomeTeam'>{5}</td>
                    <td class='AwayTeam'>{6}</td>
                    <td class='HomeOdds'>{7}</td>
                    <td class='DrawOdds'>{8}</td>
                    <td class='AwayOdds'>{9}</td>
                    <td class='HomeStrength'>{10}</td>
                    <td class='AwayStrength'>{11}</td>
                    <td class='HomeLevel'>{12}</td>
                    <td class='AwayLevel'>{13}</td>
                    <td class='Result'>{14}</td>
                </tr>";
                string tableFormat = @"<p>{0}</p><p1>{1}</p1>
                    <table border='1' style='font-family:Verdana;font-size:12px;'>
                        <thead>
                            <tr>
                                <th>League</th> <th>Season</th> <th>Round</th><th>HomeTeam</th> <th>AwayTeam</th><th>HomeOdds</th> <th>DrawOdds</th> <th>AwayOdds</th>
                                <th>HomeStrength</th> <th>AwayStrength</th><th>HomeLevel</th> <th>AwayLevel</th> <th>Result</th>
                            </tr>
                        </thead>
                        <tbody>{2}</tbody>
                    </table><br/>";
                Betting bet = new Betting(true);
                DataPoint dataPoint;
                Dictionary<LeaguesKey, Betting[]> htmlDict = new Dictionary<LeaguesKey, Betting[]>();
                LeaguesKey lKey = new LeaguesKey();
                List<string> alLeagues = new List<string>();
                Dictionary<LRTKey, Power> dict;
                Series series;
                StringBuilder html = new StringBuilder();

                b1 = bX = b2 = false;
                Invoke((MethodInvoker)(() =>
                {
                    foreach (LeaguesKey item in lbLeagues.SelectedItems)
                    {
                        alLeagues.Add("Country='" + item.Country + "' AND League='" + item.League + "' AND Season='" + item.Season + "'");
                    }
                    b1 = cb1.Checked;
                    bX = cbX.Checked;
                    b2 = cb2.Checked;
                }));
                qWhere = alLeagues.Count > 0 ? "(" + String.Join(" OR ", alLeagues) + ") AND " : "";

                backgroundWorker.ReportProgress(30, new BGReport("Processing dbo.history ..."));
                connection.Open();

                dict = MethodAForm.getHistoryDict(connection);

                backgroundWorker.ReportProgress(60, new BGReport("Processing dbo.archive ..."));
                q = @"SELECT * FROM dbo.archive WHERE " + qWhere + "Round>2 AND ScoreH IS NOT NULL ORDER BY Country,League,Season,Round";
                using (SqlDataReader reader = new SqlCommand(q, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        round = (byte)reader["Round"];
                        hTeam = reader["HomeTeam"].ToString();
                        aTeam = reader["AwayTeam"].ToString();
                        hScore = (byte)reader["ScoreH"];
                        aScore = (byte)reader["ScoreA"];
                        lKey.Country = reader["Country"].ToString();
                        lKey.League = reader["League"].ToString();
                        lKey.Season = reader["Season"].ToString();
                        hLevel = dict[new LRTKey(reader, hTeam, aTeam, (byte)(round - 1))].Level;
                        aLevel = dict[new LRTKey(reader, aTeam, hTeam, (byte)(round - 1))].Level;
                        hStrength = dict[new LRTKey(reader, hTeam, aTeam, round)].PrevAvgStrength;
                        aStrength = dict[new LRTKey(reader, aTeam, hTeam, round)].PrevAvgStrength;
                        if (MethodAForm.debug) Console.WriteLine("[{0}, {1}] {2} - {3} L({4}:{5}) R({6}:{7}) S({8}:{9})\n",
                            lKey, round, hTeam, aTeam, hLevel, aLevel, hScore, aScore, hStrength, aStrength
                        );
                        hOdds = (double)reader["HomeOdds"];
                        dOdds = (double)reader["DrawOdds"];
                        aOdds = (double)reader["AwayOdds"];
                        scoreResult = hScore - aScore > 0 ? 1 : hScore - aScore < 0 ? -1 : 0;
                        strengthDiff = hStrength - aStrength;
                        strengthResult = hStrength - aStrength > 0 ? 1 : hStrength - aStrength < 0 ? -1 : 0;

                        if (!htmlDict.ContainsKey(lKey))
                        {
                            htmlDict[lKey] = new Betting[FORMGAP];
                            for (i = 0; i < FORMGAP; i++) htmlDict[lKey][i] = new Betting(true);
                        }

                        for (i = 0; i < FORMGAP; i++)
                        {
                            bet = htmlDict[lKey][i];
                            bgcolor = "white";
                            diffLevel = hLevel - aLevel;
                            if (
                              bX && Math.Abs(strengthDiff) <= i
                                //&& Math.Abs(diffLevel) <= 1
                                //&& diffLevel == 0
                                //&& hOdds > 1.9 && aOdds > 1.9
                                //aStrength - hStrength >=0 &&
                                //aStrength - hStrength <=i
                            )
                            {
                                bgcolor = "silver";
                                bet.tipsPlayed++;
                                bet.pot += Betting.MATCHPOT;
                                if (scoreResult == 0)
                                {
                                    bet.tipsWon++;
                                    bet.won += (dOdds - 0) * Betting.MATCHPOT;
                                    bgcolor = "chartreuse";
                                }
                            }
                            else if (b1 && strengthDiff > i)
                            {
                                bgcolor = "silver";
                                bet.tipsPlayed++;
                                bet.pot += Betting.MATCHPOT;
                                if (scoreResult == 1)
                                {
                                    bet.tipsWon++;
                                    bet.won += (hOdds - 0) * Betting.MATCHPOT;
                                    bgcolor = "chartreuse";
                                }
                            }
                            else if (b2 && strengthDiff < i)
                            {
                                bgcolor = "silver";
                                bet.tipsPlayed++;
                                bet.pot += Betting.MATCHPOT;
                                if (scoreResult == -1)
                                {
                                    bet.tipsWon++;
                                    bet.won += (aOdds - 0) * Betting.MATCHPOT;
                                    bgcolor = "chartreuse";
                                }
                            }
                            
                            bet.rowsHtml.Append(String.Format(rowFormat, bgcolor, "black",
                                 reader["League"], reader["Season"], reader["Round"], hTeam, aTeam,
                                 hOdds, dOdds, aOdds, hStrength, aStrength, hLevel, aLevel,
                                 hScore + ":" + aScore
                            ));
                            htmlDict[lKey][i] = bet;
                        }
                    }
                    //Console.WriteLine("{0:0.00}% {1}", bet.winningPot / bet.pot * 100, lKey.ToString());
                    //Console.WriteLine("Invested:{0}, Won:{1}, Profit:{2}", bet.pot, bet.won, bet.won - bet.pot);
                }

                backgroundWorker.ReportProgress(90, new BGReport("Displaying html ..."));
                backgroundWorker.ReportProgress(90, new BGReport(""));
                Invoke((MethodInvoker)(() => { chart1.Series.Clear(); }));
                foreach (var kvp in htmlDict)
                {
                    bet = kvp.Value[3];
                    avgPercent += bet.successPercent;
                    sumProfit += bet.profit;
                    html.Append(String.Format(tableFormat, kvp.Key, bet, bet.rowsHtml));
                    backgroundWorker.ReportProgress(90, new BGReport(kvp.Key.ToString()));
                    backgroundWorker.ReportProgress(90, new BGReport("  " + bet));
                    series = new Series();
                    series.BorderWidth = 2;
                    series.ChartType = SeriesChartType.Line;
                    series.Name = kvp.Key.ToString();
                    series.ToolTip = "X:#VALX, Y:#VAL\n#SERIESNAME";
                    for (i = 0; i < FORMGAP; i++)
                    {
                        dataPoint = new DataPoint();
                        dataPoint.XValue = i;
                        dataPoint.YValues[0] = kvp.Value[i].successPercent;
                        series.Points.Add(dataPoint);
                    }
                    Invoke((MethodInvoker)(() => { chart1.Series.Add(series); }));
                }
                avgPercent /= htmlDict.Count;
                backgroundWorker.ReportProgress(90, new BGReport(""));
                backgroundWorker.ReportProgress(90, new BGReport(new String('-', 84)));
                backgroundWorker.ReportProgress(90, new BGReport(String.Format("Average percent: {0:0.00}%, Sum: {1:0.00}", avgPercent, sumProfit)));
                browser.DocumentText = "<html><head></head><body style='font-family:Verdana;font-size:12px;'>" + html.ToString() + "</body></html>";
            }
            mainForm.closeExcel(backgroundWorker);
        }

        private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _progressBar.Value = e.ProgressPercentage;
            if (e.UserState != null)
            {
                lbTrace.Items.Add((BGReport)e.UserState);
                lbTrace.SelectedIndex = lbTrace.Items.Count - 1;
                lbTrace.SelectedIndex = -1;
            }
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _progressBar.Value = 100;
            lbTrace.Items.Add(new BGReport("Processing finished successfuly."));
            lbTrace.SelectedIndex = lbTrace.Items.Count - 1;
            lbTrace.SelectedIndex = -1;
        }

        private void selectLeagues(bool selected)
        {
            for (int i = 0; i < lbLeagues.Items.Count; i++)
                lbLeagues.SetSelected(i, selected);
            lbLeagues.TopIndex = 0;
        }

        private void cbOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(MainForm.STRCONN))
            {
                connection.Open();
                OrderBy orderBy = (OrderBy)cbOrderBy.SelectedItem;
                string q = String.Format("SELECT Country,League,Season{0} FROM dbo.Leagues ORDER BY {1} {2}",
                    orderBy.SelectCol == "" ? "" : "," + orderBy.SelectCol, orderBy.OrderCol, cbOrderDir.SelectedItem);
                LeaguesKey lKey;
                lbLeagues.Items.Clear();
                using (SqlDataReader reader = new SqlCommand(q, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lKey = new LeaguesKey();
                        lKey.Country = reader.GetString(0);
                        lKey.League = reader.GetString(1);
                        lKey.Season = reader.GetString(2);
                        if (reader.FieldCount >= 4) lKey.ExtraCol = reader.GetValue(3);
                        lbLeagues.Items.Add(lKey);
                    }
                }
            }
        }

        private void btnMethodA_Click(object sender, EventArgs e)
        {
            lbTrace.Items.Clear();
            backgroundWorker.RunWorkerAsync();
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            tabGraph.Focus();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 1) tabGraph.Select();
        }

        private void tsmiSelectAll_Click_1(object sender, EventArgs e)
        {
            selectLeagues(true);
        }

        private void tsmiDeselectAll_Click(object sender, EventArgs e)
        {
            selectLeagues(false);
        }

        private void tsmiInvertSel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbLeagues.Items.Count; i++) lbLeagues.SetSelected(i,!lbLeagues.GetSelected(i));
        }

        private void tsmiSaveSelection_Click(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("SoccerBase"))
            {
                byte[] selectedIndices = new byte[lbLeagues.SelectedIndices.Count];
                for (int i = 0; i < lbLeagues.SelectedIndices.Count; i++) selectedIndices[i] = (byte)lbLeagues.SelectedIndices[i];
                key.SetValue("lbLeaguesSelectedIndices", selectedIndices);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            mainForm.openExcel(backgroundWorker);
            byte i = (byte)(mainForm.excel.getByte("C" + (48 + 2)) * 100);
            Console.WriteLine(i);
            mainForm.closeExcel(backgroundWorker);
        }

        public static Dictionary<LRTKey, Power> getHistoryDict(SqlConnection connection)
        {
            string q = "SELECT * FROM dbo.history WHERE grStrength IS NOT NULL ORDER BY Country,League,Season,Team,Round";
            Dictionary<LRTKey, Power> dict = new Dictionary<LRTKey, Power>();
            LRTKey lrtKey = new LRTKey();
            
            using (SqlDataReader reader = new SqlCommand(q, connection).ExecuteReader())
            {
                while (reader.Read())
                {
                    lrtKey.Country = reader["Country"].ToString();
                    lrtKey.League = reader["League"].ToString();
                    lrtKey.OtherTeam = reader["OtherTeam"].ToString();
                    lrtKey.Round = (byte)reader["Round"];
                    lrtKey.Season = reader["Season"].ToString();
                    lrtKey.Team = reader["Team"].ToString();
                    dict[lrtKey] = new Power(reader);
                }
            }
            return dict;
        }

        private void cb_CheckedChanged(object sender, EventArgs e)
        {
            bool hasCheckedCb = false;
            CheckBox thisCb = sender as CheckBox;

            foreach (CheckBox cb in cbPanel.Controls)
            {
                if (cb.Checked && cb != thisCb)
                {
                    hasCheckedCb = true;
                    break;
                }
            }
            if (!hasCheckedCb) thisCb.Checked = true;
        }

    }
}
