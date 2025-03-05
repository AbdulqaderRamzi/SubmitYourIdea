using ErrorOr;
using SubmitYourIdea.ApiModels.Idea;
using SubmitYourIdea.DataAccess.Entities;

namespace SubmitYourIdea.Services.Interfaces;

public interface IIdeaService
{
    Task<ErrorOr<List<IdeaResponse>>> Get();
    Task<ErrorOr<IdeaResponse>> Get(int id);
    Task<ErrorOr<IdeaResponse>>  Add(AddIdeaRequest request);
    Task<ErrorOr<Success>> Update(UpdateIdeaRequest request);
    Task<ErrorOr<Success>> Delete(int id);
    public Task<ErrorOr<Success>> Approve(int id);
    public Task<ErrorOr<Success>> Decline(int id);
}