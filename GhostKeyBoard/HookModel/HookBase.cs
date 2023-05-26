using GhostKeyBoard.Enum;
using System;

namespace GhostKeyBoard.HookModel
{
    abstract class HookBase
    {
        internal Hook KindOfHook { get; set; }
        internal TimeSpan Time { get; set; }
    }
}
