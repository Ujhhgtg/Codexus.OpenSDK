using System;
using System.Linq;
using System.Security.Cryptography;

namespace Codexus.Cipher.Extensions;

public static class ByteArrayExtensions
{
    extension(byte[] content)
    {
        public byte[] Xor(byte[] key)
        {
            if (content.Length != key.Length) throw new ArgumentException("Key length must be equal to content length.");

            var result = new byte[content.Length];
            for (var i = 0; i < content.Length; i++) result[i] = (byte)(content[i] ^ key[i]);
            return result;
        }

        public byte[] CombineWith(byte[] second)
        {
            ArgumentNullException.ThrowIfNull(content);
            ArgumentNullException.ThrowIfNull(second);
            return content.Concat(second).ToArray();
        }

        public string ComputeMd5AsHex()
        {
            return Convert.ToHexString(MD5.HashData(content));
        }
    }
}