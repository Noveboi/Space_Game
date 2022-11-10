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

namespace Space_Game
{
    public partial class Options : Form
    {
        UserSettings settings = new UserSettings();
        public Options()
        {
            InitializeComponent();
            //Prevent resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void Options_Load(object sender, EventArgs e)
        {
            settings.GrabFromJson(); //load the data from settings.json

            UpdateControls();

            AlignControl(colorPick, colorLabel);
            AlignControl(diffLabel, enemyLabel);
            AlignControl(gameTimeLabel, timeLabel);

            gameTimeLabel.Font = new Font("Niagara Solid", 48);

            gameTimeLabel.TextAlign = ContentAlignment.MiddleCenter;
            diffLabel.TextAlign = ContentAlignment.MiddleCenter;

            label3.Location = new Point(Width / 2 - label3.Width/2, label3.Location.Y+30);
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

        private void label3_Click(object sender, EventArgs e) { new Help().Show(); }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void colorPick_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Color randomColor = Color.FromArgb(255, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            settings.BulletColor = ColorTranslator.ToHtml(randomColor);
            settings.Sync();
            UpdateControls();
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
    }

}
