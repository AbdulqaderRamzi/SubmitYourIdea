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
        if (result.IsSuccess)
        {
            return Ok(result);
        } 
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _ideaService.Get(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        } 
        return BadRequest(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddIdeaRequest request)
    {
        var result = await _ideaService.Add(request);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateIdeaRequest request)
    {
        var result = await _ideaService.Update(request);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _ideaService.Delete(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        } 
        return BadRequest(result);
    }
    
    [HttpPost("approve/{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Approve(int id)
    {
        var result = await _ideaService.Approve(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        } 
        return BadRequest(result);
    }

    [HttpPost("decline/{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Decline(int id)
    {
        var result = await _ideaService.Decline(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        } 
        return BadRequest(result);
    } 
}