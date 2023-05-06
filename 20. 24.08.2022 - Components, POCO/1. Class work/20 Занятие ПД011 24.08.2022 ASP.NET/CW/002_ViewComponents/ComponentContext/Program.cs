// для решения проблемы ввода дробных чисел в поля типа number
using System.Globalization;
using ComponentContext.Model;


CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

// построение приложения
var builder = WebApplication.CreateBuilder(args);

// добавление функционала MVC
builder.Services.AddControllersWithViews();

// регистрация сервиса для DI (dependency injection)
builder.Services.AddSingleton<ProductRepository>();

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

// controller=home - опциональный параметр маршрута с значением по умолчанию 
// если сегмент для параметра controller отсутствует, будет использоваться 
// значение home
// {controller} и {action} - зарезервированные названия параметров маршрута
// значения по умолчанию - регистронезависимы,т.к. это часть маршрута, но 
// не имена программных объектов
// {key?} - опциональный параметр. Если параметр есть -
//          роутер получит его значение, если нет - проигнорирует.

app.Run();