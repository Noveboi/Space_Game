using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Game
{
    public partial class Logger : Form
    {
        Game gameRef;
        public Logger(Game Game)
        {
            gameRef = Game;
            InitializeComponent();
            resizeBoxes();
            label1.Location = new Point(label1.Location.X, posBox.Height + 30);
            label2.Location = new Point(label1.Location.X, posBox.Height + label1.Height + 10);
            Focus();
        }

        private void Logger_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }

        private void Logger_Resize(object sender, EventArgs e)
        {
            resizeBoxes();
            label1.Location = new Point(label1.Location.X, posBox.Height + 30);
            label2.Location = new Point(label1.Location.X, posBox.Height + label1.Height + 10);

        }

        private void resizeBoxes()
        {
            int lMargin = logBox.Location.X;
            setSize(2.8, Height - 200);
            posBox.Location = new Point(lMargin + logBox.Size.Width, posBox.Location.Y);
            moveBox.Location = new Point(posBox.Size.Width + posBox.Location.X, logBox.Location.Y);
        }
        
        private void setSize(double proportions, int height)
        {
            if (proportions > 3 || proportions <= 2) throw new Exception("Cannot have posBox being larger than the other 2");
            logBox.Size = new Size((int)(Width / proportions), height);
            moveBox.Size = new Size((int)(Width / proportions), height);
            posBox.Size = new Size((int)((proportions - 2) * Width / proportions) - 40, height);
        }

        private void Logger_Load(object sender, EventArgs e)
        {
            Focus();
            foreach (var tb in Controls.OfType<TextBox>()) { tb.MouseClick += ClickBox; }
        }

        private void Logger_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().ToLower() == "p") { Hide(); gameRef.Focus(); }
        }

        private void ClickBox(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
        }

        private void Logger_Activated(object sender, EventArgs e)
        {
            foreach (var tb in Controls.OfType<TextBox>()) tb.Enabled = true;
        }
    }
}
