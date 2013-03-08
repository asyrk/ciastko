using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
