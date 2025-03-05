using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SubmitYourIdea.ApiModels.Auth;
using SubmitYourIdea.ApiModels.Idea;
using SubmitYourIdea.WebUI.Services.IServices;

namespace SubmitYourIdea.WebUI.Controllers;

[Authorize]
public class IdeaController : Controller
{
    private readonly IIdeaService _ideaService;
    private readonly ICategoryService _categoryService;

    public IdeaController(IIdeaService ideaService, ICategoryService categoryService)
    {
        _ideaService = ideaService;
        _categoryService = categoryService;
    }
    
    public async Task<IActionResult> Index()
    {
        var response = await _ideaService.GetIdeas();
        if (response.IsSuccess) return View(response.Data);
        return RedirectToAction("Index", "Home");
    }
    
    public async Task<IActionResult> Add()
    {
        var categoriesResponse = await _categoryService.GetCategories();
        if (!categoriesResponse.IsSuccess)
        {
            return RedirectToAction("Index");
        }
        
        ViewBag.Categories = new SelectList(categoriesResponse.Data, "Id", "Name");
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(AddIdeaRequest request)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetCategories();
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
            return View(request);
        }
        
        var response = await _ideaService.AddIdea(request);
        if (response.IsSuccess) return RedirectToAction("Index");
        
        ModelState.AddModelError("", response.Error!.Title!);
        
        var categoriesResponse = await _categoryService.GetCategories();
        ViewBag.Categories = new SelectList(categoriesResponse.Data, "Id", "Name");
        return View(request);
    }
    
    
    public async Task<IActionResult> Update(int id)
    {
        if (id is 0) return NotFound();
        
        var response = await _ideaService.GetIdeaById(id);
        var categoriesResponse = await _categoryService.GetCategories();
        
        if (!response.IsSuccess || !categoriesResponse.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        var viewModel = new UpdateIdeaRequest(
            Id: response.Data!.Id,
            Title: response.Data.Title,
            Description: response.Data.Description,
            CategoryId: response.Data.Category.Id);
        
        ViewBag.Categories = new SelectList(categoriesResponse.Data, "Id", "Name");
        return View(viewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(UpdateIdeaRequest request)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetCategories();
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
            return View(request);
        }
        
        var response = await _ideaService.UpdateIdea(request);
        if (response.IsSuccess) return RedirectToAction("Index");
        
        ModelState.AddModelError("", response.Error!.Title!);
        
        var categoriesResponse = await _categoryService.GetCategories();
        ViewBag.Categories = new SelectList(categoriesResponse.Data, "Id", "Name");
        return View(request);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        if (id is 0) return NotFound();
        await _ideaService.DeleteIdea(id);
        return RedirectToAction("Index");
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
        if (id is 0) return NotFound();
        await _ideaService.ApproveIdea(id);
        return RedirectToAction("Index");
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Decline(int id)
    {
        if (id is 0) return NotFound();
        await _ideaService.DeclineIdea(id);
        return RedirectToAction("Index");
    }
}