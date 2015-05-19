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
    /// Logique d'interaction pour ListBoxSheetItemControl.xaml
    /// </summary>
    public partial class ListBoxSheetItemControl : UserControl
    {
        public ListBoxSheetItemControl()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewmodel = this.DataContext as Models.DashboardItemImpls.ListBoxSheetItem;
            var listbox = sender as ListBox ;

            if (viewmodel != null )
            {
                viewmodel.UpdateSelection(listbox.SelectedItems.Cast<Analysis.DimensionItem>());
            }
        }
    }
}
