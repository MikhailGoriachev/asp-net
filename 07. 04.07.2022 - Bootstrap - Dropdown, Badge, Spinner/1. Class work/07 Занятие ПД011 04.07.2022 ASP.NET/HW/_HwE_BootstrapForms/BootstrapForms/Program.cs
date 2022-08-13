using System.Globalization;

// для решения проблемы ввода дробных чиел в поля типа number
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// для доступа к HTML, CSS в папке wwwroot
app.UseStaticFiles();

// разрешение маршутизации для Razor Pages
app.MapRazorPages();

// создать файлы с данными

app.Run();