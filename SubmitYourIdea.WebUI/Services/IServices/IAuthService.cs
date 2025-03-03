using System.Security.Claims;
using SubmitYourIdea.ApiModels.Auth;
using SubmitYourIdea.WebUI.Models;

namespace SubmitYourIdea.WebUI.Services.IServices;

public interface IAuthService
{
    Task<ApiResponse<AuthenticationResponse>> LoginAsync(LoginRequest request);
    Task<ApiResponse<AuthenticationResponse>> RegisterAsync(RegisterRequest request);
    Task<ApiResponse<object>> LogoutAsync(string userId);
    ClaimsPrincipal AddClaims(AuthenticationResponse response);
}