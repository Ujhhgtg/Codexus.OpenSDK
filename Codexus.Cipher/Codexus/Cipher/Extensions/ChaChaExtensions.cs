using System;
using System.IO.Hashing;
using Codexus.Cipher.Connection.ChaCha;

namespace Codexus.Cipher.Extensions;

// Token: 0x02000028 RID: 40
public static class ChaChaExtensions
{
	// Token: 0x06000131 RID: 305 RVA: 0x00006EB0 File Offset: 0x000050B0
	public static byte[] PackMessage(this ChaChaOfSalsa cipher, byte type, byte[] data)
	{
		var array = new byte[data.Length + 10];
		Array.Copy(BitConverter.GetBytes((short)(array.Length - 2)), 0, array, 0, 2);
		array[6] = type;
		array[7] = 136;
		array[8] = 136;
		array[9] = 136;
		Array.Copy(data, 0, array, 10, data.Length);
		Array.Copy(Crc32.Hash(array.AsSpan(6)), 0, array, 2, 4);
		cipher.ProcessBytes(array, 2, array.Length - 2, array, 2);
		return array;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00006F3C File Offset: 0x0000513C
	public static ValueTuple<byte, byte[]> UnpackMessage(this ChaChaOfSalsa cipher, byte[] data)
	{
		cipher.ProcessBytes(data, 0, data.Length, data, 0);
		var array = new byte[4];
		Crc32.Hash(data.AsSpan(4, data.Length - 4), array);
		for (var i = 0; i < 4; i++)
		{
			var flag = array[i] != data[i];
			if (flag)
			{
				throw new Exception("Unpacking failed");
			}
		}
		var array2 = new byte[data.Length - 8];
		Array.Copy(data, 8, array2, 0, array2.Length);
		return new ValueTuple<byte, byte[]>(data[4], array2);
	}
}