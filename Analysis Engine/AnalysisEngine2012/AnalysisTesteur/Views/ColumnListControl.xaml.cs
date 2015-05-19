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
    /// Logique d'interaction pour ColumnListControl.xaml
    /// </summary>
    public partial class ColumnListControl : UserControl
    {
        public ColumnListControl()
        {
            InitializeComponent();
        }

        void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var brd = sender as Border;

            DragDrop.DoDragDrop(brd, brd.DataContext, DragDropEffects.Copy);
        }
    }
}
