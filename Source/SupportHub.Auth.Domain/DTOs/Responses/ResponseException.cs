namespace SupportHub.Auth.Domain.DTOs.Responses;


public class ResponseException(List<string> mensagem)
{
	public List<string> Mensagens { get; set; } = mensagem;
}