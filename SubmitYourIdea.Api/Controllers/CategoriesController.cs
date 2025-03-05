using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubmitYourIdea.ApiModels.Auth;
using SubmitYourIdea.ApiModels.Category;
using SubmitYourIdea.Services.Interfaces;

namespace SubmitYourIdea.Api.Controllers;

[Route("api/categories")]
[Authorize(Roles = Roles.Admin)]
public class CategoriesController : ApiController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _categoryService.Get();
        if (result.IsSuccess)
        {
            return Ok(result);
        } 
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _categoryService.Get(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        } 
        return BadRequest(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddCategoryRequest request)
    {
        var result = await _categoryService.Add(request);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCategoryRequest request)
    {
        var result = await _categoryService.Update(request);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _categoryService.Delete(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        } 
        return BadRequest(result);
    }
}