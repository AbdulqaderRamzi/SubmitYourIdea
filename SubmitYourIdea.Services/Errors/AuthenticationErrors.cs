using System.Net;
using ErrorOr;
using SubmitYourIdea.ApiModels.Api;

namespace SubmitYourIdea.Services.Errors;

public static class AuthenticationErrors {
    
    public static Error RegistrationFailed(string? description)
    {
        return Error.Failure(
            code: "User.RegistrationFailed",
            description: description ?? "Registration failed."
        );
    }
    
    public static Error InvalidCredentials => Error.Validation(
        code: "User.InvalidCredentials", 
        description: "Invalid credentials"
    );
        
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
    
    public static Error AccessDenied => Error.Validation(
        code: "User.AccessDenied", 
        description: "Access denied"
    );
}
