using System.Security.Cryptography;
using System.Text;

namespace Codexus.Cipher.Utils;
public class RandomUtil
{

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

	public static string GenerateSessionId()
	{
		return "captchaReq" + GetRandomString(16);
	}
}