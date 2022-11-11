namespace Space_Game
{
    partial class Options
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
            this.label3 = new System.Windows.Forms.Label();
            this.colorLabel = new System.Windows.Forms.Label();
            this.enemyLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorPick = new System.Windows.Forms.Button();
            this.gameTimeLabel = new System.Windows.Forms.Label();
            this.diffLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Niagara Solid", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(499, 447);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 50);
            this.label3.TabIndex = 5;
            this.label3.Text = "Help!";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            this.label3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label3_MouseMove);
            // 
            // colorLabel
            // 
            this.colorLabel.BackColor = System.Drawing.Color.Transparent;
            this.colorLabel.Font = new System.Drawing.Font("Niagara Solid", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.colorLabel.Location = new System.Drawing.Point(80, 80);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(367, 85);
            this.colorLabel.TabIndex = 6;
            this.colorLabel.Text = "Bullet Color:";
            // 
            // enemyLabel
            // 
            this.enemyLabel.BackColor = System.Drawing.Color.Transparent;
            this.enemyLabel.Font = new System.Drawing.Font("Niagara Solid", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enemyLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.enemyLabel.Location = new System.Drawing.Point(80, 180);
            this.enemyLabel.Name = "enemyLabel";
            this.enemyLabel.Size = new System.Drawing.Size(333, 85);
            this.enemyLabel.TabIndex = 7;
            this.enemyLabel.Text = "Enemy Player:";
            this.enemyLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.BackColor = System.Drawing.Color.Transparent;
            this.timeLabel.Font = new System.Drawing.Font("Niagara Solid", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.timeLabel.Location = new System.Drawing.Point(80, 280);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(333, 85);
            this.timeLabel.TabIndex = 8;
            this.timeLabel.Text = "Game Time:";
            // 
            // colorDialog1
            // 
            this.colorDialog1.Color = System.Drawing.Color.DarkGray;
            // 
            // colorPick
            // 
            this.colorPick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.colorPick.FlatAppearance.BorderSize = 0;
            this.colorPick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorPick.ForeColor = System.Drawing.Color.Chocolate;
            this.colorPick.Location = new System.Drawing.Point(434, 99);
            this.colorPick.Name = "colorPick";
            this.colorPick.Size = new System.Drawing.Size(100, 50);
            this.colorPick.TabIndex = 12;
            this.colorPick.UseVisualStyleBackColor = false;
            this.colorPick.Click += new System.EventHandler(this.colorPick_Click);
            this.colorPick.MouseDown += new System.Windows.Forms.MouseEventHandler(this.colorPick_MouseDown);
            // 
            // gameTimeLabel
            // 
            this.gameTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.gameTimeLabel.Font = new System.Drawing.Font("Niagara Solid", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameTimeLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.gameTimeLabel.Location = new System.Drawing.Point(419, 280);
            this.gameTimeLabel.Name = "gameTimeLabel";
            this.gameTimeLabel.Size = new System.Drawing.Size(156, 85);
            this.gameTimeLabel.TabIndex = 13;
            this.gameTimeLabel.Text = "60s";
            this.gameTimeLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gameTimeLabel_MouseDown);
            // 
            // diffLabel
            // 
            this.diffLabel.BackColor = System.Drawing.Color.Transparent;
            this.diffLabel.Font = new System.Drawing.Font("Niagara Solid", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diffLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.diffLabel.Location = new System.Drawing.Point(419, 180);
            this.diffLabel.Name = "diffLabel";
            this.diffLabel.Size = new System.Drawing.Size(156, 85);
            this.diffLabel.TabIndex = 14;
            this.diffLabel.Text = "Easy";
            this.diffLabel.Click += new System.EventHandler(this.diffLabel_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1045, 506);
            this.Controls.Add(this.diffLabel);
            this.Controls.Add(this.gameTimeLabel);
            this.Controls.Add(this.colorPick);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.enemyLabel);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.label3);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.Label enemyLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button colorPick;
        private System.Windows.Forms.Label gameTimeLabel;
        private System.Windows.Forms.Label diffLabel;
    }
}