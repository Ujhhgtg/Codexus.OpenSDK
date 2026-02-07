using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Utils;

// Token: 0x02000011 RID: 17
public static class JsonConventer
{
	// Token: 0x020000B6 RID: 182
	public class SingleOrArrayConverter<T> : JsonConverter<List<T>>
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
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

		// Token: 0x06000692 RID: 1682 RVA: 0x0000C454 File Offset: 0x0000A654
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

	// Token: 0x020000B7 RID: 183
	public class StringFromNumberOrStringConverter : JsonConverter<string>
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
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

		// Token: 0x06000695 RID: 1685 RVA: 0x0000C517 File Offset: 0x0000A717
		public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value);
		}
	}
}