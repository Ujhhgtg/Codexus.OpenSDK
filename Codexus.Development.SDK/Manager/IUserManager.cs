using Codexus.Development.SDK.Entities;

namespace Codexus.Development.SDK.Manager;
public interface IUserManager
{
	EntityAvailableUser? GetAvailableUser(string entityId);
	public static IUserManager Instance;
	public static IUserManager CppInstance;
}