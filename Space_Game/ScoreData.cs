using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace Space_Game
{
    [Serializable]
    public class DateScore
    {
        public int score { get; set; }
        public string date { get; set; }
    }

    /// <summary>
    /// Provides C# to JSON interaction for all scores and the corresponding date that
    /// they were submitted at.
    /// </summary>
    [Serializable]
    internal class ScoreData
    {
        public Dictionary<string, int> scores { get; set; }
        public Dictionary<string, string> dates { get; set; }

        /// <summary>
        /// Add the latest scores after loading JSON data
        /// </summary>
        public void AppendLatestScores(string newScoreKey, int newScore, string newDateKey, string newDate)
        {
            scores.Add(newScoreKey, newScore);
            dates.Add(newDateKey, newDate);
        }
        public ScoreData GetJson()
        {
            string rawJsonText = File.ReadAllText("../../scores.json");
            return JsonSerializer.Deserialize<ScoreData>(rawJsonText);
        }

        public List<DateScore> ToList()
        {
            List<DateScore> list = new List<DateScore>();
            for (int i = 0; i < scores.Count; i++)
            {
                DateScore dateScore = new DateScore()
                {
                    score = scores.ElementAt(i).Value,
                    date = dates.ElementAt(i).Value
                };
                list.Add(dateScore);
            }
            return list;
        }

        /// <summary>
        /// Provides info about how to compare the DateScore object
        /// </summary>
        /// <param name="ds1">DateScore to be compared to ds2</param>
        /// <param name="ds2">DateScore to compare to ds1</param>
        /// <returns>
        /// -- -1: ds1 is SMALLER than ds2
        /// -- 0: ds1 is EQUAL to ds2
        /// -- 1: ds1 is LARGER than ds2
        /// </returns>
        private static int compareScore(DateScore ds1, DateScore ds2)
        {
            return ds1.score.CompareTo(ds2.score);
        }

        public List<DateScore> SortScores()
        {
            var list = ToList();
            list.Sort(compareScore);
            return list;
        }

    }
}
