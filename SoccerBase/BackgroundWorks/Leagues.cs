using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SoccerBase.BackgroundWorks
{
    struct LeagueKey
    {
        public byte PyramidLevel { get { return Leagues.PyramidLevels[this.Country + "," + this.League]; } }
        public string Country, League, Season;
    }

    struct LeagueValue
    {
        public int Matches, HomeWins, Draws, AwayWins;
        public DateTime startDate, endDate;
        public HashSet<string> Teams;

        public LeagueValue(DateTime date, string hTeam, string aTeam, int result)
        {
            this.Matches = 1;
            this.HomeWins = result > 0 ? 1 : 0;
            this.Draws = result == 0 ? 1 : 0;
            this.AwayWins = result < 0 ? 1 : 0;
            this.startDate = this.endDate = date;
            this.Teams = new HashSet<string>() { hTeam, aTeam };
        }
    }

    class Leagues
    {
        public static Dictionary<string, byte> PyramidLevels = new Dictionary<string, byte>()
        {
            {"Argentina,Primera Nacional B", 2}
            ,{"Austria,Bundesliga", 1}
            ,{"Belgium,Pro League", 1}
            ,{"Denmark,Superligaen", 1}
            ,{"England,Championship", 2}
            ,{"England,Premier League", 1}
            ,{"France,Ligue 1", 1}
            ,{"Germany,Bundesliga", 1}
            ,{"Germany,2nd Bundesliga", 2}
            ,{"Greece,Super League", 1}
            ,{"Italy,Serie A", 1}
            ,{"Netherlands,Eredivisie", 1}
            ,{"Portugal,Primeira Liga", 1}
            ,{"Romania,First Division", 1}
            ,{"Russia,Premier League", 1}
            ,{"Scotland,Premier League", 1}
            ,{"Spain,Primera Division", 1}
            ,{"Turkey,Super Lig", 1}
            ,{"Ukraine,Premier League", 1}
        };
        public SqlConnection Connection;

        public void process()
        {
            int result;
            string hTeam, aTeam;
            string q = @"SELECT Country,League,Season,Round,Date,HomeTeam,AwayTeam,ScoreH,ScoreA FROM dbo.archive ORDER BY Country,League,Season,Round";
            DateTime date;
            Dictionary<LeagueKey, LeagueValue> dict = new Dictionary<LeagueKey, LeagueValue>();
            LeagueKey lKey = new LeagueKey();
            LeagueValue lVal = new LeagueValue();

            using (SqlDataReader reader = new SqlCommand(q, Connection).ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader["ScoreA"] is DBNull || reader["ScoreH"] is DBNull) continue;
                    lKey.Country = reader["Country"].ToString();
                    lKey.League = reader["League"].ToString();
                    lKey.Season = reader["Season"].ToString();
                    date = (DateTime)reader["Date"];
                    hTeam = reader["HomeTeam"].ToString();
                    aTeam = reader["AwayTeam"].ToString();
                    result = (byte)reader["ScoreH"] - (byte)reader["ScoreA"];
                    if (!dict.ContainsKey(lKey))
                        lVal = new LeagueValue(date, hTeam, aTeam, result);
                    else
                    {
                        lVal = dict[lKey];
                        lVal.Matches++;
                        if (result > 0) lVal.HomeWins++;
                        else if (result < 0) lVal.AwayWins++;
                        else lVal.Draws++;
                        if (lVal.startDate < date) lVal.startDate = date;
                        if (lVal.endDate > date) lVal.endDate = date;
                        lVal.Teams.Add(hTeam);
                        lVal.Teams.Add(aTeam);
                    }
                    dict[lKey] = lVal;
                }
            }
            foreach (KeyValuePair<LeagueKey, LeagueValue> kvp in dict)
            {
                if (insert(kvp.Key, kvp.Value) == 0)
                {
                    Console.WriteLine("something is wrong [Leagues.process]");
                    break;
                }
            }
        }

        protected int insert(LeagueKey lKey, LeagueValue lVal)
        {
            string q = @"SELECT ID FROM dbo.Leagues WHERE Country=@Country AND League=@League AND Season=@Season";
            SqlCommand selectCmd = new SqlCommand(q, Connection);
            selectCmd.Parameters.AddWithValue("@Country", lKey.Country);
            selectCmd.Parameters.AddWithValue("@League", lKey.League);
            selectCmd.Parameters.AddWithValue("@Season", lKey.Season);
            SqlCommand modifyCmd;
            using (SqlDataReader reader = selectCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    q = @"UPDATE dbo.Leagues SET Country=@Country, League=@League, Season=@Season, StartDate=@StartDate, EndDate=@EndDate, Teams=@Teams, Matches=@Matches,
                        HomeWinsPercent=@HomeWinsPercent, DrawsPercent=@DrawsPercent, AwayWinsPercent=@AwayWinsPercent, PyramidLevel=@PyramidLevel WHERE ID=@ID";
                    modifyCmd = new SqlCommand(q, Connection);
                    reader.Read();
                    modifyCmd.Parameters.AddWithValue("@ID", reader[0]);
                }
                else
                {
                    q = @"INSERT INTO dbo.Leagues (
                        Country,League,Season,StartDate,EndDate,Teams,Matches,HomeWinsPercent,DrawsPercent,AwayWinsPercent,PyramidLevel
                    ) VALUES(@Country,@League,@Season,@StartDate,@EndDate,@Teams,@Matches,@HomeWinsPercent,@DrawsPercent,@AwayWinsPercent,@PyramidLevel)";
                    modifyCmd = new SqlCommand(q, Connection);
                }
            }
            modifyCmd.Parameters.AddWithValue("@Country", lKey.Country);
            modifyCmd.Parameters.AddWithValue("@League", lKey.League);
            modifyCmd.Parameters.AddWithValue("@Season", lKey.Season);
            modifyCmd.Parameters.AddWithValue("@StartDate", lVal.endDate);
            modifyCmd.Parameters.AddWithValue("@EndDate", lVal.startDate);
            modifyCmd.Parameters.AddWithValue("@Teams", lVal.Teams.Count);
            modifyCmd.Parameters.AddWithValue("@Matches", lVal.Matches);
            modifyCmd.Parameters.AddWithValue("@HomeWinsPercent", Math.Round((float)lVal.HomeWins / lVal.Matches * 100));
            modifyCmd.Parameters.AddWithValue("@DrawsPercent", Math.Round((float)lVal.Draws / lVal.Matches * 100));
            modifyCmd.Parameters.AddWithValue("@AwayWinsPercent", Math.Round((float)lVal.AwayWins / lVal.Matches * 100));
            modifyCmd.Parameters.AddWithValue("@PyramidLevel", lKey.PyramidLevel);
            return modifyCmd.ExecuteNonQuery();
        }
    }
}
