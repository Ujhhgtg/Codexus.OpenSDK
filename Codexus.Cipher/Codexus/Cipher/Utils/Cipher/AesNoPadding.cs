using System.Security.Cryptography;

namespace Codexus.Cipher.Utils.Cipher;

// Token: 0x02000019 RID: 25
public static class AesNoPadding
{
	// Token: 0x06000085 RID: 133 RVA: 0x00003788 File Offset: 0x00001988
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