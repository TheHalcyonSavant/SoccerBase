using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SoccerBase.BackgroundWorks
{
    public struct LRTKey
    {
        public byte Round;
        public string Country, League, OtherTeam, Team, Season;

        public LRTKey(SqlDataReader reader, string team, string otherTeam, byte round = 0)
        {
            this.Country = reader["Country"].ToString();
            this.League = reader["League"].ToString();
            this.OtherTeam = otherTeam;
            this.Round = round > 0 ? round : (byte)reader["Round"];
            this.Season = reader["Season"].ToString();
            this.Team = team;
        }

        // overrided Equals and GetHashCode for speed optimization
        public override bool Equals(object obj)
        {
            LRTKey o = (LRTKey)obj;
            return this.Round == o.Round && this.Country.Equals(o.Country) && this.Team.Equals(o.Team) && this.Season.Equals(o.Season);
        }

        public override int GetHashCode()
        {
            return this.Round.GetHashCode() ^ this.Country.GetHashCode() ^ this.Team.GetHashCode() ^ this.Season.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("[{0}, {1}, {2}, {3}] [{4} : {5}]", this.Country, this.League, this.Season, this.Round, this.Team, this.OtherTeam);
        }

        public static LRTKey getOtherKey(LRTKey lrtKey)
        {
            LRTKey newLrtKey = (LRTKey)lrtKey.MemberwiseClone();
            newLrtKey.OtherTeam = lrtKey.Team;
            newLrtKey.Team = lrtKey.OtherTeam;
            return newLrtKey;
        }
    }

    public struct Power
    {
        public byte Level, PrevAvgStrength, Strength;

        public Power(SqlDataReader reader)
        {
            this.Level = (byte)reader["grLevel"];
            this.Strength = (byte)reader["grStrength"];
            this.PrevAvgStrength = (byte)reader["PrevAvgStrength"];
        }

        public Power(SqlDataReader reader, double[] rates, ref List<byte> lstStreingth)
        {
            double avgStrength = 0;
            this.Level = (byte)reader["grLevel"];
            this.PrevAvgStrength = this.Strength = (byte)reader["grStrength"];
            if (lstStreingth.Count > 0 && lstStreingth.Count < rates.Length)
            {
                foreach (int strength in lstStreingth)
                    avgStrength += strength;
                this.PrevAvgStrength = (byte)(Math.Round(avgStrength / lstStreingth.Count));
                //Console.WriteLine("{0}: [{1}] {2}", (byte)reader["Round"], String.Join(",", lstStreingth), this.PrevAvgStrength);
            }
            else if (lstStreingth.Count >= rates.Length)
            {
                for (int i = 0; i < rates.Length; i++)
                    avgStrength += lstStreingth[i] * rates[i];
                this.PrevAvgStrength = (byte)Math.Round(avgStrength);
                //Console.WriteLine("{0}: [{1}] {2}", (byte)reader["Round"], String.Join(",", lstStreingth), this.PrevAvgStrength);
                lstStreingth.RemoveAt(0);
            }
            lstStreingth.Add(this.Strength);
        }

        public override string ToString()
        {
            return String.Format("Level:{0}, Strength:{1}, PrevAvgStrength:{2}", this.Level, this.Strength, this.PrevAvgStrength);
        }
    }

    class PrevAvgStrength
    {
        public SqlConnection Connection;
        public static MainForm mainForm;

        public void process()
        {
            byte thisPyramidLvl, prevPyramidLvl;
            double[] rates = mainForm.getExcelRates(15);
            int i;
            string prevCountry = "", prevPyramidKey = "", prevSeason = "", prevTeam = "", whDebug = "";
            Dictionary<LRTKey, Power> dict = new Dictionary<LRTKey, Power>();
            List<byte> lstStreingth = new List<byte>();
            LRTKey lrtKey = new LRTKey();

            mainForm.backgroundWorker.ReportProgress(10, new BGReport("  Preparing PrevAvgStrength ..."));

            //whDebug = " WHERE Country='Argentina' AND Season='2009/10' AND Team='Aldosivi' AND Round<=18";
            using (SqlDataReader reader = new SqlCommand("SELECT * FROM dbo.history" + whDebug + " ORDER BY Country,Team,Season,Round", this.Connection).ExecuteReader())
            {
                while (reader.Read())
                {
                    lrtKey.Country = reader["Country"].ToString();
                    lrtKey.League = reader["League"].ToString();
                    lrtKey.OtherTeam = reader["OtherTeam"].ToString();
                    lrtKey.Round = (byte)reader["Round"];
                    lrtKey.Season = reader["Season"].ToString();
                    lrtKey.Team = reader["Team"].ToString();
                    if (lrtKey.Country != prevCountry)
                        mainForm.backgroundWorker.ReportProgress(25, new BGReport(String.Format("  Updating {0} ...", lrtKey.Country)));
                    if (prevTeam != lrtKey.Team) lstStreingth.Clear();
                    else if (prevSeason != lrtKey.Season)
                    {
                        thisPyramidLvl = Leagues.PyramidLevels[lrtKey.Country + "," + lrtKey.League];
                        prevPyramidLvl = Leagues.PyramidLevels[prevPyramidKey];
                        if (thisPyramidLvl > prevPyramidLvl)
                        {   // relegated
                            for (i = 0; i < lstStreingth.Count; i++)
                            {
                                if (lstStreingth[i] < 80) lstStreingth[i] = (byte)(lstStreingth[i] + 20);
                            }
                        }
                        else if (thisPyramidLvl < prevPyramidLvl)
                        {   // promoted
                            for (i = 0; i < lstStreingth.Count; i++)
                            {
                                if (lstStreingth[i] > 20) lstStreingth[i] = (byte)(lstStreingth[i] - 20);
                            }
                        }
                    }

                    insert(reader, lrtKey, new Power(reader, rates, ref lstStreingth));

                    prevCountry = lrtKey.Country;
                    prevPyramidKey = lrtKey.Country + "," + lrtKey.League;
                    prevSeason = lrtKey.Season;
                    prevTeam = lrtKey.Team;
                }
            }
        }

        protected int insert(SqlDataReader reader, LRTKey lrtKey, Power power)
        {
            SqlCommand modifyCmd = new SqlCommand(@"UPDATE dbo.history SET PrevAvgStrength=@PrevAvgStrength WHERE ID=@ID", Connection);
            modifyCmd.Parameters.AddWithValue("@PrevAvgStrength", power.PrevAvgStrength);
            modifyCmd.Parameters.AddWithValue("@ID", reader[0]);
            return modifyCmd.ExecuteNonQuery();
        }

    }
}
