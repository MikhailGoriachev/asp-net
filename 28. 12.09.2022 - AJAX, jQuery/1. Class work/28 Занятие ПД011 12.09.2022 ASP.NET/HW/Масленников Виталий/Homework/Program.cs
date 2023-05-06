using System.Globalization;
using Homework.Models;
using Microsoft.EntityFrameworkCore;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<WholeSaleContext>(options =>
    options.UseSqlite(connection));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
    using (var context = scope.ServiceProvider.GetService<WholeSaleContext>())
        context?.Database.EnsureCreated();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();