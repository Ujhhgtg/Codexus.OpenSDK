using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Converter;

public class NetEaseIntConverter : JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var flag = reader.TokenType != JsonTokenType.Number;
        string text;
        if (flag)
        {
            text = reader.GetString();
            var flag2 = text == null;
            if (flag2) return string.Empty;
        }
        else
        {
            text = reader.GetInt32().ToString();
        }

        return text;
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}