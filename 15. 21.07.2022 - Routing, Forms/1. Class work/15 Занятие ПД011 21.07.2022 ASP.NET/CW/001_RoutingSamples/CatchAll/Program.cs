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
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=home}/{action=index}"
    );

    // {*data} - catch all параметр, которых подходит в тех случаях, когда изначально
    //           не известно количество сегментов.
    //           значение catch all параметра, будет соответствовать оставшейся строке
    //           URL адреса, которая не подошла под другие параметры
    // /home/values/10/20 в параметре {*data} будет находиться значение "10/20"
    endpoints.MapControllerRoute(
        name: "CatchAll",
        pattern: "{controller=home}/{action=values}/{*data}"
    );
});


app.Run();