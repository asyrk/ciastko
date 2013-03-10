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
			/*
			 * IF higher leaf demanded - remove lower branches & add new branch from that leaf
			 * else add new branch
			 */
			if (TreeChart.CurrentOpenedBranchLevel != parentBranch.Level){
				
				int nextLevel = parentBranch.Level + 1;
				//System.Diagnostics.Debug.WriteLine("Removing, nextLevel:"+ nextLevel + ", parentBranchlevel: " + parentBranch.Level + ", currOpenedBLevel: "+ TreeChart.CurrentOpenedBranchLevel +", roznica: " + (TreeChart.CurrentOpenedBranchLevel - parentBranch.Level));
				TreeBranch.ParentTreeChart.ChartLayout.Children.RemoveRange(nextLevel ,
					TreeChart.CurrentOpenedBranchLevel - parentBranch.Level);

				int id = parentBranch.Leaves.IndexOf((TreeLeaf)sender, 0); //clicked TreeLeaf id (index) in it's branch
				DirectoryTreeViewItem newNode = (DirectoryTreeViewItem)parentBranch.Nodes.GetItemAt(id);
				TreeChart.CurrentOpenedBranchLevel = parentBranch.Level + 1;
				TreeBranch.ParentTreeChart.Root = newNode;
				TreeBranch.ParentTreeChart.Ctrl.expandTreeNode(newNode);

			} else {
				int id = parentBranch.Leaves.IndexOf((TreeLeaf)sender, 0); //clicked TreeLeaf id (index) in it's branch
				DirectoryTreeViewItem newNode = (DirectoryTreeViewItem)parentBranch.Nodes.GetItemAt(id);
				TreeChart.CurrentOpenedBranchLevel++;
				TreeBranch.ParentTreeChart.Root = newNode;
				TreeBranch.ParentTreeChart.Ctrl.expandTreeNode(newNode);

				//System.Diagnostics.Debug.WriteLine("Add new branch, currOpenedBLevel: " + TreeChart.CurrentOpenedBranchLevel);
			}
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