using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AnalysisTesteur.Models.DashboardItemImpls
{
    public class DashboardDataItem : DashboardItem 
    {
        #region Properties

        private ObservableCollection<DashboardItemDimension> _RowDimensionList = new ObservableCollection<DashboardItemDimension>();
        public ObservableCollection<DashboardItemDimension> RowDimensionList
        {
            get
            {
                return _RowDimensionList;
            }
        }

        private ObservableCollection<DashboardItemDimension> _ColumnDimensionList = new ObservableCollection<DashboardItemDimension>();
        public ObservableCollection<DashboardItemDimension> ColumnDimensionList
        {
            get
            {
                return _ColumnDimensionList;
            }
        }

        private ObservableCollection<DashboardItemDimension> _SerieDimensionList = new ObservableCollection<DashboardItemDimension>();
        public ObservableCollection<DashboardItemDimension> SerieDimensionList
        {
            get
            {
                return _SerieDimensionList;
            }
        }

        private ObservableCollection<DashboardItemMeasure> _MeasureList = new ObservableCollection<DashboardItemMeasure>();
        public ObservableCollection<DashboardItemMeasure> MeasureList
        {
            get
            {
                return _MeasureList;
            }
        }


        public virtual bool AcceptRow { get { return true; } }
        public virtual bool AcceptColumn { get { return true; } }
        public virtual bool AcceptSerie { get { return true; } }
        public virtual bool AcceptMeasure { get { return true; } }
        public virtual bool AcceptMeasureTarget { get { return true; } }
        public virtual int AcceptCount {
            get { 
                return (AcceptRow ? 1 : 0) +
                    (AcceptColumn  ? 1 : 0) +
                   ( AcceptSerie ?  1 : 0) +
                   ( AcceptMeasure ? 1 : 0); 
            }
        }

        #endregion
    }
}
