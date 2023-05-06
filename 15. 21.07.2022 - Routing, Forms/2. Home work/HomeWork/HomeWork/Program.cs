using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// ��������� �����������
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

// ���������� MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ���������� ��� ������������� ����������� ��������
app.UseStaticFiles();

// ���������� ��� ������������� �������������
app.UseRouting();

// ��������� ���������
app.UseEndpoints(endpoints =>
    {
        // ������� �� ���������
        app.MapControllerRoute(
            name: "updateAndDelete",
            pattern: "{controller}/{action}/{id:int}"
        );

        // ������� ��� ������� �����
        app.MapControllerRoute(
            name: "figureSelection",
            pattern: "{controller}/{action}/{selectionName}",
            constraints: new
            {
                controller = "Figures",
                action = "SelectionBy",
            }
        );

        // ������� ��� ���������� �����
        endpoints.MapControllerRoute(
            name: "figureOrder",
            pattern: "{controller}/{action}/{orderName}",
            constraints: new
            {
                controller = "Figures",
                action = "OrderBy",
            }
        );

        // ������� ��� ������� ����
        app.MapControllerRoute(
            name: "bookSelection",
            pattern: "{controller}/{action}/{selectionName}",
            constraints: new
            {
                controller = "Library",
                action = "SelectionBy",
            }
        );

        // ������� ��� ���������� ����
        endpoints.MapControllerRoute(
            name: "bookOrder",
            pattern: "{controller}/{action}/{orderName}",
            constraints: new
            {
                controller = "Library",
                action = "OrderBy",
            }
        );

        // ������� �� ���������
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );
    }
);

// ������ ����������
app.Run();
