namespace SubmitYourIdea.ApiModels.Idea;

public record AddIdeaRequest(string Title, string Description, int CategoryId);