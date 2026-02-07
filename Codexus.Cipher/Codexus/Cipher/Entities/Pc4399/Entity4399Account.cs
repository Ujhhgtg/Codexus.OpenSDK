using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Pc4399;
public class Entity4399Account : IEquatable<Entity4399Account>
{
	[JsonPropertyName("account")]
	public string Account { get; set; }
	[JsonPropertyName("password")]
	public string Password { get; set; }

	[CompilerGenerated]
	protected Entity4399Account(Entity4399Account original)
	{
		Account = original.Account;
		Password = original.Password;
	}

	public Entity4399Account()
	{
		Account = string.Empty;
		Password = string.Empty;
	}

	public bool Equals(Entity4399Account other)
	{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		return Account == other.Account && Password == other.Password;
	}
}