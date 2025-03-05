using System.Net;

namespace SubmitYourIdea.ApiModels.Api;

public class ProblemDetails
{
    public required int Status { get; set; }
    public required string Title { get; set; }
    public required string Detail { get; set; }
}