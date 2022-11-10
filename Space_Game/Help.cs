using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Effects;

namespace Space_Game
{
    public partial class Help : Form
    {
        List<Tuple<Label, string>> labelKeys = new List<Tuple<Label, string>>();
        public Help()
        {
            InitializeComponent();
            controlLabel1.Location = new Point(controlLabel1.Location.X, controlLabel1.Location.Y + 20);
            OrganizeLabelGroup("controlLabel");
            labelKeys.Add(new Tuple<Label, string>(controlLabel2, "Space"));
            labelKeys.Add(new Tuple<Label, string>(controlLabel3, "Esc"));
            labelKeys.Add(new Tuple<Label, string>(controlLabel2, "E"));
        }

        private void OrganizeLabelGroup(string sharedName)
        {
            for (int i = 1; i < Controls.OfType<Label>().Count() && Controls.ContainsKey($"{sharedName}{i}"); i++)
            { 
                

                Label label = (Label)Controls.Find($"{sharedName}{i}", true)[0];
                //assign click event to controlLabel
                label.Click += Label_Click;
                label.AutoSize = false;
                label.Size = new Size(Width, 50);
                if (i > 1)
                {
                    Label above = (Label)Controls.Find($"{sharedName}{i - 1}", true)[0];
                    label.Location = new Point(label.Location.X, above.Location.Y + above.Height);
                }
            }
        }
        private void Label_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            foreach(var labelKey in labelKeys)
            {
                if(labelKey.Item1 == label)
                {
                    new AssignControl(labelKey.Item2).Show();
                    break;
                }
            }
            
        }
    }
}
