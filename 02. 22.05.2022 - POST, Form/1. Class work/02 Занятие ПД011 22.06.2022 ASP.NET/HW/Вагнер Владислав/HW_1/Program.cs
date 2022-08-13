using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;


namespace HW_1
{
    public class Program
    {
        #region Main
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        #endregion

        var builder = WebApplication.CreateBuilder(Args);
        // Add services to the container.
        Builder.Services.AddRazorPages();

        var app = builder.Build();
        
                // ��� ������� � HTML, CSS � ����� wwwroot
                app.UseStaticFiles();
        
        // ���������� ������������ ��� Razor Pages
        app.MapRazorPages();
        
        app.Run();
    }
}
