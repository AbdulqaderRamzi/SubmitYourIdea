using Microsoft.AspNetCore.Mvc;
using SubmitYourIdea.ApiModels.Category;
using SubmitYourIdea.Services.Interfaces;

namespace SubmitYourIdea.Api.Controllers;

[Route("api/categories")]
public class CategoriesController : ApiController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _categoryService.GetCategories();
        return result.Match(
            categories => Ok(categories),
            error => Problem(error));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var result = await _categoryService.GetCategoryById(id);
        return result.Match(
            category => Ok(category),
            error => Problem(error));
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(AddCategoryRequest request)
    {
        var result = await _categoryService.AddCategory(request);
        return result.Match(
            category => CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }),
            error => Problem(error));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryRequest request)
    {
        var result = await _categoryService.UpdateCategory(request);
        return result.Match(
            _ => NoContent(),
            error => Problem(error));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _categoryService.DeleteCategory(id);
        return result.Match(
            _ => NoContent(),
            error => Problem(error));
    }
}