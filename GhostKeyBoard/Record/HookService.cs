using GhostKeyBoard.DLLEvents;
using GhostKeyBoard.Enum;
using GhostKeyBoard.HookModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GhostKeyBoard.Record
{
    internal class HookService
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

        private MouseEvents mouseEvents = new MouseEvents();
        private TimerService TimerService { get { return TimerService.Instance; } }
        private List<HookBase> HookList = new List<HookBase>();
        private object hookObject = new object();

        private HookService()
        {
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

        public void StartPlay()
        {
            this.TimerService.OnTimerTickEvent += OnTimerTickCheckExecuteAvialable;
            this.TimerService.Start();
        }

        public void StopPlay()
        {
            this.TimerService.OnTimerTickEvent -= OnTimerTickCheckExecuteAvialable;
            this.TimerService.Stop();
        }

        private void OnTimerTickCheckExecuteAvialable<TimeSpan>(object o, TimeSpan time)
        {
            if (HookList.Count == 0)
            {
                this.StopPlay();
                return;
            }

            if (time != null && time is System.TimeSpan realTime)
            {
                for (int i = 0; i < this.HookList.Count; i++)
                {
                    if (System.TimeSpan.Compare(realTime, this.HookList[i].Time) == 1)
                    {
                        lock (hookObject)
                        {
                            if (HookList.Count == 0)
                            {
                                this.StopPlay();
                                return;
                            }

                            this.ExecuteHook(this.HookList[i]);
                            this.HookList.Remove(this.HookList[i]);
                        }
                    }
                    else
                        break;
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
