namespace Space_Game
{
    partial class Logger
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
            this.logBox = new System.Windows.Forms.TextBox();
            this.moveBox = new System.Windows.Forms.TextBox();
            this.posBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // logBox
            // 
            this.logBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logBox.Location = new System.Drawing.Point(13, 12);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(307, 436);
            this.logBox.TabIndex = 0;
            this.logBox.Text = "Test text";
            // 
            // moveBox
            // 
            this.moveBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveBox.Location = new System.Drawing.Point(725, 13);
            this.moveBox.Multiline = true;
            this.moveBox.Name = "moveBox";
            this.moveBox.ReadOnly = true;
            this.moveBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.moveBox.Size = new System.Drawing.Size(347, 437);
            this.moveBox.TabIndex = 2;
            this.moveBox.Text = "Test text";
            // 
            // posBox
            // 
            this.posBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.posBox.Location = new System.Drawing.Point(326, 13);
            this.posBox.Multiline = true;
            this.posBox.Name = "posBox";
            this.posBox.ReadOnly = true;
            this.posBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.posBox.Size = new System.Drawing.Size(234, 436);
            this.posBox.TabIndex = 3;
            this.posBox.Text = "Test text";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 451);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 41);
            this.label1.TabIndex = 4;
            this.label1.Text = "Existing stars:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 492);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(308, 41);
            this.label2.TabIndex = 5;
            this.label2.Text = "Existing bullets:";
            // 
            // Logger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 621);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.posBox);
            this.Controls.Add(this.moveBox);
            this.Controls.Add(this.logBox);
            this.Name = "Logger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Logger_FormClosing);
            this.Resize += new System.EventHandler(this.Logger_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox logBox;
        public System.Windows.Forms.TextBox moveBox;
        public System.Windows.Forms.TextBox posBox;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
    }
}