using MVVM.Library.Commands;
using MVVM.Library.ViewModels;
using WPF.UI.Dialogs;
using WPF.UI.Dialogs.Service;

namespace WPF.UI.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        #region Fields

        private static string ColorGray = "Gray";
        private static string ColorGreen = "Green";
        private static string ColorRed = "Red";
        private readonly IDialogService _dialogService;

        #endregion

        #region Properties

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }
        private string _title = "Alert";

        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }
        private string _message = "This is an Alert";

        public string OkBrush
        {
            get => _okBrush;
            set {  _okBrush = value; OnPropertyChanged(); }
        }
        private string _okBrush = ColorGray;

        public string CancelBrush
        {
            get => _cancelBrush; 
            set {  _cancelBrush = value; OnPropertyChanged(); }
        }
        private string _cancelBrush = ColorGray;

        public IActionCommand DisplayMessageCommand => new ActionCommand(x => DisplayMessage());

        #endregion

        #region Constructor

        public ShellViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        private bool CanExecuteAlert() 
            => !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Message);

        #endregion

        #region Methods

        private void DisplayMessage()
        {
            bool? result = _dialogService.ShowDialog(new DialogViewModel(Title, Message));
            if (!result.HasValue)
                return;

            SetOkCancelForeground(result.Value ? DialogResults.Yes : DialogResults.No);
        }

        private void SetOkCancelForeground(DialogResults state)
        {
            OkBrush = state == DialogResults.Yes ? ColorGreen : ColorGray;
            CancelBrush = state == DialogResults.No ? ColorRed : ColorGray;
        }

        #endregion
    }
}