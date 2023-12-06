namespace SupportHub.Auth.Domain.Dtos.Responses.Exceptions;

public class ResponseException
{
    public ResponseException(List<string> mensagens)
    {
        Mensagens = mensagens;
    }

    public List<string> Mensagens { get; set; }
}