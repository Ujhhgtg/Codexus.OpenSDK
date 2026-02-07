using System;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;

namespace Codexus.Development.SDK.Extensions;

public static class CryptExtensions
{
    public static string ToSha1(this MemoryStream data)
    {
        string text;
        using var sha = SHA1.Create();
        var array = sha.ComputeHash(data);
        Array.Reverse(array);
        var bigInteger = new BigInteger(array);
        var flag = bigInteger < 0L;
        if (flag)
            text = "-" + (-bigInteger).ToString("x").TrimStart('0');
        else
            text = bigInteger.ToString("x").TrimStart('0');

        return text;
    }
}