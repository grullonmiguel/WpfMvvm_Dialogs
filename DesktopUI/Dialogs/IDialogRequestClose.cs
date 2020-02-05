using System;

namespace WPF.UI.Dialogs
{
    public interface IDialogRequestClose
    {
        event EventHandler<DialogCloseRequestEventArgs> CloseRequested;
    }
}