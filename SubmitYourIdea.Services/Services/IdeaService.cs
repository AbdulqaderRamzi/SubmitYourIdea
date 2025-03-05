using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SubmitYourIdea.ApiModels.Api;
using SubmitYourIdea.ApiModels.Auth;
using SubmitYourIdea.ApiModels.Idea;
using SubmitYourIdea.DataAccess;
using SubmitYourIdea.DataAccess.Entities;
using SubmitYourIdea.Services.Errors;
using SubmitYourIdea.Services.Interfaces;
using SubmitYourIdea.Services.Mappings;

namespace SubmitYourIdea.Services.Services;

public class IdeaService : IIdeaService
{
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailSender _emailSender;
    
    public IdeaService(
        ApplicationDbContext db,
        IHttpContextAccessor httpContextAccessor,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IEmailSender emailSender)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _emailSender = emailSender;
    }

    public async Task<ApiResponse<List<IdeaResponse>>> Get()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return AuthenticationErrors.NotFound<List<IdeaResponse>>();
        }
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return AuthenticationErrors.NotFound<List<IdeaResponse>>();
        }

        var role = _userManager.GetRolesAsync(user).Result.First();

        var ideas = role == Roles.Admin
            ? await _db.Ideas
                .Include(x => x.Category)
                .Select(x => x.ToIdeaResponse())
                .ToListAsync()
            : await _db.Ideas
                .Include(x => x.Category)
                .Where(x => x.UserId == userId)
                .Select(x => x.ToIdeaResponse())
                .ToListAsync();

        return new ApiResponse<List<IdeaResponse>>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = ideas
        };
    }

    public async Task<ApiResponse<IdeaResponse>> Get(int id)
    {
        var idea = await _db.Ideas
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (idea is null)
        {
            return IdeaErrors.NotFound<IdeaResponse>();
        }

        return new ApiResponse<IdeaResponse>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = idea.ToIdeaResponse()
        };
        
    }

    public async Task<ApiResponse<IdeaResponse>> Add(AddIdeaRequest request)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return AuthenticationErrors.NotFound<IdeaResponse>();
        }

        if (request.CategoryId is 0)
        {
            return CategoryErrors.NotFound<IdeaResponse>();
        }

        var idea = request.ToIdea();
        
        idea.CreatedAt = DateTime.UtcNow;
        idea.CategoryId = request.CategoryId;
        idea.UserId = userId;
        
         await _db.Ideas.AddAsync(idea);
         await _db.SaveChangesAsync();
        
        var savedIdea = await _db.Ideas
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == idea.Id);

        if (savedIdea is null)
        {
            return IdeaErrors.NotFound<IdeaResponse>();
        }
        
        return new ApiResponse<IdeaResponse>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = idea.ToIdeaResponse()
        };
        
    }

    public async Task<ApiResponse<object>> Update(UpdateIdeaRequest request)
    {
        var idea = await _db.Ideas.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (idea is null)
        {
            return IdeaErrors.NotFound<object>();
        }
        idea.Title = request.Title;
        idea.Description = request.Description;
        idea.CategoryId = request.CategoryId;
        await _db.SaveChangesAsync();
        
        return new ApiResponse<object>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = idea.ToIdeaResponse()
        };
    }

    public async Task<ApiResponse<object>> Delete(int id)
    {
        var idea = await _db.Ideas.FirstOrDefaultAsync(x => x.Id == id);
        if (idea is null)
        {
            return IdeaErrors.NotFound<object>();
        }

        _db.Ideas.Remove(idea);
        await _db.SaveChangesAsync();
        return new ApiResponse<object>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = idea.ToIdeaResponse()
        };
    }
    
    public async Task<ApiResponse<object>> Approve(int id)
    {
        var idea = await _db.Ideas.FirstOrDefaultAsync(x => x.Id == id);
        if (idea is null)
        {
            return IdeaErrors.NotFound<object>();
        }

        if (idea.Status != Status.Pending)
        {
            return IdeaErrors.InvalidOperation<object>();
        }

        idea.Status = Status.Approved;
        await _db.SaveChangesAsync();
        await _emailSender.SendEmailAsync(
            "3boodg9@gmail.com",
            "Idea Response",
            "Your Idea has been approved.");
        return new ApiResponse<object>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = idea.ToIdeaResponse()
        };
    }

    public async Task<ApiResponse<object>> Decline(int id)
    {
        var idea = await _db.Ideas.FirstOrDefaultAsync(x => x.Id == id);
        if (idea is null)
        {
            return AuthenticationErrors.NotFound<object>();
        }
        
        if (idea.Status != Status.Pending)
        {
           return IdeaErrors.InvalidOperation<object>();
        }
        
        idea.Status = Status.Declined;
        await _db.SaveChangesAsync();
        await _emailSender.SendEmailAsync(
            "3boodg9@gmail.com",
            "Idea Response",
            "Your Idea has been declined.");
        return new ApiResponse<object>
        {
            IsSuccess = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = idea.ToIdeaResponse()
        };
    }
}