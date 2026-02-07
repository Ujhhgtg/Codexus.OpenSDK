using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace Codexus.Development.SDK.Analysis;

// Token: 0x02000039 RID: 57
public class NettyEncryptionDecoder : ByteToMessageDecoder
{
	// Token: 0x0600013A RID: 314 RVA: 0x00006A1A File Offset: 0x00004C1A
	public NettyEncryptionDecoder(byte[] key)
	{
		_decipher = new CfbBlockCipher(new AesEngine(), 8);
		_decipher.Init(false, new ParametersWithIV(new KeyParameter(key), key));
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00006A50 File Offset: 0x00004C50
	protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
	{
		var readableBytes = input.ReadableBytes;
		var byteBuffer = context.Allocator.HeapBuffer(readableBytes);
		var num = input.ReaderIndex + input.ArrayOffset + readableBytes;
		var num2 = byteBuffer.ArrayOffset;
		for (var i = input.ReaderIndex + input.ArrayOffset; i < num; i++)
		{
			_decipher.ProcessBlock(input.Array, i, byteBuffer.Array, num2);
			num2++;
		}
		byteBuffer.SetWriterIndex(readableBytes);
		input.SkipBytes(readableBytes);
		output.Add(byteBuffer);
	}

	// Token: 0x04000097 RID: 151
	private readonly CfbBlockCipher _decipher;
}