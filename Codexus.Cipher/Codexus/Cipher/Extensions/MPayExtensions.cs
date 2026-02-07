using System;
using System.Security.Cryptography;
using System.Text;

namespace Codexus.Cipher.Extensions;

// Token: 0x02000029 RID: 41
public static class MPayExtensions
{
	// Token: 0x06000133 RID: 307 RVA: 0x00006FD4 File Offset: 0x000051D4
	public static string EncodeMd5(this string input)
	{
		var flag = string.IsNullOrEmpty(input);
		string text;
		if (flag)
		{
			text = string.Empty;
		}
		else
		{
			text = Encoding.UTF8.GetBytes(input).EncodeMd5();
		}
		return text;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000700C File Offset: 0x0000520C
	public static string EncodeMd5(this byte[] inputBytes)
	{
		return MD5.HashData(inputBytes).EncodeHex();
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000702C File Offset: 0x0000522C
	public static string EncodeBase64(this string input)
	{
		var flag = string.IsNullOrEmpty(input);
		string text;
		if (flag)
		{
			text = string.Empty;
		}
		else
		{
			text = Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
		}
		return text;
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00007064 File Offset: 0x00005264
	public static string EncodeHex(this byte[] input)
	{
		return Convert.ToHexString(input).Replace("-", "").ToLower();
	}

	// Token: 0x06000137 RID: 311 RVA: 0x00007090 File Offset: 0x00005290
	public static byte[] DecodeHex(this string input)
	{
		return Convert.FromHexString(input);
	}

	// Token: 0x06000138 RID: 312 RVA: 0x000070A8 File Offset: 0x000052A8
	public static byte[] EncodeAes(this string input, byte[] key)
	{
		using var aes = Aes.Create();
		aes.Key = key;
		aes.Mode = CipherMode.ECB;
		aes.Padding = PaddingMode.PKCS7;
		using var cryptoTransform = aes.CreateEncryptor();
		var bytes = Encoding.UTF8.GetBytes(input);
		var array = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);

		return array;
	}
}