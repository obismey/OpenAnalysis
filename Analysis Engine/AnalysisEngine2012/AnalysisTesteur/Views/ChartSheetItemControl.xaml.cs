using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Infragistics.Controls.Charts;

namespace AnalysisTesteur.Views
{
    /// <summary>
    /// Logique d'interaction pour ChartSheetItemControl.xaml
    /// </summary>
    public partial class ChartSheetItemControl : UserControl
    {
        public ChartSheetItemControl()
        {
            InitializeComponent();
        }

        private void XamDataChart_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var chart = sender as XamDataChart;
            var tableitem = this.DataContext as Models.DashboardItemImpls.ChartSheetItem;
            chart.Series.Clear();
            //this.ColumnSeries.ItemsSource = null;
            //this.CategoryXAxis.ItemsSource = null;

            if (tableitem == null) return;
            var rowset = tableitem.RowSet;
            if (rowset == null) return;

            this.CategoryXAxis.ItemsSource = rowset.RowList;
            //var label =string.Join(Environment.NewLine,  from i in Enumerable.Range(0, rowset.DimensionIdList.Length) select "{DimensionItems[" + i + "].Value:}");
            //this.CategoryXAxis.Label = "{DimensionItems[0]}";

            var dimdic = tableitem.RowDimensionList.ToDictionary((d) => d.SheetItemDimension.UniqueID);

            var mdic = tableitem.MeasureList.ToDictionary((m) => m.SheetItemMeasure.UniqueID);

            //var counter = 0;

            //for (int i = 0; i < rowset.DimensionIdList.Length; i++)
            //{
            //    var gcol = new GridViewColumn();
            //    gcol.Header = dimdic[rowset.DimensionIdList[i]].SheetItemDimension.Column;
            //    gcol.DisplayMemberBinding = new Binding("DimensionItems[" + i + "].Value") { Mode = BindingMode.OneWay };
            //    counter += 1;
            //    gview.Columns.Add(gcol);
            //}

            for (int i = 0; i < rowset.MeasureIdList.Length; i++)
            {
                var serie = new ColumnSeries();
                //var m = mdic[rowset.MeasureIdList[i]].SheetItemMeasure;
                //gcol.Header = m.ValueAggregate + "(" + m.ValueColumn + ")";
                //gcol.DisplayMemberBinding = new Binding("MeasureValues[" + i + "]") { Mode = BindingMode.OneWay, StringFormat = "N0" };
                //gview.Columns.Add(gcol);
                serie.ItemsSource = rowset.RowList;
                serie.XAxis = this.CategoryXAxis;
                serie.YAxis = this.NumericYAxis;
                serie.ValueMemberPath = "MeasureValues[" + i + "]";
                chart.Series.Add(serie);
            }
        }
    }
}
