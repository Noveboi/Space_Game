namespace Space_Game
{
    partial class Results
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Results));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.complimentLabel = new System.Windows.Forms.Label();
            this.scoreResult = new System.Windows.Forms.Label();
            this.timeResult = new System.Windows.Forms.Label();
            this.rankingResult = new System.Windows.Forms.Label();
            this.backLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(132, 363);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(0, 0);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // complimentLabel
            // 
            this.complimentLabel.BackColor = System.Drawing.Color.Transparent;
            this.complimentLabel.Font = new System.Drawing.Font("Niagara Engraved", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.complimentLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(121)))), ((int)(((byte)(91)))));
            this.complimentLabel.Location = new System.Drawing.Point(12, 9);
            this.complimentLabel.Name = "complimentLabel";
            this.complimentLabel.Size = new System.Drawing.Size(560, 96);
            this.complimentLabel.TabIndex = 1;
            this.complimentLabel.Text = "You did ____!";
            this.complimentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scoreResult
            // 
            this.scoreResult.BackColor = System.Drawing.Color.Transparent;
            this.scoreResult.Font = new System.Drawing.Font("Niagara Solid", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreResult.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.scoreResult.Location = new System.Drawing.Point(12, 105);
            this.scoreResult.Name = "scoreResult";
            this.scoreResult.Size = new System.Drawing.Size(560, 96);
            this.scoreResult.TabIndex = 2;
            this.scoreResult.Text = "Score:";
            this.scoreResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timeResult
            // 
            this.timeResult.BackColor = System.Drawing.Color.Transparent;
            this.timeResult.Font = new System.Drawing.Font("Niagara Solid", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeResult.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.timeResult.Location = new System.Drawing.Point(12, 201);
            this.timeResult.Name = "timeResult";
            this.timeResult.Size = new System.Drawing.Size(560, 96);
            this.timeResult.TabIndex = 3;
            this.timeResult.Text = "Game Time:";
            this.timeResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rankingResult
            // 
            this.rankingResult.BackColor = System.Drawing.Color.Transparent;
            this.rankingResult.Font = new System.Drawing.Font("Niagara Engraved", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rankingResult.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rankingResult.Location = new System.Drawing.Point(12, 297);
            this.rankingResult.Name = "rankingResult";
            this.rankingResult.Size = new System.Drawing.Size(560, 96);
            this.rankingResult.TabIndex = 4;
            this.rankingResult.Text = "You placed __th in your score rankings!";
            this.rankingResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // backLabel
            // 
            this.backLabel.BackColor = System.Drawing.Color.Transparent;
            this.backLabel.Font = new System.Drawing.Font("Niagara Solid", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.backLabel.Location = new System.Drawing.Point(12, 393);
            this.backLabel.Name = "backLabel";
            this.backLabel.Size = new System.Drawing.Size(560, 147);
            this.backLabel.TabIndex = 5;
            this.backLabel.Text = "Back to menu";
            this.backLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.backLabel.Click += new System.EventHandler(this.backLabel_Click);
            this.backLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.backLabel_MouseMove);
            // 
            // Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumPurple;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.backLabel);
            this.Controls.Add(this.rankingResult);
            this.Controls.Add(this.timeResult);
            this.Controls.Add(this.scoreResult);
            this.Controls.Add(this.complimentLabel);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Results";
            this.Text = "Results";
            this.Load += new System.EventHandler(this.Results_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label complimentLabel;
        private System.Windows.Forms.Label scoreResult;
        private System.Windows.Forms.Label timeResult;
        private System.Windows.Forms.Label rankingResult;
        private System.Windows.Forms.Label backLabel;
    }
}