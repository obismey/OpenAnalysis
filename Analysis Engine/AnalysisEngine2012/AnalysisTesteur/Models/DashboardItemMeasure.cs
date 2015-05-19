using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalysisTesteur.Helpers;
using System.Windows.Input;

namespace AnalysisTesteur.Models
{
    public class DashboardItemMeasure : DashboardObject
    {
        public Analysis.SheetItemMeasure SheetItemMeasure { get ; set ; }

        public Analysis.MeasureAggregate MeasureAggregate { get { return this.SheetItemMeasure.ValueAggregate; } }

        public DashboardItemMeasure(Analysis.SheetItemMeasure measure)
        {
            // TODO: Complete member initialization
            this.SheetItemMeasure = measure;
        }

        public ICommand DeleteCommand { get { return new DelegateCommand(DeleteCommandHandler); } }
        private void DeleteCommandHandler()
        {
            ViewModels.MainWindowViewModel.GetInstance().DeleteMeasureHandler(this);
        }
        public ICommand ChangeAggregateCommand { get { return new DelegateCommand(ChangeAggregateCommandHandler); } }
        private void ChangeAggregateCommandHandler(object parameter)
        {
            ViewModels.MainWindowViewModel.GetInstance().ChangeAggregateHandler(this, parameter);

            OnPropertyChanged("MeasureAggregate");
        }

        public DashboardItemImpls.DashboardDataItem Owner { get; set; }
    }
}
