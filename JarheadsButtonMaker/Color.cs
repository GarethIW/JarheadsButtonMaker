using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarheadsButtonMaker
{
    public struct Color
    {
        public float r, g, b, a;

        public Color(float red, float green, float blue, float alpha)
        {
            r = red;
            g = green;
            b = blue;
            a = alpha;
        }
        public Color(float red, float green, float blue)
        {
            r = red;
            g = green;
            b = blue;
            a = 1f;
        }
    }
}
