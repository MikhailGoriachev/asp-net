using System.Globalization;
using Home_work.Models;
using Home_work.Infrastructure;
using Home_work.Services;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

var builder = WebApplication.CreateBuilder(args);

// ���������� ����������� MVC
builder.Services.AddControllersWithViews();


//���������� ������� - ����� ������ ��� ������ ��������
builder.Services.AddSingleton<GeneralModelService>();


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