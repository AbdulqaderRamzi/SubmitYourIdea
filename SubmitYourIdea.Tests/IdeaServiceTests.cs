/*using System.Security.Claims;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SubmitYourIdea.DataAccess;
using SubmitYourIdea.DataAccess.Entities;
using SubmitYourIdea.Services.Services;
using SubmitYourIdea.Services.Errors;
 
namespace SubmitYourIdea.Tests;

public class IdeaServiceTests : IDisposable
{
    private readonly IdeaService _ideaService;
    private readonly ApplicationDbContext _dbContext;

    public IdeaServiceTests()
    {
        // Setup In-Memory Database
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        _dbContext = new ApplicationDbContext(options);

        // Mock Dependencies
        var httpContextAccessor = A.Fake<IHttpContextAccessor>();
        var userManager = A.Fake<UserManager<AppUser>>();
        var roleManager = A.Fake<RoleManager<IdentityRole>>();
        var emailSender = A.Fake<IEmailSender>();

        var fakeHttpContext = new DefaultHttpContext();
        var fakeUser = new ClaimsPrincipal(new ClaimsIdentity([
            new Claim(ClaimTypes.NameIdentifier, "TestUserId")
        ], "TestAuthType"));

        fakeHttpContext.User = fakeUser;
        A.CallTo(() => httpContextAccessor.HttpContext).Returns(fakeHttpContext);

        // Configure UserManager to return a fake user
        var fakeAppUser = new AppUser
        {
            Id = "TestUserId",
            UserName = "TestUser",
            FirstName = "abdulqader",
            LastName = "ramzi"
        };
        A.CallTo(() => userManager.FindByIdAsync("TestUserId"))!.Returns(Task.FromResult(fakeAppUser));

        // Initialize IdeaService
        _ideaService = new IdeaService(_dbContext, httpContextAccessor, userManager, roleManager, emailSender);
    }

    [Fact]
    public async Task GetIdeasById_Returns_Idea_When_Found()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "TestCategory" };
        _dbContext.Categories.Add(category);
        
        var idea = new Idea
        {
            Id = 1,
            UserId = "TestUserId",
            CategoryId = 1,
            Title = "Test Idea",
            CreatedAt = DateTime.UtcNow,
            Description = "Test Description"
        };
        await _dbContext.Ideas.AddAsync(idea);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _ideaService.Get(1);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Test Idea", result.ProblemDetails.Title);
        Assert.Equal("Test Description", result.ProblemDetails.Detail);
        Assert.Equal(1, result.Data.Id);
    }

    [Theory]
    [InlineData(999)]
    [InlineData(-1)]
    public async Task GetIdeasById_Returns_Error_When_Not_Found(int invalidId)
    {
        // Act
        var result = await _ideaService.Get(invalidId);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal(IdeaErrors.IdeaNotFound, result.FirstError);
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}*/