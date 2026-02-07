using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Converter;

// Token: 0x0200009B RID: 155
public class NetEaseIntConverter : JsonConverter<string>
{
	// Token: 0x060005DB RID: 1499 RVA: 0x0000AA68 File Offset: 0x00008C68
	public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var flag = reader.TokenType != JsonTokenType.Number;
		string text;
		if (flag)
		{
			text = reader.GetString();
			var flag2 = text == null;
			if (flag2)
			{
				return string.Empty;
			}
		}
		else
		{
			text = reader.GetInt32().ToString();
		}
		return text;
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x0000AAB9 File Offset: 0x00008CB9
	public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value);
	}
}