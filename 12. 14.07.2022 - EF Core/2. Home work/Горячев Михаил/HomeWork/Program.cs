using System.Globalization;
using HomeWork.Models;
using Microsoft.EntityFrameworkCore;

// установка локализации
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

// строитель
var builder = WebApplication.CreateBuilder(args);

// установка режима Razor Pages
// AddRazorRuntimeCompilation - для автоматического обновления приложения после сохранения файлов
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// строка подключения к базе данных
var connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<TouristAgencyContext>(options => options.UseSqlServer(connection));

// создание приложения
var app = builder.Build();

// использование статических файлов
app.UseStaticFiles();

// разрешение маршрутизации Razor
app.MapRazorPages();

// запуск приложения
app.Run();
