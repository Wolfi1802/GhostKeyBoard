using GhostKeyBoard.mvvm.Commands;
using GhostKeyBoard.Record;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Xml.Linq;

namespace GhostKeyBoard.mvvm.ViewModel
{
    public class RecordViewModel : ViewModelBase
    {
        public RecordViewModel()
        {
            Debug.WriteLine("INIT TIMER");
            TimerService.Instance.OnTimerTickEvent += Event;
            this.StartIsEnabled = true;
            this.StopIsEnabled = false;
            this.SaveIsEnabled = false;
            this.RecordTime = default;
        }

        ~RecordViewModel()
        {
            Debug.WriteLine("DESPO TIMER");
            TimerService.Instance.OnTimerTickEvent -= Event;
        }

        private void Event<TimeSpan>(object o, TimeSpan time)
        {
            if (time is not null)
                this.RecordTime = $"{time}".Substring(0, 8);//8 is seconds, 10 is miliseconds
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

        public bool StartIsEnabled
        {
            set => SetProperty(nameof(StartIsEnabled), value);
            get => GetProperty<bool>(nameof(StartIsEnabled));
        }

        public bool StopIsEnabled
        {
            set => SetProperty(nameof(StopIsEnabled), value);
            get => GetProperty<bool>(nameof(StopIsEnabled));
        }

        public bool SaveIsEnabled
        {
            set => SetProperty(nameof(SaveIsEnabled), value);
            get => GetProperty<bool>(nameof(SaveIsEnabled));
        }

        public ICommand StartRecordCommand => new RelayCommand(param =>
        {
            try
            {
                TimerService.Instance.Start();
                HookService.Instance.StartRecord();
                this.StartIsEnabled = false;
                this.StopIsEnabled = true;
                this.SaveIsEnabled = false;
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

                this.StartIsEnabled = true;
                this.StopIsEnabled = false;
                this.SaveIsEnabled = true;
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
                if (string.IsNullOrWhiteSpace(this.RecordName))
                {
                    MainWindowViewModel.SendUserMessageEvent?.Invoke(null, $"Dein Makro Name ist leer. Er Muss Buchstaben, Zahlen oder Zeichen enthalten.");
                }
                else
                {
                    this.SaveIsEnabled = false;
                    this.RecordTime = default;
                    HookService.Instance.Save(this.RecordName);
                    this.RecordName = default;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(RecordViewModel)},{nameof(SaveRecordCommand)},\nEX :[{ex}]");
            }
        });
    }
}
