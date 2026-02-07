using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Codexus.Development.SDK.Extensions;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace Codexus.Development.SDK.Analysis;

public class NettyCompressionDecoder : ByteToMessageDecoder
{
    public int Threshold { get; set; }

    public NettyCompressionDecoder(int threshold)
    {
        Threshold = threshold;
    }

    protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
    {
        var flag = input.ReadableBytes == 0;
        if (!flag)
        {
            var num = input.ReadVarIntFromBuffer();
            var flag2 = num == 0;
            if (flag2)
            {
                output.Add(input.ReadBytes(input.ReadableBytes));
            }
            else
            {
                var flag3 = num < Threshold;
                if (flag3)
                {
                    var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 2);
                    defaultInterpolatedStringHandler.AppendLiteral("Decompressed length ");
                    defaultInterpolatedStringHandler.AppendFormatted(num);
                    defaultInterpolatedStringHandler.AppendLiteral(" is below threshold ");
                    defaultInterpolatedStringHandler.AppendFormatted(Threshold);
                    throw new DecoderException(defaultInterpolatedStringHandler.ToStringAndClear());
                }

                var array = new byte[input.ReadableBytes];
                input.ReadBytes(array);
                var array2 = _arrayPool.Rent(Math.Max(4096, num));
                try
                {
                    _inflater.Reset();
                    _inflater.SetInput(array);
                    var isNeedingDictionary = _inflater.IsNeedingDictionary;
                    if (isNeedingDictionary) throw new DecoderException("Inflater requires dictionary");
                    var byteBuffer = context.Allocator.HeapBuffer(num);
                    var num2 = 0;
                    while (!_inflater.IsFinished && num2 < num)
                    {
                        var num3 = _inflater.Inflate(array2);
                        var flag4 = num3 == 0 && _inflater.IsNeedingInput;
                        if (flag4) throw new DecoderException("Incomplete compressed data");
                        byteBuffer.WriteBytes(array2, 0, num3);
                        num2 += num3;
                    }

                    var flag5 = num2 != num;
                    if (flag5)
                    {
                        byteBuffer.Release();
                        var defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(45, 2);
                        defaultInterpolatedStringHandler2.AppendLiteral("Decompressed length mismatch: expected ");
                        defaultInterpolatedStringHandler2.AppendFormatted(num);
                        defaultInterpolatedStringHandler2.AppendLiteral(", got ");
                        defaultInterpolatedStringHandler2.AppendFormatted(num2);
                        throw new DecoderException(defaultInterpolatedStringHandler2.ToStringAndClear());
                    }

                    output.Add(byteBuffer);
                }
                finally
                {
                    _arrayPool.Return(array2);
                }
            }
        }
    }

    private const int InitialBufferSize = 4096;
    private readonly ArrayPool<byte> _arrayPool = ArrayPool<byte>.Shared;
    private readonly Inflater _inflater = new();
}