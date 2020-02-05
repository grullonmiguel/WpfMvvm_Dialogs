using System.Windows;

namespace WPF.UI.Dialogs.Service
{
    public interface IDialogWindow
    {
        object DataContext { get; set; }

        bool? DialogResult { get; set; }

        Window Owner { get; set; }

        void Close();

        bool? ShowDialog();
    }
}