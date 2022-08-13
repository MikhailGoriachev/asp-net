using System.Globalization;

// установка локализации
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

// строитель
var builder = WebApplication.CreateBuilder(args);

// установка режима Razor Pages
builder.Services.AddRazorPages();

// создание приложения
var app = builder.Build();

// использование статических файлов
app.UseStaticFiles();

// разрешение маршрутизации Razor
app.MapRazorPages();

// запуск приложения
app.Run();
