using SupportHub.Auth.Application.Abstract;
using SupportHub.Auth.Domain.Dtos.Requests.Companies;

namespace SupportHub.Auth.Application.UseCases.Companies.ForgotPassword;

public interface IForgotPasswordUseCase : IUseCaseBase
{
    Task ExecuteAsync(RequestForgotPassword request);
}