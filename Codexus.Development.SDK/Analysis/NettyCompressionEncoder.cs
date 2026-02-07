using Codexus.Development.SDK.Extensions;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace Codexus.Development.SDK.Analysis;

public class NettyCompressionEncoder : MessageToByteEncoder<IByteBuffer>
{
    public int Threshold { get; set; }

    public NettyCompressionEncoder(int threshold)
    {
        Threshold = threshold;
    }

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
            while (!_deflater.IsFinished) output.WriteBytes(_buffer, 0, _deflater.Deflate(_buffer));
        }
    }

    private readonly byte[] _buffer = new byte[4096];
    private readonly Deflater _deflater = new();
}