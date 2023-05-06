using Microsoft.EntityFrameworkCore;

// ��� ������� �������� ����� ������� ����� � ���� ���� number
using System.Globalization;
using UsingTagHelpers2.Models;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");

// ���������� ����������
var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� MVC
builder.Services.AddControllersWithViews();

// ���������� ����������� EntityFramework Core ��� ������� ����������
// string connection = "Server = (localdb)\\mssqllocaldb;Database = store_050922_lvdb;Trusted_Connection=true";
// ������ ����������� ����� ������ � appsettings.json
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
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
