using System;
using System.Linq;
using System.Security.Cryptography;

namespace Codexus.Cipher.Extensions;

public static class ByteArrayExtensions
{
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
        if (content.Length != key.Length) throw new ArgumentException("Key length must be equal to content length.");

        var result = new byte[content.Length];
        for (var i = 0; i < content.Length; i++) result[i] = (byte)(content[i] ^ key[i]);
        return result;
    }

    public static byte[] CombineWith(this byte[] first, byte[] second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);
        return first.Concat(second).ToArray();
    }

    public static string ComputeMd5AsHex(this byte[] data)
    {
        return Convert.ToHexString(MD5.HashData(data));
    }
}