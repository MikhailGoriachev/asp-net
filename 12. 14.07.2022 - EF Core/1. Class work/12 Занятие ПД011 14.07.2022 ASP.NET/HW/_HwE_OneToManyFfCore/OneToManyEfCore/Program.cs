using System.Globalization;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

// ��� ������� �������� ����� ������� ����� � ���� ���� number
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();

// ����������� EF ��� ������� - ���������� ������
// ������ ����������� ���������� � appsettings.json
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

// ��� ������� � HTML, CSS � ����� wwwroot
app.UseStaticFiles();

// ���������� ������������ ��� Razor Pages
app.MapRazorPages();

app.Run();