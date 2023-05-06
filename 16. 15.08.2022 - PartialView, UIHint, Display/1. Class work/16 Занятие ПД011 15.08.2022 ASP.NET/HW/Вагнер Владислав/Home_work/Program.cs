var builder = WebApplication.CreateBuilder(args);

// добавление функционала MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseStaticFiles();

// разрешение работы маршрутизации
app.UseRouting();

//Маршруты
app.UseEndpoints(endpoints => {

    //Стандартный маршрут
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    //Маршрут для перехода из формы редактирования/создания книги
    endpoints.MapControllerRoute(
        name: "route-form",
        pattern: "{controller=Home}/{action=Index}/{addingmode}");
});

app.Run();