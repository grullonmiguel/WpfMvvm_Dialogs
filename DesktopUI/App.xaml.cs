using System.Windows;
using WPF.UI.Dialogs.Service;
using WPF.UI.ViewModels;
using WPF.UI.Views;

namespace DesktopUI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var dialogService = new DialogService(MainWindow);
            dialogService.Register<DialogViewModel, DialogWindow>();

            var view = new ShellView { DataContext = new ShellViewModel(dialogService) };
            view.ShowDialog();
        }
    }
}