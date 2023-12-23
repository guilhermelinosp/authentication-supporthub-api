namespace Customer.SupportHub.Domain.Exceptions;

public record MessageException
{
	public static string SESSION_ATIVA => "Session active.";
	public static string CONTA_NAO_ENCONTRADA => "Account not found.";
	public static string EMAIL_INVALIDO => "The user's email is invalid.";
	public static string EMAIL_NAO_INFORMADO => "The user's email must be provided.";
	public static string EMAIL_NAO_ENCONTRADO => "The user's email not found";

	// Password
	public static string SENHA_INVALIDA => "The user's password is invalid.";
	public static string SENHA_NAO_CONFERE => "The user's password does not match.";
	public static string SENHA_NAO_INFORMADO => "The user's password must be entered.";
	public static string SENHA_MINIMO_OITO_CARACTERES => "The user's password must contain at least 8 characters.";
	public static string SENHA_MAXIMO_DEZESSEIS_CARACTERES => "The user's password must contain a maximum of 16 characters.";
	
	// Codigo
	public static string CODIGO_INVALIDO => "The code is invalid.";

	// Token
	public static string TOKEN_EXPIRADO => "The token has expired.";

	// Others
	public static string ERRO_DESCONHECIDO => "Unknown error.";

}