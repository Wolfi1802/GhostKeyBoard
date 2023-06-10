using GhostKeyBoard.Helper;
using GhostKeyBoard.HookModel;
using GhostKeyBoard.Record;
using GhostKeyBoard.SaveData;
using System.Collections.Generic;

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
                this.ItemsSource.Add(ModelConverter.CreateRecordModel(item));
            }

            if (this.ItemsSource.Count == 0)
                MakroFileService.Load();
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
    }
}
