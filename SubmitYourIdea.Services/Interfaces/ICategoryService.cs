using ErrorOr;
using SubmitYourIdea.ApiModels.Category;
using SubmitYourIdea.DataAccess.Entities;

namespace SubmitYourIdea.Services.Interfaces;

public interface ICategoryService
{
    Task<ErrorOr<List<CategoryResponse>>> GetCategories();
    Task<ErrorOr<CategoryResponse>> GetCategoryById(int id);
    Task<ErrorOr<CategoryResponse>> AddCategory(AddCategoryRequest category);
    Task<ErrorOr<Success>> UpdateCategory(UpdateCategoryRequest idea);
    Task<ErrorOr<Success>> DeleteCategory(int id);
}