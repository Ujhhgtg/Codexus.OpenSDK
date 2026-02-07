using Codexus.Development.SDK.Extensions;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Codexus.Development.SDK.Analysis;
public class MessageSerializer21Bit : MessageToByteEncoder<IByteBuffer>
{

	protected override void Encode(IChannelHandlerContext context, IByteBuffer message, IByteBuffer output)
	{
		var readableBytes = message.ReadableBytes;
		var varIntSize = readableBytes.GetVarIntSize();
		var flag = varIntSize <= 3;
		if (flag)
		{
			output.EnsureWritable(varIntSize + readableBytes);
			output.WriteVarInt(readableBytes);
			output.WriteBytes(message, message.ReaderIndex, readableBytes);
		}
	}
}