using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    public class PartitionInfo
    {
        public string Name { get; set; }
        public long TotalSpace { get; set; }
        public long UsedSpace { get; set; }
        public long FreeSpace { get; set; }
    }
}
