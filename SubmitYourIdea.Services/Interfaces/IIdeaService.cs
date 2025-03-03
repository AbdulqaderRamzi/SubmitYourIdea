using ErrorOr;
using SubmitYourIdea.ApiModels.Idea;
using SubmitYourIdea.DataAccess.Entities;

namespace SubmitYourIdea.Services.Interfaces;

public interface IIdeaService
{
    Task<ErrorOr<List<IdeaResponse>>> GetIdeas();
    Task<ErrorOr<IdeaResponse>> GetIdeasById(int id);
    Task<ErrorOr<IdeaResponse>>  AddIdea(AddIdeaRequest request);
    Task<ErrorOr<Success>> UpdateIdea(UpdateIdeaRequest request);
    Task<ErrorOr<Success>> DeleteIdea(int id);
    
    Task<ErrorOr<Success>> ApproveIdea(int id);
    Task<ErrorOr<Success>> DeclineIdea(int id);
}