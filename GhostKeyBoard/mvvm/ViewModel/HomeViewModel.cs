using GhostKeyBoard.mvvm.Commands;
using GhostKeyBoard.Record;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace GhostKeyBoard.mvvm.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
            this.HomeText = "Welcome User";
            this.ItemsSource = new List<RecordModel>();

            foreach (var item in HookService.Instance.SavedMakroList)
            {
                RecordModel model = new RecordModel();
                model.Time = item.Key[item.Key.Count - 1].Time;
                model.Name = item.Value;
                model.ListOfActions = item.Key;

               this.ItemsSource.Add(model);
            }
        }

        public string HomeText
        {
            set => SetProperty(nameof(HomeText), value);
            get => GetProperty<string>(nameof(HomeText));
        }

        public List<RecordModel> ItemsSource
        {
            set => SetProperty(nameof(ItemsSource), value);
            get => GetProperty<List<RecordModel>>(nameof(ItemsSource));
        }

        public RecordModel SelectedRecord
        {
            set => SetProperty(nameof(SelectedRecord), value);
            get => GetProperty<RecordModel>(nameof(SelectedRecord));
        }

        public ICommand StartPlayCommand => new RelayCommand(param =>
        {
            try
            {
                HookService.Instance.StartPlay(SelectedRecord.ListOfActions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(RecordViewModel)},{nameof(StartPlayCommand)},\nEX :[{ex}]");
            }
        });


        public ICommand StopPlayCommand => new RelayCommand(param =>
        {
            try
            {
                HookService.Instance.StopPlay();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(RecordViewModel)},{nameof(StopPlayCommand)},\nEX :[{ex}]");
            }
        });
    }
}
