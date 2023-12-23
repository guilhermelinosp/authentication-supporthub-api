using Employee.SupportHub.Domain.DTOs.Requests;
using Employee.SupportHub.Domain.Exceptions;
using Employee.SupportHub.Domain.Messages;
using FluentValidation;

namespace Employee.SupportHub.Application.UseCases.Validators;

public class ForgotPasswordValidator : AbstractValidator<RequestForgotPassword>
{
	public ForgotPasswordValidator()
	{
		RuleFor(e => e.Email)
			.NotEmpty()
			.WithMessage(MessageException.EMAIL_NAO_INFORMADO)
			.EmailAddress()
			.WithMessage(MessageException.EMAIL_INVALIDO);
	}
}