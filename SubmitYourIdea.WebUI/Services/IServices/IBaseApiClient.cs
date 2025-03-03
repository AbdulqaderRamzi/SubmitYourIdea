using SubmitYourIdea.WebUI.Models;

namespace SubmitYourIdea.WebUI.Services.IServices;

public interface IBaseApiClient
{
    Task<ApiResponse<TResponse>> SendAsync<TRequest, TResponse>(
        HttpMethod httpMethod, string endpoint, TRequest? data = default, 
        bool requiresAuth = true);
}