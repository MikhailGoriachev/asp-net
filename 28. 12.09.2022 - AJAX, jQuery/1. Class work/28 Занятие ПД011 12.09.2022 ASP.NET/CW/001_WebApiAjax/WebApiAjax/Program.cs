using WebApiAjax.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ���������� WebAPI
builder.Services.AddControllers();

// �������� ������-�����������
builder.Services.AddSingleton<PeopleRepository>();

var app = builder.Build();

// ������������� �������� HTML+CSS+JS
app.UseStaticFiles();

// ���������� �������������
app.MapControllers();

app.Run();
