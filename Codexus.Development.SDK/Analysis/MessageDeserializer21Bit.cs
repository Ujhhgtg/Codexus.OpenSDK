using System;
using System.Collections.Generic;
using Codexus.Development.SDK.Extensions;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Serilog;

namespace Codexus.Development.SDK.Analysis;
public class MessageDeserializer21Bit : ByteToMessageDecoder
{

	protected override void Decode(IChannelHandlerContext context, IByteBuffer message, List<object> output)
	{
		message.MarkReaderIndex();
		var array = new byte[3];
		for (var i = 0; i < 3; i++)
		{
			var flag = !message.IsReadable();
			if (flag)
			{
				message.ResetReaderIndex();
				break;
			}
			array[i] = message.ReadByte();
			var flag2 = array[i] >= 128;
			if (!flag2)
			{
				try
				{
					var num = array.ReadVarInt();
					var flag3 = message.ReadableBytes >= num;
					if (flag3)
					{
						output.Add(message.ReadBytes(num));
					}
					else
					{
						message.ResetReaderIndex();
					}
					break;
				}
				catch (Exception ex)
				{
					Log.Error(ex, "Failed to decode message.", Array.Empty<object>());
					break;
				}
			}
		}
	}
}