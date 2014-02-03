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
    public interface INavigationView
    {
        /// <summary>
        /// 
        /// </summary>
        string Caption { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Icone { get; set; }
    }

    public sealed class NavigationViewCollection : ObservableCollection<INavigationView>
    {
        internal NavigationViewCollection(NavigationNode Owner)
        {

        }

        public NavigationViewCollection()
        {

        }
    }
    
    public class NavigationView : UIObject, Vivei.Tools.Core.UI.INavigationView
    {
        private string _Caption;
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
        private string _Icone;
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
    }
}
