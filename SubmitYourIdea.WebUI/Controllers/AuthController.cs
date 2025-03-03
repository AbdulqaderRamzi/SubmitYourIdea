using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MovieMate.Web.Services.IServices;
using SubmitYourIdea.ApiModels.Auth;
using SubmitYourIdea.WebUI.Services.IServices;

namespace SubmitYourIdea.WebUI.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }
    
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var response = await _authService.RegisterAsync(request);
        if (!response.IsSuccess)
        {
            ModelState.AddModelError("", response.Error!.Title!);
            return View(request);
        }
        var claimsPrincipal = _authService.AddClaims(response.Data!);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal);
        _tokenService.SetToken(response.Data!.AccessToken);
        return RedirectToAction(nameof(Index), "Home");
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);
        if (!response.IsSuccess)
        {
            ModelState.AddModelError("", response.Error!.Title!);
            return View(request);
        }
        var claimsPrincipal = _authService.AddClaims(response.Data!);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal); 
        _tokenService.SetToken(response.Data!.AccessToken);
        return RedirectToAction(nameof(Index), "Home");
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        var userClaims = HttpContext.User.Claims.ToList();
        var userId = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

        await HttpContext.SignOutAsync();

        if (userId is null)
        {
            return RedirectToAction(nameof(Login));
        }

        _tokenService.ClearToken();

        return RedirectToAction(nameof(Login));
    }
} 