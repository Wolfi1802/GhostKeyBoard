using GhostKeyBoard.HookModel;
using GhostKeyBoard.mvvm.Commands;
using GhostKeyBoard.Record;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace GhostKeyBoard.mvvm.ViewModel
{
    public class RecordModel
    {
        public List<HookBase> ListOfActions { set; get; } = new List<HookBase>();
        public string Name { set; get; }
        public TimeSpan Time { set; get; }

        public bool IsRepeat { set; get; }

        private int repeatCount;
        public int RepeatCount { set { this.repeatCount = value; } get { return this.repeatCount; } }

        public ICommand StartPlayCommand => new RelayCommand(param =>
        {
            try
            {
                HookService.Instance.StartPlay(this);
                this.repeatCount--;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(RecordModel)},{nameof(StartPlayCommand)},\nEX :[{ex}]");
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
                Debug.WriteLine($"{nameof(RecordModel)},{nameof(StopPlayCommand)},\nEX :[{ex}]");
            }
        });

    }
}
