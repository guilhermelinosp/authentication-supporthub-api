using SupportHub.Auth.Application.Abstract;
using SupportHub.Auth.Domain.Dtos.Requests.Companies;

namespace SupportHub.Auth.Application.UseCases.Companies.ResetPassword;

public interface IResetPasswordUseCase : IUseCaseBase
{
    Task ExecuteAsync(RequestResetPassword request, string code);
}