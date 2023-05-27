using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
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

        #region HexaCodes

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
        public const int VK_RCONTROL = 0xA3; //Right Control key code
        public const int VK_KEY_A = 0x41;

        private static Dictionary<char, byte> keyboardPressValues = new Dictionary<char, byte>() { {'0',0x30},
{'1',0x31},
{'2',0x32},
{'3',0x33},
{'4',0x34},
{'5',0x35},
{'6',0x36},
{'7',0x37},
{'8',0x38},
{'9',0x39},
{'A',0x41},
{'B',0x42},
{'C',0x43},
{'D',0x44},
{'E',0x45},
{'F',0x46},
{'G',0x47},
{'H',0x48},
{'I',0x49},
{'J',0x4A},
{'K',0x4B},
{'L',0x4C},
{'M',0x4D},
{'N',0x4E},
{'O',0x4F},
{'P',0x50},
{'Q',0x51},
{'R',0x52},
{'S',0x53},
{'T',0x54},
{'U',0x55},
{'V',0x56},
{'W',0x57},
{'X',0x58},
{'Y',0x59},
{'Z',0x5A},
{' ',0x20} };

        #endregion

        public static void mouseClick(int x, int y)
        {
            SetCursorPos(x, y);
            mouse_event((int)MOUSEEVENTF_LEFTDOWN, (uint)x, (uint)y, 0, UIntPtr.Zero);
            Thread.Sleep(100);
            mouse_event((int)MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, UIntPtr.Zero);
        }

        public static void keyBoardClick(char click)
        {
            bool result = keyboardPressValues.TryGetValue(Char.ToUpper(click), out byte value);
            if (result)
            {
                keybd_event(value, 0, KEYEVENTF_EXTENDEDKEY, 0);
                Thread.Sleep(100);
                keybd_event(value, 0, KEYEVENTF_KEYUP, 0);
            }
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
