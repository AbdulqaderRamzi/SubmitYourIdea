using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SubmitYourIdea.DataAccess;
using SubmitYourIdea.DataAccess.DbInitializer;
using SubmitYourIdea.Services.Interfaces;
using SubmitYourIdea.Services.Services;

namespace SubmitYourIdea.Api.Extensions;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>( opt =>
            opt.UseSqlite(connectionString));
        services.AddValidatorsFromAssembly(
            typeof(ApplicationServices).Assembly,
            includeInternalTypes: true);
        services.AddFluentValidationAutoValidation();

        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IIdeaService, IdeaService>();
        services.AddTransient<IMailer, Mailer>();
        services.AddTransient<IEmailSender, EmailSenderAdapter>();
        
        return services;
    }
}