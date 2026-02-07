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

    private sealed class Scope(Lock owner) : IDisposable
    {
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                Monitor.Exit(owner._sync);
            }
        }

        private bool _disposed;
    }
}