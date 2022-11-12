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
    public partial class ColorPrompt : Form
    {
        private bool validColor = true;
        UserSettings settings = new UserSettings();
        private string finalBulletColor;
        public ColorPrompt()
        {
            InitializeComponent();
            CreateRGBControls();
            CreateHexControl();
            settings.GrabFromJson();
        }

        #region Control Creation
        /// <summary>
        /// Create and properly align the three RGB Controls
        /// </summary>
        private void CreateRGBControls()
        {
            for (int i = 0; i < 3; i++)
            {
                TextBox valueBox = new TextBox();
                if (i == 0) 
                { valueBox.Name = "rValue"; valueBox.BackColor = Color.Red; valueBox.TabIndex = 1; }
                if (i == 1) 
                { valueBox.Name = "gValue"; valueBox.BackColor = Color.Green; valueBox.TabIndex = 1; }
                if (i == 2) 
                { valueBox.Name = "bValue"; valueBox.BackColor = Color.Blue; valueBox.TabIndex = 1; }
                valueBox.ForeColor = Color.White;
                valueBox.Multiline = true;
                valueBox.Text = "255";
                valueBox.Size = new Size(40,20);
                valueBox.BorderStyle = BorderStyle.None;
                valueBox.Font = new Font("Arial", 13);
                valueBox.Font = new Font(valueBox.Font, FontStyle.Bold);
                valueBox.TextAlign = HorizontalAlignment.Center;
                valueBox.Location = new Point(
                    rgbLabel.Location.X + (rgbLabel.Width/3) * (i+1) - 67,
                    rgbLabel.Location.Y + rgbLabel.Height + 6);
                Controls.Add(valueBox);
                valueBox.TextChanged += RGBTextChanged;
                valueBox.KeyPress += RGBTextKeyPress;
            }
        }
        /// <summary>
        /// Creates and properly alings the hexValue textBox
        /// </summary>
        private void CreateHexControl()
        {
            TextBox valueBox = new TextBox();
            TextBox gValue = (TextBox)Controls.Find("gValue", true)[0];
            valueBox.Name = "hexValue";

            valueBox.Multiline = true;
            valueBox.Size = new Size(90, 30);
            valueBox.BorderStyle = BorderStyle.None;
            valueBox.Font = new Font("Roboto", 13);
            valueBox.Font = new Font(valueBox.Font, FontStyle.Bold);
            valueBox.TextAlign = HorizontalAlignment.Center;

            valueBox.ForeColor = Color.FromArgb(255, 10, 10, 10);
            valueBox.BackColor = Color.White;


            valueBox.Location = new Point(
                gValue.Location.X + gValue.Width / 2 - valueBox.Width / 2,
                hexLabel.Location.Y + hexLabel.Height);
            Controls.Add(valueBox);
            //valueBox.KeyPress += HexKeyPressed;
            //valueBox.TextChanged += HexTextChanged;
        }
        #endregion

        #region Color Methods
        private void SaveColor()
        {
            if (validColor)
            {
                settings.BulletColor = ColorTranslator.ToHtml(CurrentColor(false));
                settings.SaveToJson();
                MessageBox.Show("Color saved successfully!");
                Close();
            }
        }
        /// <summary>
        /// Get the current color values from the RGB controls 
        /// </summary>
        /// <returns>List of the color values ([0] = R, [1] = G, [2] = B)</returns>
        private List<int> CurrentColorValues()
        {
            TextBox rValue = (TextBox)Controls.Find("rValue", true)[0];
            TextBox gValue = (TextBox)Controls.Find("gValue", true)[0];
            TextBox bValue = (TextBox)Controls.Find("bValue", true)[0];
            int r = rValue.Text != "" && Int32.Parse(rValue.Text) <= 255 ? Int32.Parse(rValue.Text) : -1;
            int g = gValue.Text != "" && Int32.Parse(gValue.Text) <= 255 ? Int32.Parse(gValue.Text) : -1;
            int b = bValue.Text != "" && Int32.Parse(bValue.Text) <= 255 ? Int32.Parse(bValue.Text) : -1;
            List<int> l = new List<int>();
            l.Add(r);
            l.Add(g);
            l.Add(b);
            return l;
        }
        /// <param name="complement">Set to true to return the complementary color</param>
        /// <returns>The current color  from the RGB controls</returns>
        private Color CurrentColor(bool complement)
        {
            var cc = CurrentColorValues();
            if (!complement && !(cc[0] == -1 || cc[1] == -1 || cc[2] == -1)) 
                return Color.FromArgb(255, cc[0], cc[1], cc[2]);

            if (complement && !(cc[0] == -1 || cc[1] == -1 || cc[2] == -1))
                return Color.FromArgb(255, 255 - cc[0], 255 - cc[1], 255 - cc[2]);
            else
                return Color.FromArgb(0,0,0,0);
        }
        #endregion

        #region TextChanged and KeyPress Methods
        /// <summary>
        /// Makes sure user cannot enter anything but 1-long to 3-long digit sequences
        /// </summary>
        private void RGBTextKeyPress(object sender, KeyPressEventArgs e) 
        {
            TextBox s = sender as TextBox;

            if (e.KeyChar == (char)Keys.Enter) SaveColor();

            if (s.Text.Length >= 3 && !char.IsControl(e.KeyChar) && s.SelectionLength != s.Text.Length) 
            { e.Handled = true; }

            //Copied and modified from: https://stackoverflow.com/a/463335
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }
       
        /// <summary>
        /// If the RGB controls contain a valid color, change the display bullet's color and
        /// change the hex value to display the corresponding hex color code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RGBTextChanged(object sender, EventArgs e) 
        {
            TextBox hexValue = (TextBox)(Controls.Find("hexValue", true))[0];
            var cc = CurrentColorValues();

            if (cc[0] == -1 || cc[1] == -1 || cc[2] == -1)
            {
                bullet.BackColor = Color.White;
                hexValue.Text = "";
                validColor = false;
            }
            else
            {
                Color c = Color.FromArgb(255, cc[0], cc[1], cc[2]);
                bullet.BackColor = c;
                hexValue.Text = ColorTranslator.ToHtml(c);
                validColor = true;
            }
        }

        //private void HexTextChanged(object sender, EventArgs e)
        //{
        //    TextBox rValue = (TextBox)Controls.Find("rValue", true)[0];
        //    TextBox gValue = (TextBox)Controls.Find("gValue", true)[0];
        //    TextBox bValue = (TextBox)Controls.Find("bValue", true)[0];
        //    var c = CurrentColor(false);
        //    if (c.A == 0)
        //    {
        //        bullet.BackColor = Color.White;
        //        rValue.Text = "";
        //        gValue.Text = "";
        //        bValue.Text = "";
        //        validColor = false;
        //    }
        //    else
        //    {
        //        bullet.BackColor = c;
        //        rValue.Text = c.R.ToString();
        //        gValue.Text = c.G.ToString();
        //        bValue.Text = c.B.ToString();
        //        validColor = true;
        //    }
        //}

        //private bool IsAThroughForSharp(char c)
        //{
        //    if ((c >= 97 && c <= 102) || (c >= 65 && c <= 70) || c == '#') return true;
        //    return false;
        //}

        //private void HexKeyPressed(object sender, KeyPressEventArgs e)
        //{
        //    TextBox sen = sender as TextBox;
        //    bool notValidChar = !IsAThroughForSharp(e.KeyChar)
        //        && !char.IsControl(e.KeyChar)
        //        && !char.IsDigit(e.KeyChar);

        //    if (e.KeyChar == (char)Keys.Enter) SaveColor();

        //    if (sen.Text.Length >= 7 && !char.IsControl(e.KeyChar)) e.Handled = true;

        //    if (notValidChar) e.Handled = true;

        //    sen.TextChanged += (s, args) =>
        //    {
        //        if (sen.Text.Length == 1 && !char.IsControl(e.KeyChar))
        //        {
        //            sen.Text = sen.Text.Replace(sen.Text, $"#{e.KeyChar}");

        //            //Copied from https://stackoverflow.com/a/20423272
        //            sen.SelectionStart = sen.Text.Length;
        //            sen.SelectionLength = 0;
        //        }
        //        else if (char.IsControl(e.KeyChar)) { }
        //    };
        //}
        #endregion

        private void ColorPrompt_Load(object sender, EventArgs e)
        {
            TextBox rValue = (TextBox)(Controls.Find("rValue", true)[0]);
            rValue.Focus();
            rValue.SelectAll();
        }
    }
}
