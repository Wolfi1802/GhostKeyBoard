using GhostKeyBoard.Helper;
using GhostKeyBoard.HookModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace GhostKeyBoard.SaveData
{
    public static class MakroFileService
    {
        public static readonly string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ghostMouse.json";

        public static async void Save(Dictionary<List<HookBase>, string> makros)
        {
            await using FileStream createStream = File.Create(Path);
            List<SaveModel> datas = ModelConverter.Convert(ModelConverter.CreateRecordModel(makros));
            await JsonSerializer.SerializeAsync(createStream, datas);
        }

        public static async Task<Dictionary<List<HookBase>, string>> Load()
        {
            if (File.Exists(Path))
            {
                await using FileStream stream = File.OpenRead(Path);
                List<SaveModel>? datas = await JsonSerializer.DeserializeAsync<List<SaveModel>>(stream);

                Dictionary<List<HookBase>, string> result = ModelConverter.Load(datas);

                return result;
            }

            return default;
        }

    }
}
