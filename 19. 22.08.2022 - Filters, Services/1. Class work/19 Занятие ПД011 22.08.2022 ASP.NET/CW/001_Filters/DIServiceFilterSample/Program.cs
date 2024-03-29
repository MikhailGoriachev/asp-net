// ��� ������� �������� ����� ������� ����� � ���� ���� number
using System.Globalization;
using DIServiceFilterSample.Infrastructure;
using DITypeFilterSample.Services;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

// ���������� ����������
var builder = WebApplication.CreateBuilder(args);

// ������ ��� ������ 
builder.Services.AddSingleton<Logger>();

// ������ ��� ������ 
builder.Services.AddSingleton<ExceptionLogingAttribute>();

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
    pattern: "{controller=Home}/{action=index}/{key?}");

// controller=home - ������������ �������� �������� � ��������� �� ��������� 
// ���� ������� ��� ��������� controller �����������, ����� �������������� 
// �������� home
// {controller} � {action} - ����������������� �������� ���������� ��������
// �������� �� ��������� - ������������������,�.�. ��� ����� ��������, �� 
// �� ����� ����������� ��������
// {key?} - ������������ ��������. ���� �������� ���� -
//          ������ ������� ��� ��������, ���� ��� - �������������.

app.Run();