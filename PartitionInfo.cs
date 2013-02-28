using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WpfApplication1
{
    public class PartitionInfo
    {
        public string Name { get; set; }
        public long TotalSpace { get; set; }
        public long UsedSpace { get; set; }
        public long FreeSpace { get; set; }
        public string Percentage { get; set; }

        public PartitionInfo(string Name, long Total, long Used, long Free)
        {
            this.TotalSpace = Total;
            this.UsedSpace = Used;
            this.FreeSpace = Free;
            this.Name = Name;
            Percentage = Math.Round(((float)Used/(float)Total) * 100.0, 2) + "%";

        }
    }
}
