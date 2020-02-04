using System;
using System.Windows.Input;

namespace MVVM.Library
{
    public class ActionCommand : ICommand
    {
        #region Fields

        private bool isExecuting;
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute; 

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="action">The action to invoke on command.</param>
        public ActionCommand(Action<object> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="execute">The action to invoke on command.</param>
        /// <param name="canExecute">The predicate that determines if the action can be invoked.</param>
        public ActionCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute), @"You must specify an Action<T>.");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        public event EventHandler CanExecuteChanged;

        //// <summary>
        //// Occurs when the <see cref="System.Windows.Input.CommandManager"/> detects conditions that might change the ability of a command to execute.
        //// </summary>
        //public event EventHandler CanExecuteChanged
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

        /// <summary>
        /// Determines whether the command can execute.
        /// </summary>
        /// <param name="parameter">A custom parameter object.</param>
        /// <returns>
        ///     Returns true if the command can execute, otherwise returns false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) 
                return isExecuting == false;

            return !isExecuting && _canExecute(parameter);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            Execute(null);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">A custom parameter object.</param>
        public void Execute(object parameter)
        {
            try
            {
                isExecuting = true;
                OnCanExecuteChanged();
                _execute(parameter);
            }
            catch (Exception)
            {
                isExecuting = false;
                OnCanExecuteChanged();
            }
        }

        //Method to raise event
        private void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        #endregion
    }
}