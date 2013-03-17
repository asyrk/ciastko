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
		public static TextBlock SelectedLeafInfo {
			get { return selectedLeafInfo; }
		}

		static int currentOpenedBranchLevel = 0;
		public static int CurrentOpenedBranchLevel {
			get { return currentOpenedBranchLevel; }
			set { currentOpenedBranchLevel=value; }
		}

		List<TreeBranch> branches;
		public List<TreeBranch> Branches {
			get { return branches; }
		}

		StackPanel chartLayout;
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
				branches.Add(tmp);
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

			branches = new List<TreeBranch>();

			selectedLeafInfo = new TextBlock();
			selectedLeafInfo.MinHeight = 30.0;
			selectedLeafInfo.FontSize = 15;
			selectedLeafInfo.Text = "Current: , size: ";

			ScrollViewer chartScrollViewer = new ScrollViewer();
			chartScrollViewer.HorizontalScrollBarVisibility=System.Windows.Controls.ScrollBarVisibility.Auto;
			chartScrollViewer.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;

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
			//System.Diagnostics.Debug.WriteLine("TreeChart MeasuerOverride ,aSize " + availableSize.Width);
			Size idealSize = new Size();

			double windowSize = availableSize.Width;
			foreach(TreeBranch branch in chartLayout.Children){
				//System.Diagnostics.Debug.WriteLine("Set branch width "+ windowSize);
				branch.Width = windowSize;
			}
			//idealSize.Width = Width;
			return idealSize;
			
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
