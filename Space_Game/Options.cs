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
            MessageBox.Show(settings.BulletColor.ToString());
            diffLabel.Text = settings.EnemyDifficulty == 1 ? "Easy" : "Veteran";
            textBox1.Text = settings.GameTime.ToString();
            label3.Location = new Point(Width / 2 - label3.Width/2, label3.Location.Y+30);
        }

        private void label3_MouseMove(object sender, MouseEventArgs e) { label3.Cursor = Cursors.Hand; }

        private void label3_Click(object sender, EventArgs e) { new Help().Show(); }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

}
