using System.Runtime.CompilerServices;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace Codexus.Cipher.Connection.ChaCha;

// Token: 0x020000AD RID: 173
public sealed class ChaChaOfSalsa : ChaCha7539Engine
{
	// Token: 0x17000275 RID: 629
	// (get) Token: 0x0600067D RID: 1661 RVA: 0x0000B614 File Offset: 0x00009814
	public override string AlgorithmName
	{
		get
		{
			var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
			defaultInterpolatedStringHandler.AppendLiteral("ChaCha");
			defaultInterpolatedStringHandler.AppendFormatted(rounds);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0000B64C File Offset: 0x0000984C
	public ChaChaOfSalsa(byte[] key, byte[] iv, bool encryption, int rounds = 8)
	{
		this.rounds = rounds;
		Init(encryption, new ParametersWithIV(new KeyParameter(key), iv));
	}
}