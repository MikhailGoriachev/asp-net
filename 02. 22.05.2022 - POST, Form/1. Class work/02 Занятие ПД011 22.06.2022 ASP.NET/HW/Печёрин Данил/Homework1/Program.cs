var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// для доступа к HTML, Css в папке wwwroot
app.UseStaticFiles();

//разрешение маршрутизации для Razor Pages
app.MapRazorPages();



app.Run();
