using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubmitYourIdea.ApiModels;
using SubmitYourIdea.ApiModels.Auth;
using SubmitYourIdea.Services.Interfaces;

namespace SubmitYourIdea.Api.Controllers;

[Route("api/auth")]
[AllowAnonymous]
public class AuthController : ApiController
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var authResult = await _userService.RegisterAsync(request);
        return authResult.Match(
            result => Ok(result),
            error => Problem(error));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var authResult = await _userService.LoginAsync(request);
        return authResult.Match(
            result => Ok(result),
            error => Problem(error));
    }
}