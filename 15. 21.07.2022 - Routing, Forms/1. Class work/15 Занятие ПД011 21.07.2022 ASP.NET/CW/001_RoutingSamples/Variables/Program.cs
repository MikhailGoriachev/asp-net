var builder = WebApplication.CreateBuilder(args);

// добавление функционала MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// HTML + CSS
app.UseStaticFiles();

// разрешение работы маршрутизации
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    // Запрос, полученный приложением, будет сопоставляется с каждым маршрутом, определенным
    // при настройке UseEndpoints middleware
    // (сравнение происходит в порядке добавления маршрутов)
    // Как только будет найден подходящий маршрут, начнется обработка запроса контроллером.
    // Более специфические маршруты должны находиться в начале списка маршрутов

    // В этом примере данный маршрут позволяет обработать запрос home/calc/10/20
    // при этом два последних сегмента будут доступны в методе действия calc
    // как параметры с именами x и y
    endpoints.MapControllerRoute(
        name: "TwoParameterRoute",
        pattern: "{controller}/{action}/{x}/{y}"
    );

    // запрос с литеральным префиксом для, например, API и переменной маршрута
    endpoints.MapControllerRoute(
        name: "ApiRoute", // название маршрута              
        /* 
         * в маршруте присутствует литерал api все запросы должны содержать такой сегмент в URL.
         * в данном примере будет работать запрос по адресу /api/home/values/42                         
         */
        pattern: "api/{controller}/{action}/{value}");

    // Маршрут по умолчанию со значениями по умолчанию - должен быть в конце
    // для предотвращения конфликта - перехвата всех маршрутов за счет значений
    // по умолчанию
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=home}/{action=index}"
    );
});

app.Run();