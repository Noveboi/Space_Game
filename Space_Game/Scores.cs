using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Game
{
   

    public partial class Scores : Form
    {

        private List<Label> createNumCol()
        {
            List<Label> labels = new List<Label>();
            for (int i = 0; i < 10; i++)
            {
                Label label = new Label();
                label.Name = $"no{i + 1}";
                label.AutoSize = false;
                label.Size = new Size(60, 30);
                label.Text = $"{i + 1}";
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.BackColor = Color.Transparent;
                label.ForeColor = i % 2 == 0 ? Color.White : Color.FromArgb(255, 255, 161, 209);
                label.Font = new Font("Niagara Solid", 22);
                label.Location = new Point(8, 170 + i * 37);
                labels.Add(label);
            }
            return labels;
        }
        public Scores()
        {
            InitializeComponent();

            bg.Size = new Size(Width, Height);
            bg.Location = new Point(0, 0);
        }

        private void Scores_Load(object sender, EventArgs e)
        {
            Controls.SetChildIndex(bg, -1);

            //For proper transparency
            title.Parent = bg;
            numLabel.Parent = bg;
            scoreLabel.Parent = bg;
            dateLabel.Parent = bg;

            foreach(var label in createNumCol())
            { 
                Controls.Add(label);
                Controls.SetChildIndex(label, 1); //Bring labels to front
                label.Parent = bg;
            }
        }
    }
}
