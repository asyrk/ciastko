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
    class PieChart : ResultChart
    {

        public PieChart(Controller c) {
            ctrl = c;
            Button btn = new Button();
            btn.Content = "Back";
            btn.BorderBrush = btn.Background = Brushes.AliceBlue;
            btn.FontSize = 20;
            btn.Click +=new RoutedEventHandler(backClick);
            Canvas.SetLeft(btn, 5);
            Canvas.SetTop(btn, 5);
            this.Children.Add(btn);
        }

        private List<PiePice> pcs = new List<PiePice>();

        private List<PiePice> Pieces
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
            get{
                return pcs;
            }
        }

        public override ItemCollection Nodes
        {
            get { return root.Items; }
        }

         public override DirectoryTreeViewItem Root
        {
            get { return root; }
            set 
            {
                root = value;
                ItemCollection nodes = root.Items;
                List<PiePice> p = new List<PiePice>();
                foreach (DirectoryTreeViewItem n in nodes)
                    p.Add(new PiePice((string)n.Header, n.Size, false));
                foreach(var fI in root.Files)
                    p.Add(new PiePice(fI.Name, fI.Length, true));
                Pieces = p;
                InvalidateVisual();
            }
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
                bool big = Math.Abs(pp.SweepArc) > Math.PI;

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
                    Point pt = new Point();
                    double arc = p.BeginArc + p.SweepArc / 2;
                    pt.X = Size / 20 * Math.Cos(arc);
                    pt.Y = Size / 20 * Math.Sin(arc);
                    Transform t = new TranslateTransform(pt.X, pt.Y);
                    piece.Transform = t;
                    FormattedText txt = new FormattedText(p.Name + "\nSize: " + p.SizeForm,
                      CultureInfo.GetCultureInfo("pl"),
                      FlowDirection.RightToLeft,
                      new Typeface("Verdana"),
                      Size/35, System.Windows.Media.Brushes.Black);
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
            PiePice p = GetPieceAt(pt);
            foreach (var pc in pcs)
                pc.Selected = false;
            if (p != null)
            {
                p.Selected = true;
            }
            InvalidateVisual();
        }

        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Point pt = e.GetPosition(this);
            PiePice p = GetPieceAt(pt);
            if (!p.IsFile && p!= null)
            {
                int id = pcs.IndexOf(p);
                DirectoryTreeViewItem newN = (DirectoryTreeViewItem) Nodes.GetItemAt(id);
                Root = newN;
                ctrl.expandTreeNode(newN);
            }
        }

        protected override void OnMouseRightButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            ContextMenu cm = createContextMenu();
            cm.IsOpen = true;
        }

        protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
        {
            System.Console.WriteLine("reSize");
            base.MeasureOverride(constraint);
            FrameworkElement parent = this.Parent as FrameworkElement;
            double width, height;
            width = parent.ActualWidth;
            height = parent.ActualHeight;
            double size = width > height ? height : width;
            width = height = size;
            Size newSize = new Size(width, height);
            Width = width;
            Height = height;
            return newSize;
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            if (Nodes.Count != 0)
            {
                TreeViewItem root = (TreeViewItem)Nodes.GetItemAt(0);
                DependencyObject parent = root.Parent;
                if (parent is DirectoryTreeViewItem)
                {
                    var grandparent = ((TreeViewItem)parent).Parent;
                    if (grandparent is DirectoryTreeViewItem )
                    {
                        Root = (DirectoryTreeViewItem)grandparent;
                    }
                }
            }
        }

        private ContextMenu createContextMenu(){
            ContextMenu menu = new ContextMenu();
            MenuItem open = new MenuItem();
            open.Header = "Open";
            menu.Items.Add(open);
            MenuItem del = new MenuItem();
            del.Header = "Delete";
            menu.Items.Add(del);
            open.MouseDown += new System.Windows.Input.MouseButtonEventHandler(open_MouseDown);
            return menu;
        }

        void open_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private PiePice GetPieceAt(Point pt)
        {
            PiePice piece = null;
            double Size = Width > Height ? Height : Width;
            pt.X -= Size / 2;
            pt.Y -= Size / 2;
            double r = Size * 2 / 5;
            double mr = Math.Sqrt(Math.Pow(pt.X, 2) + Math.Pow(pt.Y, 2));
            if (mr < r)
            {
                double arcRad = Math.Asin(pt.Y / mr);
                if (pt.X < 0)
                    arcRad = Math.PI - arcRad;
                if (arcRad < 0)
                    arcRad = Math.PI * 2 + arcRad;
                foreach (var p in pcs)
                    if (arcRad > p.BeginArc && arcRad < (p.BeginArc + p.SweepArc))
                    {
                        piece = p;
                        break;
                    }
            }
            return piece;
        }
    }
}
