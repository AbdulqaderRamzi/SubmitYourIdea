using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using SubmitYourIdea.ApiModels.Auth;
using SubmitYourIdea.WebUI.Models;
using SubmitYourIdea.WebUI.Services.IServices;

namespace SubmitYourIdea.WebUI.Services;

public class AuthService : IAuthService
{
    private readonly IBaseApiClient _client;

    public AuthService(IBaseApiClient client)
    {
        _client = client;
    }

    public async Task<ApiResponse<AuthenticationResponse>> LoginAsync(LoginRequest request)
    {
        return await _client.SendAsync<LoginRequest, AuthenticationResponse>(
            HttpMethod.Post, 
            "auth/login",
            request, 
            false);
    }

    public async Task<ApiResponse<AuthenticationResponse>> RegisterAsync(RegisterRequest request)
    {
        return await _client.SendAsync<RegisterRequest, AuthenticationResponse>(
            HttpMethod.Post, 
            "auth/register",
            request, 
            false);
    }

    public async Task<ApiResponse<object>> LogoutAsync(string userId)
    {
        return await _client.SendAsync<string, object>(
            HttpMethod.Delete, 
            $"auth/revoke/{userId}", 
            userId);
    }


    public ClaimsPrincipal AddClaims(AuthenticationResponse response)
    {
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, response.UserId));
        identity.AddClaim(new Claim(ClaimTypes.Name, response.FullName));
        identity.AddClaim(new Claim(ClaimTypes.Email, response.Email));
        identity.AddClaim(new Claim(ClaimTypes.Role, response.Role));
        return new ClaimsPrincipal(identity);
    }
}