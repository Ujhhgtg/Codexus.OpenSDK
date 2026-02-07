using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Security;

namespace Codexus.Cipher.Utils.Cipher;

// Token: 0x0200001B RID: 27
public static class Rsa
{
	// Token: 0x0600008A RID: 138 RVA: 0x000039EC File Offset: 0x00001BEC
	public static AsymmetricKeyParameter LoadPublicKey(string base64PublicKey)
	{
		var array = Convert.FromBase64String(base64PublicKey);
		return PublicKeyFactory.CreateKey(array);
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00003A0C File Offset: 0x00001C0C
	public static AsymmetricKeyParameter LoadPrivateKey(string base64PrivateKey)
	{
		var array = Convert.FromBase64String(base64PrivateKey);
		return PrivateKeyFactory.CreateKey(array);
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00003A2C File Offset: 0x00001C2C
	public static byte[] RsaWithPkcs1(AsymmetricKeyParameter key, byte[] data, bool forEncryption)
	{
		var pkcs1Encoding = new Pkcs1Encoding(new RsaEngine());
		pkcs1Encoding.Init(forEncryption, key);
		var inputBlockSize = pkcs1Encoding.GetInputBlockSize();
		using var memoryStream = new MemoryStream();
		int num;
		for (var i = 0; i < data.Length; i += num)
		{
			num = Math.Min(inputBlockSize, data.Length - i);
			var array = pkcs1Encoding.ProcessBlock(data, i, num);
			memoryStream.Write(array, 0, array.Length);
		}
		var array2 = memoryStream.ToArray();
		return array2;
	}
}