using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Codexus.Cipher.Extensions;

namespace Codexus.Cipher.Utils.Cipher;
public static class TokenUtil
{

	static TokenUtil()
	{
		Aes.Mode = CipherMode.CBC;
		Aes.Padding = PaddingMode.Zeros;
		Aes.KeySize = 128;
		Aes.BlockSize = 128;
		Aes.Key = "debbde3548928fab"u8.ToArray();
		Aes.IV = "afd4c5c5a7c456a1"u8.ToArray();
	}

	public static Dictionary<string, string> ComputeHttpRequestToken(string requestPath, string sendBody, string userId, string userToken)
	{
		return ComputeHttpRequestToken(requestPath, Encoding.UTF8.GetBytes(sendBody), userId, userToken);
	}

	private static Dictionary<string, string> ComputeHttpRequestToken(string requestPath, byte[] sendBody, string userId, string userToken)
	{
		requestPath = requestPath.StartsWith('/') ? requestPath : "/" + requestPath;
		using var memoryStream = new MemoryStream();
		memoryStream.Write(Encoding.UTF8.GetBytes(userToken.EncodeMd5().ToLowerInvariant()));
		memoryStream.Write(sendBody);
		memoryStream.Write("0eGsBkhl"u8);
		memoryStream.Write(Encoding.UTF8.GetBytes(requestPath));
		var text = memoryStream.ToArray().EncodeMd5().ToLowerInvariant();
		var text2 = HexToBinary(text);
		var text3 = text2;
		text2 = text3.Substring(6, text3.Length - 6) + text2.Substring(0, 6);
		var bytes = Encoding.UTF8.GetBytes(text);
		ProcessBinaryBlock(text2, bytes);
		var text4 = (Convert.ToBase64String(bytes, 0, 12) + "1").Replace('+', 'm').Replace('/', 'o');
		var dictionary = new Dictionary<string, string>
		{
			["user-id"] = userId,
			["user-token"] = text4
		};
		return dictionary;
	}

	// private static unsafe void ProcessBinaryBlock(string secretBin, byte[] httpToken)
	// {
	// 	for (var i = 0; i < secretBin.Length / 8; i++)
	// 	{
	// 		var readOnlySpan = secretBin.AsSpan(i * 8, Math.Min(8, secretBin.Length - i * 8));
	// 		byte b = 0;
	// 		for (var j = 0; j < readOnlySpan.Length; j++)
	// 		{
	// 			var flag = *readOnlySpan[7 - j] == 49;
	// 			if (flag)
	// 			{
	// 				b |= (byte)(1 << j);
	// 			}
	// 		}
	//
	// 		httpToken[i] ^= b;
	// 	}
	// }
	private static void ProcessBinaryBlock(string secretBin, byte[] httpToken)
	{
		// The outer loop runs for each byte in the token (or until secretBin segments run out)
		for (var i = 0; i < secretBin.Length / 8; i++)
		{
			// Get a segment of 8 characters from the secretBin
			var segment = secretBin.AsSpan(i * 8, Math.Min(8, secretBin.Length - i * 8));
        
			byte mask = 0;

			// Inner loop: Convert the "binary string" segment (e.g., "01000001") into a byte
			for (var j = 0; j < segment.Length; j++)
			{
				// IL_002C: Check if the character at (7 - j) is '1' (ASCII 49)
				if (segment[7 - j] == '1')
				{
					// If '1', set the corresponding bit in the mask
					mask |= (byte)(1 << (j & 31));
				}
			}

			// XOR the byte in the httpToken with the generated mask
			httpToken[i] ^= mask;
		}
	}

	private static string HexToBinary(string hexString)
	{
		var stringBuilder = new StringBuilder();
		foreach (var text in hexString.Select(hex => Convert.ToString(hex, 2).PadLeft(8, '0')))
		{
			stringBuilder.Append(text);
		}
		return stringBuilder.ToString();
	}

	public static string GenerateEncryptToken(string userToken)
	{
		var text = RandomUtil.GetRandomString(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789").ToUpper();
		var text2 = RandomUtil.GetRandomString(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789").ToUpper();
		var text3 = text + userToken + text2;
		var bytes = Encoding.ASCII.GetBytes(text3);
		return Convert.ToHexString(Aes.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length)).ToUpper();
	}
	private const string TokenSalt = "0eGsBkhl";
	private static readonly Aes Aes = Aes.Create();
}