using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace WpfApplication1
{
    class PiePice
    {
        private int id;
        public int Id
        {
            get { return id; }
            set 
            {
                id = value;
                col = COLOR_TAB[id];
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private long sizeRel;
        public long SizeRel
        {
            get { return sizeRel; }
            set { sizeRel = value; }
        }

        private double beginArc;
        public double BeginArc
        {
            get { return beginArc; }
            set { beginArc = value; }
        }

        private double sizeArc;
        public double SizeArc
        {
            get { return sizeArc; }
            set { sizeArc = value; }
        }

        private Color col;
        public Color Color
        {
            get { return col; }
        }

        public PiePice(string n, long s)
        {
            name = n;
            sizeRel = s;
        }

        public override string ToString()
        {
            return id + ": " + name + ", size: " + sizeRel + ", arc size: " + sizeArc;
        }

        public static Color[] COLOR_TAB = { Colors.BlueViolet, Colors.DarkOrange, Colors.DarkKhaki, Colors.Firebrick, Colors.LightCoral, Colors.LemonChiffon,
                                               Colors.LightPink, Colors.LightSkyBlue, Colors.Silver, Colors.Tan, Colors.Red, Colors.Purple,  Colors.OrangeRed};
    }
}
