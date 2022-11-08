using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Game
{
    internal class ScoreData
    {
        public Dictionary<string, int> scores { get; set; }
        public Dictionary<string, string> dates { get; set; }

        public void AppendLatestScores(string newScoreKey, int newScore, string newDateKey, string newDate)
        {
            scores.Add(newScoreKey, newScore);
            dates.Add(newDateKey, newDate);
        }
    }
}
