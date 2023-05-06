using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

/*
app.MapControllerRoute(
    name: "Task2", pattern: "{area:exists}/{controller=home}/{action=index}");
    
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
*/

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

app.Run();