using GhostKeyBoard.HookModel;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GhostKeyBoard.SaveData
{
    public class HookBaseListConverter : JsonConverter<List<HookBase>>
    {
        public override List<HookBase> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = new List<HookBase>();
            var array = JsonDocument.ParseValue(ref reader).RootElement;

            foreach (var element in array.EnumerateArray())
            {
                var json = element.GetRawText();
                var hook = JsonSerializer.Deserialize<HookBase>(json, options);
                list.Add(hook!);
            }

            return list;
        }

        public override void Write(Utf8JsonWriter writer, List<HookBase> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }

}
