﻿using SupportHub.Domain.DTOs.Requests.Companies;
using SupportHub.Domain.DTOs.Responses;

namespace SupportHub.Application.UseCases.Employees.ForgotPassword.Confirmation;

public interface IResetPasswordUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(RequestResetPassword request, string accountId, string code);
}