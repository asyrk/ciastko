using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Controller ctl {get; set;}

        public MainWindow()
        {
            InitializeComponent();
        }

        public ResultChart Chart
        {
            set
            {
                chartGrid.Children.Add(value);
            }
        }

        public void addTreeViewRoot(DirectoryTreeViewItem root)
        {
            directoryTree.Items.Add(root);
            root.IsExpanded = true;
        }

        private void directoryTree_Expanded(object sender, RoutedEventArgs e)
        {
            ctl.setChartRoot(e.OriginalSource as DirectoryTreeViewItem);
        }

        private void OpenInExplorer_Click(object sender, RoutedEventArgs e)
        {
            DirectoryTreeViewItem selectedItem = directoryTree.SelectedItem as DirectoryTreeViewItem;
            ctl.OpenExplorer(selectedItem.FullPath);
        }

        private void DeleteFile_Click(object sender, RoutedEventArgs e)
        {
            DirectoryTreeViewItem selectedItem = directoryTree.SelectedItem as DirectoryTreeViewItem;
            if (ctl.DeleteItem(selectedItem.FullPath))
            {
                ctl.setChartRoot(selectedItem.Parent as DirectoryTreeViewItem);
                ctl.RemoveItemFromTree(directoryTree.Items[0] as DirectoryTreeViewItem, selectedItem);
            }
        }

        private void directoryTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DirectoryTreeViewItem selectedItem = directoryTree.SelectedItem as DirectoryTreeViewItem;
            directoryTree.ContextMenu = directoryTree.Resources["TreeItemContext"] as ContextMenu;
        }
    }
}
