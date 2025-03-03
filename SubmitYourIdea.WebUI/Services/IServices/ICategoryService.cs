﻿using SubmitYourIdea.ApiModels.Category;
using SubmitYourIdea.WebUI.Models;

namespace SubmitYourIdea.WebUI.Services.IServices;

public interface ICategoryService
{
    Task<ApiResponse<List<CategoryResponse>>> GetCategories();
    Task<ApiResponse<CategoryResponse>> GetCategoryById(int id);
    Task<ApiResponse<CategoryResponse>> AddCategory(AddCategoryRequest request);
    Task<ApiResponse<object>> UpdateGenre(UpdateCategoryRequest request);
    Task<ApiResponse<object>> DeleteGenre(int id);
}