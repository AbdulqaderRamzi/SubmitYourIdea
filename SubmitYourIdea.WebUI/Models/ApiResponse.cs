using System.Net;

namespace SubmitYourIdea.WebUI.Models;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; } 
    public T? Data { get; set; }
    public ApiError? Error { get; set; }
    public HttpStatusCode StatusCode { get; set; } 
}