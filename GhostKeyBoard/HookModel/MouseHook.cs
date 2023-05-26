using System.Drawing;
using System.Windows.Forms;

namespace GhostKeyBoard.HookModel
{
    internal class MouseHook : HookBase
    {
        public MouseButtons MouseButtons { set; get; }
        public Point Point { set; get; }
    }
}
