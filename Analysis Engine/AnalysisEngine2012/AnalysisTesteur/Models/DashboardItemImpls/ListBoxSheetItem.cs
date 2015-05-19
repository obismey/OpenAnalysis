using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalysisTesteur.Models.DashboardItemImpls
{
    public class ListBoxSheetItem : DashboardDataItem
    {

        private AnalysisTesteur.Views.ListBoxSheetItemControl _View;
        public override object View
        {
            get
            {
                return this._View;
            }
        }

        private Analysis.DimensionItem[] _DimensionItemList;
        public Analysis.DimensionItem[] DimensionItemList
        {
            get
            {
                return _DimensionItemList;
            }
            set
            {
                _DimensionItemList = value;
                OnPropertyChanged("DimensionItemList");
            }
        }

        public void UpdateSelection(IEnumerable<Analysis.DimensionItem> selecteddimension)
        {

        }

        public ListBoxSheetItem()
        {
            this._View = new Views.ListBoxSheetItemControl();

            this._View.DataContext = this;
        }

        public override void Update()
        {
            var serviceclient = new Analysis.AnalysisService();

            if (this.RowDimensionList.Count == 0)
            {
                this.DimensionItemList = null;

                return;
            }
            this.Caption = this.RowDimensionList[0].SheetItemDimension.Column;
            this.DimensionItemList = serviceclient.GetDimensionItems(this.RowDimensionList[0].SheetItemDimension);
        }
        public override bool AcceptRow
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
                return 1;
            }
        }
        public override bool AcceptColumn
        {
            get
            {
                return false ;
            }
        }
        public override bool AcceptMeasure
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
