using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Ciastko
{
    public partial class MainForm : Form
    {
        List<Thread> threads = new List<Thread>();

        public MainForm()
        {
            InitializeComponent();
            InitializeTreeView();
        }

        private void InitializeTreeView()
        {
            explorerTree.Sorted = true;
            String[] drives = Environment.GetLogicalDrives();
            TreeNode node = new TreeNode();
            node.Name = "My computer";
            explorerTree.Nodes.Add(node);
            foreach(String drive in drives)
            {
                TreeNode tmpNode = new TreeNode(drive);
                node.Nodes.Add(tmpNode);
                if (drive.Equals(drives[0]))
                {
                    foreach (DirectoryInfo d in new DirectoryInfo(drive).GetDirectories())
                    {
                        Thread th = new Thread(() => ReadDirectories(d, tmpNode));
                        th.Start();
                        threads.Add(th);
                    }
                }
            }
            foreach (Thread th in threads)
            {
                th.Join();
            }
        }

        private void ReadDirectories(DirectoryInfo d, TreeNode parent)
        {
            try
            {
                StringBuilder b = new StringBuilder();
                foreach (DirectoryInfo dir in d.GetDirectories("*", SearchOption.TopDirectoryOnly))
                {
                    TreeNode tmpNode = new TreeNode(dir.Name);
                    if (!explorerTree.InvokeRequired)
                    {
                        parent.Nodes.Add(tmpNode);
                    }
                    else
                    {
                        explorerTree.Invoke(new ExplorerAddNodeDelegate(ExplorerAddNode), new object[] {parent, tmpNode});
                    }
                    ReadDirectories(dir, tmpNode);
                    //ReadDirectories(dir.FullName);
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                System.Console.WriteLine("Skipped directory \"" + d.Name + "\", reason: " + ex.Message);
            }
        }

        private void explorerTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void ExplorerAddNode(TreeNode parent, TreeNode child)
        {
            parent.Nodes.Add(child);
        }

        private delegate void ExplorerAddNodeDelegate(TreeNode parent, TreeNode child);

        private void PoolThreads()
        {
            
        }
    }
}
