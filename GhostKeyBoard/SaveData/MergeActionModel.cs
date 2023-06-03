using GhostKeyBoard.Enum;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GhostKeyBoard.SaveData
{
    public class MergeActionModel
    {
        public Hook KindOfHook { get; set; }
        public TimeSpan Time { get; set; }
        public MouseButtons MouseButtons { set; get; }
        public Point Point { set; get; }

        public char KeyChar { set; get; }
    }
}
