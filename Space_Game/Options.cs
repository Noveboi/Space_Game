using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Security.Cryptography;

namespace Space_Game
{
    public partial class Options : Form
    {
        UserSettings settings = new UserSettings();
        UserControls controls = new UserControls();
        Stars bgStars;
        List<Tuple<Label, int>> starsList = new List<Tuple<Label, int>>();
        Timer starAnimationTimer = new Timer() { Interval = 38 };
        int tickCounter = 0;

        public Options()
        {
            InitializeComponent();
            //Prevent resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            starAnimationTimer.Tick += StarAnimationTimer_Tick;
        }

        private void StarAnimationTimer_Tick(object sender, EventArgs e)
        {
            foreach(var star in starsList.ToList())
            {
                int shine = RandomNumberGenerator.Create().GetHashCode() % 10;
                int brightnessVar = RandomNumberGenerator.Create().GetHashCode() % 75;
                if (tickCounter % 2 == 0 && shine == 3)
                {
                    int b = 180 + brightnessVar;
                    star.Item1.BackColor = Color.FromArgb(255, b, b, b);
                }
                else if (tickCounter % 2 == 1 && shine == 3)
                {
                    if (star.Item1.BackColor.R >= 100)
                    { 
                        int b = star.Item1.BackColor.R - brightnessVar;
                        star.Item1.BackColor = Color.FromArgb(255, b, b, b);
                    } 
                }
            }
            tickCounter++;
        }

        private void Options_Load(object sender, EventArgs e)
        {
            settings.GrabFromJson(); //load the data from settings.json
            bgStars = new Stars(Width, Height, Controls, starsList);

            UpdateControls();

            AlignControl(colorPick, colorLabel);
            AlignControl(diffLabel, enemyLabel);
            AlignControl(gameTimeLabel, timeLabel);

            gameTimeLabel.Font = new Font("Niagara Solid", 48);

            gameTimeLabel.TextAlign = ContentAlignment.MiddleCenter;
            diffLabel.TextAlign = ContentAlignment.MiddleCenter;

            label3.Location = new Point(Width / 2 - label3.Width/2, label3.Location.Y+30);

            bgStars.CreateStars(40, 0);
            starAnimationTimer.Start();
        }

        private void UpdateControls()
        {
            diffLabel.Text = settings.EnemyDifficulty == 1 ? "Easy" : "Hard";
            gameTimeLabel.Text = settings.GameTime.ToString() + "s";
            colorPick.BackColor = ColorTranslator.FromHtml(settings.BulletColor);
            colorPick.FlatAppearance.MouseOverBackColor = colorPick.BackColor;
            colorPick.FlatAppearance.MouseDownBackColor = colorPick.BackColor;
        }

        private void AlignControl(Control control, Control toControl)
        {
            control.Location = new Point(
                toControl.Location.X + toControl.Width,
                toControl.Location.Y + toControl.Height / 2 - control.Height / 2);
        }

        private void label3_MouseMove(object sender, MouseEventArgs e) { label3.Cursor = Cursors.Hand; }

        private void label3_Click(object sender, EventArgs e) 
        { 
            new SetControls().Show(); 
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void colorPick_Click(object sender, EventArgs e)
        {
            
        }

        private void diffLabel_Click(object sender, EventArgs e)
        {
            settings.EnemyDifficulty = settings.EnemyDifficulty == 1 ? 2 : 1;
            settings.Sync();
            UpdateControls();
        }

        private void gameTimeLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) settings.GameTime++;
            if (e.Button == MouseButtons.Right) settings.GameTime--;
            settings.Sync();
            UpdateControls();
        }

        private void colorPick_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Random rnd = new Random();
                Color randomColor = Color.FromArgb(255, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                settings.BulletColor = ColorTranslator.ToHtml(randomColor);
                settings.Sync();
                UpdateControls();
            }
            if (e.Button == MouseButtons.Right)
            {
                ColorPrompt clrp = new ColorPrompt();
                clrp.Show();
                clrp.FormClosed += (s, args) => settings.GrabFromJson();
            }
        }
    }

}
