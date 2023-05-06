using System.Globalization;
using Homework.Infrastructure;
using Homework.Services;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddMemoryCache()
    .AddSingleton<SWAPIService>()
    .AddSingleton<Logger>()
    .AddSingleton<ProfileAttribute>()
    .AddHttpClient("SWAPI", 
        client =>
        {
            client.BaseAddress = new Uri("https://swapi.dev/api/");
            client.Timeout = TimeSpan.FromMinutes(5);
        });

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();