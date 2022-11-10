using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Game
{
    public partial class Results : Form
    {
        //Passed from Game.cs
        private int finalScore;
        private int gameTime;
        private int rawScore;
        private double difficultyMultiplier;
        private int rank;

        private string date;

        public Results()
        {
            InitializeComponent();
        }

        //This is a cubic-linear composite function of gameTime, where for gameTime = 60, ScoreMultiplier = 1 and for gameTime = 30, scoreMultiplier ~= 0.5
        //After gameTime = 60, the function turns linear 
        //Check the scoreMultiplier.gif animation in the Details folder to see how the 6.21 in the division was approximated
        private double ScoreMultiplier(int gametime)
        {
            return gameTime <= 60 ? new MyMath().CubeRoot(gametime - 60) / 6.21  + 1 : (gameTime - 60)/239 + 1;
        }
        
        public Results(int Score,int GameTime, int enemyDifficulty)
        {
            InitializeComponent();
            rawScore = Score;
            difficultyMultiplier = enemyDifficulty == 1 ? 1 : 1.5;
            finalScore = (int)(Score * ScoreMultiplier(GameTime) * difficultyMultiplier); ;
            gameTime = GameTime;

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            date = String.Format("{0:g}", DateTime.Now.ToString());
        }

        private void Results_Load(object sender, EventArgs e)
        {
            saveData();

            pictureBox1.Size = new Size(Width, Height);
            pictureBox1.Location = new Point(0, 0);
            Controls.SetChildIndex(pictureBox1, -1);

            complimentLabel.Parent = pictureBox1;
            scoreResult.Parent = pictureBox1;
            timeResult.Parent = pictureBox1;
            rankingResult.Parent = pictureBox1;
            backLabel.Parent = pictureBox1;

            scoreResult.Text += $" {finalScore} ({rawScore} x {Math.Round(ScoreMultiplier(gameTime),2)} x {difficultyMultiplier}) ";
            timeResult.Text += " " + TimeSpan.FromSeconds(gameTime).ToString(@"mm\:ss");
            rankingResult.Text = ShowRanking();
            ShowerWithPraise();

        }

        private void backLabel_Click(object sender, EventArgs e) { Close(); }

        private void backLabel_MouseMove(object sender, MouseEventArgs e) { backLabel.Cursor = Cursors.Hand; }

        private void ShowerWithPraise()
        {
            if (rank == 1) complimentLabel.Text = "New Highscore!!!";
            if (rank > 1 && rank <= 5) complimentLabel.Text = "You did amazing!";
            if (rank > 5 && rank <= 10) complimentLabel.Text = "You did fantastic!";
            else complimentLabel.Text = "You did great!";

        }

        private string ShowRanking()
        {
            ScoreData scoreData = new ScoreData().GetJson();
            List<DateScore> sortedData = scoreData.SortScores();
            sortedData.Reverse();
            int ranking = 0;
            DateTime properDate = DateTime.Parse(date);

            for (int i = 0; i < sortedData.Count; i++)
            {
                if (DateTime.Parse(sortedData[i].date) == properDate)
                {
                    ranking = i + 1;
                    break;
                }
            }
            rank = ranking;
            if (ranking.ToString().EndsWith("1") && ranking != 11) return $"You placed {ranking}st in your score rankings!";
            else if (ranking.ToString().EndsWith("2") && ranking != 12) return $"You placed {ranking}nd in your score rankings!";
            else if (ranking.ToString().EndsWith("3") && ranking != 13) return $"You placed {ranking}rd in your score rankings!";
            else if (ranking >= 10 && ranking < 20) return $"You placed {ranking}th in your score rankings!";
            else return $"You placed {ranking}th in your score rankings!";
        }
        private void saveData()
        {
            //scoreData contains all of the JSON info in a simple ScoreData object
            ScoreData scoreData = new ScoreData().GetJson();

            int scoreAmt = scoreData.scores.Count;
            
            scoreData.AppendLatestScores($"score{scoreAmt + 1}", finalScore, $"date{scoreAmt + 1}", date);

            string outJson = JsonSerializer.Serialize(scoreData);
            File.WriteAllText("../../scores.json",outJson);
        }
    }
}
