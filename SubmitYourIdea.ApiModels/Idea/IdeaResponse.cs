using SubmitYourIdea.ApiModels.Category;

namespace SubmitYourIdea.ApiModels.Idea;
using SubmitYourIdea.DataAccess.Entities;

public record IdeaResponse(
    int Id, 
    string Title, 
    string Description,
    string Status,
    CategoryResponse Category, 
    DateTime CreatedAt);