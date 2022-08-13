using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Homework.Models;
using Microsoft.AspNetCore.Mvc;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages(options => options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()));

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PeriodicalsContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();
app.Run();
