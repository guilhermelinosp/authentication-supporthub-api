using Microsoft.AspNetCore.Mvc;
using SupportHub.API.Controllers.Abstract;

namespace SupportHub.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeeController : BaseController;