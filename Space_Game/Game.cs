using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
        private int elapsedSeconds = 1;

        //player attributes
        private int vehicleSpeed;
        private Size vehicleSize;

        //bullet variables
        private List<PictureBox> bullets = new List<PictureBox>();
        private Timer bulletTimer = new Timer { Interval = 25 };
        int bulletSpeed = 20;

        //movement variables
        private Timer playerMovementTimer = new Timer { Interval = 50 };
        private bool mu, md, ml, mr;
        private Keys kdk = Keys.None;

        private Timer enemyMovementTimer = new Timer { Interval = 50 };
        private double sweepProbability = 0.0025;

        private int wait = 0;
        //Initiate state 2 of Sweep movement
        private bool sweep = false;
        //Initiate Sweep movement (Start the procedure) and the opposite
        private bool startSweep = false;
        private bool finishSweep = false;
        //Gather if current enemy position if more to the left of the window or otherwise
        private bool moreLeft = false;
        private bool begin = false;

        //paths
        private string spritePath = "../../Sprites/";
        #endregion
        public Game()
        {
            InitializeComponent();
            playerMovementTimer.Tick += PlayerMovementTimer_Tick;
            bulletTimer.Tick += BulletTimer_Tick;
            gameTimer.Tick += GameTimer_Tick;
            enemyMovementTimer.Tick += EnemyMovementTimer_Tick;
        }

        #region Timers
        private void EnemyMovementTimer_Tick(object sender, EventArgs e)
        {
            int maxRand = (int)(1/sweepProbability);
            //Random decision
            int dec = RandomNumberGenerator.Create().GetHashCode() % (maxRand + 1);

            #region Sweep Movement 
            //Condition to begin sweep
            if (dec == maxRand && !startSweep) startSweep = true;

            if (startSweep && !finishSweep) {
                //Determine ONCE if enemy is moreLeft
                if (!begin && enemyIsMoreLeft()) moreLeft = true;
                if (!begin) begin = true; //So as to not trigger the above if statement again

                int s = enemyMove_Sweep(wait, sweep, moreLeft); //Do the sweep movement and return the current state
                if (s == 1) wait++; //Wait 10 ticks before going to state 2
                else if (s == 2)
                {
                    wait = 10;
                    //this is passed to enemyMove_Sweep() and it will know that state 2 is a-go
                    if (!sweep) sweep = true; 
                }

                else if (s == 3) //reset
                {
                    finishSweep = true;
                    startSweep = false;
                    begin = false;
                }
                log.AppendText($"Enemy Sweeping! (state = {s}, wait = {wait}, sweep = {sweep}{Environment.NewLine})");
            }
            #endregion

            if (dec > 0 && dec <= maxRand - 1 && !startSweep)
            {
                enemyMove_Sporadic();
            }
        }
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
                TranslateBullet(bullet, 1);

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

        private void PlayerMovementTimer_Tick(object sender, EventArgs e)
        {
            PlayerMove();
        }
        #endregion

        private void Game_Load(object sender, EventArgs e)
        {
            vehicleSpeed = 20;
            vehicleSize = p.Size;

            Focus();

            timeLabel.Text = "00:00";
            gameTimer.Start();
            enemyMovementTimer.Start();
            Controls.SetChildIndex(timeLabel, -1);

            log.Hide();

            enemy.Location = new Point(Width/2, enemy.Location.Y);
        }

        #region Player Controls
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
                case Keys.Escape:
                    Close();
                    break;
            }
            PlayerMove();
            playerMovementTimer.Start();
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
                playerMovementTimer.Stop();
            }

        }
        #endregion

        //Player movement system inspired by: https://stackoverflow.com/a/29957353
        #region Player Movement Methods

        private int yReduction = 10;

        void MoveUp(PictureBox p)
        {
            if (p.Location.Y > Height / 2 - 100)
            { p.Location = new Point(p.Location.X, p.Location.Y - (vehicleSpeed - yReduction)); }
        }
        void MoveLeft(PictureBox p)
        {
            if (p.Location.X > 10)
            { p.Location = new Point(p.Location.X - vehicleSpeed, p.Location.Y); }
        }
        void MoveDown(PictureBox p)
        {
            if (p.Location.Y < Height - (vehicleSize.Height + vehicleSpeed + 20))
            { p.Location = new Point(p.Location.X, p.Location.Y + (vehicleSpeed - yReduction)); }
        }
        void MoveRight(PictureBox p)
        {
            if (p.Location.X < Width - (vehicleSize.Width + vehicleSpeed + 10))
            { p.Location = new Point(p.Location.X + vehicleSpeed, p.Location.Y); }
        }

        //Overload left and right for enemy
        void MoveLeft(PictureBox p, double moveScalar)
        {
            if (p.Location.X > 10)
            { p.Location = new Point(p.Location.X - (int)(vehicleSpeed * moveScalar), p.Location.Y); }
        }

        void MoveRight(PictureBox p, double moveScalar)
        {
            if (p.Location.X < Width - (vehicleSize.Width + vehicleSpeed + 10))
            { p.Location = new Point(p.Location.X + (int)(vehicleSpeed * moveScalar), p.Location.Y); }
        }

        void PlayerMove()
        {
            if (mu) MoveUp(p);
            if (ml) MoveLeft(p);
            if (md) MoveDown(p);
            if (mr) MoveRight(p);
        }
        #endregion

        #region Enemy Movement and Attack Methods (inherits from Player)

        bool enemyIsMoreLeft() { return enemy.Location.X < Width / 2 ? true : false; }
        /// <summary>
        /// Most common enemy movement. Moves small distances, but moves very frequently.
        /// Does communicate with timer (no return type)
        /// </summary>
        void enemyMove_Sporadic()
        {
            int move = RandomNumberGenerator.Create().GetHashCode()%3;
            if (move == 0)
            {
                //Chance to move up to 2 times more (takes values from 1 to 2)
                int moveDirection = RandomNumberGenerator.Create().GetHashCode() % 50;
                double moveWeight = (double)enemy.Location.X / (double)Width * 3 + 1;

                if (moveDirection >= 0 && moveDirection < 25) MoveLeft(enemy, moveWeight);
                if (moveDirection >= 25 && moveDirection <= 50) MoveRight(enemy, 4-moveWeight);
                

            }
        }
        /// <summary>
        /// Enemy goes to one end of the window (left or right, whichever they're closest to) and 
        /// goes from that end to the other while firing bullets rapidly
        /// Returns:
        /// - 0: Initial State: going to one of the ends of the window
        /// - 1: Waiting State: hold at one end of the screen for a brief moment
        /// - 2: Sweeping State: move across the window up till the other side
        /// - 3: Finished State: the sweeping movement has concluded
        /// </summary>
        /// <param name="moreLeft">True - Sweep Right | False - Sweep Left</param>
        /// <param name="sweep">Gets if sweeping state has started</param>
        /// <param name="wait">Gets how much more to wait when in waiting state</param>
        int enemyMove_Sweep(int wait,bool sweep, bool moreLeft)
        {
            //for state 1
            bool reachedLeft = enemy.Location.X <= enemy.Width / 2;
            bool reachedRight = enemy.Location.X >= Width - enemy.Width - 40;

            if (moreLeft && !sweep)
            {
                if(!reachedLeft) { MoveLeft(enemy, 1.5); return 0; }
                else 
                {
                    if (wait < 10) return 1;
                    else return 2;
                }
            } 
            else if (!moreLeft && !sweep)
            {
                if (!reachedRight) { MoveRight(enemy, 1.5); return 0; }
                else
                {
                    if (wait < 10) return 1;
                    else return 2;
                }
            }
            else if (moreLeft && sweep)
            {
                if (!reachedRight) { MoveRight(enemy); return 2; }
                else return 3;
            }
            else
            {
                if(!reachedLeft) { MoveLeft(enemy); return 2; }
                return 3;
            }
        }
        #endregion

        #region Bullet Mechanics
        void SpawnBullet(Point currentEntityLoc)
        {
            PictureBox bullet = new PictureBox();
            bullet.Image = Image.FromFile($"{spritePath}bullet.png");
            bullet.SizeMode = PictureBoxSizeMode.StretchImage;
            bullet.Size = new Size(10, 60);
            if (currentEntityLoc.Y >= Height / 2 - 120) 
            { // Spawn bullet ABOVE the entity (player)
                bullet.Location = new Point(currentEntityLoc.X + vehicleSize.Width / 2 - 5, currentEntityLoc.Y - vehicleSize.Height / 2 + 6);
            }
            else
            { // Spawn bullet BELOW the entity (enemy)
                bullet.Location = new Point(currentEntityLoc.X + vehicleSize.Width / 2 - 5, currentEntityLoc.Y + vehicleSize.Height / 2 + 6);
            }
            Controls.Add(bullet);
            bullets.Add(bullet);
        }

        /// <summary>
        /// Move the bullet object vertically through space
        /// </summary>
        /// <param name="bullet">The bullet object to move</param>
        /// <param name="direction">
        /// 1 -> Moves up (from bigger Y to smaller Y) |
        /// -1 -> Moves down (from smaller Y to bigger Y
        /// </param>
        void TranslateBullet(PictureBox bullet, int direction)
        {
            if (direction == -1 || direction == 1)
            {
                bullet.Location = new Point(bullet.Location.X, bullet.Location.Y - bulletSpeed * direction);
            }
            else throw new Exception("Bullet is bi-directional, it takes direction = -1 or 1.");
        }
        void TranslateBullet(PictureBox bullet, int direction, double speedMultiplier)
        {
            if (direction == 0 || direction == 1)
            {
                bullet.Location = new Point(bullet.Location.X, bullet.Location.Y - (int)(bulletSpeed * direction * speedMultiplier));
            }
            else throw new Exception("Bullet is bi-directional, it takes direction = -1 or 1.");
        }
        #endregion
    }
}
