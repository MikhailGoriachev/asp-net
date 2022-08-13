using Microsoft.EntityFrameworkCore;

namespace OneToManyEfCore.Models
{
    // контекст отображения на базу данных
    public class ApplicationContext : DbContext
    {
        public DbSet<Publication> Publications { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            // гарантированное удаление БД, если она существует
            // Database.EnsureDeleted();

            // гарантированное создание БД если ее нет
            Database.EnsureCreated();
        } // ApplicationContext
        

        // инициализация данных в таблице
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // иниициализация категорий
            modelBuilder
                .Entity<Category>()
                .HasData(
                    new Category {Id = 1, CategoryName = "журнал"},
                    new Category {Id = 2, CategoryName = "газета"}
            );

            // инициализация мзданий, указывать ид обязательно
            modelBuilder
                .Entity<Publication>()
                .HasData(
                    new Publication { Id =  1,  PubIndex = "464646",  CategoryId = 1, Title = "Программист",                   Cost = 1200, Start = new DateTime(2021,12, 1), Duration =  6},
                    new Publication { Id =  2,  PubIndex = "565555",  CategoryId = 2, Title = "Земля и вода",                  Cost =  250, Start = new DateTime(2021,12, 1), Duration =  3},
                    new Publication { Id =  3,  PubIndex = "464787",  CategoryId = 1, Title = "Тестировщик",                   Cost =  850, Start = new DateTime(2022, 1, 1), Duration = 12},
                    new Publication { Id =  4,  PubIndex = "464858",  CategoryId = 1, Title = "Микропроцессорные системы",     Cost = 1200, Start = new DateTime(2022, 1, 1), Duration = 12},
                    new Publication { Id =  5,  PubIndex = "565585",  CategoryId = 2, Title = "Новости ветеринарии",           Cost =  500, Start = new DateTime(2022, 3, 1), Duration =  6},
                    new Publication { Id =  6,  PubIndex = "565592",  CategoryId = 2, Title = "Земля людей",                   Cost =  680, Start = new DateTime(2022, 1, 1), Duration =  6},
                    new Publication { Id =  7,  PubIndex = "565612",  CategoryId = 2, Title = "Полевая хирургия",              Cost =  550, Start = new DateTime(2022, 6, 1), Duration =  6},
                    new Publication { Id =  8,  PubIndex = "464888",  CategoryId = 1, Title = "Моделист - конструктор",        Cost = 2500, Start = new DateTime(2021, 1, 1), Duration = 12},
                    new Publication { Id =  9,  PubIndex = "464885",  CategoryId = 1, Title = "Юный техник",                   Cost = 1800, Start = new DateTime(2020, 7, 1), Duration = 12},
                    new Publication { Id = 10,  PubIndex = "565105",  CategoryId = 2, Title = "Авиадиспетчер Юга",             Cost =  600, Start = new DateTime(2021, 1, 1), Duration = 12},
                    new Publication { Id = 11,  PubIndex = "565777",  CategoryId = 2, Title = "Поиск и сортировка",            Cost =  590, Start = new DateTime(2021, 1, 1), Duration =  6},
                    new Publication { Id = 12,  PubIndex = "464755",  CategoryId = 1, Title = "Политики безопасности Windows", Cost = 3500, Start = new DateTime(2021, 1, 1), Duration =  3},
                    new Publication { Id = 13,  PubIndex = "585989",  CategoryId = 2, Title = "Земляничная поляна",            Cost =  250, Start = new DateTime(2021, 1, 1), Duration =  6},
                    new Publication { Id = 14,  PubIndex = "581189",  CategoryId = 2, Title = "Новости мышиных гор",           Cost =  210, Start = new DateTime(2021, 1, 1), Duration =  6},
                    new Publication { Id = 15,  PubIndex = "477777",  CategoryId = 2, Title = "Земля.Вселенная.Человек",       Cost =  220, Start = new DateTime(2021, 3, 1), Duration =  3}
            );
        } // OnModelCreating

    } // class ApplicationContext
}
