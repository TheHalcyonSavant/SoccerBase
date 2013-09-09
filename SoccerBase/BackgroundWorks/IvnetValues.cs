using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoccerBase
{
    class IvnetValues
    {
        public string Country = "";
        public string League = "";
        public string Season = "2011/12";
        public string Round = "";
        public string Date = "";
        public string HomeTeam = "";
        public string AwayTeam = "";
        public string ForecastH = "";
        public string ForecastD = "";
        public string ForecastA = "";
        public string TipGoalsH = null;
        public string TipGoalsA = null;
        public string ScoreH = null;
        public string ScoreA = null;
        public string Guessed
        {
            get
            {
                if (TipGoalsH == null || TipGoalsA == null || ScoreH == null || ScoreA == null) return null;
                int tipDiff = Convert.ToInt32(TipGoalsH) - Convert.ToInt32(TipGoalsA);
                tipDiff = tipDiff >= 0 ? (tipDiff > 0 ? 1 : 0) : -1;
                int scoreDiff = Convert.ToInt32(ScoreH) - Convert.ToInt32(ScoreA);
                scoreDiff = scoreDiff >= 0 ? (scoreDiff > 0 ? 1 : 0) : -1;
                return tipDiff == scoreDiff ? "1" : "0";
            }
        }
    }
}
