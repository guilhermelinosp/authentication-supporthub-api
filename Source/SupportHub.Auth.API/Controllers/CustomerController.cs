using System.Net;
using Microsoft.AspNetCore.Mvc;
using SupportHub.Auth.API.Controllers.Abstract;
using SupportHub.Auth.Domain.Shared.Returns;

namespace SupportHub.Auth.API.Controllers;

[Route("api/v1/[controller]")]
public class CustomerController : BaseController;