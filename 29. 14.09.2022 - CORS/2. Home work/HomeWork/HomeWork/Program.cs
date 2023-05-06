using System.Text.Json.Serialization;
using HomeWork.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    // выключение обработки зацикленных связей
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// добавление сервиса для работы с базой данных
builder.Services.AddDbContext<TouristAgencyContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// добавление CORS
builder.Services.AddCors();

var app = builder.Build();

// разрешение использования CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod());

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
