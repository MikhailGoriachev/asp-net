var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// добавление функционала контроллеров API - модно не указывать ,
// если добавляем функционал контроллеров и прелдставлений MVC
// builder.Services.AddControllers();

// добавление функционала контроллеров и представлений
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.

// для работы с HTML+CSS+JS,...
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
