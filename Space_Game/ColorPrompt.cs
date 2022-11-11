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
                valueBox.TextChanged += rgbTextChanged;
                valueBox.KeyPress += rgbTextKeyPress;
            }
        }
        //https://stackoverflow.com/a/463335
        private void rgbTextKeyPress(object sender, KeyPressEventArgs e) 
        {
            TextBox s = sender as TextBox;

            if (e.KeyChar == (char)Keys.Enter)
            {
                s.ReadOnly = true;
                int ct = 0;
                foreach(var tb in Controls.OfType<TextBox>())
                {
                    if (tb.ReadOnly) ct++;
                }
                if (ct == 3)
                {
                    settings.BulletColor = ColorTranslator.ToHtml(CurrentColor(false));
                    settings.SaveToJson();
                    MessageBox.Show($"Color saved successfully! {ColorTranslator.ToHtml(CurrentColor(false))}");
                }
            }

            if(s.Text.Length >= 3 && !char.IsControl(e.KeyChar) && s.SelectionLength != s.Text.Length) 
            { e.Handled = true; }

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) 
                && (e.KeyChar != '.')) e.Handled = true;
        }
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

        private Color CurrentColor(bool complement)
        {
            TextBox rValue = (TextBox)Controls.Find("rValue", true)[0];
            TextBox gValue = (TextBox)Controls.Find("gValue", true)[0];
            TextBox bValue = (TextBox)Controls.Find("bValue", true)[0];
            int r = rValue.Text != "" && Int32.Parse(rValue.Text) <= 255 ? Int32.Parse(rValue.Text) : -1;
            int g = gValue.Text != "" && Int32.Parse(gValue.Text) <= 255 ? Int32.Parse(gValue.Text) : -1;
            int b = bValue.Text != "" && Int32.Parse(bValue.Text) <= 255 ? Int32.Parse(bValue.Text) : -1;
            if (!complement) return Color.FromArgb(255, r, g, b);
            else return Color.FromArgb(255, 255 - r, 255 - g, 255 - b);
        }

        private void rgbTextChanged(object sender, EventArgs e) 
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
                gValue.Location.X + gValue.Width/2 - valueBox.Width/2,
                hexLabel.Location.Y + hexLabel.Height);
            Controls.Add(valueBox);
        }

        private void ColorPrompt_Load(object sender, EventArgs e)
        {
            TextBox rValue = (TextBox)(Controls.Find("rValue", true)[0]);
            rValue.Focus();
            rValue.SelectAll();
        }
    }
}
