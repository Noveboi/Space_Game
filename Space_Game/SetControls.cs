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
    public partial class SetControls : Form
    {
        List<Tuple<Label, string>> controlFunctions = new List<Tuple<Label, string>>();
        UserControls userControls = new UserControls();
        KeysConverter kc = new KeysConverter();

        public SetControls()
        {
            userControls.GrabFromJson();

            InitializeComponent();
            controlLabel1.Location = new Point(controlLabel1.Location.X, controlLabel1.Location.Y + 20);
            OrganizeLabelGroup("controlLabel");
            controlFunctions.Add(new Tuple<Label, string>(controlLabel2, "Shoot"));
            controlFunctions.Add(new Tuple<Label, string>(controlLabel3, "PauseGame"));
            controlFunctions.Add(new Tuple<Label, string>(controlLabel4, "ExitGame"));

            UpdateLabels();

        }

        private int GetSharedNameLabelCount(string sharedName)
        {
            int count = 0;
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls.ContainsKey($"{sharedName}{i}")) count++;
            }
            return count;
        }
        private void OrganizeLabelGroup(string sharedName)
        {
            for (int i = 1; i <= GetSharedNameLabelCount(sharedName); i++)
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
            foreach(var controlFunction in controlFunctions)
            {
                if(controlFunction.Item1 == label)
                {
                    AssignControl asCtrl = new AssignControl(controlFunction.Item2);
                    asCtrl.Show();
                    asCtrl.FormClosed += (s,args) => 
                    { 
                        userControls.GrabFromJson();
                        UpdateLabels(); 
                    };
                    break;
                }
            }
            
        }

        private void UpdateLabels()
        {
            controlLabel2.Text = kc.ConvertToString(userControls.Shoot) + " | Shoot";
            controlLabel3.Text = kc.ConvertToString(userControls.PauseGame) + " | (Un)pause Game";
            controlLabel4.Text = kc.ConvertToString(userControls.ExitGame) + " | Exit Game (when Paused)";
        }
    }
}
