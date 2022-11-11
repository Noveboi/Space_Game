namespace Space_Game
{
    partial class ColorPrompt
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
            this.label1 = new System.Windows.Forms.Label();
            this.rgbLabel = new System.Windows.Forms.Label();
            this.bullet = new System.Windows.Forms.Button();
            this.hexLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Niagara Solid", 36F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 86);
            this.label1.TabIndex = 0;
            this.label1.Text = "Color Your Bullet!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rgbLabel
            // 
            this.rgbLabel.Font = new System.Drawing.Font("Niagara Solid", 24F);
            this.rgbLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rgbLabel.Location = new System.Drawing.Point(12, 95);
            this.rgbLabel.Name = "rgbLabel";
            this.rgbLabel.Size = new System.Drawing.Size(358, 53);
            this.rgbLabel.TabIndex = 1;
            this.rgbLabel.Text = "RGB";
            this.rgbLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bullet
            // 
            this.bullet.BackColor = System.Drawing.Color.White;
            this.bullet.FlatAppearance.BorderSize = 0;
            this.bullet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bullet.Location = new System.Drawing.Point(353, 268);
            this.bullet.Name = "bullet";
            this.bullet.Size = new System.Drawing.Size(17, 73);
            this.bullet.TabIndex = 2;
            this.bullet.UseVisualStyleBackColor = false;
            // 
            // hexLabel
            // 
            this.hexLabel.Font = new System.Drawing.Font("Niagara Solid", 24F);
            this.hexLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.hexLabel.Location = new System.Drawing.Point(12, 189);
            this.hexLabel.Name = "hexLabel";
            this.hexLabel.Size = new System.Drawing.Size(358, 53);
            this.hexLabel.TabIndex = 3;
            this.hexLabel.Text = "HEX";
            this.hexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Niagara Solid", 16F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(47, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(300, 30);
            this.label2.TabIndex = 4;
            this.label2.Text = "This is your bullet ->";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ColorPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(382, 353);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hexLabel);
            this.Controls.Add(this.bullet);
            this.Controls.Add(this.rgbLabel);
            this.Controls.Add(this.label1);
            this.Name = "ColorPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ColorPrompt";
            this.Load += new System.EventHandler(this.ColorPrompt_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label rgbLabel;
        private System.Windows.Forms.Button bullet;
        private System.Windows.Forms.Label hexLabel;
        private System.Windows.Forms.Label label2;
    }
}