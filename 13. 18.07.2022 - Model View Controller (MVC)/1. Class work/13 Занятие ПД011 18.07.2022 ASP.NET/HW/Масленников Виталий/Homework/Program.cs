using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Homework.Models;
using Microsoft.AspNetCore.Mvc;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddRazorPages(options => options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()))
    .AddRazorRuntimeCompilation();

var t = Directory.GetCurrentDirectory();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TravelAgencyContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();

// Проверка на существование БД 
using (var scope = app.Services.CreateScope())
    using (var context = scope.ServiceProvider.GetService<TravelAgencyContext>())
        context?.Database.EnsureCreated();

app.UseStaticFiles();
app.MapRazorPages();
app.Run();