using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OKNet.App.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void SetValue<T>(ref T property, T value, [CallerMemberName]string propertyName = null)
        {
            if (property != null && property.Equals(value))
            {
                return;
            }
            property = value;
            OnPropertyChanged(propertyName);
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Refresh()
        {
        }

        public virtual void Cleanup()
        {
        }
    }
}