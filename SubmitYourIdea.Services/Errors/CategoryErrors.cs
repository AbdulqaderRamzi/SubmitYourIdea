using System.Net;
using SubmitYourIdea.ApiModels.Api;

namespace SubmitYourIdea.Services.Errors;

public static class CategoryErrors
{
    public static ApiResponse<T> NotFound<T>()
    {
        return new ApiResponse<T>
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.NotFound,
            ProblemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Title = "Category Not Found",
                Detail = "Could not find the specified Idea.",
            }
        };
    }
    public static ApiResponse<T> Duplication<T>()
    {
        return new ApiResponse<T>
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.NotFound,
            ProblemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Title = "Duplicated category",
                Detail = "A category with this name already exists",
            }
        };
    }
}