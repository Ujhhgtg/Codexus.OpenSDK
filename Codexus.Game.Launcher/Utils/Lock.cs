using System;
using System.Threading;

namespace Codexus.Game.Launcher.Utils;

public class Lock
{
    public IDisposable EnterScope()
    {
        Monitor.Enter(_sync);
        return new Scope(this);
    }

    private readonly object _sync = new();

    private sealed class Scope : IDisposable
    {
        public Scope(Lock owner)
        {
            _owner = owner;
        }

        public void Dispose()
        {
            var disposed = _disposed;
            if (!disposed)
            {
                _disposed = true;
                Monitor.Exit(_owner._sync);
            }
        }

        private readonly Lock _owner;
        private bool _disposed;
    }
}