using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Converter;

// Token: 0x0200009C RID: 156
public class NetEaseStringConverter : JsonConverter<int>
{
	// Token: 0x060005DE RID: 1502 RVA: 0x0000AAD0 File Offset: 0x00008CD0
	public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var flag = reader.TokenType != JsonTokenType.String;
		int num;
		if (flag)
		{
			num = reader.GetInt32();
		}
		else
		{
			num = int.Parse(reader.GetString() ?? string.Empty);
		}
		return num;
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x0000AB10 File Offset: 0x00008D10
	public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(value);
	}
}