using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace Space_Game
{
    /// <summary>
    /// Create randomly placed, sized and bright labels in the window.
    /// Here they are used only for aesthetic purposes and serve no gameplay purpose
    /// </summary>
    internal class Stars
    {
        Control.ControlCollection Controls;
        private int formWidth;
        private int formHeight;
        List<Tuple<Label, int>> starsList;

        public Stars(int FormWidth, int FormHeight, Control.ControlCollection ctrls, 
            List<Tuple<Label, int>> FormStarsList)
        {
            Controls = ctrls;
            formWidth = FormWidth;
            formHeight = FormHeight;
            starsList = FormStarsList;
        }

        Color randomBrightness()
        {
            int b = (RandomNumberGenerator.Create().GetHashCode() % 55) + 200;
            return Color.FromArgb(255, b, b, b);
        }
        Point randomLocation(int yOffset)
        {
            int x = RandomNumberGenerator.Create().GetHashCode() % formWidth;
            int y = RandomNumberGenerator.Create().GetHashCode() % formHeight;
            
            return new Point(x, y + yOffset);
        }

        Size randomSize()
        {
            int s = (RandomNumberGenerator.Create().GetHashCode() % 3) + 3;
            return new Size(s, s);
        }

        /// <param name="amount">How many stars to create</param>
        /// <param name="yOffset">How high or low to place the stars</param>
        /// <returns>List containing all visible star controls</returns>
        public List<Tuple<Label,int>> CreateStars(int amount, int yOffset)
        {
            for (int i = 0; i < amount; i++)
            {
                Label star = new Label();
                star.AutoSize = false;
                star.Size = randomSize();
                star.BackColor = randomBrightness();
                star.Location = randomLocation(yOffset);
                starsList.Add(new Tuple<Label, int>(star, (RandomNumberGenerator.Create().GetHashCode() % 6) + 3));
                Controls.Add(star);
                Controls.SetChildIndex(star, -1);
            }
            return starsList;
        }
    }
}
