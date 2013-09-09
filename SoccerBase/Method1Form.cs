using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SoccerBase.BackgroundWorks;
using System.Globalization;

namespace SoccerBase
{
    public partial class Method1Form : Form
    {
        public MainForm mainForm;

        public Method1Form()
        {
            InitializeComponent();
            cboxGroup.SelectedIndex = 1;
            cboxGroup.SelectionChangeCommitted += new EventHandler(btnMethod1_Click);
            using (SqlConnection connection = new SqlConnection(MainForm.STRCONN))
            {
                connection.Open();
                string q = @"SELECT ID FROM dbo.Leagues WHERE Season='2011/12' ORDER BY GuessedPercent DESC";
                using (SqlDataReader reader = new SqlCommand(q, connection).ExecuteReader())
                {
                    int i = 1;
                    cboxTopLeagues.Items.Add("All Leagues");
                    while (reader.Read()) cboxTopLeagues.Items.Add(String.Format("TOP {0}", i++));
                    cboxTopLeagues.Items.RemoveAt(cboxTopLeagues.Items.Count - 1);
                    if (cboxTopLeagues.Items.Count == 0) return;
                    cboxTopLeagues.SelectedIndex = 0;
                    cboxTopLeagues.SelectionChangeCommitted += new EventHandler(btnMethod1_Click);
                }
            }
            //browser.AllowWebBrowserDrop = false;
            //browser.IsWebBrowserContextMenuEnabled = false;
            //browser.WebBrowserShortcutsEnabled = false;
            browser.ObjectForScripting = this;
        }

        private void Method1Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void btnMethod1_Click(object sender, EventArgs e)
        {
            double smallBet, mediumBet, bigBet, minInvest = 0.0, percentTake = 0.0, percentMoneyTake = 0.0;
            parseBets(out smallBet, out mediumBet, out bigBet);
            int groups = 0;
            string topLeaguesValue = "All Leagues";
            Invoke(new MethodInvoker(delegate()
            {
                int.TryParse(cboxGroup.SelectedItem.ToString(), out groups);
                topLeaguesValue = cboxTopLeagues.SelectedItem.ToString();
                numMinMatches.Minimum = groups;
            }));
            string q = @"SELECT sr.Date FROM dbo.Ivnet i, dbo.SportRadar sr
                                     WHERE i.Guessed is not null AND i.SportRadarID=sr.ID AND sr.Date>=@FromDate AND sr.Date<=@ToDate
                                     GROUP BY sr.Date ORDER BY sr.Date";        // HAVING COUNT(sr.Date)>3
            using (SqlConnection connection = new SqlConnection(MainForm.STRCONN))
            {
                connection.Open();
                SqlCommand selectCmd = new SqlCommand(q, connection);
                selectCmd.Parameters.AddWithValue("@FromDate", dtpFrom.Value.ToString("yyyy-MM-dd"));
                selectCmd.Parameters.AddWithValue("@ToDate", dtpTo.Value.ToString("yyyy-MM-dd"));
                List<string> dates = new List<string>();
                SqlDataReader reader;
                using (reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dates.Add(reader.GetDateTime(0).ToString("yyyy-MM-dd"));
                    }
                }
                Method1 method1 = new Method1();
                q = String.Format("SELECT {0} Country,League FROM dbo.Leagues WHERE Season='2011/12' ORDER BY GuessedPercent DESC",
                    topLeaguesValue == "All Leagues" ? "" : topLeaguesValue);
                method1.topCountries = new List<string>();
                method1.topLeagues = new List<string>();
                using (reader = new SqlCommand(q, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        method1.topCountries.Add(reader["Country"].ToString());
                        method1.topLeagues.Add(reader["League"].ToString());
                    }
                }
                method1.Connection = connection;
                method1.groups = groups;
                method1.minMatches = (int)numMinMatches.Value;
                method1.smallBet = smallBet;
                method1.mediumBet = mediumBet;
                method1.bigBet = bigBet;
                method1.isTip1 = cbTip1.Checked;
                method1.isTipX = cbTipX.Checked;
                method1.isTip2 = cbTip2.Checked;
                method1.isScore1 = cbScore1.Checked;
                method1.isScoreX = cbScoreX.Checked;
                method1.isScore2 = cbScore2.Checked;
                method1.isSmallBet = cbSmallBet.Checked;
                method1.isMediumBet = cbMediumBet.Checked;
                method1.isBigBet = cbBigBet.Checked;
                method1.isGroupAll = cbGroupAll.Checked;
                method1.isIncremental = cbIncrement.Checked;
                if (method1.isIncremental)
                {
                    double.TryParse(tbMinInvest.Text, out minInvest);
                    double.TryParse(tbPercentTake.Text, out percentTake);
                    double.TryParse(tbMaxInvest.Text, out method1.maxInvest);
                    double.TryParse(tbMaxGroupInvest.Text, out method1.maxGroupInvest);
                    method1.minInvest = minInvest;
                }
                foreach (string date in dates)
                {
                    method1.propInvest = 1.0;
                    method1.processRound(date, false);
                    if (method1.isIncremental) method1.propInvest = method1.minInvest / method1.investedMoney;
                    //Console.WriteLine(", maxInvest: {0}, investedMoney: {1}", method1.maxInvest, method1.investedMoney);
                    //Console.WriteLine("minInvest: {0}, percentMoneyTake: {1}", method1.profit, percentMoneyTake);
                    method1.processRound(date);
                    if (method1.isIncremental)
                    {
                        if (cbPercentTake.Checked && method1.profit > 0)
                        {
                            percentMoneyTake = method1.profit * percentTake / 100;
                            method1.allProfit += percentMoneyTake;
                            method1.profit -= percentMoneyTake;
                        }
                        method1.minInvest = method1.minInvest + method1.profit;
                        if (method1.minInvest < minInvest) method1.minInvest = minInvest;
                        if (cbMaxInvest.Checked && method1.minInvest > method1.maxInvest) method1.minInvest = method1.maxInvest;
                    }
                }
                string summary = @"<div style='border-top:3px solid black;'><table>
    <thead>
        <tr cellspan='2'><th>Summary:</th></tr>
    </thead>
    <tbody>
        <tr><td>All Investment:</td><td><b>{0:0.00}</b></td>    <td>&nbsp;&nbsp;</td><td>Won Rounds</td><td><b>{3}</b></td></tr>
        <tr><td>All Profit:</td><td><b>{1:0.00}</b></td>        <td>&nbsp;&nbsp;</td><td>Lost Rounds</td><td><b>{4}</b></td></tr>
        <tr><td>Percent:</td><td><b>{2:0}%</b></td>             <td>&nbsp;&nbsp;</td><td></td><td><b></b></td></tr>
    </tbody>
                            </table></div>";
                summary = String.Format(summary, method1.allInvestment, method1.allProfit, method1.allProfit / method1.allInvestment * 100,
                    method1.wonRounds, method1.lostRounds);
                browser.DocumentText = "<html><head></head><body style='font-family:Verdana;font-size:12px;'>" + method1.html + summary + "</body></html>";
            }
        }

        private void parseBets(out double smallBet, out double mediumBet, out double bigBet)
        {
            NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");
            smallBet = mediumBet = bigBet = 1.0;
            if (!double.TryParse(tbSmallBet.Text, style, culture, out smallBet) ||
                !double.TryParse(tbMediumBet.Text, style, culture, out mediumBet) ||
                !double.TryParse(tbBigBet.Text, style, culture, out bigBet))
            {
                MessageBox.Show("Invalid bets!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
