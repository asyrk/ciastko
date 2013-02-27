using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;

namespace WpfApplication1
{
    public class PartitionListData
    {

        private ObservableCollection<PartitionInfo> _Partitions = new ObservableCollection<PartitionInfo>();
        public ObservableCollection<PartitionInfo> Partitions { get { return _Partitions; } }

        public PartitionListData()
        {
            String[] drives = Environment.GetLogicalDrives();
            foreach (String drive in drives)
            {
                DriveInfo dInfo = new DriveInfo(drive);
                PartitionInfo tmp = new PartitionInfo();
                tmp.FreeSpace = dInfo.AvailableFreeSpace;
                tmp.Name = drive;
                tmp.TotalSpace = dInfo.TotalSize;
                tmp.UsedSpace = tmp.TotalSpace - tmp.FreeSpace;
                _Partitions.Add(tmp);
            }
        }



    }
}
