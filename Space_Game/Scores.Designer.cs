namespace Space_Game
{
    partial class Scores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scores));
            this.bg = new System.Windows.Forms.PictureBox();
            this.title = new System.Windows.Forms.Label();
            this.numLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.vertSeparator1 = new System.Windows.Forms.Label();
            this.vertSeparator2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bg)).BeginInit();
            this.SuspendLayout();
            // 
            // bg
            // 
            this.bg.Image = ((System.Drawing.Image)(resources.GetObject("bg.Image")));
            this.bg.Location = new System.Drawing.Point(173, 471);
            this.bg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bg.Name = "bg";
            this.bg.Size = new System.Drawing.Size(0, 0);
            this.bg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bg.TabIndex = 0;
            this.bg.TabStop = false;
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Niagara Engraved", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(517, 135);
            this.title.TabIndex = 1;
            this.title.Text = "SCORES";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numLabel
            // 
            this.numLabel.BackColor = System.Drawing.Color.Transparent;
            this.numLabel.Font = new System.Drawing.Font("Niagara Solid", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.numLabel.Location = new System.Drawing.Point(16, 135);
            this.numLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numLabel.Name = "numLabel";
            this.numLabel.Size = new System.Drawing.Size(80, 74);
            this.numLabel.TabIndex = 2;
            this.numLabel.Text = "#";
            this.numLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scoreLabel
            // 
            this.scoreLabel.BackColor = System.Drawing.Color.Transparent;
            this.scoreLabel.Font = new System.Drawing.Font("Niagara Solid", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.scoreLabel.Location = new System.Drawing.Point(96, 135);
            this.scoreLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(211, 74);
            this.scoreLabel.TabIndex = 3;
            this.scoreLabel.Text = "Score";
            this.scoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateLabel
            // 
            this.dateLabel.BackColor = System.Drawing.Color.Transparent;
            this.dateLabel.Font = new System.Drawing.Font("Niagara Solid", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.dateLabel.Location = new System.Drawing.Point(307, 135);
            this.dateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(211, 74);
            this.dateLabel.TabIndex = 4;
            this.dateLabel.Text = "Date";
            this.dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // vertSeparator1
            // 
            this.vertSeparator1.BackColor = System.Drawing.Color.MidnightBlue;
            this.vertSeparator1.Location = new System.Drawing.Point(96, 135);
            this.vertSeparator1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.vertSeparator1.Name = "vertSeparator1";
            this.vertSeparator1.Size = new System.Drawing.Size(3, 542);
            this.vertSeparator1.TabIndex = 5;
            // 
            // vertSeparator2
            // 
            this.vertSeparator2.BackColor = System.Drawing.Color.MidnightBlue;
            this.vertSeparator2.Location = new System.Drawing.Point(307, 135);
            this.vertSeparator2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.vertSeparator2.Name = "vertSeparator2";
            this.vertSeparator2.Size = new System.Drawing.Size(3, 542);
            this.vertSeparator2.TabIndex = 6;
            // 
            // Scores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumPurple;
            this.ClientSize = new System.Drawing.Size(520, 690);
            this.Controls.Add(this.vertSeparator2);
            this.Controls.Add(this.vertSeparator1);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.numLabel);
            this.Controls.Add(this.title);
            this.Controls.Add(this.bg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Scores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scores";
            this.Load += new System.EventHandler(this.Scores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox bg;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label numLabel;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.Label vertSeparator1;
        private System.Windows.Forms.Label vertSeparator2;
    }
}