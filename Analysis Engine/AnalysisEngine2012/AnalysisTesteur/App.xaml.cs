using System.Web.Services.Protocols;
using System.Windows;

namespace AnalysisTesteur
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is SoapException)
            {
                MessageBox.Show(e.Exception.ToString());
                e.Handled = true;
            }
        }
    }
}
