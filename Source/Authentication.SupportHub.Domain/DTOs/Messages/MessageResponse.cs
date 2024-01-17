namespace Authentication.SupportHub.Domain.DTOs.Messages;

public record MessageResponse
{
	public static string CODIGO_ENVIADO => "sending the code to your email or phone.";
	public static string EMAIL_ENVIADO => "sending the code to your email.";
	public static string CODIGO_CONFIRMADO => "confirmed successfully.";
	public static string SENHA_RESETADA => "password reset successfully.";
	public static string SIGN_OUT_CONFIRMADO => "session successfully closed.";
};