using System.Globalization;
using Homework.Services;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<SWAPIService>();

builder.Services.AddHttpClient("JsonPlaceholder", 
    client => client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/"));

builder.Services.AddHttpClient("SWAPI", 
    client => client.BaseAddress = new Uri("https://swapi.dev/api/"));



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