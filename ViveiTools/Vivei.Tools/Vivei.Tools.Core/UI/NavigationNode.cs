using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Vivei.Tools.Core.UI
{
    public class NavigationNode : UIObject
    {
        private string _Caption;
        private INavigationView _View;
        private Boolean _Expanded;
        private string _Icone;

        public NavigationNode(string Caption)
        {
            // TODO: Complete member initialization
            this.Children = new NavigationNodeCollection(this);
            this.ContextualMenu = new MenuItemCollection(this);
            //this.Views = new NavigationViewCollection(this);
            this._Caption = Caption;
            _View = new NavigationView() { Caption = _Caption };
            //this.Views.Add(_View);
        }
        public NavigationNode(string Caption, NavigationViewCollection Views)
        {
            // TODO: Complete member initialization
            this.Children = new NavigationNodeCollection(this);
            this.ContextualMenu = new MenuItemCollection(this);
            this.Views = Views;
            this._Caption = Caption;
            _View = new NavigationView() { Caption = _Caption };
            this.Views.Add(_View);
        }
        public NavigationNode(string Caption, NavigationViewCollection Views, INavigationView View)
        {
            // TODO: Complete member initialization
            this.Children = new NavigationNodeCollection(this);
            this.ContextualMenu = new MenuItemCollection(this);
            this.Views = Views;
            this._Caption = Caption;
            this._View = View;
            if (View != null)
            {
                if (!Views.Contains(View))
                {
                    Views.Add(View);
                }
            }

        }
        public NavigationNode()
        {
            // TODO: Complete member initialization
            this.Children = new NavigationNodeCollection(this);
            this.ContextualMenu = new MenuItemCollection(this);
        }

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
        public virtual string Icone
        {
            get
            {
                return _Icone;
            }
            set
            {
                _Icone = value;
                OnPropertyChanged("Icone");
            }
        }
        public virtual INavigationView View
        {
            get
            {
                return _View;
            }
            set
            {
                _View = value;
                OnPropertyChanged("View");
            }
        }

        public virtual NavigationNodeCollection Children { get; protected set; }
        public virtual MenuItemCollection ContextualMenu { get; protected set; }
        public virtual NavigationViewCollection Views { get; protected set; }
        public NavigationNode Parent { get; private set; }

        internal void SetParent(NavigationNode Parent)
        {
            this.Parent = Parent;
            OnPropertyChanged("Parent");
        }

        public virtual Boolean Expanded
        {
            get
            {
                return _Expanded;
            }
            set
            {
                _Expanded = value;
                OnPropertyChanged("Expanded");
            }
        }

    }
}
