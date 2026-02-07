using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Codexus.Cipher.Extensions;

public static class StreamExtensions
{
    public static async Task<MemoryStream> ReadSteamWithInt16Async(this NetworkStream stream)
    {
        var lengthBytes = new byte[2];
        if (await stream.ReadAsync(lengthBytes) != 2)
        {
            throw new EndOfStreamException("Could not read the length prefix.");
        }
        var num = BitConverter.ToInt16(lengthBytes, 0);
        if (num < 0)
        {
            throw new InvalidDataException("Length cannot be negative.");
        }
        var memoryStream = new MemoryStream(num);
        var buffer = new byte[1024];
        int remainingBytes = num;
        while (remainingBytes > 0)
        {
            var num2 = await stream.ReadAsync(buffer.AsMemory(0, Math.Min(buffer.Length, remainingBytes)));
            if (num2 == 0)
            {
                throw new EndOfStreamException("End of stream reached before reading complete data.");
            }
            memoryStream.Write(buffer, 0, num2);
            remainingBytes -= num2;
        }
        memoryStream.Position = 0L;
        return memoryStream;
    }
}
