using SubmitYourIdea.ApiModels.Category;
using SubmitYourIdea.DataAccess.Entities;

namespace SubmitYourIdea.Services.Mappings;

public static class CategoryMapper
{

    public static CategoryResponse ToCategoryResponse(this Category category)
    {
        return new CategoryResponse(category.Id, category.Name);
    }
    
    public static Category ToCategory(this AddCategoryRequest category)
    {
        return new Category {Name = category.Name.Trim().ToLower()};
    }
    
    public static Category ToCategory(this UpdateCategoryRequest category)
    {
        return new Category {Name = category.Name.Trim().ToLower()};
    }
}