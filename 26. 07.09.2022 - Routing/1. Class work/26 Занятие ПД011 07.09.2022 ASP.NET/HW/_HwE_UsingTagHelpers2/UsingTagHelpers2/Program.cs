using Microsoft.EntityFrameworkCore;

// для решения проблемы ввода дробных чисел в поля типа number
using System.Globalization;
using UsingTagHelpers2.Models;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");

// построение приложения
var builder = WebApplication.CreateBuilder(args);

// добавление функционала MVC
builder.Services.AddControllersWithViews();

// Добавление функционала EntityFramework Core как сервиса приложения
// string connection = "Server = (localdb)\\mssqllocaldb;Database = store_050922_lvdb;Trusted_Connection=true";
// строку подключения можно задать в appsettings.json
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WholesaleStoreContext>(options => options.UseSqlServer(connection));


var app = builder.Build();

// Configure the HTTP request pipeline.
// HTML + CSS
app.UseStaticFiles();

// разрешение работы маршрутизации
app.UseRouting();

// обязательное задание маршрута
app.MapControllerRoute(
    name: "Default",  // название маршрута              
    pattern: "{controller=Home}/{action=index}/{id?}");

app.Run();
