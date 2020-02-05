using MVVM.Library.Commands;
using MVVM.Library.ViewModels;
using System;
using WPF.UI.Dialogs.Service;

namespace WPF.UI.ViewModels
{
    public class DialogViewModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestEventArgs> CloseRequested;

        public string Title { get; }

        public string Message { get; }

        public IActionCommand OkCommand { get; }

        public IActionCommand CancelCommand { get; }

        public DialogViewModel(string title, string message)
        {
            Title = title;
            Message = message;
            OkCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestEventArgs(false)));
        }
    }
}