using SubmitYourIdea.ApiModels.Category;
using SubmitYourIdea.ApiModels.Idea;
using SubmitYourIdea.WebUI.Models;

namespace SubmitYourIdea.WebUI.Services.IServices;

public interface IIdeaService
{
    Task<ApiResponse<List<IdeaResponse>>> GetIdeas();
    Task<ApiResponse<IdeaResponse>> GetIdeaById(int id);
    Task<ApiResponse<IdeaResponse>> AddIdea(AddIdeaRequest request);
    Task<ApiResponse<object>> UpdateIdea(UpdateIdeaRequest request);
    Task<ApiResponse<object>> DeleteIdea(int id);
    Task<ApiResponse<object>> ApproveIdea(int id);
    Task<ApiResponse<object>> DeclineIdea(int id);
}