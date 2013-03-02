using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace WpfApplication1
{
    class PieChart : Canvas
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
                    p.SizeArc = proc * System.Math.PI * 2;
                }
            }
            private get{
                System.Console.WriteLine("get1");
                return pcs;
            }
        }

        public PieChart()
        {
        }

        public void SetDirectory(DirectoryTreeViewItem[] tab)
        {
            List<PiePice> p = new List<PiePice>();
            foreach (var ti in tab)
            {
                p.Add(new PiePice(ti.Name,ti.size));
            }
            Pieces = p;
        }

        private StreamGeometry CreatePiePiece(PiePice pp, double startRad, Rect r)
        {
            double centerx = r.Width / 2;
            double centery = r.Height / 2;

            double startx = r.X + centerx + (Math.Cos(startRad) * centerx);
            double starty = r.Y + centery + (Math.Sin(startRad) * centery);

            double endx = r.X + centerx + (Math.Cos(startRad + pp.SizeArc) * centerx);
            double endy = r.Y + centery + (Math.Sin(startRad + pp.SizeArc) * centery);

            StreamGeometry sG = new StreamGeometry();
            using (StreamGeometryContext ctx = sG.Open())
            {
                bool big = Math.Abs(pp.SizeArc) > 180;

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
            System.Console.WriteLine("onRender");
            double size = Width > Height ? Height : Width;
            Rect r = new Rect(size / 10, size / 10, size * 4 / 5, size * 4 / 5);
            foreach (var p in pcs)
            {
                p.BeginArc = rad;
                StreamGeometry piece = CreatePiePiece(p, rad, r);
                Brush b = new SolidColorBrush(p.Color);
                Pen pen = new Pen(b, 1);
                //Todo: można użyć do obracania i 'wysuwania' kawałka ciastka.
                Transform t = new TranslateTransform(10, 10);
                //piece.Transform = t;
                dc.DrawGeometry(b, pen, piece);
                rad += p.SizeArc;
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Width = sizeInfo.NewSize.Width;
            Height = sizeInfo.NewSize.Height;
            System.Console.WriteLine("sizeChanged");
            InvalidateVisual();
        }

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
        {
            System.Console.WriteLine("resize");
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
