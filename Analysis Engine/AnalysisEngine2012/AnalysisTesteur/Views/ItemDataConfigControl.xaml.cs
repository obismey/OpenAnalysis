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
    /// Logique d'interaction pour ItemDataConfig.xaml
    /// </summary>
    public partial class ItemDataConfigControl : UserControl
    {
        public ItemDataConfigControl()
        {
            InitializeComponent();
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            var ditem = this.DataContext as Models.DashboardItemImpls.DashboardDataItem;
            var col = (Analysis.ColumnData)e.Data.GetData(typeof(Analysis.ColumnData));
                 
            if (ditem != null && col != null)
            {
                if (sender == this.RowListbox )
                {
                    ViewModels.MainWindowViewModel.GetInstance().AddDimensionHandler(Analysis.DimensionLocation.Row, ditem, col);
                }
                if (sender == this.MeasureListbox)
                {
                    ViewModels.MainWindowViewModel.GetInstance().AddMeasureHandler(ditem, col);
                }

            }
        }
    }
}
