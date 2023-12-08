using System.Net;
using Microsoft.AspNetCore.Mvc;
using SupportHub.Auth.API.Controllers.Abstract;
using SupportHub.Auth.Domain.Shared.Returns;

namespace SupportHub.Auth.API.Controllers;

[Route("api/v1/[controller]")]
public class CustomerController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBase<string>))]
    public async Task<ActionResult> Teste()
    {
        return ResponseBase(HttpStatusCode.OK, BasicReturn.Failure(new("400", "Deu Ruim")), "Deu Certo");
    }
}