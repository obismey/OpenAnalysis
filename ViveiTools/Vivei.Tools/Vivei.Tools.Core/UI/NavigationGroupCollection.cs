using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Vivei.Tools.Core.UI
{
    public sealed class NavigationGroupCollection : ObservableCollection<NavigationGroup>
    {
        public NavigationGroup Add(string caption)
        {
            var result = new NavigationGroup(caption);
            base.Add(result);
            return result;
        }
    }
}
