using System;
using System.Collections.Generic;

namespace Codexus.Game.Launcher.Services.Java.RPC.Events;

public class SocketCallback
{
    public Action<string>? LostConnectCallback { get; set; }

    public void RegisterReceiveCallback(ushort sid, Action<byte[]> callback)
    {
        _receiveCallbacks[sid] = callback;
    }

    public bool InvokeCallback(ushort sid, byte[] parameters)
    {
        if (!_receiveCallbacks.TryGetValue(sid, out var action)) return false;

        action(parameters);
        return true;
    }

    private readonly Dictionary<ushort, Action<byte[]>> _receiveCallbacks = new();
}