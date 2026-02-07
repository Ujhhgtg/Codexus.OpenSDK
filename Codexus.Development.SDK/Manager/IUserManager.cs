using System.Runtime.CompilerServices;
using Codexus.Development.SDK.Entities;

namespace Codexus.Development.SDK.Manager;

// Token: 0x0200001C RID: 28
public interface IUserManager
{
	// Token: 0x0600009A RID: 154
	EntityAvailableUser? GetAvailableUser(string entityId);

	// Token: 0x04000041 RID: 65
	public static IUserManager Instance;

	// Token: 0x04000042 RID: 66
	public static IUserManager CppInstance;
}