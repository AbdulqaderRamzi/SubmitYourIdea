using SubmitYourIdea.ApiModels.Api;
using SubmitYourIdea.ApiModels.Auth;

namespace SubmitYourIdea.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponse<AuthenticationResponse>> Register(RegisterRequest request);
    Task<ApiResponse<AuthenticationResponse>> Login(LoginRequest request);
}