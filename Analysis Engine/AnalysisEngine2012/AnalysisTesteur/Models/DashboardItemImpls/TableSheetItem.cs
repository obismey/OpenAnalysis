using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalysisTesteur.Models.DashboardItemImpls
{
    public class TableSheetItem : DashboardDataItem
    {

        private AnalysisTesteur.Views.TableSheetItemControl _View;
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

        public TableSheetItem()
        {
            this._View = new Views.TableSheetItemControl();

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
                return 2;
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
                return false;
            }
        }
    }
}
