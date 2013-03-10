using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WpfApplication1
{
    class DirectoryTreeElement
    {
        public String FullPath { get; set; }
        public long Size = 0;
        public FileInfo[] Files { get; set; }
        public List<DirectoryTreeElement> Children { get; set; }
        public DirectoryTreeElement Parent { get; set; }
        public String Header { get; set; }
        public DirectoryTreeElement()
        {
            Children = new List<DirectoryTreeElement>();
        }
    }
}
