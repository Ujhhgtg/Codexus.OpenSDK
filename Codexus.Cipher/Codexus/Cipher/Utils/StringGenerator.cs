using System;
using System.Text;

namespace Codexus.Cipher.Utils;

public class StringGenerator
{
    public static string GenerateHexString(int length)
    {
        var array = new byte[length];
        Random.NextBytes(array);
        return Convert.ToHexString(array);
    }

    public static string GenerateRandomString(int length, bool includeNumbers = true, bool includeUppercase = true,
        bool includeLowercase = true)
    {
        var flag = length <= 0;
        if (flag) throw new ArgumentException("Length must be greater than 0", nameof(length));
        var flag2 = !includeNumbers && !includeUppercase && !includeLowercase;
        if (flag2)
            throw new ArgumentException("Must include at least one character type",
                "includeNumbers, includeUppercase, includeLowercase");
        var stringBuilder = new StringBuilder();
        if (includeNumbers) stringBuilder.Append("0123456789");
        if (includeUppercase) stringBuilder.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        if (includeLowercase) stringBuilder.Append("abcdefghijklmnopqrstuvwxyz");
        var length2 = stringBuilder.Length;
        var stringBuilder2 = new StringBuilder(length);
        for (var i = 0; i < length; i++) stringBuilder2.Append(stringBuilder[Random.Next(length2)]);
        return stringBuilder2.ToString();
    }

    // public static unsafe string GenerateRandomMacAddress(string separator = ":", bool uppercase = true)
    // {
    // 	var array = new byte[6];
    // 	Random.NextBytes(array);
    // 	array[0] = (array[0] & 254) | 2;
    // 	var text = uppercase ? "X2" : "x2";
    // 	var y__InlineArray = default(_y__InlineArray61<string>);
    // 	*PrivateImplementationDetails_.InlineArrayElementRef<_y__InlineArray61<string>, string>(ref y__InlineArray, 0) = array[0].ToString(text);
    // 	*PrivateImplementationDetails_.InlineArrayElementRef<_y__InlineArray61<string>, string>(ref y__InlineArray, 1) = array[1].ToString(text);
    // 	*PrivateImplementationDetails_.InlineArrayElementRef<_y__InlineArray61<string>, string>(ref y__InlineArray, 2) = array[2].ToString(text);
    // 	*PrivateImplementationDetails_.InlineArrayElementRef<_y__InlineArray61<string>, string>(ref y__InlineArray, 3) = array[3].ToString(text);
    // 	*PrivateImplementationDetails_.InlineArrayElementRef<_y__InlineArray61<string>, string>(ref y__InlineArray, 4) = array[4].ToString(text);
    // 	*PrivateImplementationDetails_.InlineArrayElementRef<_y__InlineArray61<string>, string>(ref y__InlineArray, 5) = array[5].ToString(text);
    // 	return string.Join(separator, PrivateImplementationDetails_.InlineArrayAsReadOnlySpan<_y__InlineArray61<string>, string>(in y__InlineArray, 6));
    // }
    public static string GenerateRandomMacAddress(string separator = ":", bool uppercase = true)
    {
        var array = new byte[6];
        Random.Shared.NextBytes(array);

        array[0] = (byte)((array[0] & 0xFE) | 0x02);

        var format = uppercase ? "X2" : "x2";

        var hexParts = new string[6];
        hexParts[0] = array[0].ToString(format);
        hexParts[1] = array[1].ToString(format);
        hexParts[2] = array[2].ToString(format);
        hexParts[3] = array[3].ToString(format);
        hexParts[4] = array[4].ToString(format);
        hexParts[5] = array[5].ToString(format);

        return string.Join(separator, hexParts);
    }

    private static readonly Random Random = new();
}