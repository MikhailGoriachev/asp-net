using Microsoft.EntityFrameworkCore;


namespace TouristicAgencyMvcCore.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Purpose> Purposes { get; set; } = null!;
        public DbSet<Route> Routes { get; set; } = null!;
        public DbSet<Travel> Travels { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
            // гарантированное удаление БД, если она существует
            // Database.EnsureDeleted();

            // гарантированное создание БД если ее нет
            Database.EnsureCreated();
        } // ApplicationContext


        // инициализация данных в таблице
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Цели поездок
            modelBuilder.Entity<Purpose>().HasData(
                new Purpose { Id = 1, Name = "отдых" },
                new Purpose { Id = 2, Name = "лечение" },
                new Purpose { Id = 3, Name = "бизнес" },
                new Purpose { Id = 4, Name = "учеба" },
                new Purpose { Id = 5, Name = "научные исследования" },
                new Purpose { Id = 6, Name = "частный визит" },
                new Purpose { Id = 7, Name = "официальный визит" },
                new Purpose { Id = 8, Name = "медицинское обследование" },
                new Purpose { Id = 9, Name = "спортивное соревнование" },
                new Purpose { Id = 10, Name = "работа" }
            );

            // Страны
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Россия", TransferCost = 230, VisaCost = 100 },
                new Country { Id = 2, Name = "Казахстан", TransferCost = 330, VisaCost = 100 },
                new Country { Id = 3, Name = "Куба", TransferCost = 130, VisaCost = 80 },
                new Country { Id = 4, Name = "Сербия", TransferCost = 1_130, VisaCost = 1_100 },
                new Country { Id = 5, Name = "Египет", TransferCost = 430, VisaCost = 500 },
                new Country { Id = 6, Name = "Германия", TransferCost = 1_130, VisaCost = 2_100 },
                new Country { Id = 7, Name = "Франция", TransferCost = 1_300, VisaCost = 3_100 },
                new Country { Id = 8, Name = "Испания", TransferCost = 1_200, VisaCost = 3_200 },
                new Country { Id = 9, Name = "Италия", TransferCost = 780, VisaCost = 3_100 },
                new Country { Id = 10, Name = "Венгрия", TransferCost = 460, VisaCost = 1_500 }
            );

            // клиенты
            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, Surname = "Юрковский", Name = "Марк", Patronymic = "Максимилианович", Passport = "31 22 009001" },
                new Client { Id = 2, Surname = "Якубовская", Name = "Диана", Patronymic = "Павловна", Passport = "33 19 901763" },
                new Client { Id = 3, Surname = "Шапиро", Name = "Федор", Patronymic = "Федорович", Passport = "32 20 902342" },
                new Client { Id = 4, Surname = "Вожжаев", Name = "Сергей", Patronymic = "Денисович", Passport = "28 19 090900" },
                new Client { Id = 5, Surname = "Хроменко", Name = "Игорь", Patronymic = "Владимирович", Passport = "28 19 280911" },
                new Client { Id = 6, Surname = "Пелых", Name = "Марина", Patronymic = "Ульяновна", Passport = "32 22 123456" },
                new Client { Id = 7, Surname = "Лапотникова", Name = "Тамара", Patronymic = "Оскаровна", Passport = "31 20 098890" },
                new Client { Id = 8, Surname = "Огородников", Name = "Сергей", Patronymic = "Иванович", Passport = "32 21 678001" },
                new Client { Id = 9, Surname = "Яйло", Name = "Екатерина", Patronymic = "Николаевна", Passport = "35 18 789000" },
                new Client { Id = 10, Surname = "Лосева", Name = "Инна", Patronymic = "Степановна", Passport = "31 17 123456" },
                new Client { Id = 11, Surname = "Михайлович", Name = "Анна", Patronymic = "Валентиновна", Passport = "32 19 456449" },
                new Client { Id = 12, Surname = "Тарапата", Name = "Михаил", Patronymic = "Исаакович", Passport = "32 28 345670" }
            );

            // Маршруты
            modelBuilder.Entity<Route>().HasData(
                new Route { Id = 1, PurposeId = 1, CountryId = 1, DailyCost = 340 },
                new Route { Id = 2, PurposeId = 1, CountryId = 2, DailyCost = 300 },
                new Route { Id = 3, PurposeId = 1, CountryId = 3, DailyCost = 560 },
                new Route { Id = 4, PurposeId = 1, CountryId = 4, DailyCost = 540 },
                new Route { Id = 5, PurposeId = 1, CountryId = 5, DailyCost = 400 },
                new Route { Id = 6, PurposeId = 2, CountryId = 1, DailyCost = 550 },
                new Route { Id = 7, PurposeId = 2, CountryId = 2, DailyCost = 600 },
                new Route { Id = 8, PurposeId = 2, CountryId = 3, DailyCost = 650 },
                new Route { Id = 9, PurposeId = 2, CountryId = 4, DailyCost = 540 },
                new Route { Id = 10, PurposeId = 2, CountryId = 5, DailyCost = 290 },
                new Route { Id = 11, PurposeId = 3, CountryId = 1, DailyCost = 390 },
                new Route { Id = 12, PurposeId = 3, CountryId = 2, DailyCost = 530 },
                new Route { Id = 13, PurposeId = 3, CountryId = 3, DailyCost = 550 },
                new Route { Id = 14, PurposeId = 3, CountryId = 4, DailyCost = 290 },
                new Route { Id = 15, PurposeId = 3, CountryId = 5, DailyCost = 220 },
                new Route { Id = 16, PurposeId = 4, CountryId = 1, DailyCost = 670 },
                new Route { Id = 17, PurposeId = 4, CountryId = 2, DailyCost = 510 },
                new Route { Id = 18, PurposeId = 4, CountryId = 3, DailyCost = 320 },
                new Route { Id = 19, PurposeId = 4, CountryId = 4, DailyCost = 700 },
                new Route { Id = 20, PurposeId = 4, CountryId = 5, DailyCost = 100 }
            );

            // Поездки
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 1, ClientId = 1, RouteId = 1, Start = new DateTime(2022, 1, 10), Duration = 5 },
                new Travel { Id = 2, ClientId = 2, RouteId = 1, Start = new DateTime(2022, 1, 12), Duration = 6 },
                new Travel { Id = 3, ClientId = 3, RouteId = 2, Start = new DateTime(2022, 1, 12), Duration = 12 },
                new Travel { Id = 4, ClientId = 4, RouteId = 2, Start = new DateTime(2022, 2, 24), Duration = 22 },
                new Travel { Id = 5, ClientId = 5, RouteId = 2, Start = new DateTime(2022, 2, 24), Duration = 21 },
                new Travel { Id = 6, ClientId = 6, RouteId = 3, Start = new DateTime(2022, 2, 12), Duration = 5 },
                new Travel { Id = 7, ClientId = 7, RouteId = 3, Start = new DateTime(2022, 3, 3), Duration = 15 },
                new Travel { Id = 8, ClientId = 8, RouteId = 3, Start = new DateTime(2022, 3, 3), Duration = 22 },
                new Travel { Id = 9, ClientId = 2, RouteId = 5, Start = new DateTime(2022, 3, 3), Duration = 3 },
                new Travel { Id = 10, ClientId = 3, RouteId = 6, Start = new DateTime(2022, 3, 3), Duration = 3 }
            );
        } // OnModelCreating

    } // class ApplicationContext
}
