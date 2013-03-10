using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;

namespace WpfApplication1
{
	class TreeBranch : Panel
	{
		static TreeChart parentTreeChart;
		int level;
		
		List<TreeLeaf> leaves;
		ItemCollection nodes;

		public static TreeChart ParentTreeChart {
			get { return parentTreeChart; }
			set { parentTreeChart=value; }
		}

		public int Level {
			get { return level; }
			set { level=value; }
		}

		public List<TreeLeaf> Leaves {
			get { return leaves; }
		}

		public ItemCollection Nodes {
			get { return nodes; }
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
			Height=50;
			Background=Brushes.Gray;

			leaves = new List<TreeLeaf>();
			
			StackPanel branchLayout=new StackPanel();
			branchLayout.Orientation=System.Windows.Controls.Orientation.Horizontal;
			
			
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
