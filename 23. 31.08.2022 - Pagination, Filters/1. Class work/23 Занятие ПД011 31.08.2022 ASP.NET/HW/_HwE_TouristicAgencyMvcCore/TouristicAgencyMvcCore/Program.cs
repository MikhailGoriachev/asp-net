using Microsoft.EntityFrameworkCore;
using TouristicAgencyMvcCore.Models;

// ��� ������� �������� ����� ������� ����� � ���� ���� number
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

// ���������� ����������
var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� MVC
builder.Services.AddControllersWithViews();

// ���������� ����������� EntityFramework Core ��� ������� ����������
// ������ ����������� ����� ������ ���� � ����
// string connection = "Server=(localdb)\\mssqllocaldb;DataBase=touristic_310822_lvdb;Trusted_Connection=True";

// ������ ����������� ����� ������ � appsettings.json
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));


var app = builder.Build();

// Configure the HTTP request pipeline.
// HTML + CSS
app.UseStaticFiles();

// ���������� ������ �������������
app.UseRouting();

// ������������ ������� ��������
app.MapControllerRoute(
    name: "Default",  // �������� ��������              
    pattern: "{controller=Home}/{action=index}/{id?}");

app.Run();