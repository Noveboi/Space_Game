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
    public partial class Game : Form
    {
        private int playerSpeed;
        private Size playerSize;

        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            playerSpeed = 35;
            playerSize = p.Size;
        }

        /// <summary>
        /// Player controls are all included here
        /// </summary>
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //Move up
                case Keys.W:
                    if (p.Location.Y > Height/2) 
                    { p.Location = new Point(p.Location.X, p.Location.Y - playerSpeed); }
                    break;
                //Move left
                case Keys.A:
                    if (p.Location.X > 10) 
                    { p.Location = new Point(p.Location.X - playerSpeed, p.Location.Y); }
                    break;
                //Move down
                case Keys.S:
                    if (p.Location.Y < Height - (playerSize.Height + playerSpeed + 20)) 
                    { p.Location = new Point(p.Location.X, p.Location.Y + playerSpeed); }
                    break;
                //Move right
                case Keys.D:
                    if (p.Location.X < Width - (playerSize.Width + playerSpeed + 10) ) 
                    { p.Location = new Point(p.Location.X + playerSpeed, p.Location.Y); }
                    break;
            }

        }
    }
}
