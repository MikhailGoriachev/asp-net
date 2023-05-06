using System.Globalization;
using Home_work.Services;
using Home_work.Infrastructure;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� MVC
builder.Services.AddControllersWithViews();


//���������� ������� Logger ��� ����� ���������
builder.Services.AddSingleton<Logger>();

//������ � �������� �������
builder.Services.AddSingleton<ExceptionAttribute>();

//���������� ����������� �������
builder.Services.AddMvc().AddMvcOptions(
        options => options.Filters.AddService<ExceptionAttribute>());

var app = builder.Build();
// Configure the HTTP request pipeline
app.UseStaticFiles();

// ���������� ������ �������������
app.UseRouting();

//��������
app.UseEndpoints(endpoints => {

    //������� 
    endpoints.MapControllerRoute(
        name: "route-form",
        pattern: "{controller=Home}/{action=Index}/{value?}");
});

app.Run();