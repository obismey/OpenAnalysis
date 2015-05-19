using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalysisTesteur.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AnalysisTesteur.Models
{
    public class DashboardItem : DashboardObject
    {
        #region Properties

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
        private bool _IsSelected;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                if (_IsSelected == value) return;
                _IsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public Analysis.SheetItem SheetItem { get; set; }


        public ICommand AutoHide { get { return new DelegateCommand(AutoHideHandler); } }
        private void AutoHideHandler()
        {
          
        }

        public virtual object View { get { return null; } }

      
        #endregion

        public virtual void Update()
        {

        }
    }
}
