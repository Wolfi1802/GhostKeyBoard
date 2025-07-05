using GhostKeyBoard.Helper;
using GhostKeyBoard.mvvm.Commands;
using GhostKeyBoard.Record;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace GhostKeyBoard.mvvm.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
            this.HomeText = ProjectTexts.WELCOME;
            this.ItemsSource = new ObservableCollection<RecordModel>();

            var savedMakros = HookService.Instance.SavedMakroList;

            foreach (var item in savedMakros)
            {
                this.ItemsSource.Add(ModelConverter.CreateRecordModel(item));
            }

        }



        public string HomeText
        {
            set => SetProperty(nameof(HomeText), value);
            get => GetProperty<string>(nameof(HomeText));
        }

        public ObservableCollection<RecordModel> ItemsSource
        {
            set => SetProperty(nameof(ItemsSource), value);
            get => GetProperty<ObservableCollection<RecordModel>>(nameof(ItemsSource));
        }

        public RecordModel SelectedRecord
        {
            set => SetProperty(nameof(SelectedRecord), value);
            get => GetProperty<RecordModel>(nameof(SelectedRecord));
        }

        private bool RemoveFromUi(RecordModel modelToRemove)
        {
            if (this.ItemsSource.Contains(modelToRemove))
            {
                this.ItemsSource.Remove(modelToRemove);

                return true;
            }
            else
                return false;
        }


        public ICommand DeleteCommand => new RelayCommand(param =>
        {
            try
            {
                bool successfullRemoved = false;
                string nameOfDeleted = default;

                if (this.SelectedRecord is not null)
                {
                    nameOfDeleted = this.SelectedRecord.Name;

                    successfullRemoved = HookService.Instance.Delete(this.SelectedRecord);

                    if (successfullRemoved)
                        successfullRemoved = this.RemoveFromUi(this.SelectedRecord);
                }
                if (successfullRemoved)
                {
                    MainWindowViewModel.SendUserMessageEvent?.Invoke(null, ProjectTexts.GetDeletetSuccessfull(nameOfDeleted));
                }
                else
                    MainWindowViewModel.SendUserMessageEvent?.Invoke(null, ProjectTexts.FUNCTION_DONT_WORK);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(HomeViewModel)},{nameof(DeleteCommand)},\nEX :[{ex}]");
            }
        });
    }
}
