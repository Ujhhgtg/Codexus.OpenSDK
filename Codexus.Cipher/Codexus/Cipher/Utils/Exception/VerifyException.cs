namespace Codexus.Cipher.Utils.Exception;
public class VerifyException(string message) : System.Exception(message)
{

	public static VerifyException Clone(VerifyException exception, string append)
	{
		return new VerifyException(exception.Message + "|" + append);
	}
}