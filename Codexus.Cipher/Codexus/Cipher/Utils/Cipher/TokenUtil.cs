using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Codexus.Cipher.Extensions;
using Codexus.Cipher.Mark;
using Noya.LocalServer.Common.Extensions;

namespace Codexus.Cipher.Utils.Cipher;

public static class TokenUtil
{
	public static Dictionary<string, string> ComputeHttpRequestToken(string requestPath, string sendBody, string userId, string userToken, bool isAuth = false)
	{
		return ComputeHttpRequestToken(requestPath, Encoding.UTF8.GetBytes(sendBody), userId, userToken, isAuth);
	}

	private static Dictionary<string, string> ComputeHttpRequestToken(string requestPath, byte[] sendBody, string userId, string userToken, bool isAuth)
	{
		requestPath = (requestPath.StartsWith('/') ? requestPath : ("/" + requestPath));
		using var memoryStream = new MemoryStream();
		memoryStream.Write(Encoding.UTF8.GetBytes(userToken.EncodeMd5().ToLowerInvariant()));
		memoryStream.Write(sendBody);
		memoryStream.Write(Encoding.UTF8.GetBytes("0eGsBkhl"));
		memoryStream.Write(Encoding.UTF8.GetBytes(requestPath));
		var text = memoryStream.ToArray().EncodeMd5().ToLowerInvariant();
		var text2 = HexToBinary(text);
		var text3 = text2;
		text2 = text3[6..] + text2[..6];
		var bytes = Encoding.UTF8.GetBytes(text);
		ProcessBinaryBlock(text2, bytes);
		var value = (Convert.ToBase64String(bytes, 0, 12) + "1").Replace('+', 'm').Replace('/', 'o');
		if (isAuth)
		{
			value = Encoding.ASCII.GetBytes(x19Crypt.ComputeDynamicToken(requestPath, Encoding.UTF8.GetString(sendBody), userToken)).ToHex(toUpper: true);
		}
		return new Dictionary<string, string>
		{
			["user-id"] = userId,
			["user-token"] = value
		};
	}

	private static void ProcessBinaryBlock(string secretBin, byte[] httpToken)
	{
		for (var i = 0; i < secretBin.Length / 8; i++)
		{
			var readOnlySpan = secretBin.AsSpan(i * 8, Math.Min(8, secretBin.Length - i * 8));
			byte b = 0;
			for (var j = 0; j < readOnlySpan.Length; j++)
			{
				if (readOnlySpan[7 - j] == '1')
				{
					b |= (byte)(1 << j);
				}
			}
			httpToken[i] ^= b;
		}
	}

	private static string HexToBinary(string hexString)
	{
		var stringBuilder = new StringBuilder();
		foreach (var item in hexString.Select(hex => Convert.ToString(hex, 2).PadLeft(8, '0')))
		{
			stringBuilder.Append(item);
		}
		return stringBuilder.ToString();
	}

	extension(byte[] inputBytes)
	{
		public string EncodeMd5()
		{
			return MD5.HashData(inputBytes).EncodeHex();
		}

		public string EncodeHex()
		{
			return Convert.ToHexString(inputBytes).Replace("-", "").ToLower();
		}
	}

	extension(string input)
	{
		public byte[] DecodeHex()
		{
			return Convert.FromHexString(input);
		}

		public byte[] EncodeAes(byte[] key)
		{
			using var aes = Aes.Create();
			aes.Key = key;
			aes.Mode = CipherMode.ECB;
			aes.Padding = PaddingMode.PKCS7;
			using var cryptoTransform = aes.CreateEncryptor();
			var bytes = Encoding.UTF8.GetBytes(input);
			return cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
		}

		public string EncodeBase64()
		{
			return string.IsNullOrEmpty(input) ? string.Empty : Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
		}

		public string EncodeMd5()
		{
			return string.IsNullOrEmpty(input) ? string.Empty : Encoding.UTF8.GetBytes(input).EncodeMd5();
		}
	}
	
    public static string GenerateEncryptToken(string userToken)
    {
        var text = RandomUtil.GetRandomString(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")
            .ToUpper();
        var text2 = RandomUtil.GetRandomString(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")
            .ToUpper();
        var text3 = text + userToken + text2;
        var bytes = Encoding.ASCII.GetBytes(text3);
        return Convert.ToHexString(Aes.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length)).ToUpper();
    }

    private static readonly Aes Aes = Aes.Create();
}