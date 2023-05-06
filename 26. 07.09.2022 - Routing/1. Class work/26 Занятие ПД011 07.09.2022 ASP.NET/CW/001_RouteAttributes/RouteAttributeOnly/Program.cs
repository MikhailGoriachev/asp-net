var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

// при использовании только атрибутов для маршрутизации
// такого вызова достаточно
app.MapControllers();

// подключение wwwroot, css+html
app.UseStaticFiles();

app.Run();