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
        #region Propertys

        #region UI Propertys

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

        public bool HomeButtonIsEnabled
        {
            set => SetProperty(nameof(HomeButtonIsEnabled), value);
            get => GetProperty<bool>(nameof(HomeButtonIsEnabled));
        }

        public bool RecordButtonIsEnabled
        {
            set => SetProperty(nameof(RecordButtonIsEnabled), value);
            get => GetProperty<bool>(nameof(RecordButtonIsEnabled));
        }

        #endregion

        public static EventHandler ClosePageEvent;
        public static EventHandler<string> SendUserMessageEvent;
        public HomeViewModel HomeViewModel = new HomeViewModel();
        public RecordViewModel RecordViewModel = new RecordViewModel();

        #endregion
        public MainWindowViewModel()
        {
            this.ShowHomeCommand?.Execute(null);

            ClosePageEvent += (o, e) =>
            {
                this.ShowHomeCommand.Execute(null);
            };

            SendUserMessageEvent += (o, e) =>
            {
                this.GlobalUserMessage = e;
            };
        }

        #region Methoden

        /// <summary>
        /// Dieser button <paramref name="propertyName"/> ist aktiv, ergo muss er inaktiv geschaltet werden und alle anderen aktiv
        /// </summary>
        /// <param name="propertyName"></param>
        private void SetButtonIsEnabled(string propertyName)
        {
            this.HomeButtonIsEnabled = nameof(HomeButtonIsEnabled).Equals(propertyName) ? false : true;
            this.RecordButtonIsEnabled = nameof(RecordButtonIsEnabled).Equals(propertyName) ? false : true;
        }

        #endregion

        #region Commands

        public ICommand ShowRecordCommand => new RelayCommand(param =>
        {
            try
            {
                this.SetButtonIsEnabled(nameof(this.RecordButtonIsEnabled));
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
                this.SetButtonIsEnabled(nameof(this.HomeButtonIsEnabled));
                this.CurrentPage = this.HomeViewModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(MainWindowViewModel)},{nameof(ShowHomeCommand)},\nEX :[{ex}]");
            }
        });

        #endregion
    }
}
