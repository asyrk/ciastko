using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media;

namespace WpfApplication1
{
	class TreeBranch : Panel
	{
		StackPanel branchLayout;
		public StackPanel BranchLayout{
			get { return branchLayout; }
		}
		
		static TreeChart parentTreeChart;
		public static TreeChart ParentTreeChart {
			get { return parentTreeChart; }
			set { parentTreeChart=value; }
		}

		int level;
		public int Level {
			get { return level; }
			set { level=value; }
		}

		List<TreeLeaf> leaves;
		public List<TreeLeaf> Leaves {
			get { return leaves; }
		}

		ItemCollection nodes;
		public ItemCollection Nodes {
			get { return nodes; }
		}

		SolidColorBrush BranchColor {
			get {
				int color = (level >= TreeLeaf.COLOR_TAB.Length) ? level - TreeLeaf.COLOR_TAB.Length : level;
				return TreeLeaf.COLOR_TAB[color];
			}
		}

		/*
		 * leaves list init
		 * Horizontal StackPanel init
		 * Add TreeLeaves to list & layout in loop
		 */
		public TreeBranch(ItemCollection nodes, int level, string name) {
			this.nodes=nodes;
			this.level = level;
			Name=name;

			HorizontalAlignment=System.Windows.HorizontalAlignment.Stretch;
			Height=65;
			Background=Brushes.White;

			leaves = new List<TreeLeaf>();
			
			branchLayout=new StackPanel();
			branchLayout.Orientation=System.Windows.Controls.Orientation.Horizontal;
			
			System.Diagnostics.Debug.WriteLine("count "+leaves.Count);
			foreach(DirectoryTreeViewItem n in nodes){
				leaves.Add(new TreeLeaf(this,(string)n.Header,n.Size,false));
				branchLayout.Children.Add(leaves[leaves.Count-1]);
			 }
			foreach (var file in ParentTreeChart.Root.Files) {
				leaves.Add(new TreeLeaf(this, file.Name, file.Length, true));
				branchLayout.Children.Add(leaves[leaves.Count - 1]);
			}

			Children.Add(branchLayout);
		}


		protected override Size MeasureOverride(Size availableSize)
		{
			//System.Diagnostics.Debug.WriteLine("TreeBranch MeasuerOverride ,aSize " + availableSize.Width + "width: " + Width);
			Size idealSize = new Size();
			
			foreach (TreeLeaf leaf in branchLayout.Children)
			{
				leaf.Width = leaf.Ratio * Width;
			}
			idealSize.Width = Width;
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
