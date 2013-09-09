using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SoccerBase.BackgroundWorks;
using System.Data.SqlClient;

namespace SoccerBase
{
    struct ArchiveVal
    {
        private byte? Score1, Score2;
        public double DrawOdds;
        public int? result;

        public ArchiveVal(byte? score1, byte? score2, object drawOdds)
        {
            this.DrawOdds = (double)drawOdds;
            this.Score1 = score1;
            this.Score2 = score2;
            this.result = score1 - score2;
        }

        public override string ToString()
        {
            return String.Format("[{0}:{1}]", this.Score1, this.Score2);
        }
    }

    public struct DoublingKey
    {
        public string Country, League, Season, Team;

        public override string ToString()
        {
            return String.Format("[{0},{1},{2},{3}]", this.Country, this.League, this.Season, this.Team);
        }
    }

    public struct DoublingVal
    {
        public bool endRoundEnd;
        public byte endRound, rounds, startRound;
        public ulong spent
        {
            get
            {
                ulong dbl = 200, money = 0;
                for (int i = 0; i < this.rounds; i++)
                {
                    money += dbl;
                    dbl *= 2;
                }
                return money;
            }
        }
        public DateTime endDate, startDate;

        public void finish(bool isDblKeyEnd)
        {
            if (this.rounds < 1) return;
            this.endRoundEnd = isDblKeyEnd;
            if (isDblKeyEnd)
            {
                this.endRound = DoublingXForm.prevRound;
                this.endDate = DoublingXForm.prevDate;
            }
            else
            {
                this.endRound = DoublingXForm.round;
                this.endDate = DoublingXForm.date;
            }
            DoublingXForm.lstDblVal.Add(this);
            DoublingXForm.dblDict[isDblKeyEnd ? DoublingXForm.prevDblKey : DoublingXForm.dblKey] = DoublingXForm.lstDblVal;
        }

        public override string ToString()
        {
            return String.Format("  {0} rounds. From:{1:d}(Round:{2}) To:{3:d}(Round:{4}), Dn: {5:n}",
                this.rounds, this.startDate, this.startRound, this.endDate, this.endRoundEnd ? "END" : this.endRound.ToString(), this.spent
            );
        }
    }

    public partial class DoublingXForm : Form
    {
        private const int BANKMONEY = 150000;
        private const int BETMONEYPERROUND = 200;
        private const int STARTROUND = 6;
        private SqlConnection connection;
        public MainForm mainForm;
        public static byte prevRound = 0, round;
        public static DateTime date, prevDate = DateTime.Now;
        public static Dictionary<DoublingKey, List<DoublingVal>> dblDict = new Dictionary<DoublingKey, List<DoublingVal>>();
        public static DoublingKey dblKey, prevDblKey;
        public static List<DoublingVal> lstDblVal = new List<DoublingVal>();

        public DoublingXForm(MainForm main = null)
        {
            InitializeComponent();
            mainForm = main;
            lbTrace.DrawItem += new DrawItemEventHandler(mainForm.lbTrace_DrawItem);
        }

        private void DoublingXForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string operation = "";

            if (e.Argument != null) operation = e.Argument.ToString();
            backgroundWorker.ReportProgress(0, new BGReport("Connecting to SoccerBase (Operation: " + operation + ") ..."));
            using (this.connection = new SqlConnection(MainForm.STRCONN))
            {
                this.connection.Open();
                switch (operation)
                {
                    case "WorstSituation":
                        doWorstSituation();
                        break;
                    case "Bet":
                        mainForm.openExcel(backgroundWorker);
                        doBetSomeMoney();
                        mainForm.closeExcel(backgroundWorker);
                        break;
                }
            }
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            lbTrace.Items.Clear();
            backgroundWorker.RunWorkerAsync((sender as Control).Tag);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            DoublingVal dblVal = new DoublingVal();
            dblVal.rounds = 9;
            Console.WriteLine("On {0} rounds you spent {1:n} denars, ", dblVal.rounds, dblVal.spent);
        }

        private void doBetSomeMoney()
        {
            byte i = DoublingXForm.STARTROUND, j;
            byte? scoreA, scoreH;
            double bank = DoublingXForm.BANKMONEY, bet = DoublingXForm.BETMONEYPERROUND;
            int r;
            string aTeam, hTeam, q = @"SELECT * FROM dbo.archive WHERE Round>=" + DoublingXForm.STARTROUND + " ORDER BY Country,League,Season,Round";
            ArchiveVal aVal;
            Brush color;
            Dictionary<LRTKey, ArchiveVal> aDict = new Dictionary<LRTKey,ArchiveVal>();
            Dictionary<LRTKey, Power> hDict;

            backgroundWorker.ReportProgress(25, new BGReport("Processing dbo.archive ..."));
            using (SqlDataReader reader = new SqlCommand(q, this.connection).ExecuteReader())
            {
                while (reader.Read())
                {
                    aTeam = reader["AwayTeam"].ToString();
                    hTeam = reader["HomeTeam"].ToString();
                    scoreA = reader["ScoreA"] is DBNull ? null : (byte?)reader["ScoreA"];
                    scoreH = reader["ScoreH"] is DBNull ? null : (byte?)reader["ScoreH"];
                    aDict[new LRTKey(reader, hTeam, aTeam)] = new ArchiveVal(scoreH, scoreA, reader["DrawOdds"]);
                    aDict[new LRTKey(reader, aTeam, hTeam)] = new ArchiveVal(scoreA, scoreH, reader["DrawOdds"]);
                }
            }

            backgroundWorker.ReportProgress(50, new BGReport("Processing dbo.history ..."));
            hDict = MethodAForm.getHistoryDict(this.connection);

            var dictSortedByRound = from item in hDict
                                    let otherKey = LRTKey.getOtherKey(item.Key)
                                    where item.Key.Season == "2011/12" && Math.Abs(item.Value.PrevAvgStrength - hDict[otherKey].PrevAvgStrength) <= 1
                                    orderby item.Key.Round select item;
            var dictRound = from item in dictSortedByRound where item.Key.Round == i select item;
            while (dictRound.Count() > 0)
            {
                using (var cursor = dictRound.GetEnumerator())
                {
                    r = (new Random()).Next(dictRound.Count()) + 1;
                    for (j = 0; j < r; j++)
                        cursor.MoveNext();
                    if (aDict[cursor.Current.Key].result == null) {
                        cursor.Reset();
                        while (cursor.MoveNext()) if (aDict[cursor.Current.Key].result != null)
                            break;
                    }
                    aVal = aDict[cursor.Current.Key];
                    if (aVal.result == 0)
                    {
                        bank += bet * (aVal.DrawOdds - 0.15);
                        bet = DoublingXForm.BETMONEYPERROUND;
                        color = Brushes.Blue;
                    }
                    else
                    {
                        bank -= bet;
                        bet *= 2;
                        color = Brushes.Black;
                    }
                    if (bank < 0) color = Brushes.Red;
                    backgroundWorker.ReportProgress(70, new BGReport(
                        String.Format("{0} {1} - Bet:{2} Bank:{3}", cursor.Current.Key, aVal, bet, bank), color
                    ));
                }
                if (bank < 0) break;
                i++;
                dictRound = from item in dictSortedByRound where item.Key.Round == i select item;
            }
        }

        private void doWorstSituation()
        {
            byte prevReceived, prevScored, received, scored;
            string q = @"SELECT * FROM dbo.history ORDER BY Country,League,Season,Team,Round";
            ulong totalGames = 0;
            DoublingVal dblVal = new DoublingVal();

            prevReceived = prevScored = 0;
            DoublingXForm.dblKey = DoublingXForm.prevDblKey = new DoublingKey();
            using (SqlDataReader reader = new SqlCommand(q, this.connection).ExecuteReader())
            {
                while (reader.Read())
                {
                    DoublingXForm.dblKey.Country = reader["Country"].ToString();
                    DoublingXForm.dblKey.League = reader["League"].ToString();
                    DoublingXForm.dblKey.Season = reader["Season"].ToString();
                    DoublingXForm.dblKey.Team = reader["Team"].ToString();
                    DoublingXForm.round = (byte)reader["Round"];
                    DoublingXForm.date = (DateTime)reader["Date"];
                    scored = (byte)reader["Scored"];
                    received = (byte)reader["Received"];
                    if (!DoublingXForm.dblDict.ContainsKey(DoublingXForm.dblKey))
                    {
                        dblVal.finish(true);
                        dblVal = new DoublingVal();
                        DoublingXForm.lstDblVal = new List<DoublingVal>();
                    }
                    if (received - prevReceived != scored - prevScored)
                    {
                        if (dblVal.rounds == 0)
                        {
                            dblVal.startRound = DoublingXForm.round;
                            dblVal.startDate = DoublingXForm.date;
                        }
                        dblVal.rounds++;
                    }
                    else
                    {
                        dblVal.finish(false);
                        dblVal = new DoublingVal();
                    }
                    DoublingXForm.prevDblKey = DoublingXForm.dblKey;
                    DoublingXForm.prevRound = DoublingXForm.round;
                    DoublingXForm.prevDate = DoublingXForm.date;
                    prevScored = scored;
                    prevReceived = received;
                    totalGames++;
                }
                dblVal.finish(true);
            }

            Dictionary<DoublingKey, List<DoublingVal>> newDblDict = new Dictionary<DoublingKey, List<DoublingVal>>();
            foreach (var kvp in DoublingXForm.dblDict)
                newDblDict[kvp.Key] = (from item in kvp.Value orderby item.rounds descending select item).ToList<DoublingVal>();
            var orderedDblDict = from item in newDblDict orderby item.Value[0].rounds descending select item;

            int badOnes = 0, count = DoublingXForm.dblDict.Count, i = 0, n1 = 0, n2 = 20, progress = 0, totalRuns = 0;
            foreach (var kvp in orderedDblDict)
            {
                if (++i >= n1 && i <= n2)
                {
                    progress = (int)((double)(i) / count * 100);
                    backgroundWorker.ReportProgress(progress, new BGReport(String.Format("{0}", kvp.Key)));
                }
                foreach (DoublingVal val in kvp.Value)
                {
                    if (i >= n1 && i <= n2) backgroundWorker.ReportProgress(progress, new BGReport(String.Format("{0}", val), val.rounds > 9 ? Brushes.Red : Brushes.Black));
                    if (val.rounds > 9) badOnes++;
                    totalRuns++;
                }
            }
            Console.WriteLine("Total games: {0}, Total runs: {1}, Total Teams: {2}, Bad Ones: {3}", totalGames, totalRuns, count, badOnes);
        }

    }
}
