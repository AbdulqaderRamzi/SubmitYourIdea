using SubmitYourIdea.ApiModels.Idea;
using SubmitYourIdea.WebUI.Models;
using SubmitYourIdea.WebUI.Services.IServices;

namespace SubmitYourIdea.WebUI.Services;

public class IdeaService : IIdeaService
{
    private readonly IBaseApiClient _client;

    public IdeaService(IBaseApiClient client)
    {
        _client = client;
    }

    public async Task<ApiResponse<List<IdeaResponse>>> GetIdeas()
    {
        return await _client.SendAsync<object, List<IdeaResponse>>(
            HttpMethod.Get,
            "ideas");
    }

    public async Task<ApiResponse<IdeaResponse>> GetIdeaById(int id)
    {
        return await _client.SendAsync<object, IdeaResponse>(
            HttpMethod.Get, 
            $"ideas/{id}", 
            id);
    }

    public async Task<ApiResponse<IdeaResponse>> AddIdea(AddIdeaRequest request)
    {
        return await _client.SendAsync<AddIdeaRequest, IdeaResponse>(
            HttpMethod.Post, 
            "ideas",
            request);
    }

    public async Task<ApiResponse<object>> UpdateIdea(UpdateIdeaRequest request)
    {
        return await _client.SendAsync<UpdateIdeaRequest, object>(
            HttpMethod.Put, 
            "ideas",
            request);
        
    }

    public async Task<ApiResponse<object>> DeleteIdea(int id)
    {
        return await _client.SendAsync<int, object>(
            HttpMethod.Delete, 
            $"ideas/{id}",
            id);
        
    }

    public async Task<ApiResponse<object>> ApproveIdea(int id)
    {
        return await _client.SendAsync<int, object>(
            HttpMethod.Post, 
            $"ideas/approve/{id}",
            id);
        
    }

    public async Task<ApiResponse<object>> DeclineIdea(int id)
    {
        return await _client.SendAsync<int, object>(
            HttpMethod.Post, 
            $"ideas/decline/{id}",
            id);
    }
}