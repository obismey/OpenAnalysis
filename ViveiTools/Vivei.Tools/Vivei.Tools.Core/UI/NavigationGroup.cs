using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Vivei.Tools.Core.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class NavigationGroup : UIObject
    {
        private string _Caption;
       
        /// <summary>
        /// 
        /// </summary>
        public virtual string Caption
        {
            get
            {
                return _Caption;
            }
            set
            {
                _Caption = value;
                OnPropertyChanged("Caption");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public NavigationGroup()
        {
            this.Nodes = new NavigationNodeCollection();
            this.Nodes.Add(new NavigationNode("Root"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Caption"></param>
        public NavigationGroup(string Caption)
        {
            this.Nodes = new NavigationNodeCollection();
            this._Caption = Caption;
            this.Nodes.Add(new NavigationNode("Root"));

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual NavigationNodeCollection Nodes { get; private set; }
    }
}
