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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for DiskSelectWindow.xaml
    /// </summary

    public partial class DiskSelectControl : UserControl
    {
        public String SelectedDrive { get; set; }

        public DiskSelectControl()
        {
            InitializeComponent();
            PartitionListData data = new PartitionListData();
            this.DataContext = data;
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SelectedDrive = ((PartitionInfo)listView1.SelectedItem).Name;
            
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDrive = ((PartitionInfo)listView1.SelectedItem).Name;
        }
    }
}
