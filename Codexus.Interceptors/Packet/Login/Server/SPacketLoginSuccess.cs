using System;
using System.Collections.Generic;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Entities;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Event;
using Codexus.Development.SDK.Extensions;
using Codexus.Development.SDK.Manager;
using Codexus.Development.SDK.Packet;
using Codexus.Development.SDK.Utils;
using DotNetty.Buffers;
using Serilog;

namespace Codexus.Interceptors.Packet.Login.Server;

[RegisterPacket(EnumConnectionState.Login, EnumPacketDirection.ClientBound, 2, [
    EnumProtocolVersion.V1076,
    EnumProtocolVersion.V108X,
    EnumProtocolVersion.V1200,
    EnumProtocolVersion.V1122,
    EnumProtocolVersion.V1180,
    EnumProtocolVersion.V1210,
    EnumProtocolVersion.V1206
])]
public class SPacketLoginSuccess : IPacket
{
    private string Uuid { get; set; } = "";
    private byte[] Guid { get; set; } = new byte[16];
    private string Username { get; set; } = "";
    private List<Property>? Properties { get; set; }
    private bool StrictErrorHandling { get; set; }
    public EnumProtocolVersion ClientProtocolVersion { get; set; }

    public void ReadFromBuffer(IByteBuffer buffer)
    {
        var clientProtocolVersion = ClientProtocolVersion;
        var enumProtocolVersion = clientProtocolVersion;
        if (enumProtocolVersion <= EnumProtocolVersion.V1122)
        {
            if (enumProtocolVersion != EnumProtocolVersion.V1076 && enumProtocolVersion != EnumProtocolVersion.V108X &&
                enumProtocolVersion != EnumProtocolVersion.V1122) return;
        }
        else if (enumProtocolVersion != EnumProtocolVersion.V1180)
        {
            if (enumProtocolVersion == EnumProtocolVersion.V1200)
            {
                buffer.ReadBytes(Guid);
                Username = buffer.ReadStringFromBuffer(16);
                Properties = buffer.ReadProperties();
                return;
            }

            if (enumProtocolVersion - EnumProtocolVersion.V1206 > 1) return;
            buffer.ReadBytes(Guid);
            Username = buffer.ReadStringFromBuffer(16);
            Properties = buffer.ReadProperties();
            StrictErrorHandling = buffer.ReadBoolean();
            return;
        }

        Uuid = buffer.ReadStringFromBuffer(36);
        Username = buffer.ReadStringFromBuffer(16);
    }

    public void WriteToBuffer(IByteBuffer buffer)
    {
        var clientProtocolVersion = ClientProtocolVersion;
        var enumProtocolVersion = clientProtocolVersion;
        if (enumProtocolVersion <= EnumProtocolVersion.V1122)
        {
            if (enumProtocolVersion != EnumProtocolVersion.V1076 && enumProtocolVersion != EnumProtocolVersion.V108X &&
                enumProtocolVersion != EnumProtocolVersion.V1122) return;
        }
        else if (enumProtocolVersion != EnumProtocolVersion.V1180)
        {
            if (enumProtocolVersion == EnumProtocolVersion.V1200)
            {
                buffer.WriteBytes(Guid);
                buffer.WriteStringToBuffer(Username, 16);
                buffer.WriteProperties(Properties);
                return;
            }

            if (enumProtocolVersion - EnumProtocolVersion.V1206 > 1) return;
            buffer.WriteBytes(Guid);
            buffer.WriteStringToBuffer(Username, 16);
            buffer.WriteProperties(Properties);
            buffer.WriteBoolean(StrictErrorHandling);
            return;
        }

        buffer.WriteStringToBuffer(Uuid, 36);
        buffer.WriteStringToBuffer(Username, 16);
    }

    public bool HandlePacket(GameConnection connection)
    {
        var clientProtocolVersion = ClientProtocolVersion;
        var enumProtocolVersion = clientProtocolVersion;
        if (enumProtocolVersion <= EnumProtocolVersion.V1122)
        {
            if (enumProtocolVersion != EnumProtocolVersion.V1076 && enumProtocolVersion != EnumProtocolVersion.V108X &&
                enumProtocolVersion != EnumProtocolVersion.V1122) goto IL_00A3;
        }
        else if (enumProtocolVersion != EnumProtocolVersion.V1180)
        {
            if (enumProtocolVersion != EnumProtocolVersion.V1200 &&
                enumProtocolVersion - EnumProtocolVersion.V1206 > 1) goto IL_00A3;
            Log.Information("{0}({1}) 加入了服务器", [
                Username,
                new Guid(Guid, true)
            ]);
            goto IL_00A3;
        }

        Log.Information("{0}({1}) 加入了服务器", [Username, Uuid]);
        IL_00A3:
        connection.Uuid = Guid;
        var flag = ClientProtocolVersion >= EnumProtocolVersion.V1206;
        bool flag2;
        if (flag)
        {
            flag2 = false;
        }
        else
        {
            var isCancelled = EventManager.Instance
                .TriggerEvent(MessageChannels.AllVersions, new EventLoginSuccess(connection))
                .IsCancelled;
            if (isCancelled)
            {
                flag2 = true;
            }
            else
            {
                connection.State = EnumConnectionState.Play;
                flag2 = false;
            }
        }

        return flag2;
    }
}