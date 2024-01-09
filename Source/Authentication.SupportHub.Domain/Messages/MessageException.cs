namespace Authentication.SupportHub.Domain.Messages;

public record MessageException
{
	public static string SESSION_ATIVA => "Session active.";
	public static string SESSION_EXPIRADA => "Session expired.";
	public static string CONTA_NAO_ENCONTRADA => "Account not found.";
	public static string IDENTITY_INVALIDO => "The Account's Identity is invalid.";
	public static string IDENTITY_JA_REGISTRADO => "The Account's Identity has already been registered.";
	public static string IDENTITY_NAO_INFORMADO => "The Account's Identity must be provided.";
	public static string IDENTITY_NAO_ENCONTRADO => "The Account's Identity not found.";

	public static string IDENTITY_MAXIMO_QUATORZE_CARACTERES =>
		"The Account's Identity must contain a maximum of 14 characters.";

	public static string IDENTITY_MINIMO_ONZE_CARACTERES =>
		"The Account's Identity must contain at least 11 characters.";

	public static string CONTA_BLOQUEADO => "Account blocked.";
	public static string CONTA_DESBLOQUEADO => "Account unblocked.";
	public static string CONTA_ATIVADO => "Account activated.";
	public static string CONTA_DESATIVADA => "Account deactivated.";
	public static string CONTA_NAO_AUTORIZADO => "Account not authorized.";


	// Email
	public static string EMAIL_INVALIDO => "The Account's email is invalid.";

	public static string EMAIL_NAO_AUTENTICADO =>
		"The email is not authenticated, new code has been verified your email.";

	public static string EMAIL_NAO_INFORMADO => "The Account's email must be provided.";
	public static string EMAIL_NAO_ENCONTRADO => "The Account's email not found";
	public static string EMAIL_JA_REGISTRADO => "The  Account's email provided is already registered.";


	// Password
	public static string SENHA_INVALIDA => "The Account's password is invalid.";
	public static string SENHA_NAO_CONFERE => "The Account's password does not match.";
	public static string SENHA_NAO_INFORMADO => "The Account's password must be entered.";
	public static string SENHA_MINIMO_OITO_CARACTERES => "The Account's password must contain at least 8 characters.";

	public static string SENHA_MAXIMO_DEZESSEIS_CARACTERES =>
		"The Account's password must contain a maximum of 16 characters.";


	// Codigo
	public static string CODIGO_INVALIDO => "The code is invalid.";

	// Token
	public static string TOKEN_NAO_INFORMADO => "The token must be informed.";
	public static string TOKEN_EXPIRADO => "The token has expired.";

	// Others
	public static string ERRO_DESCONHECIDO => "Unknown error.";
}