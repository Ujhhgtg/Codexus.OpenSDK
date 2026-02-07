using System;
using System.Security.Cryptography;
using System.Text;

namespace Codexus.Cipher.Extensions;
public static class MPayExtensions
{

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

	public static string EncodeMd5(this byte[] inputBytes)
	{
		return MD5.HashData(inputBytes).EncodeHex();
	}

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

	public static string EncodeHex(this byte[] input)
	{
		return Convert.ToHexString(input).Replace("-", "").ToLower();
	}

	public static byte[] DecodeHex(this string input)
	{
		return Convert.FromHexString(input);
	}

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