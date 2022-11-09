using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Game
{
    public partial class Scores : Form
    {

        private Font font = new Font("Niagara Solid", 24);
        private const int numHeight = 30;

        private List<Label> createNumCol()
        {
            List<Label> labels = new List<Label>();
            for (int i = 0; i < 10; i++)
            {
                Label label = new Label();
                label.Name = $"no{i + 1}";
                label.AutoSize = false;
                label.Size = new Size(60, numHeight);
                label.Text = $"{i + 1}";
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.BackColor = Color.Transparent;
                label.ForeColor = i % 2 == 0 ? Color.White : Color.FromArgb(255, 255, 161, 209);
                label.Font = font;
                label.Location = new Point(8, 170 + i * 37);
                labels.Add(label);
            }
            return labels;
        }
        public Scores()
        {
            InitializeComponent();

            bg.Size = new Size(Width, Height);
            bg.Location = new Point(0, 0);
        }

        private Label createLabel(Color color, Size size, Point location, string text)
        {
            Label label = new Label();
            label.AutoSize = false;
            label.BackColor = Color.Transparent;
            label.Font = font;
            label.TextAlign = ContentAlignment.MiddleCenter;

            label.ForeColor = color;
            label.Size = size;
            label.Location = location;
            label.Text = text;
            return label;
        }

        private void createDateScoreCols(DateScore dateScore, int i) //Put all under scoreLabel
        {
            Color color = i % 2 == 0 ? Color.White : Color.FromArgb(255, 255, 161, 209);
            Size sharedSize = new Size(scoreLabel.Width, numHeight);
            Point scoreLoc = new Point(scoreLabel.Location.X, scoreLabel.Location.Y + ((numHeight + 7) * i) + numLabel.Height);
            Point dateLoc = new Point(dateLabel.Location.X, scoreLabel.Location.Y + ((numHeight + 7) * i) + numLabel.Height);

            Label sLabel = createLabel(color, sharedSize, scoreLoc, dateScore.score.ToString());
            Label dLabel = createLabel(color, sharedSize, dateLoc, dateScore.date);

            Controls.Add(sLabel);
            Controls.Add(dLabel);
            sLabel.Parent = bg;
            dLabel.Parent = bg;
        }

        private static int compareScore(DateScore ds1, DateScore ds2)
        {
            return ds1.score.CompareTo(ds2.score);
        }

        private void Scores_Load(object sender, EventArgs e)
        {
            ScoreData scoreData = new ScoreData().GetJson();
            List<DateScore> dateScore = scoreData.ConvertToList();
            dateScore.Sort(compareScore);
            dateScore.Reverse();
            if (dateScore.Count >= 10) for (int i = 0; i < 10; i++) { createDateScoreCols(dateScore[i], i); }
            else for (int i = 0; i < dateScore.Count; i++) { createDateScoreCols(dateScore[i], i); }

            Controls.SetChildIndex(bg, -1);

            //For proper transparency
            title.Parent = bg;
            numLabel.Parent = bg;
            scoreLabel.Parent = bg;
            dateLabel.Parent = bg;

            foreach(var label in createNumCol())
            { 
                Controls.Add(label);
                Controls.SetChildIndex(label, 1); //Bring labels to front
                label.Parent = bg;
            }
        }
    }
}
