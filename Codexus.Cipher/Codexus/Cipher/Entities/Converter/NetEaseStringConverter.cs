using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Converter;

public class NetEaseStringConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var flag = reader.TokenType != JsonTokenType.String;
        int num;
        if (flag)
            num = reader.GetInt32();
        else
            num = int.Parse(reader.GetString() ?? string.Empty);
        return num;
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}