using ErrorOr;
using Microsoft.EntityFrameworkCore;
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

    public async Task<ErrorOr<List<CategoryResponse>>> GetCategories()
    {
        return await _db.Categories
            .Select(x => x.ToCategoryResponse())
            .ToListAsync();
    }

    public async Task<ErrorOr<CategoryResponse>> GetCategoryById(int id)
    {
        var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category is null)
            return CategoryErrors.CategoryNotFound;
        return category.ToCategoryResponse();
    }

    public async Task<ErrorOr<CategoryResponse>> AddCategory(AddCategoryRequest request)
    {
        var categoryName = request.Name.Trim().ToLower();

        if (await _db.Categories.AnyAsync(genre => genre.Name == categoryName))
        {
            return CategoryErrors.DuplicatedCategory;
        }

        var category = request.ToCategory();
        await _db.Categories.AddAsync(category);
        await _db.SaveChangesAsync();
        return category.ToCategoryResponse();
    }

    public async Task<ErrorOr<Success>> UpdateCategory(UpdateCategoryRequest request)
    {
        var categoryName = request.Name.Trim().ToLower();
        var isExist = await _db.Categories
            .AnyAsync(c => c.Id != request.Id && c.Name == categoryName);
        if (isExist)
        {
            return CategoryErrors.DuplicatedCategory;
        }

        var category = _db.Categories.FirstOrDefault(x => x.Id == request.Id);
        if (category is null)
        {
            return CategoryErrors.CategoryNotFound;
        }

        category.Name = categoryName;
        await _db.SaveChangesAsync();
        return Result.Success;
    }

    public async Task<ErrorOr<Success>> DeleteCategory(int id)
    {
        var category = _db.Categories.FirstOrDefault(x => x.Id == id);
        if (category is null)
        {
            return CategoryErrors.DuplicatedCategory;
        }

        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
        return Result.Success;
    }
}