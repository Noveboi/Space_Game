using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Game
{ 
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            pictureBox1.Size = new Size(800, 480);

            //For proper transparency
            playText.Parent = pictureBox1;
            scoresText.Parent = pictureBox1;
            optionsText.Parent = pictureBox1;
            title.Parent = pictureBox1;

            //Prevent resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }
        private void play_Click(object sender, EventArgs e)
        {//hide Menu when PLAY is clicked
         //close Menu when Game form is closed BEFORE game has finsihed
         //show the same Menu AFTER game is finisheds
            Game game = new Game();
            game.Show();

            game.MaximizeBox = false;
            game.MinimizeBox = false;

            if (game.Focused) Hide();

            game.FormClosed += (s, args) =>
            {
                if (game.resultsClosed) Show();
                else Close();
            };
        }

        //MouseEnter event
        private void changeCursor(object sender, EventArgs e)
        {
            var label = sender as Label; 
            label.Cursor = Cursors.Hand;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            //Set pictureBox as background (Z-index = -1)
            Controls.SetChildIndex(pictureBox1, -1);
        }

        private void optionsText_MouseClick(object sender, MouseEventArgs e)
        {
            Options options = new Options();
            options.Show();
            options.Size = new Size(Width, Height);
        }

        private void scoresText_Click(object sender, EventArgs e)
        {
            Scores scores = new Scores();
            scores.Show();
            scores.Size = new Size(Width / 2, 600);
        }
    }
}
