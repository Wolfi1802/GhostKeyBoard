using GhostKeyBoard.HookModel;
using GhostKeyBoard.mvvm.ViewModel;
using GhostKeyBoard.SaveData;
using System.Collections.Generic;

namespace GhostKeyBoard.Helper
{
    public static class ModelConverter
    {

        public static List<RecordModel> CreateRecordModel(Dictionary<List<HookBase>, string> dictionary)
        {
            var temp = new List<RecordModel>();

            foreach (var item in dictionary)
            {
                temp.Add(CreateRecordModel(item));
            }

            return temp;
        }

        public static RecordModel CreateRecordModel(KeyValuePair<List<HookBase>, string> item)
        {
            RecordModel model = new RecordModel();
            model.Time = item.Key[item.Key.Count - 1].Time;
            model.Name = item.Value;
            model.ListOfActions = item.Key;

            return model;
        }

        public static List<SaveModel> Convert(List<RecordModel> makros)
        {
            List<SaveModel> temp = new List<SaveModel>();

            foreach (var record in makros)
            {
                SaveModel savedModel = new SaveModel();
                savedModel.Name = record.Name;
                savedModel.Time = record.Time;
                List<MergeActionModel> merge = new List<MergeActionModel>();

                foreach (var actions in record.ListOfActions)
                {
                    MergeActionModel model = null;

                    if (actions is KeyBoardHook keyBoardHook)
                    {
                        model = new MergeActionModel();
                        model.KindOfHook = keyBoardHook.KindOfHook;
                        model.KeyChar = keyBoardHook.KeyChar;
                        model.Time = keyBoardHook.Time;
                    }
                    else if (actions is MouseHook mouseHook)
                    {
                        model = new MergeActionModel();
                        model.KindOfHook |= mouseHook.KindOfHook;
                        model.Point = mouseHook.Point;
                        model.MouseButtons = mouseHook.MouseButtons;
                        model.Time = mouseHook.Time;
                    }

                    if (model != null)
                        merge.Add(model);
                }

                savedModel.listOfActions = new List<MergeActionModel>(merge);
                temp.Add(savedModel);
            }

            return temp;
        }
    }
}
