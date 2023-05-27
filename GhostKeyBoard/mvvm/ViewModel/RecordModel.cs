using GhostKeyBoard.HookModel;
using System;
using System.Collections.Generic;

namespace GhostKeyBoard.mvvm.ViewModel
{
    public class RecordModel
    {
        public List<HookBase> ListOfActions { set; get; } = new List<HookBase>();
        public string Name { set; get; }
        public TimeSpan Time { set; get; }

    }
}
