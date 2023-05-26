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

        public HomeViewModel()
        {
            this.HomeText = "Welcome User";
        }

        public string HomeText
        {
            set => SetProperty(nameof(HomeText), value);
            get => GetProperty<string>(nameof(HomeText));
        }

        public ICommand OpenSaveFolder => new RelayCommand(param =>
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(HomeViewModel)},{nameof(OpenSaveFolder)},\nEX :[{ex}]");
            }
        });
    }
}
