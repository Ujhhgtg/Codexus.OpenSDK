using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Extensions;
using Codexus.Development.SDK.Packet;
using DotNetty.Buffers;
using Serilog;

namespace Codexus.Interceptors.Packet.Login.Client;

[RegisterPacket(EnumConnectionState.Login, EnumPacketDirection.ServerBound, 0, [
    EnumProtocolVersion.V1076,
    EnumProtocolVersion.V108X,
    EnumProtocolVersion.V1200,
    EnumProtocolVersion.V1122,
    EnumProtocolVersion.V1180,
    EnumProtocolVersion.V1210,
    EnumProtocolVersion.V1206
])]
public class CPacketLoginStart : IPacket
{
    private string Profile { get; set; } = "";
    private bool HasPlayerUuid { get; set; }
    private byte[] Uuid { get; } = new byte[16];
    public EnumProtocolVersion ClientProtocolVersion { get; set; }

    public void ReadFromBuffer(IByteBuffer buffer)
    {
        if (ClientProtocolVersion <= EnumProtocolVersion.V1122)
        {
            if (ClientProtocolVersion != EnumProtocolVersion.V1076 &&
                ClientProtocolVersion != EnumProtocolVersion.V108X &&
                ClientProtocolVersion != EnumProtocolVersion.V1122) return;
        }
        else if (ClientProtocolVersion != EnumProtocolVersion.V1180)
        {
            if (ClientProtocolVersion == EnumProtocolVersion.V1200)
            {
                Profile = buffer.ReadStringFromBuffer(16);
                HasPlayerUuid = buffer.ReadBoolean();
                var hasPlayerUuid = HasPlayerUuid;
                if (hasPlayerUuid) buffer.ReadBytes(Uuid);
                return;
            }

            if (ClientProtocolVersion - EnumProtocolVersion.V1206 > 1) return;
            Profile = buffer.ReadStringFromBuffer(16);
            buffer.ReadBytes(Uuid);
            return;
        }

        Profile = buffer.ReadStringFromBuffer(16);
    }

    public void WriteToBuffer(IByteBuffer buffer)
    {
        if (ClientProtocolVersion <= EnumProtocolVersion.V1122)
        {
            if (ClientProtocolVersion == EnumProtocolVersion.V1076 ||
                ClientProtocolVersion == EnumProtocolVersion.V108X ||
                ClientProtocolVersion == EnumProtocolVersion.V1122) buffer.WriteStringToBuffer(Profile, 16);
        }
        else if (ClientProtocolVersion != EnumProtocolVersion.V1180)
        {
            if (ClientProtocolVersion != EnumProtocolVersion.V1200)
            {
                if (ClientProtocolVersion - EnumProtocolVersion.V1206 <= 1)
                {
                    buffer.WriteStringToBuffer(Profile, 16);
                    buffer.WriteBytes(Uuid);
                }
            }
            else
            {
                buffer.WriteStringToBuffer(Profile, 16);
                buffer.WriteBoolean(HasPlayerUuid);
                var hasPlayerUuid = HasPlayerUuid;
                if (hasPlayerUuid) buffer.WriteBytes(Uuid);
            }
        }
        else
        {
            buffer.WriteStringToBuffer(Profile);
        }
    }

    public bool HandlePacket(GameConnection connection)
    {
        Profile = connection.NickName;
        Log.Debug("{NickName} trying to login...", [Profile]);
        return false;
    }
}