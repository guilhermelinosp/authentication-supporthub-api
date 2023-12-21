using System.Net;
using Microsoft.AspNetCore.Mvc;
using SupportHub.Authentication.Domain.Shared.Returns;
using SupportHub.Authentication.API.Controllers.Abstract;

namespace SupportHub.Authentication.API.Controllers;

[Route("api/v1/[controller]")]
public class CustomerController : BaseController;