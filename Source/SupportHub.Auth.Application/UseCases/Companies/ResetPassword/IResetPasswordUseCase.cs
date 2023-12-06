using SupportHub.Auth.Domain.Dtos.Requests.Companies;

namespace SupportHub.Auth.Application.UseCases.Companies.ResetPassword;

public interface IResetPasswordUseCase
{
    Task ExecuteAsync(RequestResetPassword request, string code);
}