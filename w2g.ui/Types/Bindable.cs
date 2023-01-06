using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace w2g.ui.Types
{
    public class Bindable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetProperty<T>(ref T target, T value, [CallerMemberName] string propertyName = null)
        {
            target = value;
            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
