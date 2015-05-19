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

namespace AnalysisTesteur.Views
{
    /// <summary>
    /// Logique d'interaction pour TableSheetItemControl.xaml
    /// </summary>
    public partial class TableSheetItemControl : UserControl
    {
        public TableSheetItemControl()
        {
            InitializeComponent();
        }

        private void ListView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var lstview = sender as ListView;
            var gview = lstview.View as GridView;
            gview.Columns.Clear();
            var tableitem = this.DataContext as Models.DashboardItemImpls.TableSheetItem;
            if (tableitem == null) return;
            var rowset = tableitem.RowSet;
            if (rowset == null) return;

            var dimdic = tableitem.RowDimensionList.ToDictionary((d) => d.SheetItemDimension.UniqueID);

            var mdic = tableitem.MeasureList.ToDictionary((m) => m.SheetItemMeasure.UniqueID);

            var counter = 0;

            for (int i = 0; i < rowset.DimensionIdList.Length ; i++)
            {
                var gcol = new GridViewColumn();
                gcol.Header = dimdic[rowset.DimensionIdList[i]].SheetItemDimension.Column;
                gcol.DisplayMemberBinding = new Binding("DimensionItems[" + i + "].Value") { Mode= BindingMode.OneWay };
                counter += 1;
                gview.Columns.Add(gcol);
            }

            for (int i = 0; i < rowset.MeasureIdList.Length; i++)
            {
                var gcol = new GridViewColumn();
                var m = mdic[rowset.MeasureIdList[i]].SheetItemMeasure;
                gcol.Header =m.ValueAggregate + "(" + m.ValueColumn + ")";
                gcol.DisplayMemberBinding = new Binding("MeasureValues[" + i + "]") { Mode= BindingMode.OneWay, StringFormat="N0" };
                gview.Columns.Add(gcol);
            }
        }
    }
}
