using System;
using System.Linq;
using System.Security.Cryptography;

namespace Codexus.Cipher.Extensions;

// Token: 0x02000027 RID: 39
public static class ByteArrayExtensions
{
	// Token: 0x0600012E RID: 302 RVA: 0x00006E00 File Offset: 0x00005000
	// public static byte[] Xor(this byte[] content, byte[] key)
	// {
	// 	var flag = content.Length != key.Length;
	// 	if (flag)
	// 	{
	// 		throw new ArgumentException("Key length must be equal to content length.");
	// 	}
	// 	var array = new byte[content.Length];
	// 	for (var i = 0; i < content.Length; i++)
	// 	{
	// 		array[i] = content[i] ^ key[i];
	// 	}
	// 	return array;
	// }
	public static byte[] Xor(this byte[] content, byte[] key)
	{
		if (content.Length != key.Length)
		{
			throw new ArgumentException("Key length must be equal to content length.");
		}

		var result = new byte[content.Length];
		for (var i = 0; i < content.Length; i++)
		{
			result[i] = (byte)(content[i] ^ key[i]);
		}
		return result;
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00006E58 File Offset: 0x00005058
	public static byte[] CombineWith(this byte[] first, byte[] second)
	{
		ArgumentNullException.ThrowIfNull(first);
		ArgumentNullException.ThrowIfNull(second);
		return first.Concat(second).ToArray();
	}

	// Token: 0x06000130 RID: 304 RVA: 0x00006E90 File Offset: 0x00005090
	public static string ComputeMd5AsHex(this byte[] data)
	{
		return Convert.ToHexString(MD5.HashData(data));
	}
}