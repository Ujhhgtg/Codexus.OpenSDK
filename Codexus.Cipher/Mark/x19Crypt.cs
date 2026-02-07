using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Noya.LocalServer.Common.Cryptography;
using Noya.LocalServer.Common.Extensions;
using Noya.LocalServer.Common.Utilities;

namespace Codexus.Cipher.Mark;


public class x19Crypt
{
	private static readonly string[] Keys =
	[
		"MK6mipwmOUedplb6", "OtEylfId6dyhrfdn", "VNbhn5mvUaQaeOo9", "bIEoQGQYjKd02U0J", "fuaJrPwaH2cfXXLP", "LEkdyiroouKQ4XN1", "jM1h27H4UROu427W", "DhReQada7gZybTDk", "ZGXfpSTYUvcdKqdY", "AZwKf7MWZrJpGR5W",
		"amuvbcHw38TcSyPU", "SI4QotspbjhyFdT0", "VP4dhjKnDGlSJtbB", "UXDZx4KhZywQ2tcn", "NIK73ZNvNqzva4kd", "WeiW7qU766Q1YQZI"
	];

	private static readonly string[] KeysG79V3 =
	[
		"75yWE1DMlhP6JZre", "NtDdtr7zaCO7MGqK", "5P3gbvwC2x2qVsXK", "Qgg0y2foklzV8W2P", "ItCyfnGMte15pFXe", "bp8UGVtOcS4Cc0VS", "ZRoxt2LItMBL2Rko", "EyVV2FUOWSU3pfEE", "L9molWm6kVuE6c6m", "oPDdpwvjN2YgZzE8",
		"K5rvy5Jb2S1J4SpX", "IYDhVUqFPlVjA7to", "LCR32BrjIVqkaYbS", "RWAss9Mri8bThLgF", "cdxDfuavFR1Frds5", "euKUQqtpUkUKF5aY"
	];

	private static readonly string[] KeysG79V12 =
	[
		"60F1E0D1FD635362430747215CF1C2FF", "EA5B62D27D0338374852C4B9469D7AC6", "17238D55501C5F020B155FB3303591E6", "8C5CEAE0F443E006A050266F73ADD5B0", "1C02CE22FB22F0E72060217418F351F3", "9A01773FEBB0CFE0EBDBF37F4D23C27F", "43F32300BF252CC320E2572ACE766367", "07F161011B3101F1ED0301735631E734", "0454E7707A5F37565601E100406060AF", "647554BAD3100C43C16660F002CC10F3",
		"E157213170F842382032564265B0B043", "914FC59311B04151393EF6896A847636", "0710C0205D224237025323265C145FA1", "054E6F01165267025C3111F562A921E9", "722D1789E792E2CA0D5322211FD0F5AE", "91F7C751FCF671F34943430772341799"
	];

	private static Aes Aes
	{
		get
		{
			var aes = Aes.Create();
			aes.Padding = PaddingMode.None;
			return aes;
		}
	}

	private static byte[][] HttpKeys => (from skey in "MK6mipwmOUedplb6,OtEylfId6dyhrfdn,VNbhn5mvUaQaeOo9,bIEoQGQYjKd02U0J,fuaJrPwaH2cfXXLP,LEkdyiroouKQ4XN1,jM1h27H4UROu427W,DhReQada7gZybTDk,ZGXfpSTYUvcdKqdY,AZwKf7MWZrJpGR5W,amuvbcHw38TcSyPU,SI4QotspbjhyFdT0,VP4dhjKnDGlSJtbB,UXDZx4KhZywQ2tcn,NIK73ZNvNqzva4kd,WeiW7qU766Q1YQZI".Split(',')
		select Encoding.GetEncoding("us-ascii").GetBytes(skey)).ToArray();

	public static byte[] PickKey(byte query)
	{
		return Encoding.UTF8.GetBytes(Keys[(query >> 4) & 0xF]);
	}

	public static byte[] PickKey_g79v3(byte query)
	{
		return Encoding.UTF8.GetBytes(KeysG79V3[(query >> 4) & 0xF]);
	}

	public static byte[]? PickKey_g79v12(byte query)
	{
		return KeysG79V12[(query >> 4) & 0xF].HexToBytes();
	}

	public static string ParseLoginResponse(byte[] body)
	{
		if (body.Length < 18)
		{
			throw new ArgumentException("Input body too short");
		}
		var array = AESHelper.AES_CBC_Decrypt(PickKey(body[^1]), body.Skip(16).Take(body.Length - 17).ToArray(), body.Take(16).ToArray());
		var num = 0;
		var num2 = array.Length - 1;
		while (num < 16)
		{
			if (array[num2] != 0)
			{
				num++;
			}
			num2--;
		}
		return Encoding.UTF8.GetString(array.Take(num2 + 1).ToArray());
	}

	public static byte[] HttpEncrypt(byte[] bodyIn)
	{
		var array = new byte[(int)Math.Ceiling((bodyIn.Length + 16) / 16.0) * 16];
		Array.Copy(bodyIn, array, bodyIn.Length);
		var bytes = Encoding.GetEncoding("us-ascii").GetBytes("szkgpbyimxavqjcn");
		for (var i = 0; i < bytes.Length; i++)
		{
			array[i + bodyIn.Length] = bytes[i];
		}
		var b = (byte)((Random.Shared.Next(0, HttpKeys.Length - 1) << 4) | 2);
		var array2 = Aes.CreateEncryptor(HttpKeys[(b >> 4) & 0xF], bytes).TransformFinalBlock(array, 0, array.Length);
		var array3 = new byte[16 + array2.Length + 1];
		Array.Copy(bytes, array3, 16);
		Array.Copy(array2, 0, array3, 16, array2.Length);
		array3[^1] = b;
		return array3;
	}

	public static byte[]? HttpDecrypt(byte[] body)
	{
		if (body.Length < 18)
		{
			return null;
		}
		var array = body.Skip(16).Take(body.Length - 1 - 16).ToArray();
		var array2 = Aes.CreateDecryptor(HttpKeys[(body[^1] >> 4) & 0xF], body.Take(16).ToArray()).TransformFinalBlock(array, 0, array.Length);
		var num = 0;
		var num2 = array2.Length - 1;
		while (num < 16)
		{
			if (array2[num2--] != 0)
			{
				num++;
			}
		}
		return array2.Take(num2 + 1).ToArray();
	}

	public static byte[] HttpEncrypt_g79v3(byte[] bodyIn)
	{
		try
		{
			var array = new byte[(int)Math.Ceiling((bodyIn.Length + 16) / 16.0) * 16];
			Array.Copy(bodyIn, array, bodyIn.Length);
			var bytes = Encoding.ASCII.GetBytes(StringExtensions.RandStringRunes(16));
			for (var i = 0; i < bytes.Length; i++)
			{
				array[i + bodyIn.Length] = bytes[i];
			}
			var b = (byte)((new Random().Next(0, 15) << 4) | 3);
			var bytes2 = Encoding.ASCII.GetBytes(StringExtensions.RandStringRunes(16));
			var array2 = AESHelper.AES_CBC_Encrypt(PickKey_g79v3(b), array, bytes2);
			var array3 = new byte[16 + array2.Length + 1];
			Array.Copy(bytes2, 0, array3, 0, 16);
			Array.Copy(array2, 0, array3, 16, array2.Length);
			array3[^1] = b;
			return array3;
		}
		catch
		{
			return [];
		}
	}

	public static string ParseLoginResponse_g79v3(byte[] body)
	{
		if (body.Length < 18)
		{
			throw new ArgumentException("Input body too short");
		}
		var array = AESHelper.AES_CBC_Decrypt(PickKey_g79v3(body[^1]), body.Skip(16).Take(body.Length - 17).ToArray(), body.Take(16).ToArray());
		var num = 0;
		var num2 = array.Length - 1;
		while (num < 16)
		{
			if (array[num2] != 0)
			{
				num++;
			}
			num2--;
		}
		return Encoding.UTF8.GetString(array.Take(num2 + 1).ToArray());
	}

	public static byte[] HttpEncrypt_g79v12(byte[] bodyIn, int lengthFill = 16)
	{
		try
		{
			var array = new byte[(int)Math.Ceiling((bodyIn.Length + lengthFill) / 16.0) * 16];
			Array.Copy(bodyIn, array, bodyIn.Length);
			var bytes = Encoding.ASCII.GetBytes(StringExtensions.RandStringRunes(lengthFill));
			for (var i = 0; i < bytes.Length; i++)
			{
				array[i + bodyIn.Length] = bytes[i];
			}
			var b = (byte)((new Random().Next(0, 15) << 4) | 0xC);
			var bytes2 = Encoding.ASCII.GetBytes(StringExtensions.RandStringRunes(16));
			var array2 = AESHelper.AES_CBC_Encrypt(PickKey_g79v12(b), array, bytes2);
			var array3 = new byte[16 + array2.Length + 1];
			Array.Copy(bytes2, 0, array3, 0, 16);
			Array.Copy(array2, 0, array3, 16, array2.Length);
			array3[^1] = b;
			return array3;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
			return [];
		}
	}

	public static string HttpDecrypt_g79v12(byte[] body)
	{
		if (body.Length < 18)
		{
			throw new ArgumentException("Input body too short");
		}
		var array = AESHelper.AES_CBC_Decrypt(PickKey_g79v12(body[^1]), body.Skip(16).Take(body.Length - 17).ToArray(), body.Take(16).ToArray());
		var num = 0;
		var num2 = array.Length - 1;
		while (num < 16)
		{
			if (array[num2] != 0)
			{
				num++;
			}
			num2--;
		}
		array = array.Take(num2 + 1).ToArray();
		return Encoding.UTF8.GetString(array);
	}

	public static string HttpDecrypt_g79v12_(byte[] body)
	{
		if (body.Length < 18)
		{
			throw new ArgumentException("Input body too short");
		}
		var array = AESHelper.AES_CBC_Decrypt(PickKey_g79v12(body[^1]), body.Skip(16).Take(body.Length - 17).ToArray(), body.Take(16).ToArray());
		var num = array.Length - 1;
		while (array[num] == 0)
		{
			num--;
		}
		array = array.Take(num - 127).ToArray();
		return Encoding.UTF8.GetString(array);
	}

	public static string ParseLoginResponse_g79v12(byte[] body)
	{
		if (body.Length < 18)
		{
			throw new ArgumentException("Input body too short");
		}
		var bytes = AESHelper.AES_CBC_Decrypt(PickKey_g79v12(body[^1]), body.Skip(16).Take(body.Length - 17).ToArray(), body.Take(16).ToArray());
		return Encoding.UTF8.GetString(bytes);
	}

	public static string ComputeDynamicToken(string path, string body, string token)
	{
		var stringBuilder = new StringBuilder();
		stringBuilder.Append(HashHelper.SafeCompleteMD5Hex(Encoding.UTF8.GetBytes(token)));
		stringBuilder.Append(body);
		stringBuilder.Append("0eGsBkhl");
		stringBuilder.Append(path.TrimEnd('?'));
		var bytes = Encoding.UTF8.GetBytes(HashHelper.SafeCompleteMD5Hex(Encoding.UTF8.GetBytes(stringBuilder.ToString())));
		var text = bytes.ToBinary();
		text = text[6..] + text[..6];
		for (var i = 0; i < bytes.Length; i++)
		{
			var text2 = text.Substring(i * 8, 8);
			byte b = 0;
			for (var j = 0; j < 8; j++)
			{
				if (text2[7 - j] == '1')
				{
					b = (byte)(b | (1 << j));
				}
			}
			bytes[i] = (byte)(b ^ bytes[i]);
		}
		return Convert.ToBase64String(bytes)[..16].Replace("+", "m")
			.Replace("/", "o") + "1";
	}

	public static string ComputeDynamicToken(string path, string body, byte[] token)
	{
		var stringBuilder = new StringBuilder();
		stringBuilder.Append(token.ToHex());
		stringBuilder.Append(body);
		stringBuilder.Append("0eGsBkhl");
		stringBuilder.Append(path.TrimEnd('?'));
		var bytes = Encoding.UTF8.GetBytes(HashHelper.SafeCompleteMD5Hex(Encoding.UTF8.GetBytes(stringBuilder.ToString())));
		var text = bytes.ToBinary();
		text = text[6..] + text[..6];
		for (var i = 0; i < bytes.Length; i++)
		{
			var text2 = text.Substring(i * 8, 8);
			byte b = 0;
			for (var j = 0; j < 8; j++)
			{
				if (text2[7 - j] == '1')
				{
					b = (byte)(b | (1 << j));
				}
			}
			bytes[i] = (byte)(b ^ bytes[i]);
		}
		return Convert.ToBase64String(bytes)[..16].Replace("+", "m")
			.Replace("/", "o") + "1";
	}

	public static byte[]? DecryptModJson(byte[] array, byte[] key, string uuid)
	{
		if (Encoding.ASCII.GetString(array.Skip(4).Take(36).ToArray()) == uuid)
		{
			array = array.Skip(64).ToArray();
			if (array.Length % 16 == 0)
			{
				return AESHelper.AES_CFB_Decrypt(key, array, key);
			}
			if (array.Length < 16)
			{
				var num = 16 - array.Length;
				var array2 = new byte[array.Length + num];
				for (var i = 0; i < array.Length; i++)
				{
					array2[i] = array[i];
				}
				return AESHelper.AES_CFB_Decrypt(key, array2, key).Take(array.Length).ToArray();
			}
			var num2 = 16 - array.Length % 16;
			var array3 = new byte[array.Length + num2];
			for (var j = 0; j < array.Length; j++)
			{
				array3[j] = array[j];
			}
			return AESHelper.AES_CFB_Decrypt(key, array3, key).Take(array.Length).ToArray();
		}
		return null;
	}
}
