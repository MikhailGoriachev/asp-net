using System.Globalization;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

// для решения проблемы ввода дробных чисел в поля типа number
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();

// подключение EF как сервиса - поставщика данных
// строку подключения определяем в appsettings.json
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

// для доступа к HTML, CSS в папке wwwroot
app.UseStaticFiles();

// разрешение маршутизации для Razor Pages
app.MapRazorPages();

app.Run();