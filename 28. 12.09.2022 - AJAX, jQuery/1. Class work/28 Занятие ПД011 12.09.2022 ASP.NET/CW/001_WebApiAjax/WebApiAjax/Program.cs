using WebApiAjax.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Реализация WebAPI
builder.Services.AddControllers();

// получить сервис-репозиторий
builder.Services.AddSingleton<PeopleRepository>();

var app = builder.Build();

// использование сервером HTML+CSS+JS
app.UseStaticFiles();

// разрешение маршрутизации
app.MapControllers();

app.Run();
