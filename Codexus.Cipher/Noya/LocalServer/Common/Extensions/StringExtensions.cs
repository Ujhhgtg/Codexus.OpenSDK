using System;
using System.Globalization;
using System.Linq;

namespace Noya.LocalServer.Common.Extensions;

public static class StringExtensions
{
	public static string RandStringRunes(int length)
	{
		var random = new Random();
		return new string((from s in Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
			select s[random.Next(s.Length)]).ToArray());
	}

	public static string RandomLetter(int length)
	{
		var random = new Random();
		return new string((from s in Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", length)
			select s[random.Next(s.Length)]).ToArray());
	}

	extension(string numStr)
	{
		public uint SafeParseToUInt32()
		{
			if (uint.TryParse(numStr, out var result))
				return result;
			return 0;
		}

		public byte[]? HexToBytes()
		{
			if (string.IsNullOrEmpty(numStr))
			{
				return null;
			}
			var array = new byte[numStr.Length / 2];
			for (var i = 0; i < array.Length; i++)
			{
				try
				{
					array[i] = byte.Parse(numStr.Substring(i * 2, 2), NumberStyles.HexNumber);
				}
				catch (Exception innerException)
				{
					throw new FormatException("hex is not a valid hex number!", innerException);
				}
			}
			return array;
		}
	}
}
