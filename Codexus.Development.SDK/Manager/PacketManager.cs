using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Codexus.Development.SDK.Enums;
using Codexus.Development.SDK.Packet;
using Serilog;

namespace Codexus.Development.SDK.Manager;

// Token: 0x0200001D RID: 29
public class PacketManager
{
	// Token: 0x1700002B RID: 43
	// (get) Token: 0x0600009B RID: 155 RVA: 0x0000424C File Offset: 0x0000244C
	public static PacketManager Instance
	{
		get
		{
			PacketManager? packetManager;
			if ((packetManager = _instance) == null)
			{
				packetManager = _instance = new PacketManager();
			}
			return packetManager;
		}
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00004264 File Offset: 0x00002464
	private PacketManager()
	{
		RegisterDefaultPackets();
		_registered = true;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x000042B4 File Offset: 0x000024B4
	private void RegisterDefaultPackets()
	{
		var assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach (var assembly in assemblies)
		{
			RegisterPacketFromAssembly(assembly);
		}
	}

	// Token: 0x0600009E RID: 158 RVA: 0x000042EC File Offset: 0x000024EC
	public void RegisterPacketFromAssembly(Assembly assembly)
	{
		foreach (var type2 in from type in assembly.GetTypes()
		         where typeof(IPacket).IsAssignableFrom(type) && type is { IsAbstract: false, IsInterface: false }
		         select type)
		{
			var list = type2.GetCustomAttributes<RegisterPacket>(false).ToList();
			var flag = list.Count == 0;
			if (!flag)
			{
				foreach (var registerPacket in list)
				{
					RegisterPacket(registerPacket, type2);
				}
			}
		}
	}

	// Token: 0x0600009F RID: 159 RVA: 0x000043C0 File Offset: 0x000025C0
	public void RegisterPacket(RegisterPacket metadata, Type type)
	{
		var flag = type.GetConstructor(Type.EmptyTypes) == null;
		if (flag)
		{
			throw new InvalidOperationException("Type '" + type.FullName + "' does not have a parameterless constructor.");
		}
		_states[type] = metadata.State;
		_metadata[type] = metadata;
		var flag2 = !_ids.TryGetValue(type, out var dictionary);
		if (flag2)
		{
			dictionary = new Dictionary<EnumProtocolVersion, int>();
			_ids[type] = dictionary;
		}

		var flag3 = !_packets.TryGetValue(metadata.State, out var dictionary2);
		if (flag3)
		{
			dictionary2 = new Dictionary<EnumPacketDirection, Dictionary<EnumProtocolVersion, Dictionary<int, Type>>>();
			_packets[metadata.State] = dictionary2;
		}

		var flag4 = !dictionary2.TryGetValue(metadata.Direction, out var dictionary3);
		if (flag4)
		{
			dictionary3 = new Dictionary<EnumProtocolVersion, Dictionary<int, Type>>();
			dictionary2[metadata.Direction] = dictionary3;
		}
		var versions = metadata.Versions;
		for (var i = 0; i < versions.Length; i++)
		{
			var enumProtocolVersion = versions[i];
			var packetIds = metadata.PacketIds;
			var num = Math.Min(packetIds.Length - 1, i);
			var num2 = packetIds[num];
			var flag5 = !dictionary3.TryGetValue(enumProtocolVersion, out var dictionary4);
			if (flag5)
			{
				dictionary4 = dictionary3[enumProtocolVersion] = new Dictionary<int, Type>();
			}
			dictionary[enumProtocolVersion] = num2;
			dictionary4[num2] = type;
		}
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00004538 File Offset: 0x00002738
	public IPacket? BuildPacket(EnumConnectionState state, EnumPacketDirection direction, EnumProtocolVersion version, int packetId)
	{
		IPacket? packet;
		if (!_packets.TryGetValue(state, out var dictionary) || !dictionary.TryGetValue(direction, out var dictionary2) || !dictionary2.TryGetValue(version, out var dictionary3) || !dictionary3.TryGetValue(packetId, out var type))
		{
			packet = null;
		}
		else
		{
			try
			{
				packet = (IPacket)Activator.CreateInstance(type);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Failed to build packet", Array.Empty<object>());
				packet = null;
			}
		}
		return packet;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x000045C8 File Offset: 0x000027C8
	public int GetPacketId(EnumProtocolVersion version, IPacket packet)
	{
		var type = packet.GetType();
		var flag = !_ids.TryGetValue(type, out var dictionary);
		int num;
		if (flag)
		{
			num = -1;
		}
		else
		{
			num = dictionary.GetValueOrDefault(version, -1);
		}
		return num;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00004604 File Offset: 0x00002804
	public RegisterPacket? GetMetadata(IPacket packet)
	{
		var type = packet.GetType();
		return _metadata.GetValueOrDefault(type);
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000462C File Offset: 0x0000282C
	public EnumConnectionState GetState(IPacket packet)
	{
		var type = packet.GetType();
		return _states.GetValueOrDefault(type);
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00004654 File Offset: 0x00002854
	public void EnsureRegistered()
	{
		var registered = _registered;
		if (registered)
		{
			return;
		}
		throw new InvalidOperationException("Should never call CheckIsRegistered()");
	}

	// Token: 0x04000043 RID: 67
	private static PacketManager? _instance;

	// Token: 0x04000044 RID: 68
	private readonly Dictionary<Type, Dictionary<EnumProtocolVersion, int>> _ids = new();

	// Token: 0x04000045 RID: 69
	private readonly Dictionary<Type, RegisterPacket> _metadata = new();

	// Token: 0x04000046 RID: 70
	private readonly Dictionary<EnumConnectionState, Dictionary<EnumPacketDirection, Dictionary<EnumProtocolVersion, Dictionary<int, Type>>>> _packets = new();

	// Token: 0x04000047 RID: 71
	private readonly bool _registered;

	// Token: 0x04000048 RID: 72
	private readonly Dictionary<Type, EnumConnectionState> _states = new();
}