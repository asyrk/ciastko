using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WpfApplication1
{
    public abstract class ResultChart : Panel
    {
        protected ItemCollection nodes;

        public abstract ItemCollection Nodes
        {
            get;
            set;
        }
    }
}
