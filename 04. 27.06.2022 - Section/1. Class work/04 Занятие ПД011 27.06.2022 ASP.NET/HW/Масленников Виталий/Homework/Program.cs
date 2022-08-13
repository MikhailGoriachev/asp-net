using System.Globalization;
using Homework.Controllers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages(options => options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()));

var app = builder.Build();
app.UseStaticFiles();
app.MapRazorPages();

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

app.Run();
