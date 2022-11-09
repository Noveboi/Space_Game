using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Game
{
    internal class MyMath
    {
        public double CubeRoot(double num)
        {
            if (num < 0) return -Math.Pow(-num, 1d / 3d);
            else return Math.Pow(num, 1d / 3d);
        }
    }
}
