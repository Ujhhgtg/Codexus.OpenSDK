namespace Codexus.Development.SDK.RakNet;

// Token: 0x02000013 RID: 19
public interface IRakNetCreate
{
	// Token: 0x06000071 RID: 113
	IRakNet Create(string remoteAddress, string nexusToken, string serverId, uint userId, string userToken, string serverName, string roleName, int port, int ipv6Port, bool isRental);
}