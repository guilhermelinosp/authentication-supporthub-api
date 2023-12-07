namespace SupportHub.Auth.Domain.Exceptions;

public static class MessagesException
{
    public static string CNPJ_INVALIDO => "The user's CNPJ is invalid.";
    public static string CNPJ_JA_REGISTRADO => "The user's CNPJ has already been registered.";
    public static string CNPJ_NAO_INFORMADO => "The user's CNPJ must be provided.";
    public static string CNPJ_NAO_ENCONTRADO => "The user's CNPJ not found.";
    public static string CNPJ_NAO_CONFIRMADO => "The user's CNPJ not confirmed";
    public static string CNPJ_CODIGO_INVALIDO => "The CNPJ's code is invalid";
    public static string CNPJ_NAO_AUTENTICADO => "The CNPJ's not authenticated";
    public static string CNPJ_NAO_AUTORIZADO => "The CNPJ's not authorized";
    public static string CNPJ_NAO_PODE_SER_REMOVIDO => "The CNPJ's cannot be removed";
    public static string CNPJ_NAO_PODE_SER_ATUALIZADO => "The CNPJ's cannot be updated";
    public static string CNPJ_NAO_PODE_SER_BLOQUEADO => "The CNPJ's cannot be blocked";
    public static string CNPJ_NAO_PODE_SER_DESBLOQUEADO => "The CNPJ's cannot be unblocked";


    public static string USUARIO_BLOQUEADO => "User blocked.";
    public static string USUARIO_DESBLOQUEADO => "User unblocked.";
    public static string USUARIO_ATIVADO => "User activated.";

    public static string USUARIO_DESATIVADO => "User deactivated.";
    // Account

    // Email
    public static string EMAIL_INVALIDO => "The user's email is invalid.";
    public static string EMAIL_NAO_AUTENTICADO => "The email's not authenticated";
    public static string EMAIL_NAO_CONFIRMADO => "The user's email not confirmed";
    public static string EMAIL_NAO_INFORMADO => "The user's email must be provided.";
    public static string EMAIL_NAO_ENCONTRADO => "The user's email not found";
    public static string EMAIL_JA_VERIFICADO => "The user's email has already been verified.";
    public static string EMAIL_JA_REGISTRADO => "The  user's email provided is already registered.";


    // Password
    public static string SENHA_INVALIDA => "The user's password is invalid.";
    public static string SENHA_NAO_CONFERE => "The user's password does not match.";
    public static string SENHA_NAO_INFORMADO => "The user's password must be entered.";
    public static string SENHA_MINIMO_OITO_CARACTERES => "The user's password must contain at least 8 characters.";

    public static string SENHA_MAXIMO_DEZESSEIS_CARACTERES =>
        "The user's password must contain a maximum of 16 characters.";

    // Phone
    public static string TELEFONE_INVALIDO => "The user's phone number must be in the format XXXXXXXXXXX";
    public static string TELEFONE_NAO_INFORMADO => "The user's phone number must be provided.";
    public static string TELEFONE_JA_REGISTRADO => "The user's phone number has already been registered.";
    public static string TELEFONE_NAO_CONFIRMADO => "The user's email not confirmed";
    public static string TELEFONE_CODIGO_INVALIDO => "The phone's code is invalid";

    // Codigo
    public static string CODIGO_INVALIDO => "The code is invalid.";
    public static string CODIGO_NAO_INFORMADO => "The code must be informed.";
    public static string CODIGO_EXPIRADO => "The code has expired.";
    public static string CODIGO_SEM_PERMISSAO => "You do not have permission to access this resource.";


    // Token
    public static string TOKEN_INVALIDO => "The token is invalid.";
    public static string TOKEN_NAO_INFORMADO => "The token must be informed.";
    public static string TOKEN_EXPIRADO => "The token has expired.";
    public static string TOKEN_SEM_PERMISSAO => "You do not have permission to access this resource.";

    // Others
    public static string ERRO_DESCONHECIDO => "Unknown error.";
    public static string ERRO_AO_CRIAR_USUARIO => "Error creating user.";
    public static string ERRO_AO_ATUALIZAR_USUARIO => "Error updating user.";
    public static string ERRO_AO_DELETAR_USUARIO => "Error deleting user.";
    public static string ERRO_AO_CRIAR_RECEITA => "Error creating recipe.";
    public static string ERRO_AO_ATUALIZAR_RECEITA => "Error updating recipe.";
    public static string ERRO_AO_DELETAR_RECEITA => "Error deleting recipe.";
    public static string ERRO_AO_CRIAR_INGREDIENTE => "Error creating ingredient.";
    public static string ERRO_AO_ATUALIZAR_INGREDIENTE => "Error updating ingredient.";
    public static string ERRO_AO_DELETAR_INGREDIENTE => "Error deleting ingredient.";
}