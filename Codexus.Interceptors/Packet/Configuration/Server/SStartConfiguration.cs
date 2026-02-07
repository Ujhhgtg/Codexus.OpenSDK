using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Packet;
using DotNetty.Buffers;
using Serilog;

namespace Codexus.Interceptors.Packet.Configuration.Server;

[RegisterPacket(EnumConnectionState.Play, EnumPacketDirection.ClientBound, 105, [
    EnumProtocolVersion.V1206,
    EnumProtocolVersion.V1210
])]
public class SStartConfiguration : IPacket
{
    public EnumProtocolVersion ClientProtocolVersion { get; set; }

    public void ReadFromBuffer(IByteBuffer buffer)
    {
    }

    public void WriteToBuffer(IByteBuffer buffer)
    {
    }

    public bool HandlePacket(GameConnection connection)
    {
        connection.State = EnumConnectionState.Configuration;
        Log.Debug("Starting configuration.", []);
        return false;
    }
}