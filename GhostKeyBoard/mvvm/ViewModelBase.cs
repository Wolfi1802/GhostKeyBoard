using System.Collections.Generic;
using System.ComponentModel;

namespace GhostKeyBoard.mvvm
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public const string DEFAULT_STRING = "";
        public event PropertyChangedEventHandler PropertyChanged;
        private Dictionary<string, object> Storage { get; set; } = new Dictionary<string, object>();

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public T GetProperty<T>(string propertyName, T fallback = default)
        {
            T result = fallback;
            if (this.Storage.ContainsKey(propertyName))
                result = (T)this.Storage[propertyName];
            else
                this.SetProperty(propertyName, fallback);

            return result;
        }

        public void SetProperty<T>(string propertyName, T value)
        {
            if (!this.Storage.ContainsKey(propertyName))
                this.Storage.Add(propertyName, value);
            else
                this.Storage[propertyName] = value;

            this.OnPropertyChanged(propertyName);
        }
    }
}
