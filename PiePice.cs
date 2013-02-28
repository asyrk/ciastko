using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace WpfApplication1
{
    class PiePice
    {
        public int id;
        public string name;
        public long sizeRel;
        public double sizeArc;
        public Color col;
        public PiePice(int i, string n, long s)
        {
            id = i;
            name = n;
            sizeRel = s;
            col = COLOR_TAB[id];
        }
        public static Color[] COLOR_TAB = { Colors.AliceBlue, Colors.BlueViolet, Colors.DarkOrange, Colors.DarkKhaki, Colors.Firebrick, Colors.LightCoral, Colors.LemonChiffon,
                                               Colors.LightPink, Colors.LightSkyBlue, Colors.MintCream, Colors.Silver, Colors.Tan, Colors.Red, Colors.Purple, Colors.OrangeRed, Colors.Orange};
    }
}
