using System.Globalization;
using Homework.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TravelAgencyContext>(options =>
    options.UseSqlite(connection));

var app = builder.Build();

// Вынос проверки на существование БД из конструктора контекста 
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<TravelAgencyContext>())
    context?.Database.EnsureCreated();

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