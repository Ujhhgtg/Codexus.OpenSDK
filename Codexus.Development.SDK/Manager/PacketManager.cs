using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Packet;
using Serilog;

namespace Codexus.Development.SDK.Manager;

public class PacketManager
{
	private static PacketManager? _instance;

	private readonly Dictionary<Type, Dictionary<EnumProtocolVersion, int>> _ids = new();

	private readonly Dictionary<Type, RegisterPacket> _metadata = new();

	private readonly Dictionary<EnumConnectionState, Dictionary<EnumPacketDirection, Dictionary<EnumProtocolVersion, Dictionary<int, Type>>>> _packets = new();

	private readonly bool _registered;

	private readonly Dictionary<Type, EnumConnectionState> _states = new();

	public static PacketManager Instance => _instance ??= new PacketManager();

	private PacketManager()
	{
		RegisterDefaultPackets();
		_registered = true;
	}

	private void RegisterDefaultPackets()
	{
		var assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach (var assembly in assemblies)
		{
			RegisterPacketFromAssembly(assembly);
		}
	}

	public void RegisterPacketFromAssembly(Assembly assembly)
	{
		foreach (var item in from type in assembly.GetTypes()
			where typeof(IPacket).IsAssignableFrom(type) && type is { IsAbstract: false, IsInterface: false }
			select type)
		{
			var list = item.GetCustomAttributes<RegisterPacket>(inherit: false).ToList();
			if (list.Count == 0)
			{
				continue;
			}
			foreach (var item2 in list)
			{
				RegisterPacket(item2, item);
			}
		}
	}

	public void RegisterPacket(RegisterPacket metadata, Type type)
	{
		if (type.GetConstructor(Type.EmptyTypes) == null)
		{
			throw new InvalidOperationException("Type '" + type.FullName + "' does not have a parameterless constructor.");
		}
		_states[type] = metadata.State;
		_metadata[type] = metadata;
		if (!_ids.TryGetValue(type, out var value))
		{
			value = new Dictionary<EnumProtocolVersion, int>();
			_ids[type] = value;
		}
		if (!_packets.TryGetValue(metadata.State, out var value2))
		{
			value2 = new Dictionary<EnumPacketDirection, Dictionary<EnumProtocolVersion, Dictionary<int, Type>>>();
			_packets[metadata.State] = value2;
		}
		if (!value2.TryGetValue(metadata.Direction, out var value3))
		{
			value3 = new Dictionary<EnumProtocolVersion, Dictionary<int, Type>>();
			value2[metadata.Direction] = value3;
		}
		var versions = metadata.Versions;
		for (var i = 0; i < versions.Length; i++)
		{
			var key = versions[i];
			var packetIds = metadata.PacketIds;
			var num = packetIds[Math.Min(packetIds.Length - 1, i)];
			if (!value3.TryGetValue(key, out var value4))
			{
				value4 = (value3[key] = new Dictionary<int, Type>());
			}
			value[key] = num;
			value4[num] = type;
		}
	}

	public IPacket? BuildPacket(EnumConnectionState state, EnumPacketDirection direction, EnumProtocolVersion version, int packetId)
	{
		if (!_packets.TryGetValue(state, out var value) || !value.TryGetValue(direction, out var value2) || !value2.TryGetValue(version, out var value3) || !value3.TryGetValue(packetId, out var value4))
		{
			return null;
		}
		try
		{
			return (IPacket)Activator.CreateInstance(value4)!;
		}
		catch (Exception exception)
		{
			Log.Error(exception, "Failed to build packet", Array.Empty<object>());
			return null;
		}
	}

	public int GetPacketId(EnumProtocolVersion version, IPacket packet)
	{
		var type = packet.GetType();
		if (!_ids.TryGetValue(type, out var value))
		{
			return -1;
		}
		return value.GetValueOrDefault(version, -1);
	}

	public RegisterPacket? GetMetadata(IPacket packet)
	{
		return CollectionExtensions.GetValueOrDefault(key: packet.GetType(), dictionary: _metadata);
	}

	public EnumConnectionState GetState(IPacket packet)
	{
		return CollectionExtensions.GetValueOrDefault(key: packet.GetType(), dictionary: _states);
	}

	public void EnsureRegistered()
	{
		if (_registered)
		{
			return;
		}
		throw new InvalidOperationException("Should never call CheckIsRegistered()");
	}
}
