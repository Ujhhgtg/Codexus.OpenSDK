using System;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;

namespace Codexus.Development.SDK.Extensions;

// Token: 0x02000020 RID: 32
public static class CryptExtensions
{
	// Token: 0x060000BA RID: 186 RVA: 0x00005584 File Offset: 0x00003784
	public static string ToSha1(this MemoryStream data)
	{
		string text;
		using var sha = SHA1.Create();
		var array = sha.ComputeHash(data);
		Array.Reverse(array);
		var bigInteger = new BigInteger(array);
		var flag = bigInteger < 0L;
		if (flag)
		{
			text = "-" + (-bigInteger).ToString("x").TrimStart('0');
		}
		else
		{
			text = bigInteger.ToString("x").TrimStart('0');
		}

		return text;
	}
}