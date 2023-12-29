namespace Company.SupportHub.Domain.Messages;

public record MessageException
{
	public static string SESSION_ATIVA => "Session active.";
	public static string SESSION_EXPIRADA => "Session expired.";
	public static string CONTA_NAO_ENCONTRADA => "Account not found.";
	public static string CNPJ_INVALIDO => "The user's CNPJ is invalid.";
	public static string CNPJ_JA_REGISTRADO => "The user's CNPJ has already been registered.";
	public static string CNPJ_NAO_INFORMADO => "The user's CNPJ must be provided.";

	public static string USUARIO_BLOQUEADO => "User blocked.";
	public static string USUARIO_DESBLOQUEADO => "User unblocked.";
	public static string USUARIO_ATIVADO => "User activated.";
	public static string CONTA_DESATIVADA => "Account deactivated.";
	public static string USUARIO_NAO_AUTORIZADO => "User not authorized.";
	
	

	// Email
	public static string EMAIL_INVALIDO => "The user's email is invalid.";
	public static string EMAIL_NAO_AUTENTICADO =>
		"The email is not authenticated, new code has been verified your email.";
	public static string EMAIL_NAO_INFORMADO => "The user's email must be provided.";
	public static string EMAIL_NAO_ENCONTRADO => "The user's email not found";
	public static string EMAIL_JA_REGISTRADO => "The  user's email provided is already registered.";


	// Password
	public static string SENHA_INVALIDA => "The user's password is invalid.";
	public static string SENHA_NAO_CONFERE => "The user's password does not match.";
	public static string SENHA_NAO_INFORMADO => "The user's password must be entered.";
	public static string SENHA_MINIMO_OITO_CARACTERES => "The user's password must contain at least 8 characters.";

	public static string SENHA_MAXIMO_DEZESSEIS_CARACTERES =>
		"The user's password must contain a maximum of 16 characters.";
	

	// Codigo
	public static string CODIGO_INVALIDO => "The code is invalid.";

	// Token
	public static string TOKEN_NAO_INFORMADO => "The token must be informed.";
	public static string TOKEN_EXPIRADO => "The token has expired.";

	// Others
	public static string ERRO_DESCONHECIDO => "Unknown error.";

}