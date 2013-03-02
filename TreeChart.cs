using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1
{
	class TreeChart : Panel
	{
		
		public TreeChart() {
			this.MouseDown += new MouseButtonEventHandler(TreeChart_MouseDown);
		}

		public void TreeChart_MouseDown(object sender, MouseButtonEventArgs e){
			this.InvalidateArrange();
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			Size maxSize = new Size();

			foreach (UIElement child in Children)
			{
				child.Measure(availableSize);
				maxSize.Height = Math.Max(child.DesiredSize.Height, maxSize.Height);
				maxSize.Width = Math.Max(child.DesiredSize.Width, maxSize.Width);
			}

			return maxSize;
		}

		
		protected override Size ArrangeOverride(Size finalSize)
		{
			foreach (UIElement child in Children)
			{
				child.Arrange(new Rect(finalSize));
			}

			System.Diagnostics.Debug.WriteLine("event");

			return finalSize;
		}

	}
}
