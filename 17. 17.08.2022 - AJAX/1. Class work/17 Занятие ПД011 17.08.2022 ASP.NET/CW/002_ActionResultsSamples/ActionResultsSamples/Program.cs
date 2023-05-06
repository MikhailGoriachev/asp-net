// для решения проблемы ввода дробных чисел в поля типа number
using System.Globalization;

// для работы с файловой системой сервера
using Microsoft.Extensions.FileProviders;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

// построение приложения
var builder = WebApplication.CreateBuilder(args);

// добавление функционала MVC
builder.Services.AddControllersWithViews();

// для работы FilesController -- обеспечение внедрения зависимости в контроллер
builder.Services.AddSingleton<IFileProvider>(
    new PhysicalFileProvider(Directory.GetCurrentDirectory()));

var app = builder.Build();

// Configure the HTTP request pipeline.
// HTML + CSS
app.UseStaticFiles();

// разрешение работы маршрутизации
app.UseRouting();

// обязательное задание маршрута
app.MapControllerRoute(
    name: "Default",  // название маршрута              
    pattern: "{controller=Home}/{action=index}/{key?}");

app.Run();