using Microsoft.EntityFrameworkCore;
using TouristicAgencyMvcCore.Models;

// для решения проблемы ввода дробных чисел в поля типа number
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

// построение приложения
var builder = WebApplication.CreateBuilder(args);

// добавление функционала MVC
builder.Services.AddControllersWithViews();

// Добавление функционала EntityFramework Core как сервиса приложения
// строку подключения можно задать явно в коде
// string connection = "Server=(localdb)\\mssqllocaldb;DataBase=touristic_310822_lvdb;Trusted_Connection=True";

// строку подключения модно задать в appsettings.json
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));


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