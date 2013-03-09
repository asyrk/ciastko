using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication1
{
	class TreeLeaf : Panel
	{
		Label nameLabel=new Label();
		bool isFile;

		//private Size size;
		//private Color fillColor;

		TreeBranch parentBranch;
		public TreeBranch ParentBranch {
			get { return parentBranch; }
		}

		public TreeLeaf(TreeBranch parentTreeBranch, String name, long size,bool isFile)
		{
			this.MouseDown += new MouseButtonEventHandler(TreeElem_MouseDown);

			this.parentBranch=parentTreeBranch;

			this.isFile=isFile;
			Width=100;
			Height=50;
			//Name=name;
			HorizontalAlignment=System.Windows.HorizontalAlignment.Left;
			VerticalAlignment=System.Windows.VerticalAlignment.Top;
			Background=Brushes.Beige;
			Margin=new Thickness(0,5,5,0);
			
			nameLabel.Content=name;
			nameLabel.FontSize=17;

			Children.Add(nameLabel);
		}

		public void TreeElem_MouseDown(object sender, MouseButtonEventArgs e)
		{
			String name="";
			if(sender is TreeLeaf) {
				TreeLeaf tmp=(TreeLeaf) sender;
				name = tmp.Name;
			}
			
			System.Diagnostics.Debug.WriteLine(TreeChart.CurrentOpenedBranchLevel+"");

			/*
			 * if higher leaf demanded remove lower branches & add new branch from that leaf
			 * else add new branch
			 */
			/*if (TreeChart.CurrentOpenedBranchLevel != parentBranch.Level){
				int nextLevel = parentBranch.Level + 1;
				System.Diagnostics.Debug.WriteLine("remove old, nextLevel:" + nextLevel + ", parentBlevel: " + parentBranch.Level + " roznica: " + (TreeChart.CurrentOpenedBranchLevel - parentBranch.Level));
				TreeBranch.ParentTreeChart.ChartLayout.Children.RemoveRange(nextLevel,TreeChart.CurrentOpenedBranchLevel - parentBranch.Level);

				TreeBranch newBranch = new TreeBranch(nextLevel, "b2");
				TreeBranch.ParentTreeChart.ChartLayout.Children.Add(newBranch);
				TreeChart.CurrentOpenedBranchLevel = nextLevel;
				System.Diagnostics.Debug.WriteLine("add new branch, currOpenedBLevel: " + TreeChart.CurrentOpenedBranchLevel);

			} else {
				TreeChart.CurrentOpenedBranchLevel++;
				TreeBranch newBranch = new TreeBranch(TreeChart.CurrentOpenedBranchLevel,"b2");
				TreeBranch.ParentTreeChart.ChartLayout.Children.Add(newBranch);
				System.Diagnostics.Debug.WriteLine("add new branch, currOpenedBLevel: " + TreeChart.CurrentOpenedBranchLevel);
			}
			*/
			

			
			
			
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
			return finalSize;
		}
	}
}