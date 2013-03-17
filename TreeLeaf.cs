using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication1
{
	class TreeLeaf : Panel
	{
		int id;
		string leafName;
		long leafSize;
		Label nameLabel=new Label();
		bool isFile;

		double ratio;
		public double Ratio {
			get { return ratio; }
		}

		SolidColorBrush LeafColor {
			get {
				int colorIndex = (id + parentBranch.Level) % COLOR_TAB.Length;
				System.Diagnostics.Debug.WriteLine("id " + id + " ,leaf color index " + colorIndex);
				return COLOR_TAB[colorIndex];
			}
		}

		TreeBranch parentBranch;
		public TreeBranch ParentBranch {
			get { return parentBranch; }
		}


		public TreeLeaf(TreeBranch parentTreeBranch, String name, long size, bool isFile)
		{
			this.MouseDown += new MouseButtonEventHandler(TreeElem_MouseDown);

			this.parentBranch=parentTreeBranch;
			this.id = parentBranch.Leaves.Count;
			this.isFile=isFile;
			leafName=name;
			leafSize=size;

			/*ratio */

			double parentSize = TreeBranch.ParentTreeChart.Root.Size;
			ratio = size * 1.0 / parentSize;
			

			Height=60;
			//Name=name;
			HorizontalAlignment=System.Windows.HorizontalAlignment.Left;
			VerticalAlignment=System.Windows.VerticalAlignment.Top;
			Background = LeafColor;
			Margin=new Thickness(0,0,0,0);
			
			nameLabel.Content=name;
			nameLabel.FontSize=14;

			Children.Add(nameLabel);
		}

		public void TreeElem_MouseDown(object sender, MouseButtonEventArgs e)
		{
			TreeLeaf leaf = (TreeLeaf) sender;
			
			TreeChart.SelectedLeafInfo.Text = "Current: "+ leaf.leafName +", size: "+  TreeBranch.ParentTreeChart.FormatSize(leaf.leafSize);

			if (isFile) return; //no action if leaf is a file

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
				TreeBranch.ParentTreeChart.Root = newNode; //creates new Branch
				parentBranch.Background = Background;
				TreeBranch.ParentTreeChart.Ctrl.expandTreeNode(newNode);

			} else {
				int id = parentBranch.Leaves.IndexOf((TreeLeaf)sender, 0); //clicked TreeLeaf id (index) in it's branch
				DirectoryTreeViewItem newNode = (DirectoryTreeViewItem)parentBranch.Nodes.GetItemAt(id);
				TreeChart.CurrentOpenedBranchLevel++;
				TreeBranch.ParentTreeChart.Root = newNode; //creates new Branch
				parentBranch.Background = Background;
				//TreeBranch.ParentTreeChart.Branches[TreeBranch.ParentTreeChart.Branches.Count-1].Background = Background;
				TreeBranch.ParentTreeChart.Ctrl.expandTreeNode(newNode);

				//System.Diagnostics.Debug.WriteLine("Add new branch, currOpenedBLevel: " + TreeChart.CurrentOpenedBranchLevel);
			}
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			Size maxSize = new Size();
			//System.Diagnostics.Debug.WriteLine("TreeLeaf MeasureO, aSize: " + availableSize.Width);
			
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

		public static SolidColorBrush[] COLOR_TAB = { Brushes.BlueViolet, Brushes.DarkOrange, Brushes.DarkKhaki, Brushes.Firebrick, Brushes.LightCoral, Brushes.LemonChiffon,
                                               Brushes.LightPink, Brushes.LightSkyBlue, Brushes.Silver, Brushes.Tan, Brushes.Red, Brushes.Purple,  Brushes.OrangeRed};
	}

}