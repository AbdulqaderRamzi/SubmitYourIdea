using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SubmitYourIdea.DataAccess.Entities;

namespace SubmitYourIdea.DataAccess.DbInitializer;

public class DbInitializer : IDbInitializer
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<DbInitializer> _logger;

    public DbInitializer(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext db,
        ILogger<DbInitializer> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
        _logger = logger;
    }

    public async Task Initialize()
    {
        try
        {
            var pendingMigrations = await _db.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await _db.Database.MigrateAsync();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
        }

        if (!await _roleManager.RoleExistsAsync("admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole("admin"));
            await _roleManager.CreateAsync(new IdentityRole("visitor"));
        }
        
        var adminUser = await _userManager.FindByEmailAsync("admin@ideas.com");
        if (adminUser is null)
        {
            adminUser = new AppUser
            {
                UserName = "admin@ideas.com",
                Email = "admin@ideas.com",
                FirstName = "admin",
                LastName = "user",
            };
            var result = await _userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "admin");
            }
            else
            {
                _logger.LogError($"Failed to create admin user: {result.Errors.FirstOrDefault()?.Description}");
            }
        }

    }
}