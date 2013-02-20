using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Ciastko
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(textBox2.Text);
            foreach (DirectoryInfo dir in d.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                ReadFiles(dir.FullName);
            }
            
        }

        private void ReadFiles(String directory)
        {
            try
            {
                StringBuilder b = new StringBuilder();
                DirectoryInfo d = new DirectoryInfo(directory);
                foreach (FileInfo f in d.GetFiles("*", SearchOption.TopDirectoryOnly))
                {
                    b.Append(f.FullName + "\n");
                }
                textBox1.AppendText(b.ToString());
                foreach (DirectoryInfo dir in d.GetDirectories("*", SearchOption.TopDirectoryOnly))
                {
                    ReadFiles(dir.FullName);
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                System.Console.WriteLine("Skipped directory \"" + directory + "\", reason: " + ex.Message);
            }
        }
    }
}
