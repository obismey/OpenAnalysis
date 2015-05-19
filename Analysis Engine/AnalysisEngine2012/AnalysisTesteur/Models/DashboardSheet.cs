using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalysisTesteur.Helpers;
using System.Collections.ObjectModel;

namespace AnalysisTesteur.Models
{
    public class DashboardSheet : DashboardObject
    {
        #region Properties

        private string _Caption;
        public string Caption
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

        private DashboardItem  _ActiveDashboardItem;
        public DashboardItem  ActiveDashboardItem
        {
            get
            {
                return _ActiveDashboardItem;
            }
            set
            {
                if (value == _ActiveDashboardItem) return;
                _ActiveDashboardItem = value;
                OnPropertyChanged("ActiveDashboardItem");
            }
        }

        private UndoRedoContainer  _UndoRedo ;
        public UndoRedoContainer  UndoRedo
        {
            get
            {
                return _UndoRedo;
            }
            set
            {
                _UndoRedo = value;
                OnPropertyChanged("UndoRedo");
            }
        }   


        private ObservableCollection<DashboardItem> _DashboardItemList = new ObservableCollection<DashboardItem>();
        public ObservableCollection<DashboardItem> DashboardItemList
        {
            get
            {
                return _DashboardItemList;
            }
        }
        #endregion

        public DashboardSheet()
        {
            _UndoRedo = new UndoRedoContainer(this);
        }
    }
}
