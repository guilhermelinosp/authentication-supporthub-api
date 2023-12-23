using Company.SupportHub.Application.Services.Cryptography;
using Company.SupportHub.Application.UseCases.Validators;
using Company.SupportHub.Domain.APIs;
using Company.SupportHub.Domain.Cache;
using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.DTOs.Responses;
using Company.SupportHub.Domain.Entities;
using Company.SupportHub.Domain.Exceptions;
using Company.SupportHub.Domain.Repositories;
using Company.SupportHub.Domain.Services;

namespace Company.SupportHub.Application.UseCases.Companies.SignUp;

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

		await brazilApi.ConsultaCnpj(request.Cnpj);

		if (request.Password != request.PasswordConfirmation)
			throw new DefaultException([MessagesException.SENHA_NAO_CONFERE]);

		var company = new Domain.Entities.Company
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