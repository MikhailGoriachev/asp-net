// для решения проблемы ввода дробных чисел в поля типа number
using System.Globalization;
using GlobalFilters.Infrastructure;
using GlobalFilters.Services;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

// построение приложения
var builder = WebApplication.CreateBuilder(args);

// логгер как сервис 
builder.Services.AddSingleton<Logger>();

// фильтр как сервис 
builder.Services.AddSingleton<ExceptionLogingAttribute>();

// добавление функционала MVC
// Регистрация глобальных фильтров. Эти фильтры будут срабатывать для каждого
// контроллера и метода действия в приложении.
builder.Services.AddMvc().AddMvcOptions(
    options => {
        // Add - без внедрения зависимострей
        options.Filters.Add<ProfileAttribute>();
        // фильтр исключений всегда создается как глобальный фильтр
        // AddService - с внедрением зависимостей
        options.Filters.AddService<ExceptionLogingAttribute>(); 
    });

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