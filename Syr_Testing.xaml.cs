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
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Testing.xaml
    /// </summary>
    public partial class Testing : Window
    {
        Analizer a;

        private String SelectedDrive;

        public Testing()
        {
            InitializeComponent();
            a = new Analizer();
            //Window diskDialog = new Window
            //{
            //    Title = "Select partition to analyze",
            //    Content = new DiskSelectControl(),
            //    SizeToContent = SizeToContent.WidthAndHeight,
            //    ResizeMode = ResizeMode.NoResize
            //};
            //diskDialog.ShowDialog();
            DiskSelectDialog diskDialog = new DiskSelectDialog();
            diskDialog.ShowDialog();
            SelectedDrive = diskDialog.SelectedDrive;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            a.Analize(SelectedDrive);
            this.treeView1.Items.Add(a.Root);
            this.label1.Content = @"Total size: " + a.TotalSize + " bytes.";
        }
    }
}
