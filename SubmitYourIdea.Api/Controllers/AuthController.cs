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
        var result = await _userService.Register(request);
        if (result.IsSuccess)
        {
            return Ok(result);
        } 
        return BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _userService.Login(request);
        if (result.IsSuccess)
        {
            return Ok(result);
        } 
        return BadRequest(result);
    }
}