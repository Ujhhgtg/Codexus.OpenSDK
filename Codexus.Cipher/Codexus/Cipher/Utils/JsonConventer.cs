using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Utils;
public static class JsonConventer
{
	public class SingleOrArrayConverter<T> : JsonConverter<List<T>>
	{

		public override List<T> Read(ref Utf8JsonReader reader, Type typeToConvert,  JsonSerializerOptions options)
		{
			var list = new List<T>();
			var flag = reader.TokenType != JsonTokenType.StartArray;
			if (flag)
			{
				var t = JsonSerializer.Deserialize<T>(ref reader, options);
				list.Add(t);
			}
			else
			{
				while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
				{
					var t2 = JsonSerializer.Deserialize<T>(ref reader, options);
					list.Add(t2);
				}
			}

			return list;
		}

		public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
		{
			writer.WriteStartArray();
			foreach (var t in value)
			{
				JsonSerializer.Serialize(writer, t, options);
			}
			writer.WriteEndArray();
		}
	}
	public class StringFromNumberOrStringConverter : JsonConverter<string>
	{

		public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var tokenType = reader.TokenType;
			string text;
			if (tokenType != JsonTokenType.String)
			{
				if (tokenType != JsonTokenType.Number)
				{
					throw new JsonException("Unsupported token type for string conversion.");
				}
				text = reader.GetInt64().ToString();
			}
			else
			{
				text = reader.GetString();
			}

			return text;
		}

		public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value);
		}
	}
}