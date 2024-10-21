using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskList.Components;
using TaskList.Components.Domain.Extensions;
using TaskList.Components.Domain.Main.Services;
using TaskList.Components.Domain.Main.UseCases.Contracts;
using TaskList.Components.Domain.Main.UseCases.Create;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDataBase();
//builder.AddSmtp();
builder.AddJwtAuthentication();
builder.AddSwaggerDocumentation();
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddTransient<TokenService>();
builder.Services.AddScoped<UserHandler>();
builder.Services.AddScoped<TaskHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<Authenticationservice>();
builder.Services.AddTransient<EmailService>();
//builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7103/") });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapGet("/", () => Results.Redirect("/home"));

app.MapControllers();
app.Run();
