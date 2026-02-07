namespace Codexus.Cipher.Utils.Exception;

// Token: 0x02000018 RID: 24
public class VerifyException(string message) : System.Exception(message)
{
	// Token: 0x06000084 RID: 132 RVA: 0x00003760 File Offset: 0x00001960
	public static VerifyException Clone(VerifyException exception, string append)
	{
		return new VerifyException(exception.Message + "|" + append);
	}
}