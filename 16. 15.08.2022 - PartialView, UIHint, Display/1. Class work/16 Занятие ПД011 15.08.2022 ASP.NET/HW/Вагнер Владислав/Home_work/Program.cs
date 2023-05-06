var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseStaticFiles();

// ���������� ������ �������������
app.UseRouting();

//��������
app.UseEndpoints(endpoints => {

    //����������� �������
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    //������� ��� �������� �� ����� ��������������/�������� �����
    endpoints.MapControllerRoute(
        name: "route-form",
        pattern: "{controller=Home}/{action=Index}/{addingmode}");
});

app.Run();