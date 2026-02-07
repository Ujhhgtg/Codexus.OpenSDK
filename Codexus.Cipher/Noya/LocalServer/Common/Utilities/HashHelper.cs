using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Noya.LocalServer.Common.Extensions;

namespace Noya.LocalServer.Common.Utilities;

public static class HashHelper
{
	private static readonly ThreadLocal<MD5> Md5 = new(MD5.Create);

	private static readonly ThreadLocal<SHA256> Sha256 = new(SHA256.Create);

	public static string SafeCompleteMD5Hex(byte[] bytes)
	{
		var array = MD5.HashData(bytes);
		var stringBuilder = new StringBuilder();
		foreach (var b in array)
		{
			stringBuilder.Append(b.ToString("x2"));
		}
		return stringBuilder.ToString();
	}

	public static byte[]? SafeCompleteSha256(string str, Encoding? encoding = null)
	{
		encoding ??= Encoding.UTF8;
		return Sha256.Value?.ComputeHash(encoding.GetBytes(str));
	}

	public static byte[]? SafeCompleteMD5FromFile(string filePath)
	{
		return Md5.Value?.CompleteMD5FromFile(filePath);
	}

	public static byte[]? SafeCompleteMD5(Stream stream)
	{
		return Md5.Value?.ComputeHash(stream);
	}

	public static byte[]? SafeCompleteMD5(byte[] bytes)
	{
		return Md5.Value?.ComputeHash(bytes);
	}

	public static byte[]? SafeCompleteMD5(string str, Encoding? encoding = null)
	{
		encoding ??= Encoding.UTF8;
		return Md5.Value?.ComputeHash(encoding.GetBytes(str));
	}

	public static string GetMD5WithString(string sDataIn)
	{
		var array = MD5.HashData(Encoding.UTF8.GetBytes(sDataIn));
		return array.Aggregate("", (current, b) => current + b.ToString("x2"));
	}
}
