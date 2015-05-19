using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalysisTesteur.Helpers;
using System.Collections.ObjectModel;

namespace AnalysisTesteur.Models
{
    public class DashboardBook : DashboardObject
    {
        #region Properties

        private ObservableCollection<DashboardSheet> _DashboardSheetList= new ObservableCollection<DashboardSheet>();
        public ObservableCollection<DashboardSheet> DashboardSheetList
        {
            get
            {
                return _DashboardSheetList;
            }
        }

        private DashboardSheet  _ActiveDashboardSheet;
        public DashboardSheet  ActiveDashboardSheet
        {
            get
            {
                return _ActiveDashboardSheet;
            }
            set
            {
                _ActiveDashboardSheet = value;
                OnPropertyChanged("ActiveDashboardSheet");
                System.Windows.Input.CommandManager.InvalidateRequerySuggested();
            }
        }

        private ObservableCollection<Analysis.ColumnData> _ColumnList = new ObservableCollection<Analysis.ColumnData>();
        public ObservableCollection<Analysis.ColumnData> ColumnList
        {
            get
            {
                return _ColumnList;
            }
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}