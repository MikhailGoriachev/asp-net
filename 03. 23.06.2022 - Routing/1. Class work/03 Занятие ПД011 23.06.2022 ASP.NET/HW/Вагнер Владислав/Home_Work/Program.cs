using System.Globalization;

namespace Home_Work
{
    public class Program
    {
        public static void Main(string[] args)
        {
           CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
           CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");

            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // ��� ������� � HTML, CSS � ����� wwwroot
            app.UseStaticFiles();

            // ���������� ������������ ��� Razor Pages
            app.MapRazorPages();

            app.Run();
        }
    }
}