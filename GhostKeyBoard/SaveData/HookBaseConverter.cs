using GhostKeyBoard.Enum;
using GhostKeyBoard.HookModel;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GhostKeyBoard.SaveData
{
    public class HookBaseConverter : JsonConverter<HookBase>
    {
        public override HookBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            var kind = (Hook)root.GetProperty("KindOfHook").GetInt32();

            return kind switch
            {
                Hook.Mouse => JsonSerializer.Deserialize<MouseHook>(root.GetRawText(), options),
                Hook.KeyBoard => JsonSerializer.Deserialize<KeyBoardHook>(root.GetRawText(), options),
                _ => JsonSerializer.Deserialize<HookBase>(root.GetRawText(), options)
            };
        }

        public override void Write(Utf8JsonWriter writer, HookBase value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }

}
