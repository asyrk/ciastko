using System;
using System.Windows.Controls;

namespace WpfApplication1
{
    public abstract class ResultChart : Canvas
    {
        protected DirectoryTreeViewItem root;
        protected Controller ctrl;

        public abstract ItemCollection Nodes
        {
            get;
        }
        public abstract DirectoryTreeViewItem Root
        {
            get;
            set;
        }

		public string FormatSize(long sizeRel) {
			string suf = "";
            double size = sizeRel;
            int i = 0;
            while (size > 1)
            {
                i++;
                size /= 1024;
            }
            switch (i)
            {
                case 1:
                    suf = " B";
                    break;
                case 2:
                    suf = " KB";
                    break;
                case 3:
                    suf = " MB";
                    break;
                case 4:
                    suf = " GB";
                    break;
                default:
                    suf = " ?";
                    break;
            }
            size *= 1024;
            return String.Format("{0:F3}", size) + suf;
		}
    }
}
