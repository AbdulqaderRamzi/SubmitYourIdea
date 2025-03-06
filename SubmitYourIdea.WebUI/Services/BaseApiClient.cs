using System.Net;
using System.Net.Http.Headers;
using System.Text;
using MovieMate.Web.Services.IServices;
using Newtonsoft.Json;
using SubmitYourIdea.WebUI.Exceptions;
using SubmitYourIdea.WebUI.Models;
using SubmitYourIdea.WebUI.Services.IServices;

namespace SubmitYourIdea.WebUI.Services;

public class BaseApiClient : IBaseApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenService _tokenService;

    public BaseApiClient(
        IHttpClientFactory httpClientFactory,
        ITokenService tokenService,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
    }

    public async Task<ApiResponse<TResponse>> SendAsync<TRequest, TResponse>(
        HttpMethod httpMethod, string endpoint, TRequest? data = default,
        bool requiresAuth = true)
    {
        var client = _httpClientFactory.CreateClient("ApiClient");
        var request = new HttpRequestMessage(httpMethod, endpoint);
        if (data is not null)
        {
            request.Content = new StringContent(
                JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        if (requiresAuth)
        {
            var token = _tokenService.GetAccessToken();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        var response = await client.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.Unauthorized && requiresAuth)
            throw new AuthException();
        
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<ApiResponse<TResponse>>(content);
            if (result is null)
                throw new Exception("Something went wrong");
            return result;
        }
        
        var problemDetails = JsonConvert.DeserializeObject<ApiResponse<TResponse>>(content);
        if (problemDetails is null)
            throw new Exception("Something went wrong");
        return problemDetails;
    }
}