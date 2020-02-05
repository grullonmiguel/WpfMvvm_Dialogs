using MVVM.Library.Commands;
using MVVM.Library.ViewModels;
using System;
using WPF.UI.Dialogs;
using WPF.UI.Dialogs.Service;

namespace WPF.UI.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        #region Fields

        private string _title = "Alert";
        private string _message = "This is an Alert";
        private string _okBrush;
        private string _cancelBrush;
        private readonly string ColorGray = "Gray";
        private readonly string ColorGreen = "Green";
        private readonly string ColorRed = "Red";
        private readonly IDialogService _dialogService;

        #endregion

        #region Properties

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public IActionCommand DisplayMessageCommand { get; }

        public string OkBrush
        {
            get => _okBrush;
            set {  _okBrush = value; OnPropertyChanged(); }
        }

        public string CancelBrush
        {
            get => _cancelBrush; 
            set {  _cancelBrush = value; OnPropertyChanged(); }
        }

        #endregion

        #region Constructor

        public ShellViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            SetBrushColor(DialogResults.Undefined);
            DisplayMessageCommand = new ActionCommand(x => DisplayMessage());
        }

        private bool CanExecuteAlert()
        {            
            return !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Message);
        }

        #endregion

        #region Methods

        private void SetBrushColor(DialogResults state)
        {
            OkBrush = state == DialogResults.Yes ? ColorGreen : ColorGray;
            CancelBrush = state == DialogResults.No ? ColorRed : ColorGray;
        }

        private void DisplayMessage()
        {
            SetBrushColor(DialogResults.Undefined);

            var viewModel = new DialogViewModel(Title, Message);

            bool? result = _dialogService.ShowDialog(viewModel);

            if (result.HasValue)
            {
                if (result.Value)
                {
                    SetBrushColor(DialogResults.Yes);
                }
                else
                {
                    SetBrushColor(DialogResults.No);
                }
            }
        }

        #endregion
    }
}