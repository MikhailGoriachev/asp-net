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
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=home}/{action=index}"
    );

    // {*data} - catch all ��������, ������� �������� � ��� �������, ����� ����������
    //           �� �������� ���������� ���������.
    //           �������� catch all ���������, ����� ��������������� ���������� ������
    //           URL ������, ������� �� ������� ��� ������ ���������
    // /home/values/10/20 � ��������� {*data} ����� ���������� �������� "10/20"
    endpoints.MapControllerRoute(
        name: "CatchAll",
        pattern: "{controller=home}/{action=values}/{*data}"
    );
});


app.Run();