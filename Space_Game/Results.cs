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

        private string date;

        public Results()
        {
            InitializeComponent();
        }
        
        public Results(int Score,int GameTime)
        {
            InitializeComponent();
            finalScore = Score;
            gameTime = GameTime;

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            date = String.Format("{0:g}", DateTime.Now.ToString());
        }

        private void Results_Load(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(Width, Height);
            pictureBox1.Location = new Point(0, 0);
            Controls.SetChildIndex(pictureBox1, -1);

            complimentLabel.Parent = pictureBox1;
            scoreResult.Parent = pictureBox1;
            timeResult.Parent = pictureBox1;
            rankingResult.Parent = pictureBox1;
            backLabel.Parent = pictureBox1;

            scoreResult.Text += " " + finalScore.ToString();
            timeResult.Text += " " + TimeSpan.FromSeconds(gameTime).ToString(@"mm\:ss");

            saveData();
        }

        private void backLabel_Click(object sender, EventArgs e) { Close(); }

        private void backLabel_MouseMove(object sender, MouseEventArgs e) { backLabel.Cursor = Cursors.Hand; }

        #region JSON Methods
        private ScoreData getJson()
        {
            string rawJsonText = File.ReadAllText("../../scores.json");
            return JsonSerializer.Deserialize<ScoreData>(rawJsonText);
        }

        private void saveData()
        {
            //scoreData contains all of the JSON info in a simple ScoreData object
            ScoreData scoreData = getJson();
            int scoreAmt = scoreData.scores.Count;
            scoreData.AppendLatestScores($"score{scoreAmt + 1}", finalScore, $"date{scoreAmt + 1}", date);
            string outJson = JsonSerializer.Serialize(scoreData);
            File.WriteAllText("../../scores.json",outJson);
        }
        #endregion
    }
}
