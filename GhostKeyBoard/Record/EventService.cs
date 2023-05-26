using GhostKeyBoard.DLLEvents;
using GhostKeyBoard.Enum;
using GhostKeyBoard.HookModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GhostKeyBoard.Record
{
    internal class EventService
    {
        public static EventService Instance
        {
            get
            {
                if (singelton == null)
                    singelton = new EventService();
                return singelton;
            }
        }

        private static EventService singelton;

        private MouseEvents mouseEvents = new MouseEvents();
        private TimerService TimerService { get { return TimerService.Instance; } }

        private List<HookBase> HookList = new List<HookBase>();

        private EventService()
        {
        }

        public void StartRecordThread()
        {
            this.HookList.Clear();
            this.mouseEvents.RaiseKeyBoardHook += OnKeyBoardHook;
            this.mouseEvents.RaiseMouseHook += OnMouseHook;

            this.mouseEvents.Subscribe();
        }

        public void StopRecordThread()
        {
            this.mouseEvents.Unsubscribe();
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
            hook.KindOfHook = Hook.KeyBoard;
            hook.Time = TimerService.GetTime();

            this.HookList.Add(hook);
        }

        public void SetMousePos(int x, int y)
        {
            MouseEvents.mouseClick(x, y);
        }

        public void SetMousePos(int[] coordinates)
        {
            MouseEvents.mouseClick(coordinates);
        }

        public Point GetMousePos()
        {
            return MouseEvents.GetMousePosition();
        }
    }
}
