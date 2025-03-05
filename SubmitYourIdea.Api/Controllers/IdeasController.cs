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
    public async Task<IActionResult> Get()
    {
        var result = await _ideaService.Get();
        return result.Match(
            ideas => Ok(ideas),
            error => Problem(error));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _ideaService.Get(id);
        return result.Match(
            idea => Ok(idea),
            error => Problem(error));
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddIdeaRequest request)
    {
        var result = await _ideaService.Add(request);
        return result.Match(
            idea => Ok(idea),
            error => Problem(error));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateIdeaRequest request)
    {
        var result = await _ideaService.Update(request);
        return result.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _ideaService.Delete(id);
        return result.Match(
            success => Ok(success),
            error => Problem(error));
    }
    
    [HttpPost("approve/{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Approve(int id)
    {
        var result = await _ideaService.Approve(id);
        return result.Match(
            success => NoContent(),
            error => Problem(error));
    } 
    
    [HttpPost("decline/{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Decline(int id)
    {
        var result = await _ideaService.Decline(id);
        return result.Match(
            success => NoContent(),
            error => Problem(error));
    } 
}