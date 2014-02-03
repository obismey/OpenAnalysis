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
    public class MenuItem : UIObject
    {

        private string _Caption;
        private Action _OnClick;
        private MenuItemCollection _Children;

        /// <summary>
        /// 
        /// </summary>
        public MenuItem()
        {
            this._Children = new MenuItemCollection(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Caption"></param>
        public MenuItem(string Caption)
        {
            this._Children = new MenuItemCollection(this);
            this._Caption = Caption;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Caption"></param>
        /// <param name="OnClick"></param>
        public MenuItem(string Caption, Action OnClick)
        {
            this._Children = new MenuItemCollection(this);
            this._Caption = Caption;
            this._OnClick = OnClick;
        }

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
        public virtual Action OnClick
        {
            get
            {
                return _OnClick;
            }
            set
            {
                _OnClick = value;
                OnPropertyChanged("OnClick");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual MenuItemCollection Children { get { return _Children; } }
    }
}
