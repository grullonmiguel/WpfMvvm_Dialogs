using MVVM.Library.Commands;
using MVVM.Library.ViewModels;
using WPF.UI.Dialogs;

namespace WPF.UI.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        #region Fields

        private string _okBrush;
        private string _cancelBrush;
        private readonly string ColorGray = "Gray";
        private readonly string ColorGreen = "Green";
        private readonly string ColorRed = "Red";
        private readonly IDialogService _dialogService;

        #endregion

        #region Properties

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
            SetBrushColor(DialogResult.Default);
            DisplayMessageCommand = new ActionCommand(x => DisplayMessage());
        }

        #endregion

        #region Methods

        private void SetBrushColor(DialogResult state)
        {
            OkBrush = state == DialogResult.Ok ? ColorGreen : ColorGray;
            CancelBrush = state == DialogResult.Cancel ? ColorRed : ColorGray;
        }

        private void DisplayMessage()
        {
            SetBrushColor(DialogResult.Default);

            var viewModel = new DialogViewModel("Hello World");

            bool? result = _dialogService.ShowDialog(viewModel);

            if (result.HasValue)
            {
                if (result.Value)
                {
                    SetBrushColor(DialogResult.Ok);
                }
                else
                {
                    SetBrushColor(DialogResult.Cancel);
                }
            }
        }

        #endregion
    }
}