using Microsoft.EntityFrameworkCore;
using HomeWork.Models;

// ��� ������� �������� ����� ������� ����� � ���� ���� number
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");

// ���������� ����������
var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� MVC
builder.Services.AddControllersWithViews();

// ���������� ����������� EntityFramework Core ��� ������� ����������
string connection = "Server = (localdb)\\mssqllocaldb;Database = UserStore_290822_Db;Trusted_Connection=true";
builder.Services.AddDbContext<WholesaleStoreContext>(options => options.UseSqlServer(connection));


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
