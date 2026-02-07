using Codexus.Development.SDK.Extensions;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Codexus.Development.SDK.Analysis;

// Token: 0x02000036 RID: 54
public class MessageSerializer21Bit : MessageToByteEncoder<IByteBuffer>
{
	// Token: 0x06000130 RID: 304 RVA: 0x00006678 File Offset: 0x00004878
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