using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using Infragistics.Windows.Ribbon;
using AnalysisTesteur.ViewModels;
using Infragistics.Windows.DockManager;
using AnalysisTesteur.Models;

namespace AnalysisTesteur.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : XamRibbonWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private MainWindowViewModel MainWindowViewModel;
        private void XamRibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.MainWindowViewModel =(MainWindowViewModel)this.DataContext;

            this.MainWindowViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(MainWindowViewModel_PropertyChanged);

            this.MainWindowViewModel.DashboardBookList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(DashboardBookList_CollectionChanged);

            this.DashboardSheetDocumentContentHost.ActiveDocumentChanged += new RoutedPropertyChangedEventHandler<ContentPane>(DashboardSheetDocumentContentHost_ActiveDocumentChanged);
        }

        void DashboardSheetDocumentContentHost_ActiveDocumentChanged(object sender, RoutedPropertyChangedEventArgs<ContentPane> e)
        {
            var sheet = this.DashboardSheetDocumentContentHost.ActiveDocument.DataContext as DashboardSheet;

            if (this.MainWindowViewModel.ActiveDashboardBook != null )
            {
                if (sheet == null) this.MainWindowViewModel.ActiveDashboardBook.ActiveDashboardSheet = null;

                if (this.MainWindowViewModel.ActiveDashboardBook.DashboardSheetList.Contains(sheet))
                {
                    this.MainWindowViewModel.ActiveDashboardBook.ActiveDashboardSheet = sheet;
                }


            }

        }

        void DashboardBookList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        this.ApplicationMenu.RecentItems.Add(item);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        this.ApplicationMenu.RecentItems.Remove(item);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    this.ApplicationMenu.RecentItems.Clear();
                    break;
                default:
                    break;
            }

        }

        void MainWindowViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ActiveDashboardBook")
            {
                this.DashboardSheetTabGroupPane.Items.Clear();

                if (this.MainWindowViewModel.ActiveDashboardBook != null)
                {
                    foreach (var item in this.MainWindowViewModel.ActiveDashboardBook.DashboardSheetList)
                    {
                        var sheetcontainer = new ContentPane();
                        sheetcontainer.AllowClose = false;
                        sheetcontainer.CloseButtonVisibility = System.Windows.Visibility.Collapsed;
                        sheetcontainer.Header = item.Caption;
                        sheetcontainer.DataContext = item;
                        sheetcontainer.Content = new SheetControl(item);
                        this.DashboardSheetTabGroupPane.Items.Add(sheetcontainer);
                    }
                    this.MainWindowViewModel.ActiveDashboardBook.DashboardSheetList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(DashboardSheetList_CollectionChanged);
                }
            }
        }

        void DashboardSheetList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (var item in  e.NewItems)
                    {
                        var sheetcontainer = new ContentPane();
                        sheetcontainer.AllowClose = false;
                        sheetcontainer.CloseButtonVisibility = System.Windows.Visibility.Collapsed;
                        sheetcontainer.Header = ((DashboardSheet)item).Caption;
                        sheetcontainer.DataContext = item;
                        sheetcontainer.Content = new SheetControl(((DashboardSheet)item));

                        this.DashboardSheetTabGroupPane.Items.Add(sheetcontainer);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        this.DashboardSheetTabGroupPane.Items.Remove(item);
                    }

                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    this.DashboardSheetTabGroupPane.Items.Clear();
                    break;
                default:
                    break;
            } 
        }

        private void ButtonTool_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var sheet =this.MainWindowViewModel.ActiveDashboardBook.ActiveDashboardSheet;

                sheet.DashboardItemList.Add(new AnalysisTesteur.Models.DashboardItemImpls.DashboardDataItem() { Caption = System.DateTime.Now.ToShortTimeString() });
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
