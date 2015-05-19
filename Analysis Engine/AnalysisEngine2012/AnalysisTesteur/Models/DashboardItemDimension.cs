using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalysisTesteur.Helpers;
using System.Windows.Input;

namespace AnalysisTesteur.Models
{
    public class DashboardItemDimension : DashboardObject
    {
        public DashboardItemDimension(Analysis.SheetItemDimension col)
        {
            this.SheetItemDimension = col;
        }

        public DashboardItemDimension()
        {

        }

        public ICommand DeleteCommand { get { return new DelegateCommand(DeleteCommandHandler); } }
        private void DeleteCommandHandler()
        {
            ViewModels.MainWindowViewModel.GetInstance().DeleteDimensionHandler(this);
        }

        public Analysis.SheetItemDimension SheetItemDimension { get;  set; }

        public System.Collections.ObjectModel.ObservableCollection<DashboardItemDimension> Container { get; set; }

        public DashboardItemImpls.DashboardDataItem Owner { get; set; }
    }
}
