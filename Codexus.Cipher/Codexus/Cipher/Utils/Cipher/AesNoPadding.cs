using System.Security.Cryptography;

namespace Codexus.Cipher.Utils.Cipher;
public static class AesNoPadding
{

	public static byte[] Encrypt(byte[] data, byte[] key, bool encryption = true)
	{
		using var aes = Aes.Create();
		aes.Key = key;
		aes.Mode = CipherMode.ECB;
		aes.Padding = PaddingMode.None;
		var cryptoTransform = encryption ? aes.CreateEncryptor() : aes.CreateDecryptor();
		var array = cryptoTransform.TransformFinalBlock(data, 0, data.Length);
		return array;
	}
}