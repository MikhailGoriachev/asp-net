using System.Globalization;
using Home_work.Models;
using Home_work.Infrastructure;
using Home_work.Services;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

var builder = WebApplication.CreateBuilder(args);

// добавление функционала MVC
builder.Services.AddControllersWithViews();


//Добавление сервиса - общая модель для разных запросов
builder.Services.AddSingleton<GeneralModelService>();


builder.Services.AddSingleton<Logger>();

//Фильтр в качестве сервиса
builder.Services.AddSingleton<ExceptionAttribute>();

//Добавление глобального фильтра
builder.Services.AddMvc().AddMvcOptions(
        options => options.Filters.AddService<ExceptionAttribute>());

var app = builder.Build();
// Configure the HTTP request pipeline
app.UseStaticFiles();

// разрешение работы маршрутизации
app.UseRouting();

//Маршруты
app.UseEndpoints(endpoints => {

    //Маршрут 
    endpoints.MapControllerRoute(
        name: "route-form",
        pattern: "{controller=Home}/{action=Index}/{value?}");
});

app.Run();