using System.Runtime.CompilerServices;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace Codexus.Cipher.Connection.ChaCha;
public sealed class ChaChaOfSalsa : ChaCha7539Engine
{
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

	public ChaChaOfSalsa(byte[] key, byte[] iv, bool encryption, int rounds = 8)
	{
		this.rounds = rounds;
		Init(encryption, new ParametersWithIV(new KeyParameter(key), iv));
	}
}