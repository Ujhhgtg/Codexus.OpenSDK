using System.Security.Cryptography;
using System.Text;

namespace Codexus.Cipher.Utils;

// Token: 0x02000012 RID: 18
public class RandomUtil
{
	// Token: 0x06000068 RID: 104 RVA: 0x00003098 File Offset: 0x00001298
	public static string GetRandomString(int length,  string chars = null)
	{
		var flag = length <= 0;
		string text;
		if (flag)
		{
			text = string.Empty;
		}
		else
		{
			var flag2 = string.IsNullOrEmpty(chars);
			if (flag2)
			{
				chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghizklmnopqrstuvwxyz0123456789";
			}
			var stringBuilder = new StringBuilder(length);
			var array = new byte[length];
			RandomNumberGenerator.Fill(array);
			for (var i = 0; i < length; i++)
			{
				var num = array[i] % chars.Length;
				stringBuilder.Append(chars[num]);
			}
			text = stringBuilder.ToString();
		}
		return text;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00003128 File Offset: 0x00001328
	public static string GenerateSessionId()
	{
		return "captchaReq" + GetRandomString(16);
	}
}