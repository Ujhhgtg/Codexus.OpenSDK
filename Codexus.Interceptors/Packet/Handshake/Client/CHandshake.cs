using System;
using System.Text;
using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Extensions;
using Codexus.Development.SDK.Manager;
using Codexus.Development.SDK.Packet;
using Codexus.Development.SDK.Utils;
using Codexus.Interceptors.Event;
using DotNetty.Buffers;
using Serilog;

namespace Codexus.Interceptors.Packet.Handshake.Client;
[RegisterPacket(EnumConnectionState.Handshaking, EnumPacketDirection.ServerBound, 0)]
public class CHandshake : IPacket
{
	public int ProtocolVersion { get; set; }
	public string ServerAddress { get; set; } = "";
	public ushort ServerPort { get; set; }
	public EnumConnectionState NextState { get; set; }
	public EnumProtocolVersion ClientProtocolVersion { get; set; }
	public void ReadFromBuffer(IByteBuffer buffer)
	{
		ProtocolVersion = buffer.ReadVarIntFromBuffer();
		ServerAddress = buffer.ReadStringFromBuffer(255);
		ServerPort = buffer.ReadUnsignedShort();
		NextState = (EnumConnectionState)buffer.ReadVarIntFromBuffer();
	}
	public void WriteToBuffer(IByteBuffer buffer)
	{
		buffer.WriteVarInt(ProtocolVersion);
		buffer.WriteStringToBuffer(ServerAddress);
		buffer.WriteShort(ServerPort);
		buffer.WriteVarInt((int)NextState);
	}
	public bool HandlePacket(GameConnection connection)
	{
		var isCancelled = EventManager.Instance.TriggerEvent<EventHandshake>(MessageChannels.AllVersions, new EventHandshake(connection, this)).IsCancelled;
		bool flag;
		if (isCancelled)
		{
			flag = true;
		}
		else
		{
			connection.ProtocolVersion = (EnumProtocolVersion)ProtocolVersion;
			connection.State = NextState;
			Log.Debug("Original address: {ServerAddress}", [Convert.ToBase64String(Encoding.UTF8.GetBytes(ServerAddress))
			]);
			ServerPort = (ushort)connection.ForwardPort;
			var protocolVersion = connection.ProtocolVersion;
			var text = ((protocolVersion > EnumProtocolVersion.V1180) ? ((protocolVersion <= EnumProtocolVersion.V1206) ? (connection.ForwardAddress + "\0FML3\0") : (connection.ForwardAddress + "\0FORGE")) : ((protocolVersion <= EnumProtocolVersion.V1122) ? (connection.ForwardAddress + "\0FML\0") : (connection.ForwardAddress + "\0FML2\0")));
			ServerAddress = text;
			Log.Debug("New address: {ServerAddress}", [Convert.ToBase64String(Encoding.UTF8.GetBytes(ServerAddress))]);
			Log.Debug("Protocol version: {ProtocolVersion}, Next state: {State}, Address: {Address}", [
				connection.ProtocolVersion,
				connection.State,
				ServerAddress.Replace("\0", "|")
			]);
			flag = false;
		}
		return flag;
	}
}