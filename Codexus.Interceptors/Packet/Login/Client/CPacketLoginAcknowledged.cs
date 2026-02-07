using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Event;
using Codexus.Development.SDK.Manager;
using Codexus.Development.SDK.Packet;
using Codexus.Development.SDK.Utils;
using DotNetty.Buffers;
using Serilog;

namespace Codexus.Interceptors.Packet.Login.Client;
[RegisterPacket(EnumConnectionState.Login, EnumPacketDirection.ServerBound, 3, [
	EnumProtocolVersion.V1210,
	EnumProtocolVersion.V1206
])]
public class CPacketLoginAcknowledged : IPacket
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
		var isCancelled = EventManager.Instance.TriggerEvent(MessageChannels.AllVersions, new EventLoginSuccess(connection)).IsCancelled;
		bool flag;
		if (isCancelled)
		{
			flag = true;
		}
		else
		{
			connection.State = EnumConnectionState.Configuration;
			Log.Debug("Configuration started.", []);
			flag = false;
		}
		return flag;
	}
}