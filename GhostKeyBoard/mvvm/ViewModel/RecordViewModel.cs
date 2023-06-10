using GhostKeyBoard.mvvm.Commands;
using GhostKeyBoard.Record;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace GhostKeyBoard.mvvm.ViewModel
{
    public class RecordViewModel : ViewModelBase
    {
        public RecordViewModel()
        {
            Debug.WriteLine("INIT TIMER");
            TimerService.Instance.OnTimerTickEvent += Event;
        }

        ~RecordViewModel()
        {
            Debug.WriteLine("DESPO TIMER");
            TimerService.Instance.OnTimerTickEvent -= Event;
        }

        private void Event<TimeSpan>(object o, TimeSpan time)
        {
            if (time != null)
            {
                this.RecordTime = $"{time}".Substring(0, 8);//8 is seconds, 10 is miliseconds
            }
        }

        public bool RecordAvialable
        {
            set => SetProperty(nameof(RecordAvialable), value);
            get => GetProperty<bool>(nameof(RecordAvialable));
        }

        public string RecordName
        {
            set => SetProperty(nameof(RecordName), value);
            get => GetProperty<string>(nameof(RecordName));
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
                TimerService.Instance.Start();
                HookService.Instance.StartRecord();
                RecordAvialable = false;
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
                TimerService.Instance.Stop();
                HookService.Instance.StopRecord();
                RecordAvialable = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(RecordViewModel)},{nameof(StopRecordCommand)},\nEX :[{ex}]");
            }
        });

        public ICommand SaveRecordCommand => new RelayCommand(param =>
        {
            try
            {
                HookService.Instance.Save(this.RecordName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(RecordViewModel)},{nameof(SaveRecordCommand)},\nEX :[{ex}]");
            }
        });
    }
}
