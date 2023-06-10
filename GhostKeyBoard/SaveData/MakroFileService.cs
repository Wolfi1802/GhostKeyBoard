using GhostKeyBoard.Helper;
using GhostKeyBoard.HookModel;
using GhostKeyBoard.mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GhostKeyBoard.SaveData
{
    public static class MakroFileService
    {
        public static readonly string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ghostMouse.json";
        public static async void Save(List<RecordModel> makros)
        {
            //await using FileStream createStream = File.Create(Path);
            //var datas = ModelConverter.Convert(makros);
            //await JsonSerializer.SerializeAsync(createStream, datas);
        }

        public static async void Save(Dictionary<List<HookBase>, string> makros)
        {
            //await using FileStream createStream = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ghostMouse.json");
            //var datas = ModelConverter.Convert(ModelConverter.CreateRecordModel(makros));
            //await JsonSerializer.SerializeAsync(createStream, datas);
        }

        //TODO[TS] XML ...json cant load actions

        public static async void Load()
        {
            //using FileStream stream = File.OpenRead(Path);
            //var datas =  await JsonSerializer.DeserializeAsync<List<RecordModel>>(stream);
        }

    }
}
