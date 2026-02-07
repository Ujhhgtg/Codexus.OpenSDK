using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Codexus.Cipher.Extensions;

// Token: 0x0200002C RID: 44
public static class YggdrasilExtensions
{
	// Token: 0x0600013B RID: 315 RVA: 0x00007178 File Offset: 0x00005378
	public static byte[] EncodeSha256(this byte[] input)
	{
		return SHA256.HashData(input);
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00007190 File Offset: 0x00005390
	public static byte[] ToByteArray(this int value, bool littleEndian = true)
	{
		var bytes = BitConverter.GetBytes(value);
		var flag = BitConverter.IsLittleEndian != littleEndian;
		if (flag)
		{
			Array.Reverse(bytes);
		}
		return bytes;
	}

	// Token: 0x0600013D RID: 317 RVA: 0x000071C0 File Offset: 0x000053C0
	public static byte[] ToShortByteArray(this int value, bool littleEndian = true)
	{
		var bytes = BitConverter.GetBytes((short)value);
		var flag = BitConverter.IsLittleEndian != littleEndian;
		if (flag)
		{
			Array.Reverse(bytes);
		}
		return bytes;
	}

	// Token: 0x0600013E RID: 318 RVA: 0x000071F4 File Offset: 0x000053F4
	public static byte[] ToByteArray(this long value, bool littleEndian = true)
	{
		var bytes = BitConverter.GetBytes(value);
		var flag = BitConverter.IsLittleEndian != littleEndian;
		if (flag)
		{
			Array.Reverse(bytes);
		}
		return bytes;
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00007224 File Offset: 0x00005424
	public static byte[] ToShortByteArray(this long value, bool littleEndian = true)
	{
		var bytes = BitConverter.GetBytes((short)value);
		var flag = BitConverter.IsLittleEndian != littleEndian;
		if (flag)
		{
			Array.Reverse(bytes);
		}
		return bytes;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00007258 File Offset: 0x00005458
	public static void WriteInt(this MemoryStream stream, int value, bool littleEndian = true)
	{
		var array = value.ToByteArray(littleEndian);
		stream.Write(array);
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000727C File Offset: 0x0000547C
	public static void WriteShort(this MemoryStream stream, int value, bool littleEndian = true)
	{
		var array = value.ToShortByteArray(littleEndian);
		stream.Write(array);
	}

	// Token: 0x06000142 RID: 322 RVA: 0x000072A0 File Offset: 0x000054A0
	public static void WriteLong(this MemoryStream stream, long value, bool littleEndian = true)
	{
		var array = value.ToByteArray(littleEndian);
		stream.Write(array);
	}

	// Token: 0x06000143 RID: 323 RVA: 0x000072C3 File Offset: 0x000054C3
	public static void WriteBytes(this MemoryStream stream, byte[] data)
	{
		stream.Write(data);
	}

	// Token: 0x06000144 RID: 324 RVA: 0x000072D4 File Offset: 0x000054D4
	public static void WriteString(this MemoryStream stream, string value)
	{
		var bytes = Encoding.UTF8.GetBytes(value);
		stream.WriteByte((byte)bytes.Length);
		stream.Write(bytes);
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00007308 File Offset: 0x00005508
	public static void WriteByteLengthString(this MemoryStream stream, string value)
	{
		var bytes = Encoding.UTF8.GetBytes(value);
		stream.WriteByte((byte)bytes.Length);
		stream.Write(bytes);
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0000733C File Offset: 0x0000553C
	public static void WriteShortString(this MemoryStream stream, string value, bool littleEndian = true)
	{
		var bytes = Encoding.UTF8.GetBytes(value);
		stream.WriteShort(bytes.Length, littleEndian);
		stream.Write(bytes);
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0000736E File Offset: 0x0000556E
	public static void WriteShortBytes(this MemoryStream stream, byte[] data, bool littleEndian = true)
	{
		stream.WriteShort(data.Length, littleEndian);
		stream.Write(data);
	}
}