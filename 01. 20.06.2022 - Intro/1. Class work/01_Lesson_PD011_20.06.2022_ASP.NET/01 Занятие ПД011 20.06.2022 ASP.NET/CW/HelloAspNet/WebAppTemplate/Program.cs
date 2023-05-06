var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

// для доступа к HTML, CSS в папке wwwroot
app.UseStaticFiles();

// разрешение работы с маршутами /Index /Privacy 
app.UseRouting();

// для авторизации
app.UseAuthorization();

// разрешение маршутизации для Razor Pages
app.MapRazorPages();

app.Run();
