using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Utils;

public static class JsonConventer
{
    public class SingleOrArrayConverter<T> : JsonConverter<List<T>>
    {
        public override List<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
        {
            var list = new List<T>();
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                list.Add(JsonSerializer.Deserialize<T>(ref reader, options)!);
                return list;
            }
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                list.Add(JsonSerializer.Deserialize<T>(ref reader, options)!);
            }
            return list;
        }

        public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var item in value)
            {
                JsonSerializer.Serialize(writer, item, options);
            }
            writer.WriteEndArray();
        }
    }

    public class StringFromNumberOrStringConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var tokenType = reader.TokenType;
            var result = tokenType switch
            {
                JsonTokenType.Number => reader.GetInt64().ToString(), 
                JsonTokenType.String => reader.GetString(), 
                _ => throw new JsonException("Unsupported token type for string conversion."), 
            };
            return result;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
