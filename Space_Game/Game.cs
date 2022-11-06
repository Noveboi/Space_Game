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
        #region Private Variables
        //game variables
        private Timer gameTimer = new Timer { Interval = 1000 };
        private int elapsedSeconds = 0;

        //player attributes
        private int vehicleSpeed;
        private Size vehicleSize;

        //bullet variables
        private List<PictureBox> bullets = new List<PictureBox>();
        private Timer bulletTimer = new Timer { Interval = 25 };

        //movement variables
        private Timer movementTimer = new Timer { Interval = 25 };
        private bool mu, md, ml, mr;
        private Keys kdk = Keys.None;

        //paths
        private string spritePath = "../../Sprites/";
        #endregion
        public Game()
        {
            InitializeComponent();
            movementTimer.Tick += MovementTimer_Tick;
            bulletTimer.Tick += BulletTimer_Tick;
            gameTimer.Tick += GameTimer_Tick;
        }

        #region Timers
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (elapsedSeconds > 120) { gameTimer.Stop(); return; }
            timeLabel.Text = TimeSpan.FromSeconds(elapsedSeconds).ToString(@"mm\:ss");
            elapsedSeconds++;

        }

        private void BulletTimer_Tick(object sender, EventArgs e)
        {
            foreach (var bullet in bullets.ToList())
            {
                TranslateBullet(bullet);

                //Despawn bullet if it goes off screen
                if(bullet.Location.Y <= -bullet.Height)
                {
                    Controls.Remove(bullet);
                    bullets.Remove(bullet);
                    log.AppendText("bullet removed!"+Environment.NewLine);
                }

                //Despawn bullet if it hits enemy
                bool xLeftBound = bullet.Location.X > enemy.Location.X;
                bool xRightBound = bullet.Location.X < enemy.Location.X + enemy.Width + bullet.Width;
                if ((xLeftBound && xRightBound) && bullet.Location.Y <= enemy.Location.Y + enemy.Height)
                {
                    Controls.Remove(bullet);
                    bullets.Remove(bullet);
                    log.AppendText("Enemy Hit!!!!");
                    //ADD SCORE AND STUFF
                }
            }
        }

        private void MovementTimer_Tick(object sender, EventArgs e)
        {
            PlayerMove();
        }
        #endregion

        private void Game_Load(object sender, EventArgs e)
        {
            vehicleSpeed = 20;
            vehicleSize = p.Size;

            Focus();

            timeLabel.Text = "";
            gameTimer.Start();
            Controls.SetChildIndex(timeLabel, -1);

            log.Hide();
        }

        /// <summary>
        /// Player controls are all included here
        /// </summary>
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (kdk != Keys.None && kdk == e.KeyCode) return;
            kdk = e.KeyCode;
            switch (e.KeyCode)
            {
                //Move up
                case Keys.W:
                    mu = true;
                    break;
                //Move left
                case Keys.A:
                    ml = true;
                    break;
                //Move down
                case Keys.S:
                    md = true;
                    break;
                //Move right
                case Keys.D:
                    mr = true;
                    break;
                //Fire a bullet
                case Keys.Space:
                    SpawnBullet(p.Location);
                    log.AppendText($"Bullet Fired at {p.Location}{Environment.NewLine}");
                    log.AppendText($"Total Bullets Alive: {bullets.Count}{Environment.NewLine}");
                    bulletTimer.Start();
                    break;
                //Show or hide debugging log
                case Keys.P:
                    if (log.Visible) log.Hide();
                    else log.Show();
                    break;
            }
            PlayerMove();
            movementTimer.Start();
        }
        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            kdk = Keys.None;
            if (e.KeyCode == Keys.Space) return;

            switch (e.KeyCode)
            {
                //Move up
                case Keys.W:
                    mu = false;
                    break;
                //Move left
                case Keys.A:
                    ml = false;
                    break;
                //Move down
                case Keys.S:
                    md = false;
                    break;
                //Move right
                case Keys.D:
                    mr = false;
                    break;
            }

            if (!(mu || ml || md || mr))
            {
                movementTimer.Stop();
            }

        }

        private void Game_KeyPress(object sender, KeyPressEventArgs e)
        { 
           
        }

        //Player movement system inspired by: https://stackoverflow.com/a/29957353
        #region Player Movement Methods

        private int yReduction = 10;

        void MoveUp()
        {
            if (p.Location.Y > Height / 2 - 100)
            { p.Location = new Point(p.Location.X, p.Location.Y - (vehicleSpeed - yReduction)); }
        }
        void MoveLeft()
        {
            if (p.Location.X > 10)
            { p.Location = new Point(p.Location.X - vehicleSpeed, p.Location.Y); }
        }
        void MoveDown()
        {
            if (p.Location.Y < Height - (vehicleSize.Height + vehicleSpeed + 20))
            { p.Location = new Point(p.Location.X, p.Location.Y + (vehicleSpeed - yReduction)); }
        }
        void MoveRight()
        {
            if (p.Location.X < Width - (vehicleSize.Width + vehicleSpeed + 10))
            { p.Location = new Point(p.Location.X + vehicleSpeed, p.Location.Y); }
        }
        void PlayerMove()
        {
            if (mu) MoveUp();
            if (ml) MoveLeft();
            if (md) MoveDown();
            if (mr) MoveRight();
        }
        #endregion

        #region Bullet Mechanics
        void SpawnBullet(Point currentPlayerLoc)
        {
            PictureBox bullet = new PictureBox();
            bullet.Image = Image.FromFile($"{spritePath}bullet.png");
            bullet.SizeMode = PictureBoxSizeMode.StretchImage;
            bullet.Size = new Size(10, 60);
            bullet.Location = new Point(currentPlayerLoc.X + vehicleSize.Width / 2 - 5, currentPlayerLoc.Y - vehicleSize.Height / 2 + 6);
            Controls.Add(bullet);
            bullets.Add(bullet);
        }

        void TranslateBullet(PictureBox bullet)
        {
            bullet.Location = new Point(bullet.Location.X, bullet.Location.Y - 14);
        }
        #endregion
    }
}
