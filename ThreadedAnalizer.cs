using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.ComponentModel;

namespace WpfApplication1
{
    class ThreadedAnalizer
    {
        public long TotalSize { get; set; }
        public DirectoryTreeElement Root { get; set; }
        private List<Thread> threads;
        public bool Finished = false;
        private int depth = 0;
        public DirectoryTreeViewItem TreeRoot { get; set; }


        public void Analize(string disk)
        {
            Root = new DirectoryTreeElement()
            {
                Header = disk,
                Parent = null
            };

            threads = new List<Thread>();
            DirectoryInfo diskDir = new DirectoryInfo(disk);
            //ReadDirectories(diskDir.GetDirectories(), _Root);
            foreach (DirectoryInfo d in diskDir.GetDirectories())
            {
                DirectoryTreeElement tmpNode = new DirectoryTreeElement()
                {
                    Header = d.Name,
                    FullPath = d.FullName
                };
                Root.Children.Add(tmpNode);
                DirectoryInfo[] directories;
                try
                {
                    directories = d.GetDirectories();
                    Thread th = new Thread(() => ReadDirectories(directories, Root));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    threads.Add(th);
                }
                catch (System.UnauthorizedAccessException ex) { }

            }
            PoolThreads();
            Root.Files = diskDir.GetFiles("*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in Root.Files)
            {
                Root.Size += file.Length;
                TotalSize += file.Length;
            }
            foreach (DirectoryTreeElement tmpD in Root.Children)
            {
                Root.Size += tmpD.Size;
            }
            TreeRoot = getRootTreeNode(Root);
        }

        private void ReadDirectories(DirectoryInfo[] d, DirectoryTreeElement parent)
        {
            DirectoryInfo dir;
            int i = 0;
            try
            {
                for (i = 0; i < d.Length; ++i)
                {
                    dir = d[i];
                    //System.Console.WriteLine(d[i].ToString());
                    if (dir != null)
                    {
                        DirectoryTreeElement tmpNode = new DirectoryTreeElement()
                        {
                            Header = dir.Name,
                            FullPath = dir.FullName
                        };
                        //parent.Items.Add(tmpNode);
                        try
                        {
                            parent.Children.Add(tmpNode);
                            ReadDirectories(dir.GetDirectories("*", SearchOption.TopDirectoryOnly), tmpNode);
                            //_Root.Dispatcher.BeginInvoke(new AddNodeDelegate(AddNode), new object[] { parent, tmpNode, dir.GetDirectories("*", SearchOption.TopDirectoryOnly) });
                           //_Root.Dispatcher.Invoke((Action)(() => {
                           //     parent.Items.Add(tmpNode);
                               
                           // }));

                        }
                        catch (Exception e)
                        {
                            System.Console.WriteLine(e.Message);
                        }
                        tmpNode.Files = dir.GetFiles("*", SearchOption.TopDirectoryOnly);
                        foreach (FileInfo file in tmpNode.Files)
                        {
                            tmpNode.Size += file.Length;
                            TotalSize += file.Length;
                        }
                        foreach (DirectoryTreeElement tmpD in tmpNode.Children)
                        {
                            tmpNode.Size += tmpD.Size;
                        }

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
        }

        public DirectoryTreeViewItem getRootTreeNode(DirectoryTreeElement root)
        {
            DirectoryTreeViewItem newRoot = new DirectoryTreeViewItem()
            {
                Header = root.Header,
                FullPath = root.FullPath,
                Size = root.Size,
                Files = root.Files
            };
            foreach (DirectoryTreeElement tmp in root.Children)
            {
                newRoot.Items.Add(getRootTreeNode(tmp));
            }
            return newRoot;
        }

        private void PoolThreads()
        {
            foreach (Thread th in threads)
            {
                th.Join();
            }
            Finished = true;
        }

        private void AddNode(DirectoryTreeViewItem parent, DirectoryTreeViewItem child, DirectoryInfo[] directories)
        {

        }

        private delegate void AddNodeDelegate(DirectoryTreeViewItem parent, DirectoryTreeViewItem child, DirectoryInfo[] directories);
    }
}
