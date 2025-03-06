namespace SubmitYourIdea.WebUI.Models;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; } 
    public int StatusCode { get; set; }
    public T? Data { get; set; }
    public ProblemDetails? ProblemDetails { get; set; } 
}