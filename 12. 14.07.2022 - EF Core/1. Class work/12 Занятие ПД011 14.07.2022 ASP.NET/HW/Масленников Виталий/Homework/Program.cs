using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Homework.Models;
using Microsoft.AspNetCore.Mvc;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages(options => options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()));

var t = Directory.GetCurrentDirectory();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PeriodicalsContext>(options =>
    options.UseLazyLoadingProxies().UseSqlite(connection));

var app = builder.Build();

// Вынос проверки на существование БД из конструктора контекста 
using (var scope = app.Services.CreateScope())
    using (var context = scope.ServiceProvider.GetService<PeriodicalsContext>())
        context?.Database.EnsureCreated();

app.UseStaticFiles();
app.MapRazorPages();
app.Run();