// ��� ������� �������� ����� ������� ����� � ���� ���� number
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

// ���������� ����������
var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// HTML + CSS
app.UseStaticFiles();

// ���������� ������ �������������
app.UseRouting();

// ������������ ������� ��������
app.MapControllerRoute(
    name: "Default",  // �������� ��������              
    pattern: "{controller=home}/{action=index}/{key?}");

// controller=home - ������������ �������� �������� � ��������� �� ��������� 
// ���� ������� ��� ��������� controller �����������, ����� �������������� 
// �������� home
// {controller} � {action} - ����������������� �������� ���������� ��������
// �������� �� ��������� - ������������������,�.�. ��� ����� ��������, �� 
// �� ����� ����������� ��������
// {key?} - ������������ ��������. ���� �������� ���� -
//          ������ ������� ��� ��������, ���� ��� - �������������.

app.Run();