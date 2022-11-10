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
    public partial class AssignControl : Form
    {
        string currentKey;
        public AssignControl(string CurrentKey)
        {
            currentKey = CurrentKey;
            InitializeComponent();
            label2.Text = currentKey;
            label1.Text = "Press key to assign";
        }

        private void AssignControl_KeyDown(object sender, KeyEventArgs e)
        {
            //Add to controls.json the new key value and close this form.
        }
    }
}
