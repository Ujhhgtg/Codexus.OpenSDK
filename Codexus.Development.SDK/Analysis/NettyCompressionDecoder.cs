using System;
using System.Buffers;
using System.Collections.Generic;
using Codexus.Development.SDK.Extensions;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace Codexus.Development.SDK.Analysis;

public class NettyCompressionDecoder(int threshold) : ByteToMessageDecoder
{
    public int Threshold { get; set; } = threshold;

    protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
    {
        if (input.ReadableBytes != 0)
        {
            var varInt = input.ReadVarIntFromBuffer();
            if (varInt == 0)
            {
                output.Add(input.ReadBytes(input.ReadableBytes));
            }
            else
            {
                if (varInt < Threshold)
                {
                    throw new DecoderException($"Decompressed length {varInt} is below threshold {Threshold}");
                }

                var array = new byte[input.ReadableBytes];
                input.ReadBytes(array);
                var array2 = _arrayPool.Rent(Math.Max(4096, varInt));
                try
                {
                    _inflater.Reset();
                    _inflater.SetInput(array);
                    if (_inflater.IsNeedingDictionary) throw new DecoderException("Inflater requires dictionary");
                    var byteBuffer = context.Allocator.HeapBuffer(varInt);
                    var num2 = 0;
                    while (!_inflater.IsFinished && num2 < varInt)
                    {
                        var num3 = _inflater.Inflate(array2);
                        var flag4 = num3 == 0 && _inflater.IsNeedingInput;
                        if (flag4) throw new DecoderException("Incomplete compressed data");
                        byteBuffer.WriteBytes(array2, 0, num3);
                        num2 += num3;
                    }

                    var flag5 = num2 != varInt;
                    if (flag5)
                    {
                        byteBuffer.Release();
                        throw new DecoderException($"Decompressed length mismatch: expected {varInt}, got {num2}");
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