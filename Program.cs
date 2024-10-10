using TaskList.Components;
using TaskList.Components.Domain.Extensions;
using TaskList.Components.Domain.Main.Services;
using TaskList.Components.Domain.Main.UseCases.Contracts;
using TaskList.Components.Domain.Main.UseCases.Create;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDataBase();
builder.AddSmtp();
builder.AddJwtAuthentication();
builder.Services.AddControllers();
builder.Services.AddTransient<TokenService>();
builder.Services.AddScoped<Handler>();
builder.Services.AddScoped<IRepository, UserRepository>();
builder.Services.AddTransient<EmailService>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7103") });




// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();
app.Run();
