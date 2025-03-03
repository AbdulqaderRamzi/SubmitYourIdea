namespace SubmitYourIdea.ApiModels.Idea;

public record UpdateIdeaRequest(int Id, string Title, string Description, int CategoryId);