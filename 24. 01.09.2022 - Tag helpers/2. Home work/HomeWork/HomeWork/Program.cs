using System.Globalization;
using HomeWork.Filters;
using HomeWork.Models;
// using HomeWork.Models;
using HomeWork.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// установка локализации
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

// добавление MVC
builder.Services.AddControllersWithViews();

// добавление в сервисы логера
builder.Services.AddSingleton<Logger>();

// добавление сервиса обработки исключений
builder.Services.AddSingleton<ExceptionLoggingAttribute>();

// добавление сервиса для работы с базой данных
builder.Services.AddDbContext<SalesAccountingContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));


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

        // маршрут для пагинации
        // app.MapControllerRoute(
        //     name: "pageNumber",
        //     pattern: "{controller}/{action}/{pageNumber}"
        // );

        // маршрут для сортировки
        app.MapControllerRoute(
            name: "model",
            pattern: "{controller}/{action}/{model?}"
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
