using SubmitYourIdea.ApiModels.Category;
using SubmitYourIdea.ApiModels.Idea;
using SubmitYourIdea.DataAccess.Entities;

namespace SubmitYourIdea.Services.Mappings;

public static class IdeaMapper
{
    public static IdeaResponse ToIdeaResponse(this Idea idea)
    {
        return new IdeaResponse(
            idea.Id,
            idea.Title, 
            idea.Description,
            idea.Status, 
            new CategoryResponse(idea.CategoryId, idea.Category!.Name),
            idea.CreatedAt);
    }
    
    public static Idea ToIdea(this AddIdeaRequest idea)
    {
        return new Idea
        {
            Title = idea.Title,
            Description = idea.Description,
        };
    }
}