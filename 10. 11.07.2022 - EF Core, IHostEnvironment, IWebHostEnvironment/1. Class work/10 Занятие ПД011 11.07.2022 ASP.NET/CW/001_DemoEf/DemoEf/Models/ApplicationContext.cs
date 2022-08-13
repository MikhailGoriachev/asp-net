using Microsoft.EntityFrameworkCore;

namespace DemoEf.Models
{
    // контекст отображения на базу данных
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            // создание БД если ее нет
            Database.EnsureCreated();
        } // ApplicationContext

    } // class ApplicationContext
}
