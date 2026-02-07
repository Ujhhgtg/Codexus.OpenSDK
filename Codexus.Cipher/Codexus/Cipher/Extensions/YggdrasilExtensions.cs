using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Codexus.Cipher.Extensions;
public static class YggdrasilExtensions
{

	public static byte[] EncodeSha256(this byte[] input)
	{
		return SHA256.HashData(input);
	}

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

	public static void WriteInt(this MemoryStream stream, int value, bool littleEndian = true)
	{
		var array = value.ToByteArray(littleEndian);
		stream.Write(array);
	}

	public static void WriteShort(this MemoryStream stream, int value, bool littleEndian = true)
	{
		var array = value.ToShortByteArray(littleEndian);
		stream.Write(array);
	}

	public static void WriteLong(this MemoryStream stream, long value, bool littleEndian = true)
	{
		var array = value.ToByteArray(littleEndian);
		stream.Write(array);
	}

	public static void WriteBytes(this MemoryStream stream, byte[] data)
	{
		stream.Write(data);
	}

	public static void WriteString(this MemoryStream stream, string value)
	{
		var bytes = Encoding.UTF8.GetBytes(value);
		stream.WriteByte((byte)bytes.Length);
		stream.Write(bytes);
	}

	public static void WriteByteLengthString(this MemoryStream stream, string value)
	{
		var bytes = Encoding.UTF8.GetBytes(value);
		stream.WriteByte((byte)bytes.Length);
		stream.Write(bytes);
	}

	public static void WriteShortString(this MemoryStream stream, string value, bool littleEndian = true)
	{
		var bytes = Encoding.UTF8.GetBytes(value);
		stream.WriteShort(bytes.Length, littleEndian);
		stream.Write(bytes);
	}

	public static void WriteShortBytes(this MemoryStream stream, byte[] data, bool littleEndian = true)
	{
		stream.WriteShort(data.Length, littleEndian);
		stream.Write(data);
	}
}