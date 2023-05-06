var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ��� ������������� ������ ��������� ��� �������������
// ������ ������ ����������
app.MapControllers();

// ����������� wwwroot, css+html
app.UseStaticFiles();

app.Run();