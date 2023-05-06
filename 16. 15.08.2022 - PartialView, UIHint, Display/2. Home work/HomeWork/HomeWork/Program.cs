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
        // // ������� ��� ���������/��������
        // app.MapControllerRoute(
        //     name: "updateAndDelete",
        //     pattern: "{controller}/{action}/{id:int}"
        // );
        //
        // // ������� ��� ����������
        // app.MapControllerRoute(
        //     name: "nameHandler",
        //     pattern: "{controller}/{action}/{nameHandler}"
        // );

        // ������� �� ���������
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );
    }
);

// ������ ����������
app.Run();
