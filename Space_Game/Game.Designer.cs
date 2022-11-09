namespace Space_Game
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.p = new System.Windows.Forms.PictureBox();
            this.timeLabel = new System.Windows.Forms.Label();
            this.enemy = new System.Windows.Forms.PictureBox();
            this.scoreLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.p)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy)).BeginInit();
            this.SuspendLayout();
            // 
            // p
            // 
            this.p.BackColor = System.Drawing.Color.Transparent;
            this.p.Image = ((System.Drawing.Image)(resources.GetObject("p.Image")));
            this.p.Location = new System.Drawing.Point(753, 618);
            this.p.Margin = new System.Windows.Forms.Padding(4);
            this.p.Name = "p";
            this.p.Size = new System.Drawing.Size(200, 206);
            this.p.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.p.TabIndex = 0;
            this.p.TabStop = false;
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.BackColor = System.Drawing.Color.Transparent;
            this.timeLabel.Font = new System.Drawing.Font("Niagara Solid", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.timeLabel.Location = new System.Drawing.Point(16, 700);
            this.timeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(209, 128);
            this.timeLabel.TabIndex = 1;
            this.timeLabel.Text = "timer";
            // 
            // enemy
            // 
            this.enemy.BackColor = System.Drawing.Color.Transparent;
            this.enemy.Image = ((System.Drawing.Image)(resources.GetObject("enemy.Image")));
            this.enemy.Location = new System.Drawing.Point(753, 15);
            this.enemy.Margin = new System.Windows.Forms.Padding(4);
            this.enemy.Name = "enemy";
            this.enemy.Size = new System.Drawing.Size(200, 137);
            this.enemy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.enemy.TabIndex = 2;
            this.enemy.TabStop = false;
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.BackColor = System.Drawing.Color.Transparent;
            this.scoreLabel.Font = new System.Drawing.Font("Niagara Solid", 128.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.scoreLabel.Location = new System.Drawing.Point(1435, 602);
            this.scoreLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(219, 228);
            this.scoreLabel.TabIndex = 3;
            this.scoreLabel.Text = "SC";
            this.scoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1685, 838);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.enemy);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.p);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game";
            this.Deactivate += new System.EventHandler(this.Game_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Game_FormClosing);
            this.Load += new System.EventHandler(this.Game_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Game_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.p)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox p;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.PictureBox enemy;
        private System.Windows.Forms.Label scoreLabel;
    }
}