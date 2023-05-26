using GhostKeyBoard.mvvm.Commands;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace GhostKeyBoard.mvvm.ViewModel
{
    public class RecordViewModel : ViewModelBase
    {

        public bool RecordAvialable
        {
            set => SetProperty(nameof(RecordAvialable), value);
            get => GetProperty<bool>(nameof(RecordAvialable));
        }

        public ICommand RecordCommand => new RelayCommand(param =>
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(HomeViewModel)},{nameof(RecordCommand)},\nEX :[{ex}]");
            }
        });
    }
}
