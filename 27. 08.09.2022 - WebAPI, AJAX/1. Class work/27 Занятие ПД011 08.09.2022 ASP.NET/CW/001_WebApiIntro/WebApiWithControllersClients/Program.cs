var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ���������� ����������� ������������ API - ����� �� ��������� ,
// ���� ��������� ���������� ������������ � �������������� MVC
// builder.Services.AddControllers();

// ���������� ����������� ������������ � �������������
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.

// ��� ������ � HTML+CSS+JS,...
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
