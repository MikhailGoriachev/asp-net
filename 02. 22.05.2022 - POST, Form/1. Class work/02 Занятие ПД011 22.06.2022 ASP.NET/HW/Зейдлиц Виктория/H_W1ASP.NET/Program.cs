var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

// ��� ������� � HTML, CSS � ����� wwwroot
app.UseStaticFiles();

// ���������� ������������ ��� Razor Pages
app.MapRazorPages();


app.Run();
