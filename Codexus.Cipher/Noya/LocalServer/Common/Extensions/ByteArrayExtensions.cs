using System;
using System.Text;

namespace Noya.LocalServer.Common.Extensions;

public static class ByteArrayExtensions
{
	extension(byte[] buffer)
	{
		public byte[] Xor(byte @byte)
		{
			var array = new byte[buffer.Length];
			for (var i = 0; i < buffer.Length; i++)
			{
				array[i] = (byte)(@byte ^ buffer[i]);
			}
			return array;
		}

		public byte[] Xor(byte[] key)
		{
			if (buffer.Length != key.Length)
			{
				throw new ArgumentException("Key length must be equal to content length.");
			}
			var array = new byte[buffer.Length];
			for (var i = 0; i < buffer.Length; i++)
			{
				array[i] = (byte)(buffer[i] ^ key[i]);
			}
			return array;
		}

		public string ToHex(bool toUpper = false)
		{
			var stringBuilder = new StringBuilder();
			for (var i = 0; i < buffer.Length; i++)
			{
				stringBuilder.AppendFormat(arg0: buffer[i], format: toUpper ? "{0:X2}" : "{0:x2}");
			}
			return stringBuilder.ToString();
		}

		public string ToBinary()
		{
			var stringBuilder = new StringBuilder(buffer.Length * 8);
			for (var i = 0; i < buffer.Length; i++)
			{
				var text = Convert.ToString(buffer[i], 2);
				for (var j = 0; j < 8 - text.Length; j++)
				{
					stringBuilder.Append('0');
				}
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}
	}

	public static int bytesToInt(byte[] src, int offset)
	{
		return (src[offset] & 0xFF) | ((src[offset + 1] & 0xFF) << 8) | ((src[offset + 2] & 0xFF) << 16) | ((src[offset + 3] & 0xFF) << 24);
	}

	public static int bytesToInt2(byte[] src, int offset)
	{
		return ((src[offset] & 0xFF) << 24) | ((src[offset + 1] & 0xFF) << 16) | ((src[offset + 2] & 0xFF) << 8) | (src[offset + 3] & 0xFF);
	}

	public static byte[] intToBytes(int value)
	{
		var array = new byte[4];
		array[3] = (byte)((value >> 24) & 0xFF);
		array[2] = (byte)((value >> 16) & 0xFF);
		array[1] = (byte)((value >> 8) & 0xFF);
		array[0] = (byte)(value & 0xFF);
		return array;
	}

	public static byte[] intToBytes2(int value)
	{
		return
		[
			(byte)((value >> 24) & 0xFF),
			(byte)((value >> 16) & 0xFF),
			(byte)((value >> 8) & 0xFF),
			(byte)(value & 0xFF)
		];
	}
}
