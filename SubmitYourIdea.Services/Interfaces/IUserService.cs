using ErrorOr;
using SubmitYourIdea.ApiModels;
using SubmitYourIdea.ApiModels.Auth;

namespace SubmitYourIdea.Services.Interfaces;

public interface IUserService
{
    Task<ErrorOr<AuthenticationResponse>> RegisterAsync(RegisterRequest request);
    Task<ErrorOr<AuthenticationResponse>> LoginAsync(LoginRequest request);
}