using SupportHub.Auth.Domain.Dtos.Requests.Companies;

namespace SupportHub.Auth.Application.UseCases.Companies.ForgotPassword;

public interface IForgotPasswordUseCase
{
    Task ExecuteAsync(RequestForgotPassword request);
}