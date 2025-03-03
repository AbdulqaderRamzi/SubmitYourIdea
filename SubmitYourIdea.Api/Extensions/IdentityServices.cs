using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SubmitYourIdea.DataAccess;
using SubmitYourIdea.DataAccess.Entities;
using SubmitYourIdea.Services.Common;

namespace SubmitYourIdea.Api.Extensions;

public static class IdentityServices
{
    public static IServiceCollection AddIdentityServices(
        this IServiceCollection services, IConfiguration config)
    {
        services
            .AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.Configure<JwtSettings>(config.GetSection(JwtSettings.SectionName)); 
    
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ClockSkew = TimeSpan.Zero,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["JwtSettings:Issuer"],
            ValidAudience = config["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["JwtSettings:Secret"]!)
            )
        };
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => options.TokenValidationParameters = tokenValidationParameters);
        return services;
    }
}