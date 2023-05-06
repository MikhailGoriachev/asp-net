using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// установка локализации
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

// добавление MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// разрешение для использования статических ресурсов
app.UseStaticFiles();

// разрешение для использования маршрутизации
app.UseRouting();

// установка маршрутов
app.UseEndpoints(endpoints =>
    {
        // маршрут по умолчанию
        app.MapControllerRoute(
            name: "updateAndDelete",
            pattern: "{controller}/{action}/{id:int}"
        );

        // маршрут для выборки фигур
        app.MapControllerRoute(
            name: "figureSelection",
            pattern: "{controller}/{action}/{selectionName}",
            constraints: new
            {
                controller = "Figures",
                action = "SelectionBy",
            }
        );

        // маршрут для сортировки фигур
        endpoints.MapControllerRoute(
            name: "figureOrder",
            pattern: "{controller}/{action}/{orderName}",
            constraints: new
            {
                controller = "Figures",
                action = "OrderBy",
            }
        );

        // маршрут для выборки книг
        app.MapControllerRoute(
            name: "bookSelection",
            pattern: "{controller}/{action}/{selectionName}",
            constraints: new
            {
                controller = "Library",
                action = "SelectionBy",
            }
        );

        // маршрут для сортировки книг
        endpoints.MapControllerRoute(
            name: "bookOrder",
            pattern: "{controller}/{action}/{orderName}",
            constraints: new
            {
                controller = "Library",
                action = "OrderBy",
            }
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
