// ��� ������� �������� ����� ������� ����� � ���� ���� number
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

// ���������� ����������
var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� MVC
builder.Services.AddControllersWithViews();

// ------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
// HTML + CSS
app.UseStaticFiles();

// ���������� ������ �������������
app.UseRouting();

// ����������� exists ������������ ��� ����, ����� ������� �������������
// ������ � ���� ���������, ������� ���������� � ����������
app.MapControllerRoute(
    name: "Task2", pattern: "{area:exists}/{controller=Home}/{action=index}");

// ������������ ������� ��������
app.MapControllerRoute(
    name: "Default",  // �������� ��������              
    pattern: "{controller=Home}/{action=index}/{id?}");

app.Run();