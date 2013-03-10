using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WpfApplication1
{
    class DirectoryElement
    {
        public String FullPath { get; set; }
        public long Size = 0;
        public FileInfo[] Files { get; set; }
    }
}
