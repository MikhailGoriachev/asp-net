using System.Globalization;
using Homework.Infrastructure;
using Homework.Models.TravelCompany;
using Homework.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<TravelCompany>();

builder.Services.AddSingleton<Logger>();
builder.Services.AddSingleton<ProfileAttribute>();
builder.Services.AddSingleton<ExceptionLoggingAttribute>();
builder.Services.AddMvc().AddMvcOptions(options => options.Filters.AddService<ExceptionLoggingAttribute>());

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

app.Run();