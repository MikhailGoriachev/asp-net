using HomeWork.Models.Task1;
using HomeWork.Models.Task2;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// установка локализации
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

// добавление MVC
builder.Services.AddControllersWithViews();

// добавление сервиса получения данных персонажей
builder.Services.AddSingleton<PeopleService>();
builder.Services.AddSingleton<PlanetService>();

// добавление в сервисы логгера
//builder.Services.AddSingleton<Logger>();

// добавление сервиса обработки исключений
//builder.Services.AddSingleton<ExceptionLoggingAttribute>();

// регистрация глобальных фильтров
//builder.Services.AddMvc().AddMvcOptions(options => options.Filters.AddService<ExceptionLoggingAttribute>());

var app = builder.Build();

// разрешение для использования статических ресурсов
app.UseStaticFiles();

// разрешение для использования маршрутизации
app.UseRouting();

// обязательное задание маршрута
app.MapControllerRoute(
    name: "Default",  // название маршрута              
    pattern: "{controller=Home}/{action=index}/{id?}");

// запуск приложения
app.Run();
