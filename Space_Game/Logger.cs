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
        public Logger()
        {
            InitializeComponent();
            resizeBoxes();

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
    }
}
