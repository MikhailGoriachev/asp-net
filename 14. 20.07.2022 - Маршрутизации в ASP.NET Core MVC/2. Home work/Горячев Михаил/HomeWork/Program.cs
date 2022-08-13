var builder = WebApplication.CreateBuilder(args);

// добавление MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// разрешение для использования статических ресурсов
app.UseStaticFiles();

// разрешение для использования маршрутизации
app.UseRouting();

// установка маршрута по умолчанию
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
