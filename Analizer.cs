using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;
using System.Threading;
using System.ComponentModel;

namespace WpfApplication1
{
    public class Analizer
    {
        public long TotalSize { get; set; }
        private DirectoryTreeViewItem _Root;
        public DirectoryTreeViewItem Root { get { return _Root; } }
        private List<Thread> threads;
        public bool Finished = false;
        private int depth = 0;

        public void Analize(string disk)
        {
            _Root = new DirectoryTreeViewItem();
            _Root.Items.SortDescriptions.Add(new SortDescription("Header", ListSortDirection.Ascending));
            _Root.Header = disk;
            threads = new List<Thread>();
            ReadDirectories(new DirectoryInfo(disk), _Root);
            //foreach (DirectoryInfo d in new DirectoryInfo(disk).GetDirectories("*", SearchOption.TopDirectoryOnly))
            //{
                //DirectoryTreeViewItem tmpNode = new DirectoryTreeViewItem();
                //tmpNode.Header = d.Name;
                //_Root.Items.Add(tmpNode);
                //ReadDirectories(d, tmpNode);
                //Thread th = new Thread(() => ReadDirectories(d, _Root));
                //th.Start();
                //threads.Add(th);
            //}
            //Thread pool = new Thread(PoolThreads);
        }

        private void ReadDirectories(DirectoryInfo d, DirectoryTreeViewItem parent)
        {
            depth++;
            try
            {
                foreach (DirectoryInfo dir in d.GetDirectories("*", SearchOption.TopDirectoryOnly))
                {
                    DirectoryTreeViewItem tmpNode = new DirectoryTreeViewItem();
                    tmpNode.Header = dir.Name;
                    parent.Items.Add(tmpNode);
                    ReadDirectories(dir, tmpNode);
                    foreach (FileInfo file in dir.GetFiles("*", SearchOption.TopDirectoryOnly))
                    {
                        tmpNode.size += file.Length;
                        TotalSize += file.Length;
                    }
                    foreach (DirectoryTreeViewItem tmpD in tmpNode.Items)
                    {
                        tmpNode.size += tmpD.size;
                    }
                    //tmpNode.Header += tmpNode.size.ToString();
                    //parent.Dispatcher.BeginInvoke(new AddNodeDelegate(AddNode), new object[] { parent, tmpNode });
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                System.Console.WriteLine("Skipped directory \"" + d.Name + "\", reason: " + ex.Message);
            }
            depth--;
        }

        private void PoolThreads()
        {
            foreach (Thread th in threads)
            {
                th.Join();
            }
            Finished = true;
        }

        private void AddNode(DirectoryTreeViewItem parent, DirectoryTreeViewItem child)
        {
            parent.Items.Add(child);
        }

        private delegate void AddNodeDelegate(DirectoryTreeViewItem parent, DirectoryTreeViewItem child);
    }
}
