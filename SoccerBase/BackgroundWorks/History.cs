using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SoccerBase.BackgroundWorks
{
    struct ArchiveKey
    {
        public string Country, League, Season;

        public ArchiveKey(SqlDataReader reader)
        {
            this.Country = reader["Country"].ToString();
            this.League = reader["League"].ToString();
            this.Season = reader["Season"].ToString();
        }

        public override string ToString()
        {
            return String.Format("[{0},{1},{2}]", Country, League, Season);
        }
    }

    struct RoundValue
    {
        public byte minPoints, maxPoints;
        public Dictionary<string, HistoryValue> tDict;

        public RoundValue(RoundValue rVal)
        {
            this.maxPoints = rVal.maxPoints;
            this.minPoints = rVal.minPoints;
            this.tDict = new Dictionary<string, HistoryValue>();
        }

        public RoundValue(SqlDataReader reader, Dictionary<int, RoundValue> rDict)
        {
            this.tDict = new Dictionary<string, HistoryValue>();
            this.minPoints = this.maxPoints = 0;
            byte ahValPoints = awayHistoryVal(reader, rDict), hhValPoints = homeHistoryVal(reader, rDict);
            this.minPoints = Math.Min(hhValPoints, ahValPoints);
            this.maxPoints = Math.Max(hhValPoints, ahValPoints);
        }

        public void calc(SqlDataReader reader, Dictionary<int, RoundValue> rDict)
        {
            byte ahValPoints = awayHistoryVal(reader, rDict), hhValPoints = homeHistoryVal(reader, rDict);
            byte max = Math.Max(hhValPoints, ahValPoints);
            byte min = Math.Min(hhValPoints, ahValPoints);
            if (min < this.minPoints) this.minPoints = min;
            if (max > this.maxPoints) this.maxPoints = max;
        }

        private byte awayHistoryVal(SqlDataReader reader, Dictionary<int, RoundValue> rDict)
        {
            string aTeam = reader["AwayTeam"].ToString();
            HistoryValue hVal = getHistoryVal(reader, rDict, "AWAY");
            if (hVal.ScoreA != null) hVal.Points += (byte)(hVal.ScoreH == hVal.ScoreA ? 1 : hVal.ScoreH < hVal.ScoreA ? 3 : 0);
            hVal.OtherTeam = reader["HomeTeam"].ToString();
            hVal.SumReceived += hVal.ScoreH ?? 0;
            hVal.SumReceivedA += hVal.ScoreH ?? 0;
            hVal.SumScored += hVal.ScoreA ?? 0;
            hVal.SumScoredA += hVal.ScoreA ?? 0;
            this.tDict[aTeam] = hVal;
            return hVal.Points;
        }

        private byte homeHistoryVal(SqlDataReader reader, Dictionary<int, RoundValue> rDict)
        {
            string hTeam = reader["HomeTeam"].ToString();
            HistoryValue hVal = getHistoryVal(reader, rDict, "HOME");
            if (hVal.ScoreA != null) hVal.Points += (byte)(hVal.ScoreH == hVal.ScoreA ? 1 : hVal.ScoreH > hVal.ScoreA ? 3 : 0);
            hVal.OtherTeam = reader["AwayTeam"].ToString();
            hVal.SumReceived += hVal.ScoreA ?? 0;
            hVal.SumReceivedH += hVal.ScoreA ?? 0;
            hVal.SumScored += hVal.ScoreH ?? 0;
            hVal.SumScoredH += hVal.ScoreH ?? 0;
            this.tDict[hTeam] = hVal;
            return hVal.Points;
        }

        public static HistoryValue getHistoryVal(SqlDataReader reader, Dictionary<int, RoundValue> rDict, string field)
        {
            byte round = (byte)reader["Round"];
            string team = field == "HOME" ? reader["HomeTeam"].ToString() : reader["AwayTeam"].ToString();
            HistoryValue hVal;

            if (round == 1) hVal = new HistoryValue();
            else if (rDict[round - 1].tDict.ContainsKey(team))
                hVal = rDict[round - 1].tDict[team];
            else
            {
                History.errDict[new ArchiveKey(reader)] = String.Format("  BAD Fixture!!! '{0}' has not played in round {1}.", team, round - 1);
                hVal = new HistoryValue();
            }
            hVal.ScoreA = reader["ScoreA"] is DBNull ? null : (byte?)reader["ScoreA"];
            hVal.ScoreH = reader["ScoreH"] is DBNull ? null : (byte?)reader["ScoreH"];
            hVal.Field = field;
            hVal.Date = (DateTime)reader["Date"];
            return hVal;
        }

        public override string ToString()
        {
            return String.Format("[min:{0},max:{1}]", this.minPoints, this.maxPoints);
        }
    }

    struct HistoryValue
    {
        public byte Points, Position, SumScoredH, SumReceivedH, SumScoredA, SumReceivedA, SumScored, SumReceived, grLevel;
        public byte? ScoreA, ScoreH, grStrength;
        public string Field, OtherTeam;
        public DateTime Date;

        public override string ToString()
        {
            return String.Format("[{0},{1}:{2},P:{3}]", ScoreH, ScoreA, Points);
        }
    }
    
    class History
    {
        public SqlConnection Connection;
        public static MainForm mainForm;
        public static Dictionary<ArchiveKey, string> errDict;
        
        public void process()
        {
            byte i, round;
            float grDiff;
            int aI = 1, progress;
            string progressTitle;
            string q = @"SELECT Country,League,Season,Round,Date,HomeTeam,AwayTeam,ScoreH,ScoreA FROM dbo.archive ORDER BY Country,League,Season,Round";
            ArchiveKey aKey = new ArchiveKey();
            Dictionary<ArchiveKey, Dictionary<int, RoundValue>> aDict = new Dictionary<ArchiveKey, Dictionary<int, RoundValue>>();
            Dictionary<ArchiveKey, Dictionary<int, RoundValue>> newADict = new Dictionary<ArchiveKey, Dictionary<int, RoundValue>>();
            HistoryValue hVal;
            RoundValue rVal;
            SqlCommand selectCmd;
            SqlDataReader reader;

            mainForm.backgroundWorker.ReportProgress(0, new BGReport("  Building aDict ..."));
            History.errDict = new Dictionary<ArchiveKey, string>();

            using (reader = new SqlCommand(q, this.Connection).ExecuteReader())
            {
                while (reader.Read())
                {
                    aKey.Country = reader["Country"].ToString();
                    aKey.League = reader["League"].ToString();
                    aKey.Season = reader["Season"].ToString();
                    round = (byte)reader["Round"];
                    if (!aDict.ContainsKey(aKey))
                        aDict[aKey] = new Dictionary<int, RoundValue>();
                    else if (aDict[aKey].ContainsKey(round))
                    {
                        rVal = aDict[aKey][round];
                        rVal.calc(reader, aDict[aKey]);
                        aDict[aKey][round] = rVal;
                        continue;
                    }
                    aDict[aKey][round] = new RoundValue(reader, aDict[aKey]);
                }
            }

            mainForm.backgroundWorker.ReportProgress(10, new BGReport("  Inserting/Updating records ..."));
            
            foreach (var aKVP in aDict)
            {
                newADict[aKVP.Key] = new Dictionary<int, RoundValue>();
                selectCmd = new SqlCommand("SELECT TOP 1 Country FROM dbo.history WHERE Country=@Country AND League=@League AND Season=@Season", this.Connection);
                selectCmd.Parameters.AddWithValue("@Country", aKVP.Key.Country);
                selectCmd.Parameters.AddWithValue("@League", aKVP.Key.League);
                selectCmd.Parameters.AddWithValue("@Season", aKVP.Key.Season);
                using (reader = selectCmd.ExecuteReader()) progressTitle = reader.HasRows ? "  Updating " : "  Inserting ";
                progress = (int)((double)(aI++) / aDict.Count * 80) + 10;
                mainForm.backgroundWorker.ReportProgress(progress, new BGReport(progressTitle + aKVP.Key + " ..."));
                if (History.errDict.ContainsKey(aKVP.Key))
                    mainForm.backgroundWorker.ReportProgress(progress, new BGReport(History.errDict[aKVP.Key], System.Drawing.Brushes.Red));
                foreach (var rKVP in aKVP.Value)
                {
                    var orderedEnum = from item in rKVP.Value.tDict
                                      let diff = item.Value.SumScored - item.Value.SumReceived
                                      orderby item.Value.Points descending, diff descending
                                      select item;
                    rVal = new RoundValue(rKVP.Value);
                    grDiff = (float)(rVal.maxPoints - rVal.minPoints) / 4;
                    i = 1;
                    foreach (var kvp in orderedEnum)
                    {
                        hVal = kvp.Value;
                        hVal.Position = i++;
                        hVal.grLevel = (byte)(Math.Round((rVal.maxPoints - hVal.Points) / grDiff) + 1);
                        if (rKVP.Key == 1) hVal.grStrength = 50;
                        else
                        {
                            //hVal.grStrength = getStrength1(newADict[aKVP.Key][rKVP.Key - 1], kvp);
                            //Console.Write("{0} -> {1}). ", rKVP.Key - 1, rKVP.Key);
                            hVal.grStrength = getStrength2(newADict[aKVP.Key][rKVP.Key - 1], kvp);
                            //Console.WriteLine(", PrevAvgStrength:{0}", hVal.grStrength);
                        }
                        if (insert(aKVP.Key, rKVP.Key, kvp.Key, hVal) == 0) Console.WriteLine("something is wrong [History.process]");
                        //Console.WriteLine("{0}:{1}, {2}, P:{3}({4},{5})", aKVP.Key, rKVP.Key, kvp.Key, hVal.Points, hVal.grLevel, hVal.grStrength);
                        rVal.tDict[kvp.Key] = hVal;
                    }
                    newADict[aKVP.Key][rKVP.Key] = rVal;
                    //Console.WriteLine("-- {0} --\n", rKVP.Value);
                }
            }
        }

        protected byte? getStrength1(RoundValue prevRVal, KeyValuePair<string,HistoryValue> kvp)
        {
            bool isHome = kvp.Value.Field == "HOME";
            int? result = kvp.Value.ScoreH - kvp.Value.ScoreA;
            result = result > 0 ? 0 : result < 0 ? 2 : 1;
            StringBuilder excelCell = new StringBuilder(((char)((isHome ? 'C' : 'E') + 7 * result)).ToString());
            excelCell.Append(3 + 6 * prevRVal.tDict[isHome ? kvp.Value.OtherTeam : kvp.Key].grLevel + prevRVal.tDict[isHome ? kvp.Key : kvp.Value.OtherTeam].grLevel);
            return (byte)((mainForm.excel.getByte(excelCell.ToString()) + 100) / 2);
        }

        /// <summary>
        /// PrevAvgStrength depends on:
        /// 1). current result
        /// 2). field
        /// 3). OtherTeam previous grLevel
        /// </summary>
        protected byte? getStrength2(RoundValue prevRVal, KeyValuePair<string, HistoryValue> kvp)
        {
            bool isHome = kvp.Value.Field == "HOME";
            int? result = kvp.Value.ScoreH - kvp.Value.ScoreA;
            return (byte)(mainForm.excel.getByte(((char)((result == 0 ? 'H' : 'C') + (isHome ? 0 : 1))).ToString() + (
                (isHome && result >= 0 || !isHome && result > 0 ? MainForm.XLSXHOMEWINDRAWY : MainForm.XLSXAWAYWINDRAWY) +
                (prevRVal.tDict.ContainsKey(kvp.Value.OtherTeam) ? (int)prevRVal.tDict[kvp.Value.OtherTeam].grLevel : 5)
            )) * 100);
        }

        protected int insert(ArchiveKey aKey, int round, string team, HistoryValue hVal)
        {
            string q = @"SELECT ID FROM dbo.history WHERE Country=@Country AND League=@League AND Season=@Season AND Round=@Round AND Team=@Team";
            SqlCommand selectCmd = new SqlCommand(q, Connection);
            selectCmd.Parameters.AddWithValue("@Country", aKey.Country);
            selectCmd.Parameters.AddWithValue("@League", aKey.League);
            selectCmd.Parameters.AddWithValue("@Season", aKey.Season);
            selectCmd.Parameters.AddWithValue("@Round", round);
            selectCmd.Parameters.AddWithValue("@Team", team);
            SqlCommand modifyCmd;
            using (SqlDataReader reader = selectCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    q = @"UPDATE dbo.history SET 
                        Country=@Country, League=@League, Season=@Season, Round=@Round, Date=@Date, Field=@Field, Team=@Team,
                        ScoredH=@ScoredH, ReceivedH=@ReceivedH, ScoredA=@ScoredA, ReceivedA=@ReceivedA, Scored=@Scored, Received=@Received,
                        Points=@Points, Position=@Position, grLevel=@grLevel, grStrength=@grStrength, OtherTeam=@OtherTeam
                        WHERE ID=@ID";
                    modifyCmd = new SqlCommand(q, Connection);
                    reader.Read();
                    modifyCmd.Parameters.AddWithValue("@ID", reader[0]);
                }
                else
                {
                    q = @"INSERT INTO dbo.history (
                            Country,League,Season,Round,Date,Field,Team,ScoredH,ReceivedH,ScoredA,ReceivedA,Scored,Received,Points,Position,grLevel,grStrength,OtherTeam
                        ) VALUES(
                            @Country,@League,@Season,@Round,@Date,@Field,@Team,@ScoredH,@ReceivedH,@ScoredA,@ReceivedA,@Scored,@Received,@Points,@Position,@grLevel,@grStrength,@OtherTeam
                        )";
                    modifyCmd = new SqlCommand(q, Connection);
                }
            }
            modifyCmd.Parameters.AddWithValue("@Country", aKey.Country);
            modifyCmd.Parameters.AddWithValue("@League", aKey.League);
            modifyCmd.Parameters.AddWithValue("@Season", aKey.Season);
            modifyCmd.Parameters.AddWithValue("@Round", round);
            modifyCmd.Parameters.AddWithValue("@Date", hVal.Date);
            modifyCmd.Parameters.AddWithValue("@Team", team);
            modifyCmd.Parameters.AddWithValue("@Field", hVal.Field);
            modifyCmd.Parameters.AddWithValue("@ScoredH", hVal.SumScoredH);
            modifyCmd.Parameters.AddWithValue("@ReceivedH", hVal.SumReceivedH);
            modifyCmd.Parameters.AddWithValue("@ScoredA", hVal.SumScoredA);
            modifyCmd.Parameters.AddWithValue("@ReceivedA", hVal.SumReceivedA);
            modifyCmd.Parameters.AddWithValue("@Scored", hVal.SumScored);
            modifyCmd.Parameters.AddWithValue("@Received", hVal.SumReceived);
            modifyCmd.Parameters.AddWithValue("@Points", hVal.Points);
            modifyCmd.Parameters.AddWithValue("@Position", hVal.Position);
            modifyCmd.Parameters.AddWithValue("@grLevel", hVal.grLevel);
            modifyCmd.Parameters.AddWithValue("@grStrength", (object)hVal.grStrength ?? DBNull.Value);
            modifyCmd.Parameters.AddWithValue("@OtherTeam", hVal.OtherTeam);
            return modifyCmd.ExecuteNonQuery();
        }
    }
}
