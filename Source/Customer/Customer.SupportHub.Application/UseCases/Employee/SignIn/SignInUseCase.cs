using System.Globalization;
using Customer.SupportHub.Application.Services.Cryptography;
using Customer.SupportHub.Application.Services.Tokenization;
using Customer.SupportHub.Application.UseCases.Validators;
using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.DTOs.Responses;
using Customer.SupportHub.Domain.Exceptions;
using Customer.SupportHub.Domain.Messages;
using Customer.SupportHub.Domain.Repositories;
using Customer.SupportHub.Domain.Services;
using Microsoft.Extensions.Configuration;

namespace Customer.SupportHub.Application.UseCases.Employee.SignIn;

public class SignInUseCase(
	IEmployeeRepository repository,
	ICryptographyService cryptographyService,
	ITokenizationService tokenizationService,
	IRedisService redis,
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
			throw new DefaultException([MessageException.EMAIL_NAO_ENCONTRADO]);

		if (!cryptographyService.VerifyPassword(request.Password, account.Password!))
			throw new DefaultException([MessageException.SENHA_INVALIDA]);
		
		if (account.IsDisabled)
			throw new DefaultException([MessageException.CONTA_DESATIVADA]);

		var session = redis.ValidateSessionAccountAsync(account.EmployeeId.ToString());
		if (session)
			throw new DefaultException([MessageException.SESSION_ATIVA]);

		redis.SetSessionAccountAsync(account.EmployeeId.ToString());

		return new ResponseToken
		{
			Token = tokenizationService.GenerateToken(account.EmployeeId.ToString()),
			RefreshToken = tokenizationService.GenerateRefreshToken(),
			ExpiryDate =
				DateTime.UtcNow.Add(TimeSpan.Parse(configuration["Jwt_Expiry"]!, CultureInfo.InvariantCulture))
		};
	}
}