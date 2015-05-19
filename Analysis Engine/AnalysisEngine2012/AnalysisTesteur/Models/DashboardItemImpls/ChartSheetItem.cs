using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalysisTesteur.Models.DashboardItemImpls
{
    public class ChartSheetItem : DashboardDataItem
    {

        private AnalysisTesteur.Views.ChartSheetItemControl _View;
        public override object View
        {
            get
            {
                return this._View;
            }
        }

        private Analysis.SheetItemRowSet  _RowSet;
        public Analysis.SheetItemRowSet RowSet
        {
            get
            {
                return _RowSet;
            }
            set
            {
                _RowSet = value;
                OnPropertyChanged("RowSet");
            }
        }

        public ChartSheetItem()
        {
            this._View = new Views.ChartSheetItemControl();

            this._View.DataContext = this;
        }

        public override void Update()
        {
            var serviceclient = new Analysis.AnalysisService();

            if (this.RowDimensionList.Count == 0)
            {
                this.RowSet = null;

                return;
            }
            this.Caption =string.Join("-",  this.RowDimensionList.Select((col) => col.SheetItemDimension.Column));
            this.RowSet = serviceclient.GetSheetItemRowSet(this.SheetItem);
        }
        public override bool AcceptRow
        {
            get
            {
                return true;
            }
        }
        public override bool AcceptMeasure
        {
            get
            {
                return true;
            }
        }
        public override int AcceptCount
        {
            get
            {
                return 3;
            }
        }
        public override bool AcceptColumn
        {
            get
            {
                return false;
            }
        }

        public override bool AcceptMeasureTarget
        {
            get
            {
                return false;
            }
        }

        public override bool AcceptSerie
        {
            get
            {
                return true;
            }
        }
    }
}
