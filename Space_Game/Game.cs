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

/* TO-DO
 * -> Create algorithm to calculate score based on raw score, game time, and enemy difficulty [P1]
 * -> Add more options in Options.cs, like choose bullet color, choose game time, choose enemy difficulty [P2]
 * -> Add sounds (enemy bullet fire, player bullet fire, enemy hit, player hit, main menu select, main menu mouseEnter label) [P3]
 * -> Add background music (main menu, game) [P4]
 */
namespace Space_Game
{
    public partial class Game : Form
    {
        #region Private Variables
        //game variables & attributes
        private int score = 0;
        private const int gameTime = 60; //In seconds

        private Timer gameTimer = new Timer { Interval = 1000 };
        private int elapsedSeconds = 1;
        private Label announceLabel = new Label { Text = "", AutoSize = false, 
            Font = new Font("Niagara Engraved",196), TextAlign = ContentAlignment.MiddleCenter, 
            BackColor = System.Drawing.Color.Transparent, ForeColor = System.Drawing.Color.White,
            Location = new Point(0, 0)};
        private string getTime() { return TimeSpan.FromSeconds(elapsedSeconds).ToString(@"mm\:ss"); }
        private Timer onOpenTimer = new Timer { Interval = 1000 };
        private int countdown = 3;
        public bool resultsClosed = false;

        //stars
        private Timer starAnimateTimer = new Timer { Interval = 40 };
        private List<Tuple<Label,int>> stars = new List<Tuple<Label, int>>();
        private int counter = 0;

        //spacecraft vehicle attributes
        private int vehicleSpeed;
        private Size vehicleSize;

        //bullet variables
        private List<Tuple<PictureBox,Label>> bullets = new List<Tuple<PictureBox,Label>>();
        private Timer bulletTimer = new Timer { Interval = 25 };
        private const int bulletSpeed = 30;

        //movement variables
        private Timer playerMovementTimer = new Timer { Interval = 30 };
        private bool mu, md, ml, mr;
        private Keys kdk = Keys.None;
            
        private Timer enemyMovementTimer = new Timer { Interval = 50 };
        private double sweepProbability = 0.005;

        private int wait = 0;
        private const int waitTickAmt = 7;
        //Initiate state 2 of Sweep movement
        private bool sweep = false;
        //Initiate Sweep movement (Start the procedure) and the opposite
        private bool startSweep = false;
        //Gather if current enemy position if more to the left of the window or otherwise
        private bool moreLeft = false;
        private bool begin = false;

        //debugging
        private bool haveLog = true;
        Logger logger;

        #endregion

        #region Game Methods and Events
        public Game()
        {
            InitializeComponent();
            playerMovementTimer.Tick += PlayerMovementTimer_Tick;
            bulletTimer.Tick += BulletTimer_Tick;
            gameTimer.Tick += GameTimer_Tick;
            enemyMovementTimer.Tick += EnemyMovementTimer_Tick;
            onOpenTimer.Tick += OnOpenTimer_Tick;
            starAnimateTimer.Tick += StarAnimateTimer_Tick;

            Size = new Size(1280, 720);
            //Add in future (maybe), adjustable size with locked aspect ratio of 16:9

            if (haveLog)
            {
                logger = new Logger();
                logger.logBox.Text = "";
                logger.posBox.Text = "";
                logger.moveBox.Text = "";
            }

            announceLabel.Size = new Size(Width + 20, Height - 50);
        }

        private void doCountdown(string annouceText, int countFrom)
        {
            if (countdown == -1)
            {
                Controls.Remove(announceLabel);
                onOpenTimer.Stop();
                gameTimer.Start();
                enemyMovementTimer.Start();
                starAnimateTimer.Start();
            }
            if (countdown == countFrom) Controls.Add(announceLabel);
            if (countdown != 0) announceLabel.Text = countdown.ToString();
            if (countdown == 0) announceLabel.Text = annouceText;

            countdown--;
        }

        private void doCountdown()
        {
            if (countdown == -1) { Controls.Remove(announceLabel); }
            if (countdown == 3) { Controls.Add(announceLabel); Controls.SetChildIndex(announceLabel, -1); }
            if (countdown != 0) announceLabel.Text = countdown.ToString();
            countdown--;
        }
        void PauseGame()
        {
            bulletTimer.Stop();
            enemyMovementTimer.Stop();
            gameTimer.Stop();
            starAnimateTimer.Stop();
        }
        #region Star Creation
        Color randomBrightness()
        {
            int b = (RandomNumberGenerator.Create().GetHashCode() % 55) + 200;
            return Color.FromArgb(255, b, b, b);
        }

        Point randomLocation()
        {
            int x = RandomNumberGenerator.Create().GetHashCode() % Width;
            int y = RandomNumberGenerator.Create().GetHashCode() % Height;
            return new Point(x, y);
        }
        Point randomLocation(int yOffset)
        {
            int x = RandomNumberGenerator.Create().GetHashCode() % Width;
            int y = RandomNumberGenerator.Create().GetHashCode() % Height;
            y += yOffset;
            return new Point(x, y);
        }

        Size randomSize()
        {
            int s = (RandomNumberGenerator.Create().GetHashCode() % 3) + 3;
            return new Size(s, s);
        }

        void CreateStars(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Label star = new Label();
                star.AutoSize = false;
                star.Size = randomSize();
                star.Text = null;
                star.BackColor = randomBrightness();
                star.Location = randomLocation();
                stars.Add(new Tuple<Label,int>(star, (RandomNumberGenerator.Create().GetHashCode() % 6) + 3));
                Controls.Add(star);
                Controls.SetChildIndex(star, -1);
            }
        }
        void CreateStars(int amount, int yOffset)
        {
            logger.logBox.AppendText($"{amount} stars created with y-offset {yOffset}");
            for (int i = 0; i < amount; i++)
            {
                Label star = new Label();
                star.AutoSize = false;
                star.Size = randomSize();
                star.BackColor = randomBrightness();
                star.Location = randomLocation(yOffset);
                stars.Add(new Tuple<Label, int>(star, (RandomNumberGenerator.Create().GetHashCode() % 6) + 3));
                Controls.Add(star);
                Controls.SetChildIndex(star, -1);
            }
        }
        #endregion
        private void Game_Load(object sender, EventArgs e)
        {
            vehicleSpeed = 20;
            vehicleSize = p.Size;

            timeLabel.Text = "00:00";
            scoreLabel.Text = "0";

            Controls.SetChildIndex(timeLabel, -1);
            Controls.SetChildIndex(scoreLabel, -1);
            CreateStars(30);
            Focus();

            onOpenTimer.Start();

            enemy.Location = new Point(Width/2 - enemy.Width/2, enemy.Location.Y);
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && elapsedSeconds <= gameTime)
            {
                MessageBox.Show("Are you sure you want to exit? If yes, this run will not be recorded.", "Woah!", MessageBoxButtons.YesNo);
            }
        }
        #endregion

        #region Timers
        private void StarAnimateTimer_Tick(object sender, EventArgs e)
        {
            if (counter == 50) CreateStars(25,Height);
            logger.label1.Text = $"Exising stars: {stars.Count}";
            foreach (var star in stars.ToList())
            {
                star.Item1.Location = new Point(star.Item1.Location.X, (star.Item1.Location.Y - star.Item2));
                if (star.Item1.Location.Y < 0)
                {
                    Controls.Remove(star.Item1);
                    stars.Remove(star);
                }
            }
            //(% n) = reset every n ticks
            counter = (counter + 1) % 150;
            
        }
        private void EnemyMovementTimer_Tick(object sender, EventArgs e)
        {
            string time = getTime();
            logger.posBox.AppendText($"{time} - Enemy: {enemy.Location} {Environment.NewLine}            " +
                $"Player: {p.Location} {Environment.NewLine}");

            int maxRand = (int)(1/sweepProbability);
            //Random decision
            int dec = RandomNumberGenerator.Create().GetHashCode() % (maxRand + 1);

            //Condition to begin sweep
            if (dec == maxRand && !startSweep) startSweep = true;
            #region Sweep Movement 
            if (startSweep) {
                //Determine ONCE if enemy is moreLeft
                if (!begin && enemyIsMoreLeft()) moreLeft = true;
                if (!begin) begin = true; //So as to not trigger the above if statement again

                int s = enemyMove_Sweep(wait, sweep, moreLeft); //Do the sweep movement and return the current state
                if (s == 1) wait++; //Wait 10 ticks before going to state 2
                else if (s == 2)
                {
                    wait = waitTickAmt;
                    //this is passed to enemyMove_Sweep() and it will know that state 2 is a-go
                    if (!sweep) sweep = true; 
                }

                else if (s == 3) //reset
                {
                    wait = 0;
                    startSweep = false;
                    sweep = false;
                    begin = false;
                }
                logger.moveBox.AppendText($"{getTime()} - Sweeping! (st={s}, w={wait}, sw={sweep}){Environment.NewLine}");
            }
            #endregion

            if (dec > 0 && dec <= maxRand - 1 && !startSweep)
            {
                logger.moveBox.AppendText($"{getTime()} - {dec} | ");
                enemyMove_Sporadic();
                if (dec % 5 == 0)
                {
                    SpawnBullet(enemy.Location);
                }
            }
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if(elapsedSeconds >= gameTime - 3 && elapsedSeconds <= gameTime - 1)
            {
                if (elapsedSeconds == gameTime - 3) countdown = 3;
                doCountdown();
            }

            if (elapsedSeconds > gameTime - 1) 
            {
                PauseGame();
                gameTimer.Start();
                ClearAllBullets(bullets);
                announceLabel.Text = "GAME OVER!";
                Controls.Add(announceLabel); 
            }
            if(elapsedSeconds > gameTime + 2)
            {
                gameTimer.Stop();
                Results results = new Results(score,gameTime);
                results.Show();
                results.Focus();
                results.FormClosed += (s, args) =>
                {
                    resultsClosed = true;
                    Close();
                };
                Hide();
            }
            if (elapsedSeconds <= gameTime) timeLabel.Text = getTime();
            elapsedSeconds++;

        }
        private void OnOpenTimer_Tick(object sender, EventArgs e) 
        {
            if (countdown == -1)
            {
                onOpenTimer.Stop();
                gameTimer.Start();
                enemyMovementTimer.Start();
                starAnimateTimer.Start();
            }
            doCountdown("FIGHT!",3); 
            
        }
        private void BulletTimer_Tick(object sender, EventArgs e)
        {

            logger.label2.Text = $"Existing bullets: {bullets.Count}";
            foreach (var bullet in bullets.ToList())
            {
                if (bullet.Item1 == p) { FireBullet(bullet.Item2, 1);}
                if (bullet.Item1 == enemy) { FireBullet(bullet.Item2, -1);}

                //Despawn bullet if it goes off screen
                //condition = top of window OR bottom of window
                if (bullet.Item2.Location.Y <= -bullet.Item2.Height || bullet.Item2.Location.Y >= Height) clearBullet(bullet);
                
                //Despawn bullet if it hits enemy
                #region Enemy Location Conditions
                bool e_xLeftBound = bullet.Item2.Location.X > enemy.Location.X;
                bool e_xRightBound = bullet.Item2.Location.X < enemy.Location.X + enemy.Width + bullet.Item2.Width;
                #endregion
                if ((e_xLeftBound && e_xRightBound) && bullet.Item2.Location.Y <= enemy.Location.Y + enemy.Height)
                {
                    clearBullet(bullet);
                    score += 2;
                    scoreLabel.Text = score.ToString();
                    logger.logBox.AppendText($"{getTime()} - Enemy Hit!"+Environment.NewLine);
                }

                //Despawn bullet if it hits player
                #region Player Location Conditions
                bool p_xLeftBound = bullet.Item2.Location.X > p.Location.X;
                bool p_xRightBound = bullet.Item2.Location.X < p.Location.X + p.Width + bullet.Item2.Width;
                #endregion
                if ((p_xLeftBound && p_xRightBound) && bullet.Item2.Location.Y + bullet.Item2.Height >= p.Location.Y)
                {
                    clearBullet(bullet);
                    logger.logBox.AppendText($"{getTime()} - Player hit!" + Environment.NewLine);
                    score = score <= 0 ? 0 : score - 1;
                    scoreLabel.Text = score.ToString();

                }
            }
        }
        private void PlayerMovementTimer_Tick(object sender, EventArgs e) { PlayerMove(); }
        #endregion

        #region Player Controls
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (kdk != Keys.None && kdk == e.KeyCode) return;
            kdk = e.KeyCode;
            bool gameRunning = enemyMovementTimer.Enabled && gameTimer.Enabled;
            if (gameRunning)
            {
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
                        logger.logBox.AppendText($"{getTime()} - Bullet Fired at " +
                            $"{new Point(p.Location.X + vehicleSize.Width / 2 - 5,p.Location.Y)}{Environment.NewLine}");
                        break;
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (gameRunning) //Pause
                {
                    PauseGame();

                    announceLabel.Text = "GAME PAUSED";
                    Controls.Add(announceLabel);
                }
                else //Unpause
                {
                    gameTimer.Start();
                    enemyMovementTimer.Start();
                    bulletTimer.Start();

                    Controls.Remove(announceLabel);

                };  
            }
            if (e.KeyCode == Keys.P)
            {
                logger.Show();
                if (gameRunning) Focus();
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

        #region Enemy Movement and Attack Methods (Inherits Move Methods from Player)

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
                double moveWeight = (double)enemy.Location.X / (double)Width * 3 + 2;

                if (moveDirection >= 0 && moveDirection < 25) MoveLeft(enemy, moveWeight);
                if (moveDirection >= 25 && moveDirection <= 50) MoveRight(enemy, 6-moveWeight);
                logger.moveBox.AppendText($"Move Sporadic!" + Environment.NewLine);

            } else
            {
                logger.moveBox.AppendText("No Move!" + Environment.NewLine);
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
                if(!reachedLeft) { MoveLeft(enemy, 1.5); return 0;}
                else 
                {
                    if (wait < waitTickAmt) return 1;
                    else return 2;
                }
            } 
            else if (!moreLeft && !sweep)
            {
                if (!reachedRight) { MoveRight(enemy, 1.5); return 0; }
                else
                {
                    if (wait < waitTickAmt) return 1;
                    else return 2;
                    
                }
            }
            else if (moreLeft && sweep)
            {
                if (!reachedRight) { MoveRight(enemy,2); SpawnBullet(enemy.Location); return 2; }
                else return 3;
            }
            else
            {
                if(!reachedLeft) { MoveLeft(enemy,2); SpawnBullet(enemy.Location); return 2; }
                return 3;
            }
        }
        #endregion

        #region Bullet Mechanics
        void SpawnBullet(Point currentEntityLoc)
        {
            Label bullet = new Label();
            bullet.AutoSize = false;
            bullet.Size = new Size(10, 60);
            if (currentEntityLoc.Y >= Height / 2 - 120) 
            { // Spawn bullet ABOVE the entity (player)
                bullet.Location = new Point(currentEntityLoc.X + vehicleSize.Width / 2 - 5, currentEntityLoc.Y - vehicleSize.Height / 2 + 6);
                bullet.BackColor = System.Drawing.Color.FromArgb(255, 255, 30, 80);
                bullets.Add(new Tuple<PictureBox, Label>(p, bullet));
            }
            else
            { // Spawn bullet BELOW the entity (enemy)
                bullet.Location = new Point(currentEntityLoc.X + vehicleSize.Width / 2 - 5, currentEntityLoc.Y + vehicleSize.Height / 2 + 6);
                bullet.BackColor = System.Drawing.Color.FromArgb(255, 40, 255, 80);
                bullets.Add(new Tuple<PictureBox,Label>(enemy,bullet));
            }
            Controls.Add(bullet);
            Controls.SetChildIndex(bullet, 1);
            bulletTimer.Start();
        }

        /// <summary>
        /// Move the bullet object vertically through space
        /// </summary>
        /// <param name="bullet">The bullet object to move</param>
        /// <param name="direction">
        /// 1 -> Moves up (from bigger Y to smaller Y) |
        /// -1 -> Moves down (from smaller Y to bigger Y
        /// </param>
        void FireBullet(Label bullet, int direction)
        {
            if (direction == -1 || direction == 1)
            {
                bullet.Location = new Point(bullet.Location.X, bullet.Location.Y - bulletSpeed * direction);
            }
            else throw new Exception("Bullet is bi-directional, it takes direction = -1 or 1.");
        }

        void FireBullet(Label bullet, int direction, double speedMultiplier)
        {
            if (direction == -1 || direction == 1)
            {
                bullet.Location = new Point(bullet.Location.X, bullet.Location.Y - (int)(bulletSpeed * direction * speedMultiplier));
            }
            else throw new Exception("Bullet is bi-directional, it takes direction = -1 or 1.");
        }

        /// <summary>
        /// Remove bullet from list and controls to avoid extra unnecessary memory usage
        /// </summary>
        void clearBullet(Tuple<PictureBox,Label> bullet)
        {
            Controls.Remove(bullet.Item2);
            bullets.Remove(bullet);
        }
        void ClearAllBullets(List<Tuple<PictureBox, Label>> bullets){ foreach (var bullet in bullets.ToList()) clearBullet(bullet); }
        #endregion

    }
}
