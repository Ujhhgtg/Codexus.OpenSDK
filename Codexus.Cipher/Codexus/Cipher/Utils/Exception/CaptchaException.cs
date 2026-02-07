namespace Codexus.Cipher.Utils.Exception;

// Token: 0x02000017 RID: 23
public class CaptchaException(string message) : System.Exception(message)
{
	// Token: 0x06000082 RID: 130 RVA: 0x0000372C File Offset: 0x0000192C
	public static CaptchaException Clone(CaptchaException exception, string append)
	{
		return new CaptchaException(exception.Message + "|" + append);
	}
}