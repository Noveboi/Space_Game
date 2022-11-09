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
        private const int gameTime = 20; //In seconds
        private int enemyDifficulty = 2; // 1 - ok | 2 - hard

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
        private bool gamePaused = false;
        private bool hasBegun = false;

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
        //private double sweepProbability = 0.005;

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
        private bool firstOpen = true;
        Logger logger;

        #endregion

        #region Game Methods and Events
        private void Game_Deactivate(object sender, EventArgs e) { Cursor.Show(); }
        public Game()
        {
            InitializeComponent();
            playerMovementTimer.Tick += PlayerMovementTimer_Tick;
            bulletTimer.Tick += BulletTimer_Tick;
            gameTimer.Tick += GameTimer_Tick;
            enemyMovementTimer.Tick += EnemyMovementTimer_Tick;
            onOpenTimer.Tick += OnOpenTimer_Tick;
            starAnimateTimer.Tick += StarAnimateTimer_Tick;


            if (enemyDifficulty == 2) enemyMovementTimer.Interval = 40;

            Size = new Size(1280, 720);
            //Add in future (maybe), adjustable size with locked aspect ratio of 16:9

            if (haveLog)
            {
                logger = new Logger(this);
                logger.logBox.Text = "";
                logger.posBox.Text = "";
                logger.moveBox.Text = "";
            }

            announceLabel.Size = new Size(Width + 20, Height - 50);
        }

        /// <summary>
        /// Do a simple countdown and print the corresponding announceText through the announceLabel
        /// </summary>
        /// <param name="annouceText">Text to be displayed after countdown ends</param>
        /// <param name="countFrom">Number to count down from</param>
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
        /// <summary>
        /// Simple 3,2,1 countdown without announceText
        /// </summary>
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
            gamePaused = true;
        }
        void UnpauseGame()
        {
            bulletTimer.Start();
            enemyMovementTimer.Start();
            gameTimer.Start();
            starAnimateTimer.Start();
            gamePaused = false;
        }

        #region Star Creation
        Color randomBrightness()
        {
            int b = (RandomNumberGenerator.Create().GetHashCode() % 55) + 200;
            return Color.FromArgb(255, b, b, b);
        }
        Point randomLocation(int yOffset)
        {
            int x = RandomNumberGenerator.Create().GetHashCode() % Width;
            int y = RandomNumberGenerator.Create().GetHashCode() % Height;
            return new Point(x, y + yOffset);
        }

        Size randomSize()
        {
            int s = (RandomNumberGenerator.Create().GetHashCode() % 3) + 3;
            return new Size(s, s);
        }
        void CreateStars(int amount, int yOffset)
        {
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
            CreateStars(30,0);
            Focus();
            Cursor.Hide();

            onOpenTimer.Start();

            enemy.Location = new Point(Width/2 - enemy.Width/2, enemy.Location.Y);
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && elapsedSeconds <= gameTime)
            {
                MessageBox.Show("This match will not be recorded.", "Are you sure?", MessageBoxButtons.YesNo);
            }
        }
        #endregion

        #region Timers
        /// <summary>
        /// Every star label is translated a certain Y amount for each timer tick
        /// </summary>
        private void StarAnimateTimer_Tick(object sender, EventArgs e)
        {
            //every 50th counter tick, create new stars that spawn from the non-visible bottom part of the window
            //this is to maintain a certain amount of stars flying in the background
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
            //(% n) = reset counter every n ticks
            counter = (counter + 1) % 150;
            
        }
        private void EnemyMovementTimer_Tick(object sender, EventArgs e)
        {
            string time = getTime();
            logger.posBox.AppendText($"{time} - Enemy: {enemy.Location} {Environment.NewLine}            " +
                $"Player: {p.Location} {Environment.NewLine}");

            int maxRand = sweepProbability != 0 ? (int)(1/sweepProbability) : 400;
            //Random decision
            int dec = RandomNumberGenerator.Create().GetHashCode() % (maxRand + 1);

            //Condition to begin sweep
            if (dec == maxRand && !startSweep && sweepProbability != 0) startSweep = true;
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
        /// <summary>
        /// Keeps time of seconds passed since Game start, end is responsible for ending the game once 
        /// the specificied amount of gameTime has passed
        /// </summary>
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //Start countdown 3 seconds before GAME OVER
            if(elapsedSeconds >= gameTime - 3 && elapsedSeconds <= gameTime - 1) 
            {
                if (elapsedSeconds == gameTime - 3) countdown = 3;
                doCountdown();
            }

            //Pause the game and strip all control from the player
            if (elapsedSeconds > gameTime - 1)
            {
                PauseGame();
                gameTimer.Start();
                ClearAllBullets(bullets);
                announceLabel.Text = "GAME OVER!";
                Controls.Add(announceLabel);
            }

            //Hide the game form and show the results of the match
            if(elapsedSeconds > gameTime + 2)
            {
                gameTimer.Stop();
                Results results = new Results(score,gameTime,enemyDifficulty);
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
            //Begin timers and initiate countdown to begin the match
            if (countdown == -1)
            {
                onOpenTimer.Stop();
                gameTimer.Start();
                enemyMovementTimer.Start();
                starAnimateTimer.Start();
                hasBegun = true;
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
                        //logger.logBox.AppendText($"{getTime()} - Bullet Fired at " +
                        //    $"{new Point(p.Location.X + vehicleSize.Width / 2 - 5,p.Location.Y)}{Environment.NewLine}");
                        break;
                }
            }
            if (e.KeyCode == Keys.Escape && hasBegun)
            {
                if (gameRunning) 
                {
                    PauseGame();
                    announceLabel.Text = "GAME PAUSED";
                    Controls.Add(announceLabel);
                    Cursor.Show();
                }
                else
                {
                    Cursor.Hide();
                    UnpauseGame();
                    Controls.Remove(announceLabel);
                };  
            }
            if (e.KeyCode == Keys.P)
            {
                if (!logger.Visible) { logger.Show(); Cursor.Show(); }
                else { logger.Hide(); Cursor.Hide(); };
                if (gameRunning && firstOpen) { Focus(); firstOpen = false; }
                if(gameRunning && !firstOpen) logger.Show(); 
            }
            if (e.KeyCode == Keys.E && gamePaused) Close();

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

        private int yReduction = 10; //reduce the y amount traversed each tick

        void MoveUp(PictureBox p)
        {
            if (p.Location.Y > Height / 2 - 100)
            { p.Location = new Point(p.Location.X, p.Location.Y - (vehicleSpeed - yReduction)); }
        }
        void MoveLeft(PictureBox p)
        {
            if (p.Location.X > 40)
            { p.Location = new Point(p.Location.X - vehicleSpeed, p.Location.Y); }
        }
        void MoveDown(PictureBox p)
        {
            if (p.Location.Y < Height - (vehicleSize.Height + vehicleSpeed + 20))
            { p.Location = new Point(p.Location.X, p.Location.Y + (vehicleSpeed - yReduction)); }
        }
        void MoveRight(PictureBox p)
        {
            if (p.Location.X < Width - (vehicleSize.Width + vehicleSpeed + 40))
            { p.Location = new Point(p.Location.X + vehicleSpeed, p.Location.Y); }
        }

        //Overload left and right for enemy
        void MoveLeft(PictureBox p, double moveScalar)
        {
            if (p.Location.X > 40)
            { 
                p.Location = new Point(p.Location.X - (int)(vehicleSpeed * moveScalar), p.Location.Y);
                //logger.logBox.AppendText($"Move Left ({-(int)(vehicleSpeed * moveScalar)}px)"+Environment.NewLine);
            }
        }

        void MoveRight(PictureBox p, double moveScalar)
        {
            if (p.Location.X < Width - (vehicleSize.Width + vehicleSpeed + 40))
            { 
                p.Location = new Point(p.Location.X + (int)(vehicleSpeed * moveScalar), p.Location.Y);
                //logger.logBox.AppendText($"Move Right ({(int)(vehicleSpeed * moveScalar)}px)"+Environment.NewLine);
            }
        }

        /// <summary>
        /// Move the pictureBox according to movement bool values
        /// </summary>
        //This is where the movement actually occurs
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
            WatchForBullets();
            if (move == 0)
            {
                //Read WeightedMove() summary for more details on these 2 variables
                int directionDecision = RandomNumberGenerator.Create().GetHashCode() % 50;
                double moveWeight = (double)enemy.Location.X / (double)Width * 2 + 2;

                if(enemyDifficulty == 1) WeightedMove(moveWeight, 0, directionDecision); 
                if(enemyDifficulty == 2)
                {
                    //posDifference < 0: enemy is to the right of the player (enemy.X < p.X)
                    //posDifference > 0: enemy is to the left of the player (enemy.X > p.X)
                    int posDifference = enemy.Location.X - p.Location.X;
                    //convert posDifference into the direction weight to be passed to WeightedMove()
                    double directionWeight = (double)posDifference / Width;

                    //logger.logBox.AppendText($"pD: {posDifference} | dW: {directionWeight}");

                    WeightedMove(moveWeight, directionWeight, directionDecision);
                }

                logger.moveBox.AppendText($"Move Sporadic!" + Environment.NewLine);
            } 
            else
            {
                logger.moveBox.AppendText("No Move!" + Environment.NewLine);
            }
        }

        /// <summary>
        /// Generally used for both enemyDifficulties. For difficulty = 1, directionWeight remains constant at 1,
        /// otherwise, directionWeight is shifted according to relative player and enemy locations.
        /// </summary>
        /// <param name="moveWeight">Varies the "speed" of each movement</param>
        /// <param name="directionWeight">
        /// This is a function of posDifference (posDifference divided by Width).
        /// it provides info about which direction to move and how fast to do so. (set to 0 for random movements)
        /// -------
        /// -1: MoveRight() 49/50 times, 
        /// from -1 to 0: MoveRight() more often, 
        /// 0: equal probability of MoveRight() and MoveLeft(), 
        /// from 0 to 1: MoveLeft() more often,
        /// 1: MoveLeft() 49/50 times.
        /// </param>
        /// <param name="directionDecision">Decision making variable taking values from 0 to 50</param>
        void WeightedMove(double moveWeight, double directionWeight, int directionDecision)
        {
            //finalWeight is a function of directionWeight which is a function of posDifference, therefore
            //finalWeight is a fucntion of posDifference.
            //it influences enemy movement to guide enemy towards the player's position
            //The reasoning for its definition (formula):
            // -> 25 (constant): this is here to act as a "middle ground" between values 0 and 50
            //    since directionWeight takes values from -1 to 1.
            // -> 24 * CubeRoot(directionWeight): this seems convoluted but is very simple in terms of 
            //    its functionality: 24 * directionWeight gives finalWeight its range [0,50], but 
            //    24 * directionWeight is a linear function and it negatively impacts the enemy's decision making 
            //    in terms of whether to MoveRight or MoveLeft. The negative impact is that the enemy will tend to
            //    'hover' AROUND the player but not go ABOVE the player, which leads to the enemy missing constantly
            //    This happens because for relatively high values of posDifference (e.g 400, -200),
            //    the finalWeight will still be close to 25 and thus the enemy will not purposefully move towards the player.
            //    To counter that we add CubeRoot, where those high posDifference values will produce finalWeight values that
            //    are rapidly divergent from 25.
            //
            // -> In the Details folder of this project you can see the graphs of both a linear finalWeight function and
            //    a cubic one, along with some calculated values so you can clearly see the difference!
            double finalWeight = 25 + 24 *(new MyMath().CubeRoot(directionWeight)); //takes values from 1 to 49 (both ends inclusive)

            //logger.logBox.AppendText($" | fW = {finalWeight}"+Environment.NewLine);
            //for enemyDifficulty == 1, move around randomly
            if (directionDecision >= 0 && directionDecision < finalWeight && enemyDifficulty == 1) MoveLeft(enemy, moveWeight);
            //for enemyDifficulty == 2, follow player and continue moving sporadically while doing so.
            if (directionDecision >= finalWeight && directionDecision <= 50 && enemyDifficulty == 1) MoveRight(enemy, 6 - moveWeight);

            //In both Move functions we use a weird looking moveScalar. 
            //It's very simple as it simply states in a mathematical fashion: Move slower as you get closer to the player!
            // This will increase shot accuracy and still have a bit a randomness left, thanks to the + 1 at the end 
            // Math.Abs is used so as to not move in the opposite direction than intended
            if (directionDecision >= 0 && directionDecision < finalWeight && enemyDifficulty == 2)
            {
                MoveLeft(enemy, moveWeight * Math.Abs(directionWeight*5) + 1);
            }
            if (directionDecision >= finalWeight && directionDecision <= 50 && enemyDifficulty == 2)
            {
                MoveRight(enemy, (6 - moveWeight)* Math.Abs(directionWeight*5) + 1);
            }
        }

        /// <summary>
        /// Mechanism that enables the enemy to dodge incoming bullets
        /// </summary>
        void WatchForBullets()
        {
            foreach(var bullet in bullets.ToList())
            {
                //execute code only if bullet fired belongs to player
                if (bullet.Item1 == p)
                {
                    //bullet is to the right of the enemy

                    bool bulletCloseToEnemyRight = (bullet.Item2.Location.X + bullet.Item2.Width/2) - (enemy.Location.X + enemy.Width/2) < 140 
                        && (bullet.Item2.Location.X + bullet.Item2.Width / 2) - (enemy.Location.X + enemy.Width / 2) >= 0
                        && Math.Abs(bullet.Item2.Location.Y - (enemy.Location.Y + enemy.Height)) < bulletSpeed * 3;
                    //bullet is to the left of the enemy
                    bool bulletCloseToEnemyLeft = (bullet.Item2.Location.X + bullet.Item2.Width / 2) - (enemy.Location.X + enemy.Width / 2) > -140
                        && (bullet.Item2.Location.X + bullet.Item2.Width / 2) - (enemy.Location.X + enemy.Width / 2)  <= 0
                        && Math.Abs(bullet.Item2.Location.Y - (enemy.Location.Y + enemy.Height)) < bulletSpeed * 3;

                    if (bulletCloseToEnemyRight) { MoveLeft(enemy, 2); logger.logBox.AppendText($"Bullet to the right! {(bullet.Item2.Location.X + bullet.Item2.Width / 2) - (enemy.Location.X + enemy.Width / 2)}"+Environment.NewLine); }
                    if (bulletCloseToEnemyLeft) { MoveRight(enemy, 2); logger.logBox.AppendText($"Bullet to the left! {(bullet.Item2.Location.X + bullet.Item2.Width / 2) - (enemy.Location.X + enemy.Width / 2)}"+Environment.NewLine); }
                }
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
            bool reachedRight = enemy.Location.X >= Width - enemy.Width - 70;

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
        /// Move the bullet object vertically through space.
        /// This method is triggered when bulletTimer starts.
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
