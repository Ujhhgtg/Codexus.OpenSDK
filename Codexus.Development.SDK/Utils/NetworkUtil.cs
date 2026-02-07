using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace Codexus.Development.SDK.Utils;

// Token: 0x0200000D RID: 13
public static class NetworkUtil
{
	// Token: 0x06000045 RID: 69 RVA: 0x00003044 File Offset: 0x00001244
	public static int GetAvailablePort(int low = 25565, int high = 35565, bool reuseTimeWait = false)
	{
		var flag = low > high;
		int num;
		if (flag)
		{
			num = 0;
		}
		else
		{
			var usedPorts = GetUsedPorts(reuseTimeWait);
			for (var i = low; i <= high; i++)
			{
				var flag2 = !usedPorts.Contains(i);
				if (flag2)
				{
					return i;
				}
			}
			num = 0;
		}
		return num;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00003098 File Offset: 0x00001298
	private static HashSet<int> GetUsedPorts(bool reuseTimeWait = true)
	{
		var ipglobalProperties = IPGlobalProperties.GetIPGlobalProperties();
		var enumerable = from e in ipglobalProperties.GetActiveTcpListeners()
			select e.Port;
		var enumerable2 = from e in ipglobalProperties.GetActiveUdpListeners()
			select e.Port;
		var activeTcpConnections = ipglobalProperties.GetActiveTcpConnections();
		var flag = !reuseTimeWait;
		IEnumerable<TcpConnectionInformation> enumerable4;
		if (flag)
		{
			IEnumerable<TcpConnectionInformation> enumerable3 = activeTcpConnections;
			enumerable4 = enumerable3;
		}
		else
		{
			enumerable4 = activeTcpConnections.Where(c => c.State != TcpState.TimeWait && c.State != TcpState.CloseWait);
		}
		var enumerable5 = enumerable4.Select(c => c.LocalEndPoint.Port);
		var hashSet = new HashSet<int>();
		foreach (var num in enumerable.Concat(enumerable2).Concat(enumerable5))
		{
			hashSet.Add(num);
		}
		return hashSet;
	}
}