using ErrorOr;
using SubmitYourIdea.ApiModels.Api;
using SubmitYourIdea.ApiModels.Idea;
using SubmitYourIdea.DataAccess.Entities;

namespace SubmitYourIdea.Services.Interfaces;

public interface IIdeaService
{
    Task<ApiResponse<List<IdeaResponse>>> Get();
    Task<ApiResponse<IdeaResponse>> Get(int id);
    Task<ApiResponse<IdeaResponse>>  Add(AddIdeaRequest request);
    Task<ApiResponse<object>> Update(UpdateIdeaRequest request);
    Task<ApiResponse<object>> Delete(int id);
    Task<ApiResponse<object>> Approve(int id);
    Task<ApiResponse<object>> Decline(int id);
}