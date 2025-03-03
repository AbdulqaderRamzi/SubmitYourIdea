using ErrorOr;

namespace SubmitYourIdea.Services.Errors;

public static class IdeaErrors
{
    public static Error IdeaNotFound => Error.Failure(
        code: "Idea.IdeaNotFound",
        description: "Idea not found"
    );
    
    public static Error InvalidOperation => Error.Failure(
        code: "Idea.InvalidOperation", 
        description: "you cannot do this operation invalid"
    );
}