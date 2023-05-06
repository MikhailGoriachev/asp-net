using Microsoft.EntityFrameworkCore;
using WebApiDatabase.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Регистрация сервиса WebAPI контроллеров
// builder.Services.AddControllers();

// добавление функционала контроллеров и представлений
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


app.UseAuthorization();
app.MapControllers();

app.Run();
