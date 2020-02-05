using System;

namespace WPF.UI.Dialogs
{
    public class DialogCloseRequestEventArgs : EventArgs
    {
        public bool? DialogResult { get; }

        public DialogCloseRequestEventArgs(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}