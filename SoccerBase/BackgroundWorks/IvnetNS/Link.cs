using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;

namespace SoccerBase.BackgroundWorks.IvnetNS
{
    public struct LeagueKey
    {
        public string Country, League, Season;
    }

    public struct LeagueValue
    {
        public int Teams, Matches, Guessed, GuessedPercent;
    }

    public struct TeamsKey
    {
        public string Country, League, Season, Team;
    }

    public struct TeamsValue
    {
        public int Matches, Guessed, GuessedPercent;
    }

    public class Link
    {
        public SqlConnection Connection;

        public void process()
        {
            Ivnet.FixSportRadarID(Connection);
            string q = "SELECT ID,Country,League,Season,Date,HomeTeam,AwayTeam,Guessed FROM dbo.Ivnet";
            SqlCommand selectCmd, selectCmd2, modifyCmd;
            SqlDataReader reader, reader2;
            string[] ivnetDate;
            int day;
            Dictionary<LeagueKey, LeagueValue> leaguesDict = new Dictionary<LeagueKey, LeagueValue>();
            Dictionary<TeamsKey, TeamsValue> teamsDict = new Dictionary<TeamsKey, TeamsValue>();
            LeagueKey lKey = new LeagueKey();
            LeagueValue lVal = new LeagueValue();
            TeamsKey tKey = new TeamsKey();
            bool guessed;
            using (reader = new SqlCommand(q, Connection).ExecuteReader())
            {
                while (reader.Read())
                {
                    q = @"SELECT ID,Round,Date FROM dbo.SportRadar WHERE
                          Country=@Country AND League=@League AND Season=@Season AND HomeTeam=@HomeTeam AND AwayTeam=@AwayTeam AND
                          DATEPART(month,Date)=@Month AND DATEPART(day,Date) BETWEEN @DayStart AND @DayEnd";
                    selectCmd = new SqlCommand(q, Connection);
                    selectCmd.Parameters.AddWithValue("@Country",
                        Synonyms.Countries.ContainsKey(reader["Country"]) ? Synonyms.Countries[reader["Country"]] : reader["Country"]);
                    selectCmd.Parameters.AddWithValue("@League",
                        Synonyms.Leagues.ContainsKey(reader["Country"] + "|" + reader["League"]) ?
                        Synonyms.Leagues[reader["Country"] + "|" + reader["League"]] : reader["League"]);
                    selectCmd.Parameters.AddWithValue("@Season", reader["Season"]);
                    selectCmd.Parameters.AddWithValue("@HomeTeam",
                        Synonyms.Teams.ContainsKey(reader["Country"] + "|" + reader["HomeTeam"]) ?
                        Synonyms.Teams[reader["Country"] + "|" + reader["HomeTeam"]] : reader["HomeTeam"]);
                    selectCmd.Parameters.AddWithValue("@AwayTeam",
                        Synonyms.Teams.ContainsKey(reader["Country"] + "|" + reader["AwayTeam"]) ?
                        Synonyms.Teams[reader["Country"] + "|" + reader["AwayTeam"]] : reader["AwayTeam"]);
                    ivnetDate = reader["Date"].ToString().Split(' ');
                    selectCmd.Parameters.AddWithValue("@Month", Synonyms.Months[ivnetDate[0]]);
                    int.TryParse(ivnetDate[1], out day);
                    selectCmd.Parameters.AddWithValue("@DayStart", day - 2);
                    selectCmd.Parameters.AddWithValue("@DayEnd", day + 2);
                    using (reader2 = selectCmd.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            q = "UPDATE dbo.Ivnet SET SportRadarID=@SportRadarID,Round=@Round WHERE ID=@IvnetID";
                            modifyCmd = new SqlCommand(q, Connection);
                            modifyCmd.Parameters.AddWithValue("@SportRadarID", reader2["ID"]);
                            modifyCmd.Parameters.AddWithValue("@Round", reader2["Round"]);
                            modifyCmd.Parameters.AddWithValue("@IvnetID", reader["ID"]);
                            if (modifyCmd.ExecuteNonQuery() == 0) Console.WriteLine("something is wrong");
                            lKey.Country = tKey.Country = reader["Country"].ToString();
                            lKey.League = tKey.League = reader["League"].ToString();
                            lKey.Season = tKey.Season = reader["Season"].ToString();
                            guessed = !(reader["Guessed"] is DBNull) && (bool)reader["Guessed"];
                            if (!leaguesDict.ContainsKey(lKey))
                            {
                                lVal.Matches = 1;
                                lVal.Guessed = guessed ? 1 : 0;
                            }
                            else
                            {
                                lVal = leaguesDict[lKey];
                                lVal.Matches++;
                                if (guessed) lVal.Guessed++;
                            }
                            lVal.GuessedPercent = (int)((double)lVal.Guessed / lVal.Matches * 100);
                            leaguesDict[lKey] = lVal;
                            doTeams(teamsDict, tKey, reader["HomeTeam"], guessed);
                            doTeams(teamsDict, tKey, reader["AwayTeam"], guessed);
                        }
                    }
                }
            }
            foreach (KeyValuePair<LeagueKey,LeagueValue> kvp in leaguesDict)
            {
                q = "SELECT ID FROM dbo.Leagues WHERE Country=@Country AND League=@League AND Season=@Season";
                selectCmd = new SqlCommand(q, Connection);
                selectCmd.Parameters.AddWithValue("@Country", kvp.Key.Country);
                selectCmd.Parameters.AddWithValue("@League", kvp.Key.League);
                selectCmd.Parameters.AddWithValue("@Season", kvp.Key.Season);
                using (reader = selectCmd.ExecuteReader())
                {
                    DateTime startDate = new DateTime(), endDate = new DateTime();
                    q = @"SELECT MIN(Date) Date FROM dbo.SportRadar WHERE Country=@Country AND League=@League AND Season=@Season";
                    selectCmd2 = new SqlCommand(q, Connection);
                    selectCmd2.Parameters.AddWithValue("@Country",
                        Synonyms.Countries.ContainsKey(kvp.Key.Country) ? Synonyms.Countries[kvp.Key.Country] : kvp.Key.Country);
                    selectCmd2.Parameters.AddWithValue("@League",
                        Synonyms.Leagues.ContainsKey(kvp.Key.Country + "|" + kvp.Key.League) ?
                        Synonyms.Leagues[kvp.Key.Country + "|" + kvp.Key.League] : kvp.Key.League);
                    selectCmd2.Parameters.AddWithValue("@Season", kvp.Key.Season);
                    using (reader2 = selectCmd2.ExecuteReader()) if (reader2.Read()) startDate = (DateTime)reader2["Date"];
                    q = @"SELECT MAX(Date) Date FROM dbo.SportRadar WHERE Country=@Country AND League=@League AND Season=@Season";
                    selectCmd2 = new SqlCommand(q, Connection);
                    selectCmd2.Parameters.AddWithValue("@Country",
                        Synonyms.Countries.ContainsKey(kvp.Key.Country) ? Synonyms.Countries[kvp.Key.Country] : kvp.Key.Country);
                    selectCmd2.Parameters.AddWithValue("@League",
                        Synonyms.Leagues.ContainsKey(kvp.Key.Country + "|" + kvp.Key.League) ?
                        Synonyms.Leagues[kvp.Key.Country + "|" + kvp.Key.League] : kvp.Key.League);
                    selectCmd2.Parameters.AddWithValue("@Season", kvp.Key.Season);
                    using (reader2 = selectCmd2.ExecuteReader()) if (reader2.Read()) endDate = (DateTime)reader2["Date"];
                    if (reader.HasRows)
                    {
                        q = @"UPDATE dbo.Leagues SET
                            StartDate=@StartDate, EndDate=@EndDate, Teams=@Teams, Matches=@Matches, Guessed=@Guessed, GuessedPercent=@GuessedPercent
                            WHERE ID=@ID";
                        modifyCmd = new SqlCommand(q, Connection);
                        reader.Read();
                        modifyCmd.Parameters.AddWithValue("@ID", reader["ID"]);
                    }
                    else
                    {
                        q = @"INSERT INTO dbo.Leagues(
                                Country,League,Season,StartDate,EndDate,Teams,Matches,Guessed,GuessedPercent
                            ) VALUES(@Country,@League,@Season,@StartDate,@EndDate,@Teams,@Matches,@Guessed,@GuessedPercent)";
                        modifyCmd = new SqlCommand(q, Connection);
                        modifyCmd.Parameters.AddWithValue("@Country", kvp.Key.Country);
                        modifyCmd.Parameters.AddWithValue("@League", kvp.Key.League);
                        modifyCmd.Parameters.AddWithValue("@Season", kvp.Key.Season);
                    }
                    modifyCmd.Parameters.AddWithValue("@StartDate", startDate);
                    modifyCmd.Parameters.AddWithValue("@EndDate", endDate);
                    int teamsCount = 0;
                    foreach (KeyValuePair<TeamsKey, TeamsValue> tKVP in teamsDict)
                    {
                        if (tKVP.Key.Country == kvp.Key.Country && tKVP.Key.League == kvp.Key.League && tKVP.Key.Season == kvp.Key.Season) teamsCount++;
                    }
                    modifyCmd.Parameters.AddWithValue("@Teams", teamsCount);
                    modifyCmd.Parameters.AddWithValue("@Matches", kvp.Value.Matches);
                    modifyCmd.Parameters.AddWithValue("@Guessed", kvp.Value.Guessed);
                    modifyCmd.Parameters.AddWithValue("@GuessedPercent", kvp.Value.GuessedPercent);
                    if (modifyCmd.ExecuteNonQuery() == 0) Console.WriteLine("something is wrong");
                    //Console.WriteLine("key: {0}|{1}|{2}, value: {3}|{4}|{5}",
                    //    kvp.Key.Country, kvp.Key.League, kvp.Key.Season, kvp.Value.Matches, kvp.Value.Guessed, kvp.Value.GuessedPercent);
                }
            }
        }

        public void doTeams(Dictionary<TeamsKey, TeamsValue> teamsDict, TeamsKey tKey, object team, bool guessed)
        {
            TeamsValue tVal = new TeamsValue();
            tKey.Team = team.ToString();
            if (!teamsDict.ContainsKey(tKey))
            {
                tVal.Matches = 1;
                tVal.Guessed = guessed ? 1 : 0;
            }
            else
            {
                tVal.Matches++;
                if (guessed) tVal.Guessed++;
            }
            tVal.GuessedPercent = (int)((double)tVal.Guessed / tVal.Matches * 100);
            teamsDict[tKey] = tVal;
        }

        public static void test()
        {
            LeagueKey lKey = new LeagueKey();
            LeagueValue lVal = new LeagueValue();
            Dictionary<LeagueKey, LeagueValue> leaguesDict = new Dictionary<LeagueKey, LeagueValue>();
            leaguesDict[lKey] = lVal;
            Console.WriteLine(leaguesDict.ContainsKey(lKey));
            lKey.Country = "mk";
            lKey.League = "1st";
            lKey.Season = "2011/12";
            lVal.Guessed = 0/3;
            lVal.Matches++;
            lVal.GuessedPercent = 32;
            leaguesDict[lKey] = lVal;
            lKey.Country = "mk";
            lKey.League = "1st";
            lKey.Season = "2011/12";
            lVal.Matches++;
            //lVal.Guessed = 2;
            lVal.GuessedPercent = 39;
            leaguesDict[lKey] = lVal;
            foreach (KeyValuePair<LeagueKey, LeagueValue> kvp in leaguesDict)
            {
                Console.WriteLine("key: {0}|{1}|{2}, value: {3}|{4}|{5}",
                    kvp.Key.Country, kvp.Key.League, kvp.Key.Season, kvp.Value.Matches, kvp.Value.Guessed, kvp.Value.GuessedPercent);
            }
        }
    }

}
