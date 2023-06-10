using System;
using System.Collections.Generic;

namespace GhostKeyBoard.SaveData
{
    public class SaveModel
    {
        public List<MergeActionModel> listOfActions { set; get; } = new List<MergeActionModel>();
        public string Name { set; get; }
        public TimeSpan Time { set; get; }
    }
}
