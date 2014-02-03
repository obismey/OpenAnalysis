using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Vivei.Tools.Core.UI
{
    public sealed class MenuItemCollection : ObservableCollection<MenuItem>
    {
        internal MenuItemCollection(MenuItem Owner)
        {

        }

        internal MenuItemCollection(NavigationNode  Owner)
        {

        }

        public MenuItemCollection()
        {

        }
    }
}
