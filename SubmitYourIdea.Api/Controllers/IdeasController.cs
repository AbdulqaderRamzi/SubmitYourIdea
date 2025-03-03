using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubmitYourIdea.ApiModels.Auth;
using SubmitYourIdea.ApiModels.Idea;
using SubmitYourIdea.Services.Interfaces;

namespace SubmitYourIdea.Api.Controllers;

[Route("api/ideas")]
public class IdeasController : ApiController
{
    private readonly IIdeaService _ideaService;

    public IdeasController(IIdeaService ideaService)
    {
        _ideaService = ideaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetIdeas()
    {
        var result = await _ideaService.GetIdeas();
        return result.Match(
            ideas => Ok(ideas),
            error => Problem(error));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetIdeaById(int id)
    {
        var result = await _ideaService.GetIdeasById(id);
        return result.Match(
            idea => Ok(idea),
            error => Problem(error));
    }

    [HttpPost]
    public async Task<IActionResult> AddIdea(AddIdeaRequest request)
    {
        var result = await _ideaService.AddIdea(request);
        return result.Match(
            idea => Ok(idea),
            error => Problem(error));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateIdea(UpdateIdeaRequest request)
    {
        var result = await _ideaService.UpdateIdea(request);
        return result.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteIdea(int id)
    {
        var result = await _ideaService.DeleteIdea(id);
        return result.Match(
            success => Ok(success),
            error => Problem(error));
    }

    [HttpPost("approve/{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> ApproveIdea(int id)
    {
        var result = await _ideaService.ApproveIdea(id);
        return result.Match(
            success => NoContent(),
            error => Problem(error));
    } 
    
    [HttpPost("decline/{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> DeclineIdea(int id)
    {
        var result = await _ideaService.DeclineIdea(id);
        return result.Match(
            success => NoContent(),
            error => Problem(error));
    } 
}