using System;

namespace WPF.UI.Dialogs.Service
{
    public interface IDialogRequestClose
    {
        event EventHandler<DialogCloseRequestEventArgs> CloseRequested;
    }
}