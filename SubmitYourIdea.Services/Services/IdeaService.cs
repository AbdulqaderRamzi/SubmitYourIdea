using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
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
    private readonly RoleManager<IdentityRole> _roleManager;
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
        _roleManager = roleManager;
        _emailSender = emailSender;
    }

    public async Task<ErrorOr<List<IdeaResponse>>> Get()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return IdeaErrors.InvalidOperation;
        }
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return IdeaErrors.InvalidOperation;
        }

        var role = _userManager.GetRolesAsync(user).Result.First();

        return role == Roles.Admin
            ? await _db.Ideas
                .Include(x => x.Category)
                .Select(x => x.ToIdeaResponse())
                .ToListAsync()
            : await _db.Ideas
                .Include(x => x.Category)
                .Where(x => x.UserId == userId)
                .Select(x => x.ToIdeaResponse())
                .ToListAsync();
    }

    public async Task<ErrorOr<IdeaResponse>> Get(int id)
    {
        var idea = await _db.Ideas
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (idea is null)
            return IdeaErrors.IdeaNotFound;
        return idea.ToIdeaResponse();    }

    public async Task<ErrorOr<IdeaResponse>> Add(AddIdeaRequest request)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null || request.CategoryId is 0)
        {
            return IdeaErrors.InvalidOperation;
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
            return IdeaErrors.IdeaNotFound; 
        }
        
       
        return savedIdea.ToIdeaResponse();
        
    }

    public async Task<ErrorOr<Success>> Update(UpdateIdeaRequest request)
    {
        var idea = await _db.Ideas.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (idea is null)
        {
            return IdeaErrors.IdeaNotFound;
        }
        idea.Title = request.Title;
        idea.Description = request.Description;
        idea.CategoryId = request.CategoryId;
        
        await _db.SaveChangesAsync();
        return Result.Success;
    }

    public async Task<ErrorOr<Success>> Delete(int id)
    {
        var idea = await _db.Ideas.FirstOrDefaultAsync(x => x.Id == id);
        if (idea is null)
        {
            return IdeaErrors.IdeaNotFound;
        }

        _db.Ideas.Remove(idea);
        await _db.SaveChangesAsync();
        return Result.Success;
    }
    
    public async Task<ErrorOr<Success>> Approve(int id)
    {
        var idea = await _db.Ideas.FirstOrDefaultAsync(x => x.Id == id);
        if (idea is null)
        {
            return IdeaErrors.IdeaNotFound;
        }

        if (idea.Status != Status.Pending)
        {
            return IdeaErrors.InvalidOperation;
        }

        idea.Status = Status.Approved;
        await _db.SaveChangesAsync();
        _emailSender.SendEmailAsync(
            "3boodg9@gmail.com",
            "Idea Response",
            "Your Idea has been approved.");
        return Result.Success;
    }

    public async Task<ErrorOr<Success>> Decline(int id)
    {
        var idea = await _db.Ideas.FirstOrDefaultAsync(x => x.Id == id);
        if (idea is null)
        {
            return IdeaErrors.IdeaNotFound;
        }
        
        if (idea.Status != Status.Pending)
        {
            return IdeaErrors.InvalidOperation;
        }
        
        idea.Status = Status.Declined;
        await _db.SaveChangesAsync();
        _emailSender.SendEmailAsync(
            "3boodg9@gmail.com",
            "Idea Response",
            "Your Idea has been declined.");
        return Result.Success;
    }
}