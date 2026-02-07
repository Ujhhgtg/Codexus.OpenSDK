using System;
using System.Collections.Generic;
using System.Text;

namespace Codexus.Cipher.Utils.Cipher;

// Token: 0x0200001D RID: 29
public static class XxTeaUtil
{
	// Token: 0x06000093 RID: 147 RVA: 0x00003E3C File Offset: 0x0000203C
	public static string X19SignEncrypt(this string input)
	{
		return "!x19sign!" + EncryptToHex(input, "942894570397f6d1c9cca2535ad18a2b");
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00003E64 File Offset: 0x00002064
	public static string X19SignDecrypt(this string input)
	{
		var flag = !input.StartsWith("!x19sign!");
		string text;
		if (flag)
		{
			text = input;
		}
		else
		{
			var length = "!x19sign!".Length;
			text = input.Substring(length, input.Length - length);
		}
		return DecryptFromHex(text, "942894570397f6d1c9cca2535ad18a2b");
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00003EB8 File Offset: 0x000020B8
	private static string EncryptToHex(string data, string key)
	{
		var array = ToLongArray(Encoding.UTF8.GetBytes(data));
		var array2 = ToLongArray(Encoding.UTF8.GetBytes(key.PadRight(32, '\0')));
		return ToHexString(EncryptBlocks(array, array2));
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00003F00 File Offset: 0x00002100
	private static string DecryptFromHex(string hex, string key)
	{
		var flag = string.IsNullOrWhiteSpace(hex);
		string text;
		if (flag)
		{
			text = hex;
		}
		else
		{
			var array = FromHexString(hex);
			var array2 = ToLongArray(Encoding.UTF8.GetBytes(key.PadRight(32, '\0')));
			var array3 = ToByteArray(DecryptBlocks(array, array2));
			text = Encoding.UTF8.GetString(array3).TrimEnd('\0');
		}
		return text;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00003F64 File Offset: 0x00002164
	// private static long[] EncryptBlocks(long[] v, long[] k)
	// {
	// 	var num = v.Length;
	// 	var flag = num < 1;
	// 	long[] array;
	// 	if (flag)
	// 	{
	// 		array = v;
	// 	}
	// 	else
	// 	{
	// 		var num2 = 0L;
	// 		var num3 = (long)(6 + 52 / num);
	// 		var num4 = v[num - 1];
	// 		for (;;)
	// 		{
	// 			var num5 = num3;
	// 			num3 = num5 - 1L;
	// 			if (num5 <= 0L)
	// 			{
	// 				break;
	// 			}
	// 			num2 += (long)(ulong)-1640531527;
	// 			var num6 = (num2 >> 2) & 3L;
	// 			for (var i = 0; i < num - 1; i++)
	// 			{
	// 				var num7 = v[i + 1];
	// 				var num8 = ((num4 >> 5) ^ (num7 << 2)) + ((num7 >> 3) ^ (num4 << 4));
	// 				num8 ^= (num2 ^ num7) + (k[(int)checked((IntPtr)(unchecked(i & 3) ^ num6))] ^ num4);
	// 				num4 = v[i] += num8;
	// 			}
	// 			var num9 = v[0];
	// 			var num10 = ((num4 >> 5) ^ (num9 << 2)) + ((num9 >> 3) ^ (num4 << 4));
	// 			num10 ^= (num2 ^ num9) + (k[(int)checked((IntPtr)(unchecked((num - 1) & 3) ^ num6))] ^ num4);
	// 			v[num - 1] += num10;
	// 			num4 = v[num - 1];
	// 		}
	// 		array = v;
	// 	}
	// 	return array;
	// }

	// Token: 0x06000098 RID: 152 RVA: 0x00004074 File Offset: 0x00002274
	// private static long[] DecryptBlocks(long[] v, long[] k)
	// {
	// 	var num = v.Length;
	// 	var flag = num < 1;
	// 	long[] array;
	// 	if (flag)
	// 	{
	// 		array = v;
	// 	}
	// 	else
	// 	{
	// 		var num2 = (6 + 52 / num) * (long)(ulong)-1640531527;
	// 		var num3 = v[0];
	// 		while (num2 != 0L)
	// 		{
	// 			var num4 = (num2 >> 2) & 3L;
	// 			for (var i = num - 1; i > 0; i--)
	// 			{
	// 				var num5 = v[i - 1];
	// 				var num6 = ((num5 >> 5) ^ (num3 << 2)) + ((num3 >> 3) ^ (num5 << 4));
	// 				num6 ^= (num2 ^ num3) + (k[(int)checked((IntPtr)(unchecked(i & 3) ^ num4))] ^ num5);
	// 				num3 = v[i] -= num6;
	// 			}
	// 			var num7 = v[num - 1];
	// 			var num8 = ((num7 >> 5) ^ (num3 << 2)) + ((num3 >> 3) ^ (num7 << 4));
	// 			num8 ^= (num2 ^ num3) + (k[(int)checked((IntPtr)(0L ^ num4))] ^ num7);
	// 			v[0] -= num8;
	// 			num3 = v[0];
	// 			num2 -= (long)(ulong)-1640531527;
	// 		}
	// 		array = v;
	// 	}
	// 	return array;
	// }
	
	private const uint DELTA = 0x9E3779B9;

	private static long[] EncryptBlocks(long[] v, long[] k)
	{
		var n = v.Length;
		if (n <= 1) return v;

		long sum = 0;
		long rounds = 6 + 52 / n;
		long y, z = v[n - 1];

		while (rounds-- > 0)
		{
			sum = (uint)sum + DELTA;
			var e = (sum >> 2) & 3;

			for (var i = 0; i < n - 1; i++)
			{
				y = v[i + 1];
				v[i] += (((z >> 5) ^ (y << 2)) + ((y >> 3) ^ (z << 4))) ^ ((sum ^ y) + (k[(i & 3) ^ (int)e] ^ z));
				z = v[i];
			}

			y = v[0];
			v[n - 1] += (((z >> 5) ^ (y << 2)) + ((y >> 3) ^ (z << 4))) ^ ((sum ^ y) + (k[((n - 1) & 3) ^ (int)e] ^ z));
			z = v[n - 1];
		}

		return v;
	}

	private static long[] DecryptBlocks(long[] v, long[] k)
	{
		var n = v.Length;
		if (n <= 1) return v;

		long rounds = 6 + 52 / n;
		var sum = rounds * DELTA;
		long y = v[0], z;

		while (sum != 0)
		{
			var e = (sum >> 2) & 3;

			for (var i = n - 1; i > 0; i--)
			{
				z = v[i - 1];
				v[i] -= (((z >> 5) ^ (y << 2)) + ((y >> 3) ^ (z << 4))) ^ ((sum ^ y) + (k[(i & 3) ^ (int)e] ^ z));
				y = v[i];
			}

			z = v[n - 1];
			v[0] -= (((z >> 5) ^ (y << 2)) + ((y >> 3) ^ (z << 4))) ^ ((sum ^ y) + (k[(0 & 3) ^ (int)e] ^ z));
			y = v[0];

			sum = (uint)sum - DELTA;
		}

		return v;
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00004178 File Offset: 0x00002378
	private static long[] ToLongArray(byte[] data)
	{
		var num = (data.Length + 7) / 8;
		var array = new long[num];
		for (var i = 0; i < num; i++)
		{
			array[i] = BitConverter.ToInt64(data, i * 8 <= data.Length - 8 ? i * 8 : data.Length - 8);
		}
		return array;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x000041CC File Offset: 0x000023CC
	private static byte[] ToByteArray(long[] data)
	{
		var list = new List<byte>(data.Length * 8);
		foreach (var num in data)
		{
			list.AddRange(BitConverter.GetBytes(num));
		}
		var num2 = list.Count - 1;
		while (num2 >= 0 && list[num2] == 0)
		{
			num2--;
		}
		return list.GetRange(0, num2 + 1).ToArray();
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00004248 File Offset: 0x00002448
	private static string ToHexString(long[] data)
	{
		var stringBuilder = new StringBuilder(data.Length * 16);
		foreach (var num in data)
		{
			stringBuilder.Append(num.ToString("x16"));
		}
		return stringBuilder.ToString();
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00004298 File Offset: 0x00002498
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

	// Token: 0x04000047 RID: 71
	private const string Key = "942894570397f6d1c9cca2535ad18a2b";
}