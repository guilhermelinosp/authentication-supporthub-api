using Microsoft.AspNetCore.Mvc;
using SupportHub.Auth.Domain.Dtos.Responses.Exceptions;

namespace SupportHub.Auth.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType<ResponseException>(StatusCodes.Status400BadRequest)]
public class TokenController():Controller
{
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokenRequest()
    {
        return Ok();
    }
    
    [HttpPost("revoke")]
    public async Task<IActionResult> RevokeTokenRequest()
    {
        return Ok();
    }
    
    [HttpPost("verify")]
    public async Task<IActionResult> VerifyTokenRequest()
    {
        return Ok();
    }
}