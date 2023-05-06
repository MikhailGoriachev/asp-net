using System.Globalization;
using HomeWork.Filters;
using HomeWork.Models;
using HomeWork.Services;

var builder = WebApplication.CreateBuilder(args);

// установка локализации
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

// добавление MVC
builder.Services.AddControllersWithViews();

// добавление сервиса получения данных пользователей
builder.Services.AddSingleton<UsersService>();

// добавление в сервисы логгера
builder.Services.AddSingleton<Logger>();

// добавление сервиса обработки исключений
builder.Services.AddSingleton<ExceptionLoggingAttribute>();

// регистрация глобальных фильтров
builder.Services.AddMvc().AddMvcOptions(options => options.Filters.AddService<ExceptionLoggingAttribute>());

var app = builder.Build();

// разрешение для использования статических ресурсов
app.UseStaticFiles();

// разрешение для использования маршрутизации
app.UseRouting();

// установка маршрутов
app.UseEndpoints(endpoints =>
    {
        // маршрут для изменения/удаления
        app.MapControllerRoute(
            name: "updateAndDelete",
            pattern: "{controller}/{action}/{id:int}"
        );

        // маршрут для сортировки
        app.MapControllerRoute(
            name: "nameHandler",
            pattern: "{controller}/{action}/{nameHandler}"
        );

        // маршрут по умолчанию
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );
    }
);

// запуск приложения
app.Run();
