using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace SoccerBase.BackgroundWorks
{
    class Method1
    {
        public SqlConnection Connection;
        public string rowFormat = @"<tr style='background-color:{0};'>
    <td class='No'>{1}</td>
    <td class='ID'>{2}</td>
    <td class='Country'>{3}</td>
    <td class='League'>{4}</td>
    <td class='Season'>{5}</td>
    <td class='Round'>{6}</td>
    <td class='Date'>{7}</td>
    <td class='HomeTeam'>{8}</td>
    <td class='AwayTeam'>{9}</td>
    <td class='Forecast'>{10}</td>
    <td class='Tip'>{11}</td>
    <td class='Odds'>{12}</td>
    <td class='Score'>{13}</td>
    <td class='Guessed'>{14}</td>
    <td class='SpentMoney'>{15}</td>
    <td class='WonMoney'>{16}</td>
            </tr>";
        public string html;
        public List<string> topCountries, topLeagues;
        public int groups, groupCounter, matchIndex = 0, innerMatchIndex, roundIndex = 0, wonRounds = 0, lostRounds = 0,
            lastGroupCounter = -1, lastGroupIndex = -1, minMatches;
        public bool isTip1, isTipX, isTip2, isScore1, isScoreX, isScore2, isIncremental, isSmallBet, isMediumBet, isBigBet, isGroupAll, isBreaked = false;
        public double smallBet, mediumBet, bigBet, investedMoney, profit = 0.0, profitPercent, propInvest = 1, minInvest = 0.0, maxInvest = 0.0, maxGroupInvest,
            allInvestment = 0.0, allProfit = 0.0;

        public void processRound(string date, bool bOutput = true)
        {
            if (!bOutput) innerMatchIndex = 0;
            else if (innerMatchIndex < minMatches) return;
            string q = @"SELECT i.ID ID, i.Country Country, i.League League, i.Season Season, i.Round Round, sr.Date Date, i.HomeTeam HomeTeam, i.AwayTeam AwayTeam,
                i.ForecastH ForecastH, i.ForecastD ForecastD, i.ForecastA ForecastA, i.TipGoalsH TipGoalsH, i.TipGoalsA TipGoalsA,
                sr.HomeOdds HomeOdds, sr.DrawOdds DrawOdds, sr.AwayOdds AwayOdds, i.ScoreH ScoreH, i.ScoreA ScoreA, i.Guessed Guessed
                FROM dbo.Ivnet i, SoccerBase.dbo.SportRadar sr
                WHERE i.SportRadarID = sr.ID AND i.Guessed IS NOT NULL AND sr.Date = @Date AND i.Country IN ('" + String.Join("','", topCountries) + @"') AND
                i.League IN ('" + String.Join("','", topLeagues) + "')";
            SqlCommand selectCmd = new SqlCommand(q, Connection);
            selectCmd.Parameters.AddWithValue("@Date", date);
            SqlDataReader reader;
            using (reader = selectCmd.ExecuteReader())
            {
                int i = 0, j, tipWinner, scoreWinner, groupIndex = 0, forecastTemp, avgForecastWin = 0, avgForecastLost = 0, temp, groupLength;
                int[] forecastsGroup = new int[groups];
                double odds, currentInvestment, wonMoney = 0.0, currentWon;
                if (bOutput)
                {
                    html += String.Format("<p2>Round {0}:</p2>",++roundIndex);
                    html += @"<table border='1' style='font-family:Verdana;font-size:12px;'><thead><tr>
                        <th>No:</th><th>ID</th><th>Country</th><th>League</th><th>Season</th><th>Round</th><th>Date</th>
                        <th>Home Team</th><th>Away Team</th><th>Forecast</th><th>Tip</th><th>Odds</th><th>Score</th><th>Guessed</th>
                        <th>Spent Money</th><th>Won Money</th></tr></thead><tbody>";
                }
                bool isGuessed, takeMoney, isLastGroupIndex, isLastGroupIndex2;
                double[] investmentGroup = new double[groups], oddsGroup = new double[groups];
                bool[] wonGroup = new bool[groups];
                investedMoney = 0.0;
                groupCounter = 0;
                while (reader.Read())
                {
                    tipWinner = (byte)reader["TipGoalsH"] - (byte)reader["TipGoalsA"];
                    tipWinner = tipWinner >= 0 ? (tipWinner > 0 ? 0 : 1) : 2;
                    scoreWinner = (byte)reader["ScoreH"] - (byte)reader["ScoreA"];
                    scoreWinner = scoreWinner >= 0 ? (scoreWinner > 0 ? 0 : 1) : 2;

                    if (tipWinner == 0 && !isTip1) continue;
                    if (tipWinner == 1 && !isTipX) continue;
                    if (tipWinner == 2 && !isTip2) continue;
                    if (scoreWinner == 0 && !isScore1) continue;
                    if (scoreWinner == 1 && !isScoreX) continue;
                    if (scoreWinner == 2 && !isScore2) continue;

                    if (tipWinner == 0)
                    {
                        odds = (double)reader["HomeOdds"];
                        forecastTemp = (byte)reader["ForecastH"];
                    }
                    else if (tipWinner == 1)
                    {
                        odds = (double)reader["DrawOdds"];
                        forecastTemp = (byte)reader["ForecastD"];
                    }
                    else
                    {
                        odds = (double)reader["AwayOdds"];
                        forecastTemp = (byte)reader["ForecastA"];
                    }
                    currentInvestment = 0.0;
                    if (odds < 1.25) continue;
                    if (odds >= 1.25 && odds <= 1.5 && isBigBet) currentInvestment = bigBet;
                    else if (odds > 1.5 && odds <= 2.2 && isMediumBet) currentInvestment = mediumBet;
                    else if (odds > 2.2 && isSmallBet) currentInvestment = smallBet;
                    else continue;
                    groupIndex = i++ % groups;
                    if (!bOutput) innerMatchIndex++;
                    currentWon = 0.0;
                    isGuessed = (bool)reader["Guessed"];
                    investmentGroup[groupIndex] = currentInvestment;
                    wonGroup[groupIndex] = isGuessed;
                    oddsGroup[groupIndex] = odds;
                    forecastsGroup[groupIndex] = forecastTemp;
                    takeMoney = false;
                    isLastGroupIndex = groupIndex == groups - 1;
                    isLastGroupIndex2 = isGroupAll && bOutput && groupCounter == lastGroupCounter && groupIndex == lastGroupIndex;
                    //if (bOutput) Console.WriteLine("groupCounter: {0}, lastGroupCounter: {1}", groupCounter, lastGroupCounter);
                    if (isLastGroupIndex || isLastGroupIndex2)
                    {
                        groupCounter++;
                        if (isLastGroupIndex2) groupLength = lastGroupIndex + 1;
                        else groupLength = groups;
                        currentInvestment = 0.0;
                        for (j = 0; j < groupLength; j++) currentInvestment += investmentGroup[j];
                        currentInvestment /= groupLength;
                        if (isIncremental)
                        {
                            currentInvestment *= propInvest;
                            if (currentInvestment < smallBet) currentInvestment = smallBet;
                            if (currentInvestment > maxGroupInvest) currentInvestment = maxGroupInvest;
                            /*
                             * maxInvest can't work like this,
                             * unless you re-code everything
                            if (investedMoney + currentInvestment > maxInvest)
                            {
                                isBreaked = true;
                                //Console.WriteLine("Breaket: {0}", investedMoney + currentInvestment);
                                break;
                            }
                            */
                        }
                        investedMoney += currentInvestment;
                        currentWon = 1.0;
                        for (j = 0; j < groupLength; j++) currentWon *= oddsGroup[j];
                        currentWon *= currentInvestment;
                        takeMoney = true;
                        for (j = 0; j < groupLength; j++)
                        {
                            if (!wonGroup[j])
                            {
                                takeMoney = false;
                                break;
                            }
                        }
                        forecastTemp = 0;
                        for (j = 0; j < groupLength; j++) forecastTemp += forecastsGroup[j];
                        forecastTemp /= groupLength;
                        if (takeMoney)
                        {
                            wonMoney += currentWon;
                            avgForecastWin = avgForecastWin > 0 ? (avgForecastWin + forecastTemp) / 2 : avgForecastWin + forecastTemp;
                        }
                        else
                        {
                            temp = avgForecastLost;
                            avgForecastLost = avgForecastLost > 0 ? (avgForecastLost + forecastTemp) / 2 : avgForecastLost + forecastTemp;
                        }
                    }
                    if (bOutput) html += String.Format(rowFormat,
                        isGuessed ? "transparent" : "pink",
                        ++matchIndex,
                        reader["ID"],
                        reader["Country"],
                        reader["League"],
                        reader["Season"],
                        reader["Round"],
                        String.Format("{0:yyyy-MM-dd}", reader["Date"]),
                        reader["HomeTeam"],
                        reader["AwayTeam"],
                        boldSpan(reader["ForecastH"], 0, tipWinner) + "&nbsp;&nbsp;" +
                        boldSpan(reader["ForecastD"], 1, tipWinner) + "&nbsp;&nbsp;" +
                        boldSpan(reader["ForecastA"], 2, tipWinner),
                        reader["TipGoalsH"] + ":" + reader["TipGoalsA"],
                        boldSpan(reader["HomeOdds"], 0, scoreWinner) + "&nbsp;&nbsp;" +
                        boldSpan(reader["DrawOdds"], 1, scoreWinner) + "&nbsp;&nbsp;" +
                        boldSpan(reader["AwayOdds"], 2, scoreWinner),
                        reader["ScoreH"] + ":" + reader["ScoreA"],
                        isGuessed ? "Yes" : "No",
                        isLastGroupIndex || isLastGroupIndex2 ? currentInvestment.ToString("F") : "",
                        (isLastGroupIndex || isLastGroupIndex2) && takeMoney ? currentWon.ToString("F") : ""
                    );
                }   // while (reader.Read())
                lastGroupCounter = -1;
                lastGroupIndex = -1;
                if (!bOutput)
                {
                    //Console.WriteLine("{0}) bOutput:{1},i:{2},grIdx:{3},grCnter:{4},Breaked:{5},lastGrIdx:{6},lastGrCnter:{7}",
                    //    matchIndex, bOutput, i, groupIndex, groupCounter, isBreaked, lastGroupIndex, lastGroupCounter);
                    if (isGroupAll && groupIndex < groups - 1)
                    {
                        //Console.WriteLine(matchIndex);
                        groupLength = groupIndex + 1;
                        isBreaked = false;
                        currentInvestment = 0.0;
                        for (j = 0; j < groupLength; j++) currentInvestment += investmentGroup[j];
                        currentInvestment /= groupLength;
                        if (isIncremental)
                        {
                            currentInvestment *= propInvest;
                            if (currentInvestment < smallBet) currentInvestment = smallBet;
                            if (currentInvestment > maxGroupInvest) currentInvestment = maxGroupInvest;
                        }
                        if (!isBreaked)
                        {
                            investedMoney += currentInvestment;
                            currentWon = 1.0;
                            for (j = 0; j < groupLength; j++) currentWon *= oddsGroup[j];
                            currentWon *= currentInvestment;
                            takeMoney = true;
                            for (j = 0; j < groupLength; j++)
                            {
                                if (!wonGroup[j])
                                {
                                    takeMoney = false;
                                    break;
                                }
                            }
                            if (takeMoney) wonMoney += currentWon;
                            lastGroupCounter = groupCounter;
                            lastGroupIndex = groupIndex;
                        }
                    }
                }
                else
                {
                    //Console.WriteLine("{0}) bOutput:{1},i:{2},grIdx:{3},grCnter:{4},Breaked:{5},lastGrIdx:{6},lastGrCnter:{7}",
                    //    matchIndex, bOutput, i, groupIndex, groupCounter, isBreaked, lastGroupIndex, lastGroupCounter);
                    profit = wonMoney - investedMoney;
                    profitPercent = profit / investedMoney * 100;
                    if (profit < 0) lostRounds++;
                    else wonRounds++;
                    string sum = @"</tbody></table>
    <table style='background-color:{0};font-family:Verdana;font-size:13px;'>
        <tbody>
            <tr><td>Invested:</td><td>{1:0.00}</td>          <td>Average Forecast Winners:</td><td>{6}</td></tr>
            <tr><td>Won:</td><td>{2:0.00}</td>               <td>Average Forecast Loosers:</td><td>{7}</td></tr>
            <tr><td>Profit:</td><td>{3:0.00}</td>            <td>All Profit:</td><td>{8:0.00}</td></tr>
            <tr><td>Percent:</td><td>{4:0}%</td></tr>
            <tr><td>Date:</td><td>{5}</td></tr>
        </tbody>
                        </table><br/>";
                    allInvestment += investedMoney;
                    allProfit += profit;
                    DateTime dt = DateTime.Parse(date);
                    sum = String.Format(sum, profit < 0 ? "pink" : "transparent", investedMoney, wonMoney, profit, profitPercent, dt.ToString("yyyy-MM-dd, dddd"),
                        avgForecastWin, avgForecastLost, allProfit);
                    html += sum;
                }
            }   // using (reader = selectCmd.ExecuteReader())
        }

        public static string boldSpan(object value, int currCol, int boldedCol)
        {
            if (currCol == boldedCol) return "<b>" + value.ToString() + "</b>";
            return value.ToString();
        }
    }
}
