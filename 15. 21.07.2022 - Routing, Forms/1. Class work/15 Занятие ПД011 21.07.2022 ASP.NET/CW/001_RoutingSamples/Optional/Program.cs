var builder = WebApplication.CreateBuilder(args);

// добавление функционала MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// HTML + CSS
app.UseStaticFiles();

// разрешение работы маршрутизации
app.UseRouting();

// обязательное задание маршрута
app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=products}/{action=details}/{key?}");

// {key?} - опциональный параметр. Если параметр есть -
//          роутер получит его значение, если нет - проигнорирует.

app.Run();