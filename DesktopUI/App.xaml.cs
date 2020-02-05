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

            IDialogService dialogService = new DialogService(MainWindow);

            dialogService.Register<DialogViewModel, DialogWindow>();

            DisplayMainView(dialogService);
        }

        private void DisplayMainView(IDialogService dialogService)
        {
            var viewModel = new ShellViewModel(dialogService);

            var view = new ShellView{DataContext = viewModel};

            view.ShowDialog();
        }
    }
}
