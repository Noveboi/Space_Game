namespace Space_Game
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.title = new System.Windows.Forms.Label();
            this.playText = new System.Windows.Forms.Label();
            this.optionsText = new System.Windows.Forms.Label();
            this.scoresText = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Niagara Engraved", 86F);
            this.title.ForeColor = System.Drawing.SystemColors.Control;
            this.title.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.title.Location = new System.Drawing.Point(21, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(767, 132);
            this.title.TabIndex = 0;
            this.title.Text = "SPACE!";
            this.title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // playText
            // 
            this.playText.BackColor = System.Drawing.Color.Transparent;
            this.playText.Font = new System.Drawing.Font("Niagara Solid", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playText.ForeColor = System.Drawing.SystemColors.Control;
            this.playText.Location = new System.Drawing.Point(288, 164);
            this.playText.Name = "playText";
            this.playText.Size = new System.Drawing.Size(220, 64);
            this.playText.TabIndex = 1;
            this.playText.Text = "PLAY";
            this.playText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.playText.Click += new System.EventHandler(this.play_Click);
            this.playText.MouseEnter += new System.EventHandler(this.changeCursor);
            // 
            // optionsText
            // 
            this.optionsText.BackColor = System.Drawing.Color.Transparent;
            this.optionsText.Font = new System.Drawing.Font("Niagara Solid", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionsText.ForeColor = System.Drawing.SystemColors.Control;
            this.optionsText.Location = new System.Drawing.Point(297, 243);
            this.optionsText.Name = "optionsText";
            this.optionsText.Size = new System.Drawing.Size(211, 64);
            this.optionsText.TabIndex = 2;
            this.optionsText.Text = "OPTIONS";
            this.optionsText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optionsText.MouseClick += new System.Windows.Forms.MouseEventHandler(this.optionsText_MouseClick);
            this.optionsText.MouseEnter += new System.EventHandler(this.changeCursor);
            // 
            // scoresText
            // 
            this.scoresText.BackColor = System.Drawing.Color.Transparent;
            this.scoresText.Font = new System.Drawing.Font("Niagara Solid", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoresText.ForeColor = System.Drawing.SystemColors.Control;
            this.scoresText.Location = new System.Drawing.Point(297, 326);
            this.scoresText.Name = "scoresText";
            this.scoresText.Size = new System.Drawing.Size(211, 64);
            this.scoresText.TabIndex = 3;
            this.scoresText.Text = "SCORES";
            this.scoresText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.scoresText.Click += new System.EventHandler(this.scoresText_Click);
            this.scoresText.MouseEnter += new System.EventHandler(this.changeCursor);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-1, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(0, 0);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.scoresText);
            this.Controls.Add(this.optionsText);
            this.Controls.Add(this.playText);
            this.Controls.Add(this.title);
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Space Pew Pew";
            this.Load += new System.EventHandler(this.Menu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label playText;
        private System.Windows.Forms.Label optionsText;
        private System.Windows.Forms.Label scoresText;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

