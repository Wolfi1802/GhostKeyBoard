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

        public string RecordTime
        {
            set => SetProperty(nameof(RecordTime), value);
            get => GetProperty<string>(nameof(RecordTime));
        }

        public ICommand StartRecordCommand => new RelayCommand(param =>
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(RecordViewModel)},{nameof(StartRecordCommand)},\nEX :[{ex}]");
            }
        });

        public ICommand StopRecordCommand => new RelayCommand(param =>
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(RecordViewModel)},{nameof(StopRecordCommand)},\nEX :[{ex}]");
            }
        });
    }
}
