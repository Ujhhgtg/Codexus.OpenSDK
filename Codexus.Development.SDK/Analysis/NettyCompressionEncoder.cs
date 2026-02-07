using Codexus.Development.SDK.Extensions;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace Codexus.Development.SDK.Analysis;

// Token: 0x02000038 RID: 56
public class NettyCompressionEncoder : MessageToByteEncoder<IByteBuffer>
{
	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000136 RID: 310 RVA: 0x00006914 File Offset: 0x00004B14
	// (set) Token: 0x06000137 RID: 311 RVA: 0x0000691C File Offset: 0x00004B1C
	public int Threshold { get; set; }

	// Token: 0x06000138 RID: 312 RVA: 0x00006925 File Offset: 0x00004B25
	public NettyCompressionEncoder(int threshold)
	{
		Threshold = threshold;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00006954 File Offset: 0x00004B54
	protected override void Encode(IChannelHandlerContext context, IByteBuffer message, IByteBuffer output)
	{
		var readableBytes = message.ReadableBytes;
		var flag = readableBytes < Threshold;
		if (flag)
		{
			output.WriteVarInt(0);
			output.WriteBytes(message);
		}
		else
		{
			_deflater.Reset();
			_deflater.SetInput(message.Array, message.ArrayOffset + message.ReaderIndex, message.ReadableBytes);
			message.SetReaderIndex(message.ReaderIndex + message.ReadableBytes);
			_deflater.Finish();
			output.WriteVarInt(readableBytes);
			while (!_deflater.IsFinished)
			{
				output.WriteBytes(_buffer, 0, _deflater.Deflate(_buffer));
			}
		}
	}

	// Token: 0x04000094 RID: 148
	private readonly byte[] _buffer = new byte[4096];

	// Token: 0x04000095 RID: 149
	private readonly Deflater _deflater = new Deflater();
}