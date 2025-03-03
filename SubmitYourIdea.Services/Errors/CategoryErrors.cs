using ErrorOr;

namespace SubmitYourIdea.Services.Errors;

public static class CategoryErrors
{
    public static Error CategoryNotFound => Error.NotFound(
            code: "Category.CategoryNotFound",
            description: "Category not found"
        );
    
    public static Error DuplicatedCategory => Error.Conflict(
        code: "Category.DuplicatedCategory", 
        description: "A category with this name already exists"
    );
}