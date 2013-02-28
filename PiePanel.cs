using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace WpfApplication1
{
    class PiePanel : Panel
    {
        public List<PiePice> pieces
        {
            set
            {
                pieces = value;
                /*long s = 0;
                foreach (PiePice p in pieces)
                {
                    s += p.sizeRel;
                }
                foreach (PiePice p in pieces)
                {
                    double proc = p.sizeRel / s;
                    p.sizeArc = proc * System.Math.PI / 180;
                }*/
            }
            get{
                //System.Console.WriteLine("get1");
                return pieces;
            }
        }

        public PiePanel()
        {
            System.Console.WriteLine("K1");
            List<PiePice> pieces = new List<PiePice>();
            /*System.Console.WriteLine("K2");
            p.Add(new PiePice(0, "C:/", 1021312));
            System.Console.WriteLine("K3");
            p.Add(new PiePice(1, "C:/", 1021312));
            p.Add(new PiePice(2, "C:/", 1021312));*/
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (pieces != null)
            {
                System.Console.WriteLine("if" + pieces.Count);
                /*foreach (PiePice piece in pieces)
                {
                    System.Console.WriteLine("fe1");
                    Color endC = new Color();
                    endC.ScA = piece.col.ScA / 2;
                    endC.ScR = piece.col.ScR;
                    endC.ScG = piece.col.ScG;
                    endC.ScB = piece.col.ScB;
                    Brush br = new LinearGradientBrush(piece.col, endC, 40);
                    Pen p = new Pen(br, 1);
                    dc.DrawEllipse(br,
                        p,
                        new Point(this.ActualWidth / 2, this.ActualHeight / 2),
                        10.0, 50.0);
                }*/
            }
        }
    }
}
