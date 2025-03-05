using SubmitYourIdea.Api.Extensions;
using SubmitYourIdea.Api.Middleware;
using SubmitYourIdea.DataAccess.DbInitializer;


var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddIdentityServices(builder.Configuration);

var app = builder.Build();
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
SeedDatabase();
app.Run();

return;

void SeedDatabase()
{
    using var scope = app.Services.CreateScope();
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize().GetAwaiter().GetResult();
}