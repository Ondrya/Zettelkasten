using System.Windows;

namespace Zettelkasten.DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var msg = $"Произошла непредвиденная ошибка{Environment.NewLine}{e.Exception.Message}{Environment.NewLine}{e.Exception.StackTrace}";
            MessageBox.Show(msg, "Непредвиденная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

}
