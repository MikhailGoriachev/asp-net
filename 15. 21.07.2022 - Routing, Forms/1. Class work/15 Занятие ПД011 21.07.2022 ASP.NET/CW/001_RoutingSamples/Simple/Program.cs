// это приложение не имеет машрутов по умолчанию, требуется
// вводить маршрут /Home/Index в адресной строке браузера

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
    name: "Default",                    // название 
    pattern: "{controller}/{action}");  // route template для запросов состоящих из двух сегментов

//  {controller} и {action} - зарезервированные названия параметров маршрута

app.Run();