using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace WpfApplication1
{
    class PieCanvas : Canvas
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

        public PieCanvas()
        {
            List<PiePice> p = new List<PiePice>();
            p.Add(new PiePice("C:/", 11));
            p.Add(new PiePice("C:/", 12));
            p.Add(new PiePice("C:/", 13));
            p.Add(new PiePice("C:/", 14));
            p.Add(new PiePice("C:/", 15));
            p.Add(new PiePice("C:/", 16));
            p.Add(new PiePice("C:/", 17));
            p.Add(new PiePice("C:/", 18));
            /*p.Add(new PiePice("C:/", 19));
            p.Add(new PiePice("C:/", 20));
            p.Add(new PiePice("C:/", 21));
            p.Add(new PiePice("C:/", 22));
            p.Add(new PiePice("C:/", 30));*/
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
            Rect r = new Rect(Width / 10, Height /10, Width * 4 / 5, Height * 4 / 5);
            foreach (var p in pcs)
            {
                p.BeginArc = rad;
                StreamGeometry piece = CreatePiePiece(p, rad, r);
                Brush b = new SolidColorBrush(p.Color);
                Pen pen = new Pen(b, 1);
                //Todo: można użyć do obracania i 'wysuwania' kawałka ciastka.
                Transform t = new TranslateTransform(10, 10);
                piece.Transform = t;
                dc.DrawGeometry(b, pen, piece);
                rad += p.SizeArc;
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Width = sizeInfo.NewSize.Width;
            Height = sizeInfo.NewSize.Height;
        }

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
        {
            base.MeasureOverride(constraint);
            FrameworkElement parent = this.Parent as FrameworkElement;
            double width, height;
            if(parent != null){
                width = parent.ActualWidth;
                height = parent.ActualHeight;
            }else{
                width = Width;
                height = Height;
            }
            return new Size(width, height);
        }
        
    }
}
