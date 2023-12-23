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
	ICustomerRepository repository,
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
			throw new ExceptionDefault(validator.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindCustomerByEmailAsync(request.Email);
		if (account is null)
			throw new ExceptionDefault([MessageException.EMAIL_NAO_ENCONTRADO]);

		if (!cryptographyService.VerifyPassword(request.Password, account.Password))
			throw new ExceptionDefault([MessageException.SENHA_INVALIDA]);

		var session = redis.ValidateSessionAccountAsync(account.CompanyId.ToString());
		if (session)
			throw new ExceptionDefault([MessageException.SESSION_ATIVA]);

		redis.SetSessionAccountAsync(account.CustomerId.ToString());

		return new ResponseToken
		{
			Token = tokenizationService.GenerateToken(account.CustomerId.ToString()),
			RefreshToken = tokenizationService.GenerateRefreshToken(),
			ExpiryDate =
				DateTime.UtcNow.Add(TimeSpan.Parse(configuration["Jwt_Expiry"]!, CultureInfo.InvariantCulture))
		};
	}
}