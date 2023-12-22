using System.Net;
using Microsoft.AspNetCore.Mvc;
using SupportHub.API.Controllers.Abstract;
using SupportHub.Domain.Shared.Returns;

namespace SupportHub.API.Controllers;

[Route("api/v1/[controller]")]
public class CustomerController : BaseController;