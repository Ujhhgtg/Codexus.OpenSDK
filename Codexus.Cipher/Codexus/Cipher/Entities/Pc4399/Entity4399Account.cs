using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Codexus.Cipher.Entities.Pc4399;

// Token: 0x02000073 RID: 115
public class Entity4399Account : IEquatable<Entity4399Account>
{
	// Token: 0x1700018F RID: 399
	// (get) Token: 0x0600045E RID: 1118 RVA: 0x000099FB File Offset: 0x00007BFB
	// (set) Token: 0x0600045F RID: 1119 RVA: 0x00009A03 File Offset: 0x00007C03
	[JsonPropertyName("account")]
	public string Account { get; set; }

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x06000460 RID: 1120 RVA: 0x00009A0C File Offset: 0x00007C0C
	// (set) Token: 0x06000461 RID: 1121 RVA: 0x00009A14 File Offset: 0x00007C14
	[JsonPropertyName("password")]
	public string Password { get; set; }

	// Token: 0x06000462 RID: 1122 RVA: 0x00009A20 File Offset: 0x00007C20
	[CompilerGenerated]
	protected Entity4399Account(Entity4399Account original)
	{
		Account = original.Account;
		Password = original.Password;
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00009B9D File Offset: 0x00007D9D
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