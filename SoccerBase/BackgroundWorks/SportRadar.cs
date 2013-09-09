using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

using Mannex.Net;
using Fizzler;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace SoccerBase
{
    class SportRadar : AbstractBase
    {
        private Regex _rx = new Regex(@"^Soccer &gt; (?<Country>\w+) &gt; (?<League>.+) (?<Season>\d{4}\/\d{2})$",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private Regex _rxScore = new Regex(@"^(?<HScore>\d+):(?<AScore>\d+)",
           RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
        
        public override string ProcessLink()
        {
            KeyValuePair<string, string[]> kvpLinks = enrLinks.Current;
            string link = kvpLinks.Value[0], blockHeaderBegin = kvpLinks.Value[1], blockHeaderEnd = kvpLinks.Value[2];
            string webTxt = _wc.DownloadStringUsingResponseEncoding(link);
            string strBegin = "name=\"Fixtures\"><![CDATA[", strEnd = "]]></c></n></n></n></n></n>";
            int startIdx = webTxt.IndexOf(strBegin) + strBegin.Length, endIdx = webTxt.IndexOf(strEnd, startIdx);
            if (blockHeaderBegin.Length > 0)
            {
                blockHeaderBegin = "<h2 class=\"title\">" + blockHeaderBegin + "</h2>";
                startIdx = webTxt.IndexOf(blockHeaderBegin, startIdx) + blockHeaderBegin.Length;
            }
            if (blockHeaderEnd.Length > 0)
            {
                int endIdx2 = webTxt.IndexOf("<h2 class=\"title\">" + blockHeaderEnd + "</h2>", startIdx, endIdx - startIdx);
                if (endIdx2 > -1) endIdx = endIdx2;
            }
            string html = webTxt.Substring(startIdx, endIdx - startIdx);
            //System.IO.File.WriteAllText("debugDoc.xml", html);
            
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml2(webTxt);
            //document.Save("debugDoc.xml");
            Match m = _rx.Match(document.DocumentNode.QuerySelector("page").Attributes["title"].Value);
            SportRadarValues values = new SportRadarValues();
            values.Country = m.Groups["Country"].Value.Trim();
            values.League = m.Groups["League"].Value.Trim();
            values.Season = m.Groups["Season"].Value.Trim();

            document.LoadHtml2(html);
            List<HtmlNode> tables = new List<HtmlNode>(document.DocumentNode.QuerySelectorAll("table.normaltable").ToArray());
            List<HtmlNode> headerRounds = new List<HtmlNode>(document.DocumentNode.QuerySelectorAll("h2.title").ToArray());
            List<HtmlNode> tableRows;
            List<HtmlNode> tableCells;
            int Round = 0;
            string homeScore, awayScore, strRound = "";
            foreach (HtmlNode table in tables)
            {
                tableRows = new List<HtmlNode>(table.QuerySelectorAll("tbody tr").ToArray());
                if (Table == "archive")
                {
                    strRound = headerRounds[++Round-1].InnerText.Trim();
                    strRound = strRound.IndexOf("Round") > -1 ? strRound.Substring(6) : Round.ToString();
                }
                foreach (HtmlNode row in tableRows)
                {
                    tableCells = new List<HtmlNode>(row.QuerySelectorAll("td").ToArray());
                    values.Date = DateTime.Parse(tableCells[0].InnerText.Trim().Split(' ')[0]).ToString("yyyy-MM-dd");
                    values.HomeTeam = tableCells[1].QuerySelector("span.home").InnerText.Trim();
                    values.AwayTeam = tableCells[1].QuerySelector("span.away").InnerText.Trim();
                    values.HomeOdds = tableCells[2].InnerText.Trim();
                    values.DrawOdds = tableCells[3].InnerText.Trim();
                    values.AwayOdds = tableCells[4].InnerText.Trim();
                    m = _rxScore.Match(tableCells[6].InnerText.Trim());
                    homeScore = m.Groups["HScore"].Value;
                    awayScore = m.Groups["AScore"].Value;
                    values.ScoreH = values.ScoreA = null;
                    if (homeScore != "" && awayScore != "")
                    {
                        values.ScoreH = homeScore;
                        values.ScoreA = awayScore;
                    }
                    values.Round = Table == "archive" ? strRound : Round.ToString();
                    /*
                    Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}:{11}",
                        values.Country, values.League, values.Season, values.Round, values.Date, values.HomeTeam, values.AwayTeam,
                        values.HomeOdds, values.DrawOdds, values.AwayOdds, values.ScoreH, values.ScoreA);
                    */
                    if (insert(values) == 0) Console.WriteLine("something is wrong");
                }
            }
            return kvpLinks.Key;
        }

        protected int insert(SportRadarValues values)
        {
            string q = "SELECT ID FROM SoccerBase.dbo." + Table + @" WHERE
                        Country=@Country AND League=@League AND Season=@Season AND Round=@Round AND HomeTeam=@HomeTeam AND AwayTeam=@AwayTeam";
            SqlCommand selectCmd = new SqlCommand(q, Connection);
            selectCmd.Parameters.AddWithValue("@Country", values.Country);
            selectCmd.Parameters.AddWithValue("@League", values.League);
            selectCmd.Parameters.AddWithValue("@Season", values.Season);
            selectCmd.Parameters.AddWithValue("@Round", values.Round);
            selectCmd.Parameters.AddWithValue("@HomeTeam", values.HomeTeam);
            selectCmd.Parameters.AddWithValue("@AwayTeam", values.AwayTeam);
            SqlCommand modifyCmd;
            using (SqlDataReader reader = selectCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    q = "UPDATE dbo." + Table + @" SET 
                        Country=@Country, League=@League, Season=@Season, Round=@Round, Date=@Date,
                        HomeTeam=@HomeTeam, AwayTeam=@AwayTeam, HomeOdds=@HomeOdds, DrawOdds=@DrawOdds, AwayOdds=@AwayOdds,
                        ScoreH=@ScoreH, ScoreA=@ScoreA
                        WHERE ID=@ID";
                    modifyCmd = new SqlCommand(q, Connection);
                    reader.Read();
                    modifyCmd.Parameters.AddWithValue("@ID", reader[0]);
                }
                else
                {
                    q = @"INSERT INTO dbo." + Table + @"(
                        Country,League,Season,Round,Date,HomeTeam,AwayTeam,HomeOdds,DrawOdds,AwayOdds,ScoreH,ScoreA
                    ) VALUES(@Country,@League,@Season,@Round,@Date,@HomeTeam,@AwayTeam,@HomeOdds,@DrawOdds,@AwayOdds,@ScoreH,@ScoreA)";
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
            modifyCmd.Parameters.AddWithValue("@HomeOdds", values.HomeOdds);
            modifyCmd.Parameters.AddWithValue("@DrawOdds", values.DrawOdds);
            modifyCmd.Parameters.AddWithValue("@AwayOdds", values.AwayOdds);
            modifyCmd.Parameters.AddWithValue("@ScoreH", (object)values.ScoreH ?? DBNull.Value);
            modifyCmd.Parameters.AddWithValue("@ScoreA", (object)values.ScoreA ?? DBNull.Value);
            int result = modifyCmd.ExecuteNonQuery();
            return result;
        }
    }
}
