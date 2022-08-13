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
    name: "Default",  // название маршрута              
    pattern: "{controller=home}/{action=index}");

// controller=Home - опциональный параметр маршрута с значением по умолчанию 
// если сегмент для параметра controller отсутствует, будет использоваться значение Home
//  {controller} и {action} - зарезервированные названия параметров маршрута
// значения по умолчанию - регистронезависимы,т.к. это часть маршрута, но не имена
// программных объектов

app.Run();