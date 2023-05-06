using System.Globalization;
using Homework.Models;
using Homework.Models.Books;
using Homework.Models.Figures;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IFiguresRepository, FiguresRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "OrderBooks",
        pattern: "{controller}/{action}/{property}/",
        constraints: new
        {
            controller = "Library",
            action = "OrderBy"
        }
    );
    endpoints.MapControllerRoute(
        name: "FilterBooks",
        pattern: "{controller}/{action}/{where}/",
        constraints: new
        {
            controller = "Library",
            action = "Select"
        }
    );
    
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

app.Run();