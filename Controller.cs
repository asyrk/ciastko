using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            chart = new PieChart();
        }
        public void startApp()
        {
            diskSelectDialog.ShowDialog();
            analizer.Analize(diskSelectDialog.SelectedDrive);
            mainWindow.addTreeViewRoot(analizer.Root);
            mainWindow.Show();
            actualNode = analizer.Root;
            chart.Nodes = actualNode.Items;
            mainWindow.Chart = chart;
        }
    }
}
