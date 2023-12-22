using System.Globalization;
using Microsoft.Extensions.Configuration;
using SupportHub.Application.Services.Cryptography;
using SupportHub.Application.Services.Tokenization;
using SupportHub.Application.UseCases.Employees.Validators;
using SupportHub.Domain.Cache;
using SupportHub.Domain.DTOs.Requests;
using SupportHub.Domain.DTOs.Responses;
using SupportHub.Domain.Exceptions;
using SupportHub.Domain.Repositories;
using SupportHub.Domain.Services;

namespace SupportHub.Application.UseCases.Employees.SignIn;

public class SignInUseCase(
	IEmployeeRepository repository,
	ICryptographyService cryptographyService,
	ITokenizationService tokenizationService,
	ISessionCache sessionCache,
	IConfiguration configuration)
	: ISignInUseCase
{
	public async Task<ResponseToken> ExecuteAsync(RequestSignIn request)
	{
		var validator = await new SignInValidator().ValidateAsync(request);
		if (!validator.IsValid)
			throw new DefaultException(validator.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindEmployeeByEmailAsync(request.Email);
		if (account is null)
			throw new DefaultException([MessagesException.EMAIL_NAO_ENCONTRADO]);

		if (!cryptographyService.VerifyPassword(request.Password, account.Password))
			throw new DefaultException([MessagesException.SENHA_INVALIDA]);

		var session = sessionCache.ValidateSessionAsync(account.CompanyId.ToString());
		if (session)
			throw new DefaultException([MessagesException.SESSION_ATIVA]);

		sessionCache.SetSessionAccountAsync(account.EmployeeId.ToString());

		return new ResponseToken
		{
			Token = tokenizationService.GenerateToken(account.EmployeeId.ToString()),
			RefreshToken = tokenizationService.GenerateRefreshToken(),
			ExpiryDate =
				DateTime.UtcNow.Add(TimeSpan.Parse(configuration["Jwt_Expiry"]!, CultureInfo.InvariantCulture))
		};
	}
}