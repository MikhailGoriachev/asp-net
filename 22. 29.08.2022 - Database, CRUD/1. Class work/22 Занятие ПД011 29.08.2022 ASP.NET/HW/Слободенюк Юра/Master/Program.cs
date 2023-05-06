using Master.Common;
using Master.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddHttpClient(
    HttpClientExtensions.StarWarsClient,
    client => { client.BaseAddress = new("https://swapi.dev/api/"); }
);
builder.Services.AddScoped<ICharactersService, CharactersService>();
builder.Services.AddScoped<IPlanetsService, PlanetsService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();