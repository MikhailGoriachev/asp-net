var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// HTML + CSS
app.UseStaticFiles();

// ���������� ������ �������������
app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=home}/{action=index}/{id:int?}"
    );

    // ����������� ��������� �� ������ ������������� ���������� ������� �������� ��������
    // ���������� � ������� ���� �������� �� ������������� �����������, ����������
    // ���������� 404 ������ ���.
    // {id:int?} - :int ����������� �� ������������� ������ ������������� ��������
    //             �������� id �������� �� ������������, �� ���� �������� ���� ���
    //             ������ ���� �������������.
    // ��������� ����������� - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-2.1#route-constraint-reference

    endpoints.MapControllerRoute(
        name: "Documents",
        pattern: "documents/{controller=invoices}/{docNumber:regex([a-z]{{2}}\\d{{5}})}/{action=view}"
    );

    // ������������� ����������� ��������� ��� �����������
    // {number:regex([a-z]{{2}}\\d{{5}})}
    // ��� ��������� number
    // ����������� ��������� :regex([a-z]{{2}}\\d{{5}}) ������ { ���������� ������������ �������������� �������� {
    // � ��������� ������ { ����� ������������� ��� ��� ���������.

    // documents/invoices/ad12345/view
    // documents/invoices/ad12345/edit

});

app.Run();
