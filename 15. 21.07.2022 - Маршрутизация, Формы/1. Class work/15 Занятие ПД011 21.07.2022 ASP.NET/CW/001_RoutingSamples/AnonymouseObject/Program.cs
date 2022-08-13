using Microsoft.AspNetCore.Routing.Constraints;

var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// HTML + CSS
app.UseStaticFiles();

// ���������� ������ �������������
app.UseRouting();

// ������������� ���������� ������� ��� ����������� �������� �� ��������� � �����������.
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Default",
      pattern: "{controller}/{action}/{id?}",

      // �������� �� ��������� ��� ���������� �������� controller � action
      defaults: new {
          controller = "home",
          action = "index"
      },

      // ����������� ��� ��������� �������� id
      constraints: new {
          id = new IntRouteConstraint()
      });


    endpoints.MapControllerRoute(
        name: "Documents",
        pattern: "documents/{controller}/{number}/{action}",
        defaults: new {
            controller = "invoices",
            action = "view"
        },
        constraints: new {
            number = new RegexRouteConstraint("[a-z]{2}\\d{5}")
        });

    // � ����������� �������, ��� ���������� �������� � ��������� �������� �� ���������
    // � �������������, ������������ ���������, ���������� � ���������� ��������.
    // �� ��� ����������� �������� �� ��������� ��� ����������, ������� �� ������������
    // ���������� ������������ ���������� ������ MapControllerRoute ��������� �������������
    // ��������� ����� ������.
    endpoints.MapControllerRoute(
        name: "Users",

        // �� ������� �� �������, ����� ���������� � ����� ��������
        // ����� ������������ (������� � ���������� - �� {controller} � {action})
        pattern: "users/{name}",

        // ������ ����������, �������� �� ��������� � ���� �������������� �������� id
        defaults: new {
            controller = "users",
            action = "index",
            id = 42
        });
});

app.Run();
