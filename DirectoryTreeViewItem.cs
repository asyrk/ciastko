using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;

namespace WpfApplication1
{
    public class DirectoryTreeViewItem : TreeViewItem
    {
        public long size = 0;
        public FileInfo[] Files;
    }
}
