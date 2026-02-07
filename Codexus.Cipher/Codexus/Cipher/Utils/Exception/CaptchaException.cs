namespace Codexus.Cipher.Utils.Exception;
public class CaptchaException(string message) : System.Exception(message)
{

	public static CaptchaException Clone(CaptchaException exception, string append)
	{
		return new CaptchaException(exception.Message + "|" + append);
	}
}