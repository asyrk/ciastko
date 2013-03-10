using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1
{
	class TreeChart : ResultChart
	{
		static TextBlock selectedLeafInfo;
		StackPanel chartLayout;
		static int currentOpenedBranchLevel=0;

		public static TextBlock SelectedLeafInfo {
			get { return selectedLeafInfo; }
		}	

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
				
				//System.Diagnostics.Debug.WriteLine("Add branch in TreeChart (Root) "+ this.GetType().ToString());
				chartLayout.Children.Add(tmp);
			}
		}

		public Controller Ctrl {
			get { return ctrl; }
		}

		
		public TreeChart(Controller c) {
			ctrl = c;
			TreeBranch.ParentTreeChart = this; //define static ref in branches

			selectedLeafInfo = new TextBlock();
			selectedLeafInfo.MinHeight = 30.0;
			selectedLeafInfo.FontSize = 14;
			selectedLeafInfo.Text = "Current: ";

			ScrollViewer chartScrollViewer = new ScrollViewer();
			chartScrollViewer.HorizontalScrollBarVisibility=System.Windows.Controls.ScrollBarVisibility.Auto;

			chartLayout = new StackPanel();
			chartLayout.Orientation=System.Windows.Controls.Orientation.Vertical;
			chartLayout.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

			chartScrollViewer.Content=chartLayout;
			chartScrollViewer.Margin = new Thickness(0.0, (double)selectedLeafInfo.MinHeight, 0.0, 0.0);
			chartScrollViewer.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

			Children.Add(selectedLeafInfo);
			Children.Add(chartScrollViewer);
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
