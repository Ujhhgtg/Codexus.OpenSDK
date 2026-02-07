using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Codexus.Cipher.Utils.Cipher;
public static class HttpUtil
{
	private static Aes Aes
	{
		get
		{
			var aes = Aes.Create();
			aes.Padding = PaddingMode.None;
			return aes;
		}
	}
	private static byte[][] HttpKeys =>
		(from skey in "MK6mipwmOUedplb6,OtEylfId6dyhrfdn,VNbhn5mvUaQaeOo9,bIEoQGQYjKd02U0J,fuaJrPwaH2cfXXLP,LEkdyiroouKQ4XN1,jM1h27H4UROu427W,DhReQada7gZybTDk,ZGXfpSTYUvcdKqdY,AZwKf7MWZrJpGR5W,amuvbcHw38TcSyPU,SI4QotspbjhyFdT0,VP4dhjKnDGlSJtbB,UXDZx4KhZywQ2tcn,NIK73ZNvNqzva4kd,WeiW7qU766Q1YQZI".Split(',')
			select Encoding.GetEncoding("us-ascii").GetBytes(skey)).ToArray();

	public static byte[] HttpEncrypt(byte[] bodyIn)
	{
		var array = new byte[(int)Math.Ceiling((bodyIn.Length + 16) / 16.0) * 16];
		Array.Copy(bodyIn, array, bodyIn.Length);
		var bytes = Encoding.ASCII.GetBytes(StringGenerator.GenerateRandomString(16, false));
		for (var i = 0; i < bytes.Length; i++)
		{
			array[i + bodyIn.Length] = bytes[i];
		}
		var b = (byte)((Random.Shared.Next(0, HttpKeys.Length - 1) << 4) | 2);
		var array2 = Aes.CreateEncryptor(HttpKeys[(b >> 4) & 15], bytes).TransformFinalBlock(array, 0, array.Length);
		var array3 = new byte[16 + array2.Length + 1];
		Array.Copy(bytes, array3, 16);
		Array.Copy(array2, 0, array3, 16, array2.Length);
		array3[array3.Length - 1] = b;
		return array3;
	}

	public static byte[] HttpDecrypt(byte[] body)
	{
		var flag = body.Length < 18;
		byte[] array;
		if (flag)
		{
			array = null;
		}
		else
		{
			var array2 = body.Skip(16).Take(body.Length - 1 - 16).ToArray();
			var array3 = Aes.CreateDecryptor(HttpKeys[(body[body.Length - 1] >> 4) & 15], body.Take(16).ToArray()).TransformFinalBlock(array2, 0, array2.Length);
			var i = 0;
			var num = array3.Length - 1;
			while (i < 16)
			{
				var flag2 = array3[num--] > 0;
				if (flag2)
				{
					i++;
				}
			}
			array = array3.Take(num + 1).ToArray();
		}
		return array;
	}
	private const string SKeys = "MK6mipwmOUedplb6,OtEylfId6dyhrfdn,VNbhn5mvUaQaeOo9,bIEoQGQYjKd02U0J,fuaJrPwaH2cfXXLP,LEkdyiroouKQ4XN1,jM1h27H4UROu427W,DhReQada7gZybTDk,ZGXfpSTYUvcdKqdY,AZwKf7MWZrJpGR5W,amuvbcHw38TcSyPU,SI4QotspbjhyFdT0,VP4dhjKnDGlSJtbB,UXDZx4KhZywQ2tcn,NIK73ZNvNqzva4kd,WeiW7qU766Q1YQZI";
}