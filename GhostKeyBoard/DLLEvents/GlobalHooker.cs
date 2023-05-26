using Gma.System.MouseKeyHook;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GhostKeyBoard.DLLEvents
{
    internal class MouseEvents
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out System.Drawing.Point lpPoint);

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        public static void mouseClick(int x, int y)
        {
            SetCursorPos(x, y);
            mouse_event((int)MOUSEEVENTF_LEFTDOWN, (uint)x, (uint)y, 0, UIntPtr.Zero);
            Thread.Sleep(100);
            mouse_event((int)MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, UIntPtr.Zero);
        }

        public static void mouseClick(int[] coordinates)
        {
            SetCursorPos(coordinates[0], coordinates[1]);
            mouse_event((int)MOUSEEVENTF_LEFTDOWN, (uint)coordinates[0], (uint)coordinates[1], 0, UIntPtr.Zero);
            Thread.Sleep(100);
            mouse_event((int)MOUSEEVENTF_LEFTUP, (uint)coordinates[0], (uint)coordinates[1], 0, UIntPtr.Zero);
        }

        public static System.Drawing.Point GetMousePosition()
        {
            System.Drawing.Point lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint;
        }

        /*Nugget Packet Stuff*/

        public Action<(MouseButtons, Point)> RaiseMouseHook;
        public Action<char> RaiseKeyBoardHook;

        private IKeyboardMouseEvents m_GlobalHook;

        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            RaiseKeyBoardHook?.Invoke(e.KeyChar);
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            var point = GetMousePosition();
            RaiseMouseHook?.Invoke((e.Button, point));
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;
            m_GlobalHook.Dispose();
        }
    }
}
