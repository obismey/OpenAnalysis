using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Vivei.Tools.Core.UI
{
    public sealed class NavigationNodeCollection : ObservableCollection<NavigationNode>
    {
        public NavigationNodeCollection()
        {

        }
        public NavigationNodeCollection(NavigationNode Owner)
        {
            this.Owner = Owner;
        }
        protected override void InsertItem(int index, NavigationNode item)
        {
            item.SetParent(this.Owner);
            base.InsertItem(index, item);
        }
        public NavigationNode Add(string caption)
        {
            var result = new NavigationNode(caption);
            base.Add(result);
            return result;
        }
        public NavigationNode Add(string caption,  NavigationViewCollection views, INavigationView view)
        {
            var result = new NavigationNode(caption, views, view);
            base.Add(result);
            return result;
        }
        public NavigationNode Owner { get; private set; }
    }
}
