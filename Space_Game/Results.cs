using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Game
{
    public partial class Results : Form
    {
        private int finalScore;
        private int gameTime;

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
        }

        private void backLabel_Click(object sender, EventArgs e)
        {
            Hide();
            Menu menu = new Menu();
            menu.Show();
            menu.FormClosed += (s, args) => Close();
        }

        private void backLabel_MouseMove(object sender, MouseEventArgs e) { backLabel.Cursor = Cursors.Hand; }
    }
}
