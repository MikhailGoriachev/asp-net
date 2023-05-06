using HomeWork.Models.Task1;
using HomeWork.Models.Task2;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// ��������� �����������
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

// ���������� MVC
builder.Services.AddControllersWithViews();

// ���������� ������� ��������� ������ ����������
builder.Services.AddSingleton<PeopleService>();
builder.Services.AddSingleton<PlanetService>();

// ���������� � ������� �������
//builder.Services.AddSingleton<Logger>();

// ���������� ������� ��������� ����������
//builder.Services.AddSingleton<ExceptionLoggingAttribute>();

// ����������� ���������� ��������
//builder.Services.AddMvc().AddMvcOptions(options => options.Filters.AddService<ExceptionLoggingAttribute>());

var app = builder.Build();

// ���������� ��� ������������� ����������� ��������
app.UseStaticFiles();

// ���������� ��� ������������� �������������
app.UseRouting();

// ������������ ������� ��������
app.MapControllerRoute(
    name: "Default",  // �������� ��������              
    pattern: "{controller=Home}/{action=index}/{id?}");

// ������ ����������
app.Run();
