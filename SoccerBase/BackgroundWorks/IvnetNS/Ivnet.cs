using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Mannex.Net;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace SoccerBase.BackgroundWorks.IvnetNS
{
    class Ivnet : AbstractBase
    {
        public bool isNew = false;
        private Regex _rxHeader = new Regex(@"^(?<Country>\w+),? (?<League>.+) \((?<Round>\d{2})\)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private Regex _rx = new Regex(@"^(?<Date>.{7})(?<HTeam>.{14,15})(?<ATeam>.{15})(?<FH>\d{2})  (?<FD>\d{2})  (?<FA>\d{2})  \((?<Tip>\d{1}-\d{1})\)(?( \d{1,2}-) (?<Score>\d{1,2}-\d{1,2}))",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public override string ProcessLink()
        {
            KeyValuePair<string, string[]> kvpLinks = enrLinks.Current;
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml2(_wc.DownloadStringUsingResponseEncoding(kvpLinks.Value[0]));
            HtmlNode rootNode = document.DocumentNode;
            if (isNew) rootNode = rootNode.QuerySelector("td.ratc");
            ReadOnlyCollection<HtmlNode> nodes = new ReadOnlyCollection<HtmlNode>(rootNode.QuerySelectorAll("tr[id='resultsc'] pre").ToArray());
            string tableTxt, date;
            string[] tableLines, tip, score;
            IvnetValues values;
            Match m;
            foreach (HtmlNode node in nodes)
            {
                tableTxt = node.InnerText;
                tableLines = tableTxt.Split('\n');
                values = new IvnetValues();
                m = _rxHeader.Match(tableLines[0]);
                values.Country = m.Groups["Country"].Value;
                values.League = m.Groups["League"].Value.Trim(" -".ToCharArray());
                values.Round = m.Groups["Round"].Value;
                values.Round = Convert.ToInt32(values.Round) == 0 ? "01" : values.Round;
                for (int i = 1; i < tableLines.Length - 1; i++)
                {
                    m = _rx.Match(tableLines[i]);
                    date = m.Groups["Date"].Value.Trim();
                    if (date != "") values.Date = date;     //DateTime.Parse(date + " " + _thisYear).ToString("yyyy-MM-dd");
                    values.HomeTeam = m.Groups["HTeam"].Value.Trim();
                    values.AwayTeam = m.Groups["ATeam"].Value.Trim();
                    values.ForecastH = m.Groups["FH"].Value;
                    values.ForecastD = m.Groups["FD"].Value;
                    values.ForecastA = m.Groups["FA"].Value;
                    tip = m.Groups["Tip"].Value.Split('-');
                    values.TipGoalsH = values.TipGoalsA = null;
                    if (tip.Length==2)
                    {
                        values.TipGoalsH = tip[0];
                        values.TipGoalsA = tip[1];
                    }
                    score = m.Groups["Score"].Value.Split('-');
                    values.ScoreH = values.ScoreA = null;
                    if (score.Length == 2)
                    {
                        values.ScoreH = score[0];
                        values.ScoreA = score[1];
                    }
                    /*
                    Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}-{10} {11}:{12}", values.Country, values.League, values.Round, values.Date,
                        values.HomeTeam, values.AwayTeam, values.ForecastH, values.ForecastD, values.ForecastA, values.TipGoalsH, values.TipGoalsA,
                        values.ScoreH, values.ScoreA);
                    */
                    if (insert(values) == 0) Console.WriteLine("something is wrong [Ivnet.ProcessLink]");
                }
            }
            return kvpLinks.Key;
        }

        protected int insert(IvnetValues values)
        {
            string q = @"SELECT ID FROM dbo." + Table + @" WHERE
                        Country=@Country AND League=@League AND Season=@Season AND Round IN (@Round,@RoundP1,@RoundP2) AND HomeTeam=@HomeTeam AND AwayTeam=@AwayTeam";
            SqlCommand selectCmd = new SqlCommand(q, Connection);
            selectCmd.Parameters.AddWithValue("@Country", values.Country);
            selectCmd.Parameters.AddWithValue("@League", values.League);
            selectCmd.Parameters.AddWithValue("@Season", values.Season);
            byte round;
            byte.TryParse(values.Round, out round);
            selectCmd.Parameters.AddWithValue("@Round", round);
            selectCmd.Parameters.AddWithValue("@RoundP1", round+1);
            selectCmd.Parameters.AddWithValue("@RoundP2", round+2);
            selectCmd.Parameters.AddWithValue("@HomeTeam", values.HomeTeam);
            selectCmd.Parameters.AddWithValue("@AwayTeam", values.AwayTeam);
            SqlCommand modifyCmd;
            using (SqlDataReader reader = selectCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    q = @"UPDATE dbo." + Table + @" SET 
                        Country=@Country, League=@League, Season=@Season, Round=@Round, Date=@Date,
                        HomeTeam=@HomeTeam, AwayTeam=@AwayTeam, ForecastH=@ForecastH, ForecastD=@ForecastD, ForecastA=@ForecastA,
                        TipGoalsH=@TipGoalsH, TipGoalsA=@TipGoalsA, ScoreH=@ScoreH, ScoreA=@ScoreA, Guessed=@Guessed
                        WHERE ID=@ID";
                    modifyCmd = new SqlCommand(q, Connection);
                    reader.Read();
                    modifyCmd.Parameters.AddWithValue("@ID", reader[0]);
                }
                else
                {
                    q = @"INSERT INTO dbo." + Table + @"(
                        Country,League,Season,Round,Date,HomeTeam,AwayTeam,ForecastH,ForecastD,ForecastA,TipGoalsH,TipGoalsA,ScoreH,ScoreA,Guessed
                    ) VALUES(@Country,@League,@Season,@Round,@Date,@HomeTeam,@AwayTeam,@ForecastH,@ForecastD,@ForecastA,@TipGoalsH,@TipGoalsA,@ScoreH,@ScoreA,@Guessed)";
                    modifyCmd = new SqlCommand(q, Connection);
                }
            }
            modifyCmd.Parameters.AddWithValue("@Country", values.Country);
            modifyCmd.Parameters.AddWithValue("@League", values.League);
            modifyCmd.Parameters.AddWithValue("@Season", values.Season);
            modifyCmd.Parameters.AddWithValue("@Round", values.Round);
            modifyCmd.Parameters.AddWithValue("@Date", values.Date);
            modifyCmd.Parameters.AddWithValue("@HomeTeam", values.HomeTeam);
            modifyCmd.Parameters.AddWithValue("@AwayTeam", values.AwayTeam);
            modifyCmd.Parameters.AddWithValue("@ForecastH", values.ForecastH);
            modifyCmd.Parameters.AddWithValue("@ForecastD", values.ForecastD);
            modifyCmd.Parameters.AddWithValue("@ForecastA", values.ForecastA);
            modifyCmd.Parameters.AddWithValue("@TipGoalsH", (object)values.TipGoalsH ?? DBNull.Value);
            modifyCmd.Parameters.AddWithValue("@TipGoalsA", (object)values.TipGoalsA ?? DBNull.Value);
            modifyCmd.Parameters.AddWithValue("@ScoreH", (object)values.ScoreH ?? DBNull.Value);
            modifyCmd.Parameters.AddWithValue("@ScoreA", (object)values.ScoreA ?? DBNull.Value);
            modifyCmd.Parameters.AddWithValue("@Guessed", (object)values.Guessed ?? DBNull.Value);
            return modifyCmd.ExecuteNonQuery();
        }

        public static void FixSportRadarID(SqlConnection connection)
        {
            SqlDataReader reader;
            string q;

            q = @"UPDATE dbo.Ivnet SET AwayTeam='Pas Giannina' WHERE Country='Greece' AND League='1st Katigoria' AND Season='2011/12' AND Date='Sep 11' AND
                 HomeTeam='Panathinaikos' AND ScoreH=3 AND ScoreA=1";
            new SqlCommand(q, connection).ExecuteNonQuery();
            q = @"UPDATE dbo.Ivnet SET Date='Sep 21' WHERE Country='Greece' AND League='1st Katigoria' AND Season='2011/12' AND Date='Sep 10' AND
                 HomeTeam='Xanthi' AND AwayTeam='AEK'";
            new SqlCommand(q, connection).ExecuteNonQuery();
            q = @"UPDATE dbo.Ivnet SET Date='Sep 14' WHERE Country='Greece' AND League='1st Katigoria' AND Season='2011/12' AND Date='Aug 27' AND
                 HomeTeam='Panaitolikos' AND AwayTeam='Asteras'";
            new SqlCommand(q, connection).ExecuteNonQuery();
            q = @"UPDATE dbo.Ivnet SET Date='Jul 21' WHERE Country='Russia' AND League='Premier Liga' AND Season='2011/12' AND Date='May 21' AND
                 HomeTeam='Volga' AND AwayTeam='CSKA'";
            new SqlCommand(q, connection).ExecuteNonQuery();
            q = @"UPDATE dbo.Ivnet SET Date='May 25' WHERE Country='Russia' AND League='Premier Liga' AND Season='2011/12' AND Date='Mar 21' AND
                 HomeTeam='Kr.Sovetov' AND AwayTeam='CSKA'";
            new SqlCommand(q, connection).ExecuteNonQuery();

            q = @"UPDATE dbo.Ivnet SET AwayTeam='Udinese' WHERE Country='Italy' AND League='Seria A' AND Season='2011/12' AND Date='Sep 11' AND
                 HomeTeam='Lecce' AND ScoreH=0 AND ScoreA=2";
            new SqlCommand(q, connection).ExecuteNonQuery();

            q = @"UPDATE dbo.Ivnet SET Date='Sep 06' WHERE Country='Portuguese' AND League='Liga Sagres' AND Season='2011/12' AND Date='Aug 29' AND
                 HomeTeam='Leiria' AND AwayTeam='Porto' AND ScoreH=2 AND ScoreA=5";
            new SqlCommand(q, connection).ExecuteNonQuery();
            q = @"UPDATE dbo.Ivnet SET Date='Sep 04' WHERE Country='Portuguese' AND League='Liga Sagres' AND Season='2011/12' AND Date='Aug 22' AND
                 HomeTeam='Nacional' AND AwayTeam='Guimaraes' AND ScoreH=1 AND ScoreA=4";
            new SqlCommand(q, connection).ExecuteNonQuery();

            q = @"UPDATE dbo.Ivnet SET HomeTeam='Anji',AwayTeam='Amkar' WHERE Country='Russia' AND League='Premier Liga' AND Season='2011/12' AND
                         Date='Jun 18' AND HomeTeam='Amkar' AND AwayTeam='Anji' AND SportRadarID IS NULL";
            new SqlCommand(q, connection).ExecuteNonQuery();
            q = "UPDATE dbo.Ivnet SET Date='May 27' WHERE Country='Russia' AND League='Premier Liga' AND Season='2011/12' AND Date='May 28'";
            new SqlCommand(q, connection).ExecuteNonQuery();

            q = @"UPDATE dbo.Ivnet SET Date='Aug 06' WHERE Country='Scotland' AND League='Premier League' AND Season='2011/12' AND Date='Aug 07' AND
                 HomeTeam='Hibernian' AND AwayTeam='St_Johnstone'";
            new SqlCommand(q, connection).ExecuteNonQuery();
            q = @"SELECT ID FROM dbo.SportRadar WHERE Country='Scotland' AND League='Premier League' AND Season='2011/12' AND Date='2011-07-30' AND
                 HomeTeam='Celtic' AND AwayTeam='Dunfermline'";
            using (reader = new SqlCommand(q, connection).ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    q = @"INSERT INTO dbo.SportRadar(Country,League,Season,Round,Date,HomeTeam,AwayTeam,HomeOdds,DrawOdds,AwayOdds)
                        VALUES('Scotland','Premier League','2011/12',2,'2011-07-30','Celtic','Dunfermline',0,0,0)";
                    if (new SqlCommand(q, connection).ExecuteNonQuery() == 0) Console.WriteLine("something is wrong [Ivnet.FixSportRadarIDLinks]");
                }
            }

            q = @"UPDATE dbo.Ivnet SET Date='Oct 16' WHERE Country='Spanish' AND League='Primera Division' AND Season='2011/12' AND Date='Oct 17' AND
                 HomeTeam='Sevilla' AND AwayTeam='Sporting_G'";
            new SqlCommand(q, connection).ExecuteNonQuery();
        }
    }
}
