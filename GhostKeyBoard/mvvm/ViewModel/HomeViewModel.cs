using GhostKeyBoard.mvvm.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GhostKeyBoard.mvvm.ViewModel
{
    public class HomeViewModel : ViewModelBase
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
