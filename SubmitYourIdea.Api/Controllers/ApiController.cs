using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubmitYourIdea.ApiModels.Api;

namespace SubmitYourIdea.Api.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
   
}