using System;
using System.Collections.Generic;
using System.Text;

namespace Codexus.Cipher.Utils.Cipher;

public static class XxTeaUtil
{
	extension(string input)
	{
		public string X19SignEncrypt()
		{
			return "!x19sign!" + EncryptToHex(input, "942894570397f6d1c9cca2535ad18a2b");
		}

		public string X19SignDecrypt()
		{
			string hex;
			if (!input.StartsWith("!x19sign!"))
			{
				hex = input;
			}
			else
			{
				var length = "!x19sign!".Length;
				hex = input[length..];
			}
			return DecryptFromHex(hex, "942894570397f6d1c9cca2535ad18a2b");
		}
	}

	private static string EncryptToHex(string data, string key)
	{
		return ToHexString(EncryptBlocks(ToLongArray(Encoding.UTF8.GetBytes(data)), ToLongArray(Encoding.UTF8.GetBytes(key.PadRight(32, '\0')))));
	}

	private static string DecryptFromHex(string hex, string key)
	{
		if (string.IsNullOrWhiteSpace(hex))
		{
			return hex;
		}
		var bytes = ToByteArray(DecryptBlocks(FromHexString(hex), ToLongArray(Encoding.UTF8.GetBytes(key.PadRight(32, '\0')))));
		return Encoding.UTF8.GetString(bytes).TrimEnd('\0');
	}

	private static long[] EncryptBlocks(long[] v, long[] k)
	{
		var num = v.Length;
		if (num < 1)
		{
			return v;
		}
		var num2 = 0L;
		long num3 = 6 + 52 / num;
		var num4 = v[num - 1];
		while (num3-- > 0)
		{
			num2 += 2654435769u;
			var num5 = (num2 >> 2) & 3;
			for (var i = 0; i < num - 1; i++)
			{
				var num6 = v[i + 1];
				var num7 = (((num4 >> 5) ^ (num6 << 2)) + ((num6 >> 3) ^ (num4 << 4))) ^ ((num2 ^ num6) + (k[(i & 3) ^ num5] ^ num4));
				num4 = (v[i] += num7);
			}
			var num8 = v[0];
			var num9 = (((num4 >> 5) ^ (num8 << 2)) + ((num8 >> 3) ^ (num4 << 4))) ^ ((num2 ^ num8) + (k[((num - 1) & 3) ^ num5] ^ num4));
			v[num - 1] += num9;
			num4 = v[num - 1];
		}
		return v;
	}

	private static long[] DecryptBlocks(long[] v, long[] k)
	{
		var num = v.Length;
		if (num < 1)
		{
			return v;
		}
		var num2 = (6 + 52 / num) * 2654435769u;
		var num3 = v[0];
		while (num2 != 0)
		{
			var num4 = (num2 >> 2) & 3;
			for (var num5 = num - 1; num5 > 0; num5--)
			{
				var num6 = v[num5 - 1];
				var num7 = (((num6 >> 5) ^ (num3 << 2)) + ((num3 >> 3) ^ (num6 << 4))) ^ ((num2 ^ num3) + (k[(num5 & 3) ^ num4] ^ num6));
				num3 = (v[num5] -= num7);
			}
			var num8 = v[num - 1];
			var num9 = (((num8 >> 5) ^ (num3 << 2)) + ((num3 >> 3) ^ (num8 << 4))) ^ ((num2 ^ num3) + (k[0 ^ num4] ^ num8));
			v[0] -= num9;
			num3 = v[0];
			num2 -= 2654435769u;
		}
		return v;
	}

	private static long[] ToLongArray(byte[] data)
	{
		var num = (data.Length + 7) / 8;
		var array = new long[num];
		for (var i = 0; i < num; i++)
		{
			array[i] = BitConverter.ToInt64(data, (i * 8 <= data.Length - 8) ? (i * 8) : (data.Length - 8));
		}
		return array;
	}

	private static byte[] ToByteArray(long[] data)
	{
		var list = new List<byte>(data.Length * 8);
		foreach (var l in data)
		{
			list.AddRange(BitConverter.GetBytes(l));
		}
		var num = list.Count - 1;
		while (num >= 0 && list[num] == 0)
		{
			num--;
		}
		return list.GetRange(0, num + 1).ToArray();
	}

	private static string ToHexString(long[] data)
	{
		var stringBuilder = new StringBuilder(data.Length * 16);
		foreach (var l in data)
		{
			stringBuilder.Append(l.ToString("x16"));
		}
		return stringBuilder.ToString();
	}

	private static long[] FromHexString(string hex)
	{
		var num = hex.Length / 16;
		var array = new long[num];
		for (var i = 0; i < num; i++)
		{
			array[i] = Convert.ToInt64(hex.Substring(i * 16, 16), 16);
		}
		return array;
	}
}