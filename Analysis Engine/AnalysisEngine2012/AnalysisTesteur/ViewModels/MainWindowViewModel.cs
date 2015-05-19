using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AnalysisTesteur.Helpers;
using AnalysisTesteur.Models;
using System.Linq;

namespace AnalysisTesteur.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Properties

        private DashboardBook _ActiveDashboardBook;
        public DashboardBook ActiveDashboardBook
        {
            get
            {
                return _ActiveDashboardBook;
            }
            set
            {
                _ActiveDashboardBook = value;
                OnPropertyChanged("ActiveDashboardBook");
                OnPropertyChanged("Title");
                CommandManager.InvalidateRequerySuggested();
            }
        }


        private ObservableCollection<DashboardBook> _DashboardBookList = new ObservableCollection<DashboardBook>();
        public ObservableCollection<DashboardBook> DashboardBookList
        {
            get
            {
                return _DashboardBookList;
            }
        }

        public string Title
        {
            get
            {
                return ActiveDashboardBook == null ? "Aucun dashboard actif" : "Dashboard actif : " + ActiveDashboardBook.Name;
            }
        }
        #endregion

        #region Commands

        public ICommand CreateDashboardBook { get { return new DelegateCommand(CreateDashboardBookHandler); } }
        private void CreateDashboardBookHandler()
        {
            var name = Microsoft.VisualBasic.Interaction.InputBox("Nom du dashboard");

            var serviceclient = new Analysis.AnalysisService();

            var dashresult = serviceclient.CreateBook("default", name);

            var sheetresult = serviceclient.CreateSheet("default", name, "DefaultSheet");

            var dash = new DashboardBook();
            dash.Name = name;

            var sheet = new DashboardSheet();
            sheet.Name = "DefaultSheet";
            sheet.Caption = "Feuille 1";

            dash.DashboardSheetList.Add(sheet);
            dash.ActiveDashboardSheet = sheet;

            this.DashboardBookList.Add(dash);

            this.ActiveDashboardBook = dash;
        }
        public ICommand OpenDashboardBook { get { return new DelegateCommand(OpenDashboardBookHandler); } }
        private void OpenDashboardBookHandler()
        {
            throw new NotImplementedException();
        }
        public ICommand CloseDashboardBook { get { return new DelegateCommand(CloseDashboardBookHandler); } }
        private void CloseDashboardBookHandler()
        {
            throw new NotImplementedException();
        }
        public ICommand UpdateDashboardBookList { get { return new DelegateCommand(UpdateDashboardBookListHandler); } }
        private void UpdateDashboardBookListHandler()
        {

            var serviceclient = new Analysis.AnalysisService();

            var result = serviceclient.GetBooks("default");

            if (result == null) return;

            var dashlist = from str in result select new DashboardBook() { Name = str };

            this.DashboardBookList.Clear();

            foreach (var item in dashlist)
            {
                this.DashboardBookList.Add(item);
            }


        }

        public ICommand ReloadData { get { return new DelegateCommand(ReloadDataHandler); } }
        private void ReloadDataHandler()
        {
            if (this.ActiveDashboardBook == null)
            {
                System.Windows.MessageBox.Show("Charger un dashboard avant d'effectuer cette opération");

                return;
            }

            var serviceclient = new Analysis.AnalysisService();

            var result = serviceclient.ReloadData("default");

            System.Windows.MessageBox.Show(result);

            this.ActiveDashboardBook.ColumnList.Clear();

            var cols = serviceclient.GetColumnList("default");

            foreach (var item in cols.OrderBy((c) => c.Name))
            {
                this.ActiveDashboardBook.ColumnList.Add(item);
            }

        }


        public ICommand InsertSheetItem { get { return new DelegateCommand(InsertSheetItemHandler, CanInsertSheetItemHandler); } }
        private void InsertSheetItemHandler(object itemtypename)
        {
            var itemtype = Type.GetType("AnalysisTesteur.Models.DashboardItemImpls." + itemtypename);

            var sheet = this.ActiveDashboardBook.ActiveDashboardSheet;

            var iteminstance = (DashboardItem)Activator.CreateInstance(itemtype);

            iteminstance.Caption = itemtypename.ToString();

            sheet.DashboardItemList.Add(iteminstance);

            var serviceclient = new Analysis.AnalysisService();

            var shitem = serviceclient.CreateSheetItem("default", this.ActiveDashboardBook.Name, this.ActiveDashboardBook.ActiveDashboardSheet.Name);
            serviceclient.SetSheetItemType(shitem, itemtype.FullName);
            iteminstance.SheetItem = shitem;

            //TODO : implementation du undo redo cote serveur
            sheet.UndoRedo.Push(
                () => sheet.DashboardItemList.Remove(iteminstance),
                () => sheet.DashboardItemList.Add(iteminstance),
                "Insert " + itemtypename);
        }
        private bool CanInsertSheetItemHandler(object itemtypename)
        {
            if (this.ActiveDashboardBook == null) return false;

            if (this.ActiveDashboardBook.ActiveDashboardSheet == null) return false;

            var itemtype = Type.GetType("AnalysisTesteur.Models.DashboardItemImpls." + itemtypename);

            return itemtype != null;
        }

        public ICommand UndoRedoCommand { get { return new DelegateCommand(UndoRedoCommandHandler, CanUndoRedoCommandHandler); } }
        private void UndoRedoCommandHandler(object actiontype)
        {
            var sheet = this.ActiveDashboardBook.ActiveDashboardSheet;

            if ((string)actiontype == "Undo") sheet.UndoRedo.Undo();

            if ((string)actiontype == "Redo") sheet.UndoRedo.Redo();
        }
        private bool CanUndoRedoCommandHandler(object actiontype)
        {
            if (this.ActiveDashboardBook == null) return false;

            if (this.ActiveDashboardBook.ActiveDashboardSheet == null) return false;

            if ((string)actiontype == "Undo") return this.ActiveDashboardBook.ActiveDashboardSheet.UndoRedo.CanUndo;

            if ((string)actiontype == "Redo") return this.ActiveDashboardBook.ActiveDashboardSheet.UndoRedo.CanRedo;

            return true;
        }

        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
        }
        #endregion


        public static MainWindowViewModel GetInstance()
        {
            var mainwindow = App.Current.MainWindow as Views.MainWindow;

            if (mainwindow == null) return null;

            var result = mainwindow.DataContext as MainWindowViewModel;

            return result;
        }


        internal void AddDimensionHandler(Analysis.DimensionLocation dimensionLocation, Models.DashboardItemImpls.DashboardDataItem ditem, Analysis.ColumnData col)
        {
            var destcontainer = dimensionLocation == Analysis.DimensionLocation.Row ? ditem.RowDimensionList :
                dimensionLocation == Analysis.DimensionLocation.Column ? ditem.ColumnDimensionList :
                dimensionLocation == Analysis.DimensionLocation.Serie ? ditem.SerieDimensionList : null;

            var serviceclient = new Analysis.AnalysisService();

            var dimension = new Analysis.SheetItemDimension();
            dimension.SheetItem = ditem.SheetItem;
            dimension.Column = col.Name;

            if (serviceclient.UpdateDimensionList(ditem.SheetItem, Analysis.UpdateAction.Insert, dimension))
            {
                var localdimension = new Models.DashboardItemDimension(dimension);
                localdimension.Owner = ditem;
                localdimension.Container = destcontainer;
                destcontainer.Add(localdimension);
                ditem.Update();

                var sheet = this.ActiveDashboardBook.ActiveDashboardSheet;

                //TODO : implementation du undo redo cote serveur
                sheet.UndoRedo.Push(
                    () => { destcontainer.Remove(localdimension); ditem.Update(); },
                    () => { destcontainer.Add(localdimension); ditem.Update(); },
                    "Insert " + col.Name + " into " + dimensionLocation.ToString());
            }

        }
        internal void AddMeasureHandler(Models.DashboardItemImpls.DashboardDataItem ditem, Analysis.ColumnData col)
        {

            var serviceclient = new Analysis.AnalysisService();

            var measure = new Analysis.SheetItemMeasure();
            measure.SheetItem = ditem.SheetItem;
            measure.ValueColumn = col.Name;
            measure.ValueAggregate = Analysis.MeasureAggregate.Sum;

            if (serviceclient.UpdateMeasureList(ditem.SheetItem, Analysis.UpdateAction.Insert, measure))
            {
                var localmeasure = new Models.DashboardItemMeasure(measure);
                localmeasure.Owner = ditem;
                ditem.MeasureList.Add(localmeasure);
                ditem.Update();

                var sheet = this.ActiveDashboardBook.ActiveDashboardSheet;

                //TODO : implementation du undo redo cote serveur
                sheet.UndoRedo.Push(
                    () => { ditem.MeasureList.Remove(localmeasure); ditem.Update(); },
                    () => { ditem.MeasureList.Add(localmeasure); ditem.Update(); },
                    "Insert Sum(" + col.Name + ")");
            }

        }
        internal void DeleteDimensionHandler(DashboardItemDimension dashboardItemDimension)
        {
            var serviceclient = new Analysis.AnalysisService();

            if (serviceclient.UpdateDimensionList(dashboardItemDimension.SheetItemDimension.SheetItem, Analysis.UpdateAction.Remove, dashboardItemDimension.SheetItemDimension))
            {
                dashboardItemDimension.Container.Remove(dashboardItemDimension);
                dashboardItemDimension.Owner.Update();

                var sheet = this.ActiveDashboardBook.ActiveDashboardSheet;

                //TODO : implementation du undo redo cote serveur
                sheet.UndoRedo.Push(
                    () => { dashboardItemDimension.Container.Add(dashboardItemDimension); dashboardItemDimension.Owner.Update(); },
                    () => { dashboardItemDimension.Container.Remove(dashboardItemDimension); dashboardItemDimension.Owner.Update(); },
                    "Delete " + dashboardItemDimension.SheetItemDimension.Column + " from " + dashboardItemDimension.SheetItemDimension.Location.ToString());
            }
        }

        internal void DeleteMeasureHandler(DashboardItemMeasure dashboardItemMeasure)
        {
            var serviceclient = new Analysis.AnalysisService();

            if (serviceclient.UpdateMeasureList(dashboardItemMeasure.SheetItemMeasure.SheetItem, Analysis.UpdateAction.Remove, dashboardItemMeasure.SheetItemMeasure))
            {
                dashboardItemMeasure.Owner.MeasureList.Remove(dashboardItemMeasure);
                dashboardItemMeasure.Owner.Update();

                var sheet = this.ActiveDashboardBook.ActiveDashboardSheet;

                //TODO : implementation du undo redo cote serveur
                sheet.UndoRedo.Push(
                    () => { dashboardItemMeasure.Owner.MeasureList.Add(dashboardItemMeasure); dashboardItemMeasure.Owner.Update(); },
                    () => { dashboardItemMeasure.Owner.MeasureList.Remove(dashboardItemMeasure); dashboardItemMeasure.Owner.Update(); },
                    "Delete " + dashboardItemMeasure.SheetItemMeasure.ValueAggregate + "(" + dashboardItemMeasure.SheetItemMeasure.ValueColumn + ")");
            }
        }

        internal void ChangeAggregateHandler(DashboardItemMeasure dashboardItemMeasure, object parameter)
        {
            var serviceclient = new Analysis.AnalysisService();

            var agg = (Analysis.MeasureAggregate)Enum.Parse(typeof(Analysis.MeasureAggregate), (string)parameter);
            var oldagg = dashboardItemMeasure.SheetItemMeasure.ValueAggregate;
            dashboardItemMeasure.SheetItemMeasure.ValueAggregate = agg ;

            if (serviceclient.UpdateMeasure(dashboardItemMeasure.SheetItemMeasure, new string[]{"ValueAggregate"}))
            {
                dashboardItemMeasure.Owner.Update();
                var sheet = this.ActiveDashboardBook.ActiveDashboardSheet;

                //TODO : implementation du undo redo cote serveur
                sheet.UndoRedo.Push(
                    () => { dashboardItemMeasure.SheetItemMeasure.ValueAggregate = oldagg;  dashboardItemMeasure.Owner.Update(); },
                    () => { dashboardItemMeasure.SheetItemMeasure.ValueAggregate = agg; dashboardItemMeasure.Owner.Update(); },
                    "Delete " + dashboardItemMeasure.SheetItemMeasure.ValueAggregate + "(" + dashboardItemMeasure.SheetItemMeasure.ValueColumn + ")");

            }
            else 
            {
                dashboardItemMeasure.SheetItemMeasure.ValueAggregate = oldagg;
            }
        }

    }
}