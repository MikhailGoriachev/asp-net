using Microsoft.AspNetCore.Routing.Constraints;

var builder = WebApplication.CreateBuilder(args);

// добавление функционала MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// HTML + CSS
app.UseStaticFiles();

// разрешение работы маршрутизации
app.UseRouting();

// Использование анонимного объекта для определения значений по умолчанию и ограничений.
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Default",
      pattern: "{controller}/{action}/{id?}",

      // значения по умолчанию для параметров маршрута controller и action
      defaults: new {
          controller = "home",
          action = "index"
      },

      // ограничение для параметра маршрута id
      constraints: new {
          id = new IntRouteConstraint()
      });


    endpoints.MapControllerRoute(
        name: "Documents",
        pattern: "documents/{controller}/{number}/{action}",
        defaults: new {
            controller = "invoices",
            action = "view"
        },
        constraints: new {
            number = new RegexRouteConstraint("[a-z]{2}\\d{5}")
        });

    // В большинстве случаев, для построения маршрута и установки значений по умолчанию
    // с ограничениями, используется синтаксис, показанный в предыдущих примерах.
    // Но для определения значений по умолчанию для параметров, которые не представлены
    // сегментами используется перегрузка метода MapControllerRoute требующая использования
    // анонимных типов данных.
    endpoints.MapControllerRoute(
        name: "Users",

        // из шаблона не понятно, какой контроллер и какое действие
        // будут использованы (литерал и переменная - не {controller} и {action})
        pattern: "users/{name}",

        // задаем контроллер, действие по умолчанию и даже дополнительный параметр id
        defaults: new {
            controller = "users",
            action = "index",
            id = 42
        });
});

app.Run();
