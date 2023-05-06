using System.Globalization;
using HomeWork.Models;
using Microsoft.EntityFrameworkCore;

// установка локализации
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

// строитель
var builder = WebApplication.CreateBuilder(args);

// установка режима Razor Pages
builder.Services.AddRazorPages();

// строка подключения к базе данных
string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<PeriodicalsContext>(options => options.UseSqlServer(connection!));;

// создание приложения
var app = builder.Build();

// использование статических файлов
app.UseStaticFiles();

// разрешение маршрутизации Razor
app.MapRazorPages();

// запуск приложения
app.Run();
