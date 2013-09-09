using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace SoccerBase
{
    public partial class NewMatchesForm : Form
    {
        public MainForm mainForm;
        private string tempHTML;

        public NewMatchesForm()
        {
            InitializeComponent();
        }

        private void NewMatchesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void btnNewMatches_Click(object sender, EventArgs e)
        {
            SqlDataReader reader;
            string q = @"SELECT sr.Date FROM dbo.Ivnet i, dbo.SportRadar sr
                            WHERE i.Guessed IS NULL AND i.SportRadarID=sr.ID AND sr.Date>=CONVERT(date,GETDATE()) GROUP BY sr.Date ORDER BY sr.Date";
            List<string> dates = new List<string>();
            using (SqlConnection connection = new SqlConnection(MainForm.STRCONN))
            {
                connection.Open();
                using (reader = new SqlCommand(q, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dates.Add(reader.GetDateTime(0).ToString("yyyy-MM-dd"));
                    }
                }
                int tipWinner, matchIndex = 0;
                double odds, currentInvestment, smallBet, mediumBet, bigBet;
                SqlCommand selectCmd;
                string html = "", rowFormat = @"<tr><td class='checkbox'><input type='checkbox' /></td><td class='No'>{0}</td>
                                <td class='Country'>{1}</td><td class='League'>{2}</td>
                                <td class='HomeTeam'>{3}</td><td class='AwayTeam'>{4}</td><td class='Tip'>{5}</td><td class='Odds'>{6}</td></tr>";
                parseBets(out smallBet, out mediumBet, out bigBet);
                foreach (string date in dates)
                {
                    q = @"SELECT i.ID ID, sr.Country Country, sr.League League, sr.Season Season, sr.Round Round, sr.Date Date,
                                sr.HomeTeam HomeTeam, sr.AwayTeam AwayTeam, i.TipGoalsH TipGoalsH, i.TipGoalsA TipGoalsA,
                                sr.HomeOdds HomeOdds, sr.DrawOdds DrawOdds, sr.AwayOdds AwayOdds
                                FROM dbo.Ivnet i, SoccerBase.dbo.SportRadar sr
                                WHERE i.SportRadarID = sr.ID AND i.Guessed IS NULL AND sr.Date = @Date";
                    selectCmd = new SqlCommand(q, connection);
                    selectCmd.Parameters.AddWithValue("@Date", date);
                    using (reader = selectCmd.ExecuteReader())
                    {
                        html += String.Format("<p2>{0:dddd, MMMM d, yyyy}:</p2>", DateTime.Parse(date));
                        html += @"<table border='1' style='font-family:Verdana;font-size:12px;'><thead><tr>
                                    <th></th><th>No:</th><th>Држава</th><th>Првенство</th>
                                    <th>Домашен Тим</th><th>Гостински Тим</th><th>Тип</th><th>BWin Коефициенти</th></tr></thead><tbody>";
                        currentInvestment = 0.0;
                        while (reader.Read())
                        {
                            tipWinner = (byte)reader["TipGoalsH"] - (byte)reader["TipGoalsA"];
                            tipWinner = tipWinner >= 0 ? (tipWinner > 0 ? 0 : 1) : 2;
                            if (tipWinner == 0)
                            {
                                odds = (double)reader["HomeOdds"];
                            }
                            else if (tipWinner == 1)
                            {
                                odds = (double)reader["DrawOdds"];
                            }
                            else
                            {
                                odds = (double)reader["AwayOdds"];
                            }
                            currentInvestment = 0.0;
                            if (odds < 1.25) continue;
                            if (odds >= 1.25 && odds <= 1.5 && cbBigBet.Checked) currentInvestment = bigBet;
                            else if (odds > 1.5 && odds <= 2.2 && cbMediumBet.Checked) currentInvestment = mediumBet;
                            else if (odds > 2.2 && cbSmallBet.Checked) currentInvestment = smallBet;
                            else continue;
                            html += String.Format(rowFormat, ++matchIndex,
                                reader["Country"], reader["League"], reader["HomeTeam"], reader["AwayTeam"],
                                reader["TipGoalsH"] + ":" + reader["TipGoalsA"],
                                reader["HomeOdds"].ToString() + "&nbsp;&nbsp;" + reader["DrawOdds"].ToString() + "&nbsp;&nbsp;" + reader["AwayOdds"].ToString()
                            );
                        }
                        html += "</tbody></table><br/>";
                    }
                }
                tempHTML = html;
                browser.DocumentText = @"<html><head>
                                <script type='text/javascript'>
                                    function func() {
                                        var inputs = document.getElementsByTagName('input');
                                        var tr;
                                        for (var i = 0; i < inputs.length; i++) {
                                            if (inputs[i].getAttribute('type')=='checkbox') {
                                                inputs[i].onclick = function() {
                                                    tr = this.parentNode.parentNode;
                                                    if (this.checked) tr.style.backgroundColor = 'cyan';
                                                    else tr.style.backgroundColor = '';
                                                }
                                            }
                                        }
                                    }
                                </script>
                            </head>
                            <body style='font-family:Verdana;font-size:12px;' onload='func();'>" + html +
                    @"<p>Send to email:
	                            <select id='emailsSelect'>
                                    <option value='err8086@gmail.com'>err8086@gmail.com</option>
		                            <option value='bingo_2010_@live.com'>bingo_2010_@live.com</option>
	                            </select>
	                            <button onclick='window.external.sendEmail(document.getElementById(""emailsSelect"").value);'>Send</button>
                            </p></body></html>";
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

        public void sendEmail(string email)
        {
            SmtpClient smtpServer = new SmtpClient();
            smtpServer.Host = "smtp.gmail.com";
            smtpServer.Port = 587;
            smtpServer.EnableSsl = true;
            smtpServer.Credentials = new NetworkCredential("dennykocev", "ImccJL2o");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("dennykocev@gmail.com", "Deni", System.Text.Encoding.UTF8);
            mail.To.Add(email);
            mail.Subject = "Нови утакмици";
            mail.Body = tempHTML;
            AlternateView altView = AlternateView.CreateAlternateViewFromString(tempHTML, null, MediaTypeNames.Text.Html);
            mail.AlternateViews.Add(altView);
            mail.IsBodyHtml = true;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            mail.ReplyToList.Add(new MailAddress("dennykocev@gmail.com"));
            smtpServer.Send(mail);
            MessageBox.Show("Message successfully sent to :" + email, "client code");
            //browser.DocumentText = tempHTML;
        }

    }
}
