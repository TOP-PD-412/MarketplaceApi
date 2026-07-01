using Microsoft.AspNetCore.Mvc;

namespace UsersAPI.Auth;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("register/buyer")]
    public async Task<ActionResult<AuthResponse>> RegisterAsBuyerAsync([FromBody] RegisterRequest request)
    {
        try
        {
            var claims = await authService.RegisterBuyerAsync(request);
            var response = authService.GenerateJwtToken(claims);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> LoginAsync([FromBody] LoginRequest request)
    {
        var claims = await authService.ValidateCredentials(request);
        if (claims == null) return Unauthorized();
        var response = authService.GenerateJwtToken(claims);
        return Ok(response);
    }
}