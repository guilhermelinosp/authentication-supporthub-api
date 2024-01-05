using System.Globalization;
using Company.SupportHub.Application.Services.Cryptography;
using Company.SupportHub.Application.Services.Tokenization;
using Company.SupportHub.Application.UseCases.Validators;
using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.DTOs.Responses;
using Company.SupportHub.Domain.Exceptions;
using Company.SupportHub.Domain.Messages;
using Company.SupportHub.Domain.Repositories;
using Company.SupportHub.Domain.Services;
using Microsoft.Extensions.Configuration;

namespace Company.SupportHub.Application.UseCases.Employee.SignIn;

public class SignInUseCase(
	IEmployeeRepository repository,
	ICryptographyService cryptographyService,
	ITokenizationService tokenizationService,
	IRedisService redis,
	IConfiguration configuration)
	: ISignInUseCase
{
	public async Task<ResponseToken> ExecuteAsync(RequestSignInEmployee request)
	{
		var validator = await new ValidatorSignInEmployee().ValidateAsync(request);
		if (!validator.IsValid)
			throw new DefaultException(validator.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindEmployeeByEmailAsync(request.Email);
		if (account is null)
			throw new DefaultException([MessageException.EMAIL_NAO_ENCONTRADO]);

		if (!cryptographyService.VerifyPassword(request.Password, account.Password!))
			throw new DefaultException([MessageException.SENHA_INVALIDA]);

		if (account.IsDisabled)
			throw new DefaultException([MessageException.CONTA_DESATIVADA]);

		var session = redis.ValidateSessionStorageAsync(account.EmployeeId.ToString());
		if (session)
			throw new DefaultException([MessageException.SESSION_ATIVA]);

		redis.SetSessionStorageAsync(account.EmployeeId.ToString());

		return new ResponseToken
		{
			Token = tokenizationService.GenerateToken(account.EmployeeId.ToString()),
			RefreshToken = tokenizationService.GenerateRefreshToken(),
			ExpiryDate =
				DateTime.UtcNow.Add(TimeSpan.Parse(configuration["Jwt_Expiry"]!, CultureInfo.InvariantCulture))
		};
	}
}