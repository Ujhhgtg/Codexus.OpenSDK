using Codexus.Development.SDK.Connection;
using Codexus.Development.SDK.Manager;
using Codexus.Interceptors.Packet.Handshake.Client;

namespace Codexus.Interceptors.Event;

public class EventHandshake(GameConnection connection, CHandshake handshake) : EventArgsBase(connection)
{
    public CHandshake Handshake { get; } = handshake;
}