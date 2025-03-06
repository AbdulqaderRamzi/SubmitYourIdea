namespace SubmitYourIdea.WebUI.Models;

public class ProblemDetails
{
    public required int Status { get; set; }
    public required string Title { get; set; }
    public required string Detail { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
}
