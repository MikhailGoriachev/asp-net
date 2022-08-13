var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

// дл€ доступа к HTML, CSS в папке wwwroot
app.UseStaticFiles();

// разрешение маршутизации дл€ Razor Pages
app.MapRazorPages();


app.Run();
