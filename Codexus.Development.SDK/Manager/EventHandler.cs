namespace Codexus.Development.SDK.Manager;

// Token: 0x02000019 RID: 25
// (Invoke) Token: 0x0600008B RID: 139
public delegate void EventHandler<in T>(T args) where T : IEventArgs;