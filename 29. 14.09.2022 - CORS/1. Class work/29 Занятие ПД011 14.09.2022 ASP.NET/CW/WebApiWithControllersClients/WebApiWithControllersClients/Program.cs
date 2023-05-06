var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ���������� ����������� ������������ API - ����� �� ��������� ,
// ���� ��������� ���������� ������������ � ������������� MVC
// builder.Services.AddControllers();

// ���������� ����������� ������������ � �������������
builder.Services.AddControllersWithViews();

// ���������� ������� CORS (Cross Origin Resource Sharing)
// ��� ���������� �������� � ������� �� ������ �������
// �.�. �� ���������� ����������, ��������� � ������ ��������
builder.Services.AddCors();

// ����� ����� ��� � ����� ����� �������� ����� ������ ������,
// ��������, ������ ������� � ������ 

var app = builder.Build();

// Configure the HTTP request pipeline.

// ��� ������ ������� � HTML+CSS+JS,...
app.UseStaticFiles();

// ��������� CORS - ��������� ������������ ��� ��������� ��������
// � ��� ���� REST-��������
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();