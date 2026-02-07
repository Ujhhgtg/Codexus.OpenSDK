using System.Security.Cryptography;
using System.Text;

namespace Codexus.Cipher.Utils;

public static class RandomUtil
{
    public static string GetRandomString(int length, string? chars = null)
    {
        if (length <= 0)
        {
            return string.Empty;
        }
        if (string.IsNullOrEmpty(chars))
        {
            chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghizklmnopqrstuvwxyz0123456789";
        }
        var stringBuilder = new StringBuilder(length);
        var array = new byte[length];
        RandomNumberGenerator.Fill(array);
        for (var i = 0; i < length; i++)
        {
            stringBuilder.Append(chars[array[i] % chars.Length]);
        }
        return stringBuilder.ToString();
    }

    public static string GenerateSessionId()
    {
        return "captchaReq" + GetRandomString(16);
    }
}
