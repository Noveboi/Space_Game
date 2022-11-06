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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Options_Load(object sender, EventArgs e)
        {
            customCheckBox c1 = new customCheckBox(50, checkBox1.Location);
            c1.Show();
            checkBox1.Hide();
            customCheckBox c2 = new customCheckBox(50, checkBox2.Location);
            c2.Show();
            checkBox2.Hide();
        }
    }

    #region Custom CheckBox
    /// <summary>
    /// Overwrite default checkBox graphics,
    /// Reference: https://stackoverflow.com/questions/3166244/how-to-increase-the-size-of-checkbox-in-winforms
    /// </summary>
    class customCheckBox : CheckBox
    {
        private int size;
        //Constructor params will take values of the checkBox to-be-changed
        public customCheckBox(int boxSize, Point boxLocation)
        {
            Text = "asdasd";
            size = boxSize;
            Location = boxLocation;
        }

        public override bool AutoSize 
        {
            set { base.AutoSize = false; }
            get { return base.AutoSize; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Width = 100;
            Height = 100;
            Rectangle rectangle = new Rectangle(new Point(0,0), new Size(size, size));

            ControlPaint.DrawCheckBox(e.Graphics, rectangle, Checked ? ButtonState.Checked : ButtonState.Normal);

        }
    }
    #endregion
}
