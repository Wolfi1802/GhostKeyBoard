using GhostKeyBoard.mvvm;
using GhostKeyBoard.mvvm.Commands;
using GhostKeyBoard.mvvm.ViewModel;
using GhostKeyBoard.PVersion;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace GhostKeyBoard
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string VersionTitle { get { return $"GhostKeyBoard {new ProjectVersion().GetVersion()}"; } }

        public ViewModelBase CurrentPage
        {
            set => SetProperty(nameof(CurrentPage), value);
            get => GetProperty<ViewModelBase>(nameof(CurrentPage));
        }

        public string GlobalUserMessage
        {
            set => SetProperty(nameof(GlobalUserMessage), value);
            get => GetProperty<string>(nameof(GlobalUserMessage));
        }

        public static EventHandler ClosePageEvent;
        public static EventHandler<string> SendUserMessageEvent;
        public HomeViewModel HomeViewModel = new HomeViewModel();
        public RecordViewModel RecordViewModel = new RecordViewModel();

        public MainWindowViewModel()
        {
            this.CurrentPage = this.HomeViewModel;

            ClosePageEvent += (o, e) =>
            {
                this.ShowHomeCommand.Execute(null);
            };

            SendUserMessageEvent += (o, e) =>
            {
                this.GlobalUserMessage = e;
            };
        }

        public ICommand ShowRecordCommand => new RelayCommand(param =>
        {
            try
            {
                this.CurrentPage = this.RecordViewModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(MainWindowViewModel)},{nameof(ShowRecordCommand)},\nEX :[{ex}]");
            }
        });

        public ICommand ShowHomeCommand => new RelayCommand(param =>
        {
            try
            {
                this.CurrentPage = this.HomeViewModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(MainWindowViewModel)},{nameof(ShowHomeCommand)},\nEX :[{ex}]");
            }
        });
    }
}
