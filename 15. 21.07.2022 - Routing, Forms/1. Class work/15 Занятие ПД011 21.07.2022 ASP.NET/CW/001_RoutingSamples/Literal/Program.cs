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
    name: "Default", // название маршрута              
    /* 
     * в маршруте присутствует литерал api все запросы должны содержать такой сегмент в URL.
     * в данном примере будет работать запрос по адресу /api/values/list                         
     */
    pattern: "api/{controller}/{action}");
//  {controller} и {action} - зарезервированные названия параметров маршрута

app.Run();
