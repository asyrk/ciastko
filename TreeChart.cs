using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1
{
	class TreeChart : ResultChart
	{
		StackPanel chartLayout;
		static int currentOpenedBranchLevel=0;

		public static int CurrentOpenedBranchLevel {
			get { return currentOpenedBranchLevel; }
			set { currentOpenedBranchLevel=value; }
		}

		public StackPanel ChartLayout {
			get { return chartLayout; }
		}

		public override ItemCollection Nodes
		{
			get { return root.Items; }
		}

		public override DirectoryTreeViewItem Root {
			get { return root; }
			set { 
				root = value;
				ItemCollection nodes = Nodes;
				TreeBranch tmp = new TreeBranch(nodes,currentOpenedBranchLevel, "branch");
				chartLayout.Children.Add(tmp);
			}
		}

		
		public TreeChart(Controller c) {
			ctrl = c;
			TreeBranch.ParentTreeChart = this; //define static ref in branches
			chartLayout = new StackPanel();
			chartLayout.Orientation=System.Windows.Controls.Orientation.Vertical;

			Children.Add(chartLayout);
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
			return finalSize;
		}

	}
}
