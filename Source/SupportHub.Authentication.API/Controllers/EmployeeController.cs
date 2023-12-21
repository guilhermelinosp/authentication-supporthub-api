using Microsoft.AspNetCore.Mvc;
using SupportHub.Authentication.API.Controllers.Abstract;

namespace SupportHub.Authentication.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeeController : BaseController;