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
    /// Logique d'interaction pour UserContr.xaml
    /// </summary>
    public partial class SheetControl : UserControl
    {
        private Models.DashboardSheet DashboardSheet;

        public SheetControl()
        {
            InitializeComponent();
        }

        public SheetControl(Models.DashboardSheet item)
        {
            InitializeComponent();

            this.DashboardSheet = item;

            this.DataContext = item;
        }
    }
}
