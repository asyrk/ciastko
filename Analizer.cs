using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Windows.Threading;

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
            DirectoryInfo diskDir = new DirectoryInfo(disk);
            ReadDirectories(diskDir.GetDirectories(), _Root);
            _Root.Files = diskDir.GetFiles("*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in _Root.Files)
            {
                _Root.Size += file.Length;
                TotalSize += file.Length;
            }
            foreach (DirectoryTreeViewItem tmpD in _Root.Items)
            {
                _Root.Size += tmpD.Size;
            }
            //foreach (DirectoryInfo d in diskDir.GetDirectories())
            //{
            //   DirectoryTreeViewItem tmpNode = new DirectoryTreeViewItem();
            //    tmpNode.Header = d.Name;
            //    _Root.Items.Add(tmpNode);
            //    DirectoryInfo[] directories;
            //    try
            //   {
            //        directories = d.GetDirectories();
            //        Thread th = new Thread(() => ReadDirectories(directories, _Root));
            //        th.SetApartmentState(ApartmentState.STA);
            //        th.Start();
            //        threads.Add(th);
            //    }
            //    catch (System.UnauthorizedAccessException ex) { }
            //
            //}
            //Thread pool = new Thread(PoolThreads);
        }

        private void ReadDirectories(DirectoryInfo[] d, DirectoryTreeViewItem parent)
        {
            depth++;
            DirectoryInfo dir;
            int i =0;
            try
            {
                for (i = 0; i < d.Length; ++i)
                {
                    dir = d[i];
                    //System.Console.WriteLine(d[i].ToString());
                    DirectoryTreeViewItem tmpNode = new DirectoryTreeViewItem();
                    if (dir != null)
                    {
                        tmpNode.Header = dir.Name;
                        parent.Items.Add(tmpNode);
                        ReadDirectories(dir.GetDirectories("*", SearchOption.TopDirectoryOnly), tmpNode);
                        tmpNode.Files = dir.GetFiles("*", SearchOption.TopDirectoryOnly);
                        foreach (FileInfo file in tmpNode.Files)
                        {
                            tmpNode.Size += file.Length;
                            TotalSize += file.Length;
                        }
                        foreach (DirectoryTreeViewItem tmpD in tmpNode.Items)
                        {
                            tmpNode.Size += tmpD.Size;
                        }
                        //try
                        //{
                        //    _Root.Dispatcher.BeginInvoke(new AddNodeDelegate(AddNode), new object[] { parent, tmpNode });
                        //}
                        //catch (Exception e)
                        //{
                        //    System.Console.WriteLine(e.Message);
                        //}
                    }
                    //tmpNode.Header += tmpNode.size.ToString();
                    //parent.Dispatcher.BeginInvoke(new AddNodeDelegate(AddNode), new object[] { parent, tmpNode });
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                if (d[i] != null)
                    System.Console.WriteLine("Skipped directory \"" + d[i].Name + "\", reason: " + ex.Message);
            }
            catch (System.NullReferenceException ex)
            {
            }
            finally
            {
                if (d.Length - 1 > i)
                {
                    DirectoryInfo[] tmpDir = new DirectoryInfo[d.Length - i];
                    Array.Copy(d, i + 1, tmpDir, 0, d.Length - i - 1);
                    ReadDirectories(tmpDir, parent);
                }
            }
            //System.Console.WriteLine(depth);
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
