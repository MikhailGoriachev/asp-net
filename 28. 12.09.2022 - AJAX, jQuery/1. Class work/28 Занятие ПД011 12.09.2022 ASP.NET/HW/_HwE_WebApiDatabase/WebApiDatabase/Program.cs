using Microsoft.EntityFrameworkCore;
using WebApiDatabase.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ����������� ������� WebAPI ������������
// builder.Services.AddControllers();

// ���������� ����������� ������������ � �������������
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


app.UseAuthorization();
app.MapControllers();

app.Run();
