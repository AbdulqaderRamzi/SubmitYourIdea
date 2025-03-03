using SubmitYourIdea.ApiModels.Category;
using SubmitYourIdea.WebUI.Models;
using SubmitYourIdea.WebUI.Services.IServices;

namespace SubmitYourIdea.WebUI.Services;

public class CategoryService : ICategoryService
{
    private readonly IBaseApiClient _client;

    public CategoryService(IBaseApiClient client)
    {
        _client = client;
    }
    
    public async Task<ApiResponse<List<CategoryResponse>>> GetCategories()
    {
        return await _client.SendAsync<object, List<CategoryResponse>>(
            HttpMethod.Get,
            "categories");
    }

    public async Task<ApiResponse<CategoryResponse>> GetCategoryById(int id)
    {
        return await _client.SendAsync<int, CategoryResponse>(
            HttpMethod.Get,
            $"categories/{id}", 
            id);
    }

    public async Task<ApiResponse<CategoryResponse>> AddCategory(AddCategoryRequest request)
    {
        return await _client.SendAsync<AddCategoryRequest, CategoryResponse>(
            HttpMethod.Post, 
            "categories",
            request);
    }

    public async Task<ApiResponse<object>> UpdateCategory(UpdateCategoryRequest request)
    {
        return await _client.SendAsync<UpdateCategoryRequest, object>(
            HttpMethod.Put, 
            "categories",
            request);
    }

    public async Task<ApiResponse<object>> DeleteCategory(int id)
    {
        return await _client.SendAsync<int, object>(
            HttpMethod.Delete, 
            $"categories/{id}",
            id);
    }
}