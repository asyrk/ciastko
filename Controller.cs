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

        public Controller()
        {
            mainWindow = new MainWindow();
            diskSelectDialog = new DiskSelectDialog();
            analizer = new Analizer();
        }
        public void startApp()
        {
            diskSelectDialog.ShowDialog();
            analizer.Analize(diskSelectDialog.SelectedDrive);
            mainWindow.addTreeViewRoot(analizer.Root);
            mainWindow.Show();
        }
    }
}
