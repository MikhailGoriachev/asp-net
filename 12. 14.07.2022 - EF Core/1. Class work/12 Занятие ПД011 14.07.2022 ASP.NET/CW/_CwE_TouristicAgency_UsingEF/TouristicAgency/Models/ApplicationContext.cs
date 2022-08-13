using Microsoft.EntityFrameworkCore;

namespace TouristicAgency.Models
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Client> Clients    { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Purpose> Purposes  { get; set; } = null!;
        public DbSet<Route> Routes      { get; set; } = null!;
        public DbSet<Travel> Travels    { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            // гарантированное удаление БД, если она существует
            // Database.EnsureDeleted();

            // гарантированное создание БД если ее нет
            Database.EnsureCreated();
        } // ApplicationContext

        // инициализация данных в таблице
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        } // OnModelCreating

    } // class ApplicationContext
}
