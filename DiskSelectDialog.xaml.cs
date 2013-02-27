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
    /// Interaction logic for DiskSelectDialog.xaml
    /// </summary>
    public partial class DiskSelectDialog : Window
    {
        public String SelectedDrive { get; set; }
        public DiskSelectDialog()
        {
            InitializeComponent();
            this.contentControl1.Content = new DiskSelectControl();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SelectedDrive = ((DiskSelectControl)this.contentControl1.Content).SelectedDrive;
            this.Close();
        }
    }
}
