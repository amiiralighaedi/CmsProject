using Cms.Application.Auth.DTOs;
using Cms.Application.Auth.Interfaces;
using Cms.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var res = await _authService.LoginAsync(req);
        if (res == null)
            return Unauthorized();

        Response.Headers.Add("X-Access-Token", res.AccessToken);
        Response.Headers.Add("X-Refresh-Token", res.RefreshToken);

        return Ok(res);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req)
    {
        var res = await _authService.RegisterAsync(req);
        if (!res.Success)
            return BadRequest(res);

        return Ok(res);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
        var res = await _authService.RefreshToken(refreshToken);
        if (res == null)
            return Unauthorized();

        return Ok(res);
    }

    [HttpGet("captcha")]
    public IActionResult Captcha()
    {
        var (code, image) = CaptchaService.GenerateCaptcha();
        Response.Headers.Add("Captcha-Code", code);
        return File(image, "image/png");
    }
}
