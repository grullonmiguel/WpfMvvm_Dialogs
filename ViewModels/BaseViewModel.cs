using System.ComponentModel;

namespace WPF.MVVM.Library
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}