// для решения проблемы ввода дробных чисел в поля типа number
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

// построение приложения
var builder = WebApplication.CreateBuilder(args);

// добавление функционала MVC
builder.Services.AddControllersWithViews();

// ------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
// HTML + CSS
app.UseStaticFiles();

// разрешение работы маршрутизации
app.UseRouting();

// Ограничение exists используется для того, чтобы маршрут сопоставлялся
// только с теми областями, которые определены в приложении
app.MapControllerRoute(
    name: "Task2", pattern: "{area:exists}/{controller=Home}/{action=index}");

// обязательное задание маршрута
app.MapControllerRoute(
    name: "Default",  // название маршрута              
    pattern: "{controller=Home}/{action=index}/{id?}");

app.Run();