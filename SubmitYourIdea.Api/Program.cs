using SubmitYourIdea.Api.Extensions;
using SubmitYourIdea.Api.Middleware;


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplicationServices(builder.Configuration)
        .AddIdentityServices(builder.Configuration);
}

var app = builder.Build();
{
    app.UseMiddleware<ExceptionMiddleware>();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

