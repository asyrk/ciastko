using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Globalization;

namespace WpfApplication1
{
    class PieChart : Panel
    {
        private List<PiePice> pcs = new List<PiePice>();

        public List<PiePice> Pieces
        {
            set
            {
                pcs = value;
                double s = 0;
                int i = 0;
                foreach (PiePice p in pcs)
                {
                    s += p.SizeRel;
                    p.Id = i++;
                }
                foreach (PiePice p in pcs)
                {
                    double proc = p.SizeRel / s;
                    p.SweepArc = proc * System.Math.PI * 2;
                }
            }
            private get{
                System.Console.WriteLine("get1");
                return pcs;
            }
        }

        public PieChart()
        {
            List<PiePice> p = new List<PiePice>();
            p.Add(new PiePice("c:/", 14));
            p.Add(new PiePice("d:/", 24));
            p.Add(new PiePice("e:/", 34));
            p.Add(new PiePice("f:/", 44));
            p.Add(new PiePice("g:/", 54));
            Pieces = p;
        }

        public void SetDirectory(DirectoryTreeViewItem[] tab)
        {
            List<PiePice> p = new List<PiePice>();
            foreach (var ti in tab)
            {
                p.Add(new PiePice(ti.Name,ti.Size));
            }
            Pieces = p;
        }

        private StreamGeometry CreatePiePiece(PiePice pp, double startRad, Rect r)
        {
            double centerx = r.Width / 2;
            double centery = r.Height / 2;

            double startx = r.X + centerx + (Math.Cos(startRad) * centerx);
            double starty = r.Y + centery + (Math.Sin(startRad) * centery);

            double endx = r.X + centerx + (Math.Cos(startRad + pp.SweepArc) * centerx);
            double endy = r.Y + centery + (Math.Sin(startRad + pp.SweepArc) * centery);

            StreamGeometry sG = new StreamGeometry();
            using (StreamGeometryContext ctx = sG.Open())
            {
                bool big = Math.Abs(pp.SweepArc) > 180;

                ctx.BeginFigure(new Point(r.X + centerx, r.Y + centery), true, true);
                ctx.LineTo(new Point(endx, endy), true, true);
                ctx.ArcTo(new Point(startx, starty), new Size(centerx, centery), 0, big, SweepDirection.Counterclockwise, true, false);
                ctx.LineTo(new Point(r.X + centerx, r.Y + centery), true, true);
            }
            return sG;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            double rad = 0;
            double Size = Width > Height ? Height : Width;
            Rect r = new Rect(Size / 10, Size / 10, Size * 4 / 5, Size * 4 / 5);
            foreach (var p in pcs)
            {
                p.BeginArc = rad;
                StreamGeometry piece = CreatePiePiece(p, rad, r);
                Brush b = new SolidColorBrush(p.Color);
                Pen pen = new Pen(b, 1);
                if (p.Selected)
                {
                    //Todo: można użyć do obracania i 'wysuwania' kawałka ciastka.
                    Point pt = new Point();
                    double arc = p.BeginArc + p.SweepArc / 2;
                    pt.X = Size / 20 * Math.Cos(arc);
                    pt.Y = Size / 20 * Math.Sin(arc);
                    Transform t = new TranslateTransform(pt.X, pt.Y);
                    piece.Transform = t;
                    FormattedText txt = new FormattedText(p.Name + " Size: " + p.SizeRel,
                      CultureInfo.GetCultureInfo("pl"),
                      FlowDirection.RightToLeft,
                      new Typeface("Verdana"),
                      Size/18, System.Windows.Media.Brushes.Black);
                    dc.DrawText(txt,new Point(Size,0));
                }
                dc.DrawGeometry(b, pen, piece);
                rad += p.SweepArc;
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo SizeInfo)
        {
            base.OnRenderSizeChanged(SizeInfo);
            Width = SizeInfo.NewSize.Width;
            Height = SizeInfo.NewSize.Height;
            InvalidateVisual();
        }

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point pt = e.GetPosition(this);
            double Size = Width > Height ? Height : Width;
            pt.X -= Size / 2;
            pt.Y -= Size / 2;
            double r = Size * 2 / 5;
            double mr = Math.Sqrt(Math.Pow(pt.X,2) + Math.Pow(pt.Y,2));
            if (mr < r)
            {
                double arcRad = Math.Asin(pt.Y / mr);
                if (pt.X < 0)
                    arcRad = Math.PI - arcRad;
                if (arcRad < 0)
                    arcRad = Math.PI * 2 + arcRad;
                foreach (var p in pcs)
                {
                    if (arcRad > p.BeginArc && arcRad < (p.BeginArc + p.SweepArc))
                    {
                        p.Selected = true;
                        InvalidateVisual();
                    }
                    else
                    {
                        p.Selected = false;
                    }
                }
            }
            else
            {
                foreach (var p in pcs)
                    p.Selected = false;
                InvalidateVisual();
            }
        }

        protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
        {
            System.Console.WriteLine("reSize");
            base.MeasureOverride(constraint);
            FrameworkElement parent = this.Parent as FrameworkElement;
            double width, height;
            System.Console.WriteLine("parent: " + parent.ActualWidth + ":" + parent.ActualHeight);
            width = parent.ActualWidth;
            height = parent.ActualHeight;

            Size newSize = new Size(width, height);
            Width = width;
            Height = height; 
            return newSize;
        }
        
    }
}
