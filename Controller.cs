using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace WpfApplication1
{
    public class Controller
    {
        private MainWindow mainWindow;
        private DiskSelectDialog diskSelectDialog;
        private Analizer analizer;
        private ResultChart chart;
        private DirectoryTreeViewItem actualNode;

        public Controller()
        {
            mainWindow = new MainWindow();
            diskSelectDialog = new DiskSelectDialog();
            analizer = new Analizer();
            chart = new PieChart(this);
			//chart = new TreeChart(this);
        }
        public void startApp()
        {
            mainWindow.ctl = this;
            diskSelectDialog.ShowDialog();
            analizer.Analize(diskSelectDialog.SelectedDrive);
            mainWindow.addTreeViewRoot(analizer.Root);
            mainWindow.Show();
            actualNode = analizer.Root;
            chart.Root = actualNode;
            mainWindow.Chart = chart;
        }


        public void expandTreeNode(DirectoryTreeViewItem item)
        {
            item.IsExpanded = true;
            item.BringIntoView();
        }

        public void setChartRoot(DirectoryTreeViewItem item)
        {
            chart.Root = item;
        }

        public void OpenExplorer(String path)
        {
            Process.Start(path);
        }

        public Boolean DeleteItem(String path)
        {
            DeleteConfirmationDialog confirm = new DeleteConfirmationDialog();
            confirm.ShowDialog();
            if(confirm.DoDelete == true) {
                try
                {
                    Directory.Delete(path, true);
                }
                catch (UnauthorizedAccessException uae)
                {
                    System.Console.WriteLine("Could not delete file " + path + ", reason: " + uae.Message + ".");
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Unknown error while deleting " + path + " .");
                }
            }
            return confirm.DoDelete;
        }

        internal void RemoveItemFromTree(DirectoryTreeViewItem root, DirectoryTreeViewItem selectedItem)
        {
            if (root.Items.Contains(selectedItem))
            {
                root.Items.Remove(selectedItem);
            }
            else
            {
                foreach (DirectoryTreeViewItem next in root.Items)
                {
                    RemoveItemFromTree(next, selectedItem);
                }
            }
        }
    }
}
