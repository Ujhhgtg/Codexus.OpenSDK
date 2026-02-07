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
        var hash = sha.ComputeHash(data);
        Array.Reverse(hash);
        var bigInteger = new BigInteger(hash);
        if (bigInteger < 0L)
            text = "-" + (-bigInteger).ToString("x").TrimStart('0');
        else
            text = bigInteger.ToString("x").TrimStart('0');

        return text;
    }
}