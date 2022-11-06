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
         //close Menu when Game form is closed
            Game game = new Game();
            game.Show();
            if (game.Focused) { this.Hide(); }
            //close Menu when event FormClosed is fired from the Game form
            game.FormClosed += (s, args) => this.Close(); 
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

            //Assign custom font

        }

        private void optionsText_MouseClick(object sender, MouseEventArgs e)
        {
            Options options = new Options();
            options.Show();
            options.Size = new Size(this.Width, this.Height);
        }

        private void playText_FontChanged(object sender, EventArgs e)
        { 
        }

        private void scoresText_Click(object sender, EventArgs e)
        {
            Scores scores = new Scores();
            scores.Show();
            scores.Size = new Size(this.Width / 2, 600);
        }
    }
    public class FontSetter
    {
        public void SetFont(Control parent)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("EarlyGameboy.ttf");
            foreach (var label in parent.Controls.OfType<Label>())
            {
                label.Font = new Font(pfc.Families[0],
                    label.Font.Size, label.Font.Style);
            }
        }
    }

    

}
