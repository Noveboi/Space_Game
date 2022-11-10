namespace Space_Game
{
    partial class Help
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
            this.controlsLabel = new System.Windows.Forms.Label();
            this.controlLabel1 = new System.Windows.Forms.Label();
            this.controlLabel3 = new System.Windows.Forms.Label();
            this.controlLabel2 = new System.Windows.Forms.Label();
            this.controlLabel4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // controlsLabel
            // 
            this.controlsLabel.AutoSize = true;
            this.controlsLabel.Font = new System.Drawing.Font("Niagara Solid", 48F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlsLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.controlsLabel.Location = new System.Drawing.Point(107, 9);
            this.controlsLabel.Name = "controlsLabel";
            this.controlsLabel.Size = new System.Drawing.Size(194, 85);
            this.controlsLabel.TabIndex = 0;
            this.controlsLabel.Text = "Controls";
            // 
            // controlLabel1
            // 
            this.controlLabel1.AutoSize = true;
            this.controlLabel1.Font = new System.Drawing.Font("Niagara Solid", 28F);
            this.controlLabel1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.controlLabel1.Location = new System.Drawing.Point(12, 94);
            this.controlLabel1.Name = "controlLabel1";
            this.controlLabel1.Size = new System.Drawing.Size(193, 50);
            this.controlLabel1.TabIndex = 1;
            this.controlLabel1.Text = "W, A, S, D - Move";
            // 
            // controlLabel3
            // 
            this.controlLabel3.AutoSize = true;
            this.controlLabel3.Font = new System.Drawing.Font("Niagara Solid", 28F);
            this.controlLabel3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.controlLabel3.Location = new System.Drawing.Point(12, 194);
            this.controlLabel3.Name = "controlLabel3";
            this.controlLabel3.Size = new System.Drawing.Size(248, 50);
            this.controlLabel3.TabIndex = 2;
            this.controlLabel3.Text = "Esc - (Un)Pause Game";
            // 
            // controlLabel2
            // 
            this.controlLabel2.AutoSize = true;
            this.controlLabel2.Font = new System.Drawing.Font("Niagara Solid", 28F);
            this.controlLabel2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.controlLabel2.Location = new System.Drawing.Point(12, 144);
            this.controlLabel2.Name = "controlLabel2";
            this.controlLabel2.Size = new System.Drawing.Size(164, 50);
            this.controlLabel2.TabIndex = 3;
            this.controlLabel2.Text = "Space - Shoot";
            // 
            // controlLabel4
            // 
            this.controlLabel4.AutoSize = true;
            this.controlLabel4.Font = new System.Drawing.Font("Niagara Solid", 28F);
            this.controlLabel4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.controlLabel4.Location = new System.Drawing.Point(12, 244);
            this.controlLabel4.Name = "controlLabel4";
            this.controlLabel4.Size = new System.Drawing.Size(320, 50);
            this.controlLabel4.TabIndex = 4;
            this.controlLabel4.Text = "E - Exit Game (when Paused)";
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(432, 653);
            this.Controls.Add(this.controlLabel4);
            this.Controls.Add(this.controlLabel2);
            this.Controls.Add(this.controlLabel3);
            this.Controls.Add(this.controlLabel1);
            this.Controls.Add(this.controlsLabel);
            this.Name = "Help";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "7";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label controlsLabel;
        private System.Windows.Forms.Label controlLabel1;
        private System.Windows.Forms.Label controlLabel3;
        private System.Windows.Forms.Label controlLabel2;
        private System.Windows.Forms.Label controlLabel4;
    }
}