using SubmitYourIdea.ApiModels.Api;
using SubmitYourIdea.ApiModels.Category;

namespace SubmitYourIdea.Services.Interfaces;

public interface ICategoryService
{
    Task<ApiResponse<List<CategoryResponse>>> Get();
    Task<ApiResponse<CategoryResponse>> Get(int id);
    Task<ApiResponse<CategoryResponse>> Add(AddCategoryRequest category);
    Task<ApiResponse<object>> Update(UpdateCategoryRequest idea);
    Task<ApiResponse<object>> Delete(int id);
}