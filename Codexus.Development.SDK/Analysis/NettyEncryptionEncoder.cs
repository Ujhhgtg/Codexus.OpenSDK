using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace Codexus.Development.SDK.Analysis;

// Token: 0x0200003A RID: 58
public class NettyEncryptionEncoder : MessageToByteEncoder<IByteBuffer>
{
	// Token: 0x0600013C RID: 316 RVA: 0x00006AE4 File Offset: 0x00004CE4
	public NettyEncryptionEncoder(byte[] key)
	{
		_encryptor = new CfbBlockCipher(new AesEngine(), 8);
		_encryptor.Init(true, new ParametersWithIV(new KeyParameter(key), key));
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00006B18 File Offset: 0x00004D18
	protected override void Encode(IChannelHandlerContext context, IByteBuffer message, IByteBuffer output)
	{
		var readableBytes = message.ReadableBytes;
		output.EnsureWritable(readableBytes);
		var num = readableBytes + message.ArrayOffset + message.ReaderIndex;
		var num2 = output.ArrayOffset;
		output.SetWriterIndex(readableBytes);
		for (var i = message.ArrayOffset + message.ReaderIndex; i < num; i++)
		{
			_encryptor.ProcessBlock(message.Array, i, output.Array, num2);
			num2++;
		}
		message.SkipBytes(readableBytes);
	}

	// Token: 0x04000098 RID: 152
	private readonly CfbBlockCipher _encryptor;
}