using System;
using System.Collections.Generic;
using System.Windows;

namespace WPF.UI.Dialogs
{
    public class DialogService : IDialogService
    {
        #region Fields

        /// <summary>
        ///  Specify the owner of a dialog window
        /// </summary>
        private readonly Window _owner;

        #endregion

        #region Properties

        /// <summary>
        /// Maps view models to views
        /// </summary>
        public IDictionary<Type, Type> Mappings { get; }

        #endregion

        #region Constructor

        public DialogService(Window owner)
        {
            _owner = owner;
            Mappings = new Dictionary<Type, Type>();
        }

        #endregion

        #region IDialogService

        /// <summary>
        /// Store mappings of a view model to its associated view
        /// </summary>
        public void Register<TViewModel, TView>() where TViewModel : IDialogRequestClose
                                                  where TView : IDialog
        {
            // Make sure a view model is mapped to only one view
            if (Mappings.ContainsKey(typeof(TViewModel)))
            {
                throw new ArgumentException($"Type {typeof(TViewModel)} is already mapped to type {typeof(TView)}");
            }

            Mappings.Add(typeof(TViewModel), typeof(TView));
        }

        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose
        {
            var viewType = Mappings[typeof(TViewModel)];

            var dialogWindow = (IDialog)Activator.CreateInstance(viewType);

            viewModel.CloseRequested += CreateHandler(viewModel, dialogWindow);

            dialogWindow.DataContext = viewModel;

            dialogWindow.Owner = _owner;

            return dialogWindow.ShowDialog();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates an instance of EventHandler<DialogCloseRequestEventArgs> />
        /// </summary>
        private static EventHandler<DialogCloseRequestEventArgs> CreateHandler<TViewModel>(TViewModel viewModel, IDialog dialog) 
            where TViewModel : IDialogRequestClose
        {
            void handler(object sender, DialogCloseRequestEventArgs e)
            {
                // Unsubscribe from the event when event is called
                // once the view is closed, it is not needed anymore
                viewModel.CloseRequested -= handler;

                if (e.DialogResult.HasValue)
                {
                    dialog.DialogResult = e.DialogResult;
                }
                else
                {
                    dialog.Close();
                }
            }

            return handler;
        }

        #endregion
    }
}