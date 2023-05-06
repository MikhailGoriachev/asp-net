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
    name: "Default", // �������� ��������              
    /* 
     * � �������� ������������ ������� api ��� ������� ������ ��������� ����� ������� � URL.
     * � ������ ������� ����� �������� ������ �� ������ /api/values/list                         
     */
    pattern: "api/{controller}/{action}");
//  {controller} � {action} - ����������������� �������� ���������� ��������

app.Run();
