using SupportHub.Authentication.Application.Services.Cryptography;
using SupportHub.Authentication.Application.UseCases.Companies.Validators;
using SupportHub.Domain.APIs;
using SupportHub.Domain.Cache;
using SupportHub.Domain.DTOs.Requests.Companies;
using SupportHub.Domain.DTOs.Responses;
using SupportHub.Domain.Entities;
using SupportHub.Domain.Exceptions;
using SupportHub.Domain.Repositories;
using SupportHub.Domain.Services;

namespace SupportHub.Authentication.Application.UseCases.Companies.SignUp;

public class SignUpUseCase(
	ICompanyRepository repository,
	ICryptographyService cryptography,
	ISendGridService sendGridService,
	IBrazilApi brazilApi,
	IOneTimePasswordCache oneTimePassword)
	: ISignUpUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestSignUp request)
	{
		var validatorRequest = await new SignUpValidator().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new DefaultException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var validateEmail = await repository.FindCompanyByEmailAsync(request.Email);
		if (validateEmail is not null)
			throw new DefaultException([MessagesException.EMAIL_JA_REGISTRADO]);

		var validateCnpj = await repository.FindCompanyByCnpjAsync(request.Cnpj);
		if (validateCnpj is not null)
			throw new DefaultException([MessagesException.CNPJ_JA_REGISTRADO]);

		var returnCnpj = await brazilApi.ConsultaCnpj(request.Cnpj);
		if (returnCnpj.IsFailure)
			throw new DefaultException([returnCnpj.Error.Message]);

		if (request.Password != request.PasswordConfirmation)
			throw new DefaultException([MessagesException.SENHA_NAO_CONFERE]);

		var company = new Company
		{
			Cnpj = request.Cnpj,
			Email = request.Email,
			Password = cryptography.EncryptPassword(request.Password)
		};

		var code = oneTimePassword.GenerateOneTimePassword(company.CompanyId.ToString());

		await repository.CreateCompanyAsync(company);

		await sendGridService.SendSignUpAsync(request.Email, code);

		return new ResponseDefault(company.CompanyId.ToString(), MessagesResponse.CODIGO_ENVIADO_SIGN_UP);
	}
}