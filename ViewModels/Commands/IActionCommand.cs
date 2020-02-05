using System;

namespace MVVM.Library.Commands
{
    public interface IActionCommand
    {
        event EventHandler CanExecuteChanged;

        bool CanExecute(object parameter);
        void Execute();
        void Execute(object parameter);
        void RaiseCanExecuteChanged();
    }
}