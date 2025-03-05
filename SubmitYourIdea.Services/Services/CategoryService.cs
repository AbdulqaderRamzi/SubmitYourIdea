using System.Net;
using Microsoft.EntityFrameworkCore;
using SubmitYourIdea.ApiModels.Api;
using SubmitYourIdea.ApiModels.Category;
using SubmitYourIdea.DataAccess;
using SubmitYourIdea.Services.Errors;
using SubmitYourIdea.Services.Interfaces;
using SubmitYourIdea.Services.Mappings;

namespace SubmitYourIdea.Services.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _db;

    public CategoryService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ApiResponse<List<CategoryResponse>>> Get()
    {
        var categories = await _db.Categories
            .Select(x => x.ToCategoryResponse())
            .ToListAsync();
        return new ApiResponse<List<CategoryResponse>>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = categories
        };
    }

    public async Task<ApiResponse<CategoryResponse>> Get(int id)
    {
        var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category is null)
            return CategoryErrors.NotFound<CategoryResponse>();
        return new ApiResponse<CategoryResponse>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = category.ToCategoryResponse()
        };
    }

    public async Task<ApiResponse<CategoryResponse>> Add(AddCategoryRequest request)
    {
        var categoryName = request.Name.Trim().ToLower();

        if (await _db.Categories.AnyAsync(genre => genre.Name == categoryName))
        {
            return CategoryErrors.Duplication<CategoryResponse>();
        }

        var category = request.ToCategory();
        await _db.Categories.AddAsync(category);
        await _db.SaveChangesAsync();
        
        return new ApiResponse<CategoryResponse>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = category.ToCategoryResponse()
        };
    }

    public async Task<ApiResponse<object>> Update(UpdateCategoryRequest request)
    {
        var categoryName = request.Name.Trim().ToLower();
        var isExist = await _db.Categories
            .AnyAsync(c => c.Id != request.Id && c.Name == categoryName);
        if (isExist)
        {
            return CategoryErrors.Duplication<object>();
        }

        var category = _db.Categories.FirstOrDefault(x => x.Id == request.Id);
        if (category is null)
        {
            return CategoryErrors.NotFound<object>();
        }

        category.Name = categoryName;
        await _db.SaveChangesAsync();
        return new ApiResponse<object>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = category.ToCategoryResponse()
        };
    }

    public async Task<ApiResponse<object>> Delete(int id)
    {
        var category = _db.Categories.FirstOrDefault(x => x.Id == id);
        if (category is null)
        {
            return CategoryErrors.Duplication<object>();
        }

        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
        return new ApiResponse<object>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = category.ToCategoryResponse()
        };
    }
}