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
                col = COLOR_TAB[id % COLOR_TAB.Count()];
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
            get {return sizeRel; }
            set { sizeRel = value; }
        }

        public string SizeForm
        {
            get
            {
                string suf = "";
                double size = sizeRel;
                int i = 0;
                while (size > 1)
                {
                    i++;
                    size /= 1024;
                }
                switch (i)
                {
                    case 1:
                        suf = " B";
                        break;
                    case 2:
                        suf = " KB";
                        break;
                    case 3:
                        suf = " MB";
                        break;
                    case 4:
                        suf = " GB";
                        break;
                   default:
                        suf = " ?";
                        break;
                }
                size *= 1024;
                return String.Format("{0:F3}", size) + suf;
            }
        }

        private double beginArc;
        public double BeginArc
        {
            get { return beginArc; }
            set { beginArc = value; }
        }

        private double sweepArc;
        public double SweepArc
        {
            get { return sweepArc; }
            set { sweepArc = value; }
        }

        private Color col;
        public Color Color
        {
            get { return col; }
        }

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        private bool isFile;
        public bool IsFile
        {
            get { return isFile; }
        }

        public PiePice(string n, long s, bool file)
        {
            name = n;
            sizeRel = s;
            isFile = file;
        }

        public override string ToString()
        {
            return id + ": " + name + ", size: " + sizeRel + ", arc size: " + sweepArc;
        }

        public static Color[] COLOR_TAB = { Colors.BlueViolet, Colors.DarkOrange, Colors.DarkKhaki, Colors.Firebrick, Colors.LightCoral, Colors.LemonChiffon,
                                               Colors.LightPink, Colors.LightSkyBlue, Colors.Silver, Colors.Tan, Colors.Red, Colors.Purple,  Colors.OrangeRed};
    }
}
