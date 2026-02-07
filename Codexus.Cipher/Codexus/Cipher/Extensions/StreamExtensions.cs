using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Codexus.Cipher.Extensions;

// Token: 0x0200002A RID: 42
public static class StreamExtensions
{
	// Token: 0x06000139 RID: 313 RVA: 0x00007128 File Offset: 0x00005328
	public static async Task<MemoryStream> ReadSteamWithInt16Async(this NetworkStream stream)
	{
		var lengthBytes = new byte[2];
		var num3 = await stream.ReadAsync(lengthBytes);
		var flag = num3 != 2;
		if (flag)
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
		int num2;
		for (var remainingBytes = (int)num; remainingBytes > 0; remainingBytes -= num2)
		{
			var length = Math.Min(buffer.Length, remainingBytes);
			var num4 = await stream.ReadAsync(buffer.AsMemory(0, length));
			num2 = num4;
			if (num2 == 0)
			{
				throw new EndOfStreamException("End of stream reached before reading complete data.");
			}
			memoryStream.Write(buffer, 0, num2);
		}
		memoryStream.Position = 0L;
		return memoryStream;
	}
}