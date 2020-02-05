using MVVM.Library.Commands;
using MVVM.Library.ViewModels;
using System;
using WPF.UI.Dialogs;

namespace WPF.UI.ViewModels
{
    public class DialogViewModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestEventArgs> CloseRequested;

        public string Message { get; }

        public IActionCommand OkCommand { get; }

        public IActionCommand CancelCommand { get; }

        public DialogViewModel(string message)
        {
            Message = message;
            OkCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestEventArgs(false)));
        }
    }
}