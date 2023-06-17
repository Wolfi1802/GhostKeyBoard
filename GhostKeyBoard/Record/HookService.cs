using GhostKeyBoard.DLLEvents;
using GhostKeyBoard.Enum;
using GhostKeyBoard.HookModel;
using GhostKeyBoard.mvvm.ViewModel;
using GhostKeyBoard.SaveData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GhostKeyBoard.Record
{
    public class HookService
    {
        public static HookService Instance
        {
            get
            {
                if (singelton == null)
                    singelton = new HookService();
                return singelton;
            }
        }
        private static HookService singelton;

        /// <summary>
        /// Alle gespeicherten Makros zur Runtime
        /// </summary>
        public Dictionary<List<HookBase>, string> SavedMakroList = new Dictionary<List<HookBase>, string>();
        /// <summary>
        /// wird ein makro aufgenommen oder es passiert nichts ist der Wert false, wird er abgespielt ist er true
        /// </summary>
        public bool IsPlay { get; private set; }

        private MouseEvents mouseEvents = new MouseEvents();
        private TimerService TimerService { get { return TimerService.Instance; } }
        private List<HookBase> HookList = new List<HookBase>();
        private object hookObject = new object();
        private RecordModel model;

        private HookService()
        {
        }

        public void Save(string name)
        {
            var temp = new List<HookBase>(HookList);
            var tempPair = new KeyValuePair<List<HookBase>, string>(temp, name);

            this.SavedMakroList.Add(temp, name);
            MakroFileService.Save(this.SavedMakroList);
            MainWindowViewModel.SendUserMessageEvent?.Invoke(null, $"{name} wurde erfolgreich gespeichert");
        }

        public void StartRecord()
        {
            this.HookList.Clear();
            this.mouseEvents.RaiseKeyBoardHook += OnKeyBoardHook;
            this.mouseEvents.RaiseMouseHook += OnMouseHook;

            this.mouseEvents.Subscribe();
        }

        public void StopRecord()
        {
            this.mouseEvents.Unsubscribe();
        }

        public void StartPlay(RecordModel model)
        {
            this.model = model;

            if (model != null)
            {
                this.HookList = new List<HookBase>(model.ListOfActions);
                this.IsPlay = true;
                this.TimerService.Start();
            }
        }

        public void StopPlay()
        {
            this.model = null;
            this.IsPlay = false;
            this.TimerService.Stop();
        }


        /// <summary>
        /// Spielt das aufgenommen Makro ab
        /// </summary>
        /// <param name="time"></param>
        public void HandleMakroLineup(TimeSpan time)
        {
            lock (hookObject)
            {
                try
                {
                    if (HookList.Count == 0)
                    {
                        if (this.model != null && this.model.IsRepeat && this.model.RepeatCount > 0)
                        {
                            var tempModel = this.model;
                            this.StopPlay();
                            tempModel.RepeatCount--;
                            this.StartPlay(tempModel);
                        }
                        else
                        {
                            this.StopPlay();
                            return;
                        }
                    }
                    else if (System.TimeSpan.Compare(time, this.HookList[0].Time) == 1)
                    {
                        Debug.WriteLine($"Realtime: {time} | makro time:{this.HookList[0].Time}");
                        this.ExecuteHook(this.HookList[0]);
                        this.HookList.Remove(this.HookList[0]);
                    }
                }
                catch (Exception ex)
                {
                    //[TS] OutofBounds possible here if makro too fast !
                }
            }
        }

        private void ExecuteHook(HookBase hook)
        {
            switch (hook.KindOfHook)
            {
                case Hook.Mouse:
                    if (hook is MouseHook mHook)
                        MouseEvents.mouseClick(mHook.Point.X, mHook.Point.Y);
                    break;
                case Hook.KeyBoard:
                    if (hook is KeyBoardHook kHook)
                        MouseEvents.keyBoardClick(kHook.KeyChar);
                    break;
                default: break;
            }
        }

        private void OnKeyBoardHook(char keyBoard)
        {
            KeyBoardHook hook = new KeyBoardHook();
            hook.KeyChar = keyBoard; ;
            hook.KindOfHook = Hook.KeyBoard;
            hook.Time = TimerService.GetTime();

            this.HookList.Add(hook);
        }

        private void OnMouseHook((MouseButtons, Point) mouse)
        {
            MouseHook hook = new MouseHook();
            hook.MouseButtons = mouse.Item1;
            hook.Point = mouse.Item2;
            hook.KindOfHook = Hook.Mouse;
            hook.Time = TimerService.GetTime();

            this.HookList.Add(hook);
        }
    }
}
