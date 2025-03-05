using System.Net;
using SubmitYourIdea.ApiModels.Api;

namespace SubmitYourIdea.Services.Errors;

public static class AuthenticationErrors {
    
    public static ApiResponse<T> InvalidOperation<T>()
    {
        return new ApiResponse<T>
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.NotFound,
            ProblemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Title = "Invalid Operation",
                Detail = "Could not complete the operation!",

            }
        };
    }
    
    public static ApiResponse<T> InvalidCredentials<T>()
    {
        return new ApiResponse<T>
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.NotFound,
            ProblemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Title = "Invalid Credentials",
                Detail = "User name or password is incorrect.",
            }
        };
    }
        
    public static ApiResponse<T> NotFound<T>()
    {
        return new ApiResponse<T>
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.NotFound,
            ProblemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Title = "Idea Not Found",
                Detail = "Could not find the specified Idea.",
                
            }
        };
    }
}
