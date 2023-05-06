var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// HTML + CSS
app.UseStaticFiles();

// ���������� ������ �������������
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    // ������, ���������� �����������, ����� �������������� � ������ ���������, ������������
    // ��� ��������� UseEndpoints middleware
    // (��������� ���������� � ������� ���������� ���������)
    // ��� ������ ����� ������ ���������� �������, �������� ��������� ������� ������������.
    // ����� ������������� �������� ������ ���������� � ������ ������ ���������

    // � ���� ������� ������ ������� ��������� ���������� ������ home/calc/10/20
    // ��� ���� ��� ��������� �������� ����� �������� � ������ �������� calc
    // ��� ��������� � ������� x � y
    endpoints.MapControllerRoute(
        name: "TwoParameterRoute",
        pattern: "{controller}/{action}/{x}/{y}"
    );

    // ������ � ����������� ��������� ���, ��������, API � ���������� ��������
    endpoints.MapControllerRoute(
        name: "ApiRoute", // �������� ��������              
        /* 
         * � �������� ������������ ������� api ��� ������� ������ ��������� ����� ������� � URL.
         * � ������ ������� ����� �������� ������ �� ������ /api/home/values/42                         
         */
        pattern: "api/{controller}/{action}/{value}");

    // ������� �� ��������� �� ���������� �� ��������� - ������ ���� � �����
    // ��� �������������� ��������� - ��������� ���� ��������� �� ���� ��������
    // �� ���������
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=home}/{action=index}"
    );
});

app.Run();