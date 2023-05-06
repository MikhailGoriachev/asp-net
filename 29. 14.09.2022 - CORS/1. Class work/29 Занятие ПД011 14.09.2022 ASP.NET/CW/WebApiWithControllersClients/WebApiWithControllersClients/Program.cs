var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// добавление функционала контроллеров API - модно не указывать ,
// если добавляем функционал контроллеров и представлений MVC
// builder.Services.AddControllers();

// добавление функционала контроллеров и представлений
builder.Services.AddControllersWithViews();

// добавление сервиса CORS (Cross Origin Resource Sharing)
// для разрешения запросов к серверу от других доменов
// т.е. от клиентских приложений, созданных в других проектах
builder.Services.AddCors();

// точно также как и ранее можно добавить любой другой сервис,
// например, сервис доступа к данным 

var app = builder.Build();

// Configure the HTTP request pipeline.

// для работы сервера с HTML+CSS+JS,...
app.UseStaticFiles();

// настройка CORS - разрешаем обрабатывать все источники запросов
// и все виды REST-запросов
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();