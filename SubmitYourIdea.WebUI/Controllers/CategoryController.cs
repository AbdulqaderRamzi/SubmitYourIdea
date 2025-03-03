using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubmitYourIdea.ApiModels.Category;
using SubmitYourIdea.WebUI.Services.IServices;

namespace SubmitYourIdea.WebUI.Controllers;

[Authorize]
public class CategoryController : Controller
{
   private readonly ICategoryService _categoryService;

   public CategoryController(ICategoryService categoryService)
   {
      _categoryService = categoryService;
   }
   
   public async Task<IActionResult> Index()
   {
      var response = await _categoryService.GetCategories();
      if (response.IsSuccess) return View(response.Data);
      return RedirectToAction("Index", "Home");
   }
   
   public IActionResult Add()
   {
      return View();
   }
   
   [HttpPost]
   public async Task<IActionResult> Add(AddCategoryRequest request)
   {
      if (!ModelState.IsValid) return View(request);
      var response = await _categoryService.AddCategory(request);
      if (response.IsSuccess) return RedirectToAction("Index");
      ModelState.AddModelError("", response.Error!.Title!);
      return View(request);
   }
   
   public async Task<IActionResult> Update(int id)
   {
      if (id is 0) return NotFound();
      var response = await _categoryService.GetCategoryById(id);
      if (!response.IsSuccess) return RedirectToAction("Index");
    
      var updateRequest = new UpdateCategoryRequest(
         Id: response.Data!.Id,
         Name: response.Data.Name);
    
      return View(updateRequest);
   }

   [HttpPost]
   public async Task<IActionResult> Update(UpdateCategoryRequest request)
   {
      if (!ModelState.IsValid) return View(request);
    
      var updateRequest = new UpdateCategoryRequest(Id: request.Id, Name: request.Name);
      var response = await _categoryService.UpdateCategory(updateRequest);
    
      if (response.IsSuccess) return RedirectToAction("Index");
    
      ModelState.AddModelError("", response.Error!.Title!);
      return View(request);
   }
   
   [HttpPost]
   public async Task<IActionResult> Delete(int id)
   {
      if (id is 0) return NotFound();
      await _categoryService.DeleteCategory(id);
      return RedirectToAction("Index");
   }
}