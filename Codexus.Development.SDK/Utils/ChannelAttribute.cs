using Codexus.Development.SDK.Connection;
using DotNetty.Common.Utilities;

namespace Codexus.Development.SDK.Utils;

// Token: 0x02000009 RID: 9
public static class ChannelAttribute
{
	// Token: 0x0400001B RID: 27
	public static readonly AttributeKey<GameConnection> Connection = AttributeKey<GameConnection>.ValueOf("Connection");
}