using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace stdwpf
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private string textValue = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ButtonCommand { get; }

        public string TextValue
        {
            get
            {
                return this.textValue;
            }
            set
            {
                if (value != this.textValue)
                {
                    this.textValue = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public MainWindowViewModel()
        {
            this.TextValue = "Hello, MVVM!!";
            this.ButtonCommand = new DelegateCommand(
                () => this.TextValue = "Clicked!!",
                () => !string.IsNullOrEmpty(this.TextValue));
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
