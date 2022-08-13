using H_WASP_NET.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace H_WASP_NET.Models
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<PurposeTravel> PurposeTravels { get; set; } = null!;
        public DbSet<Route> Routes { get; set; } = null!;
        public DbSet<Travel> Travels { get; set; } = null!;


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            // гарантированное удаление БД, если она существует
             //Database.EnsureDeleted();
            // создание базы данных при её отсутствии
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DateTime GetDate() => Utils.RandomDate(new DateTime(2022, 1, 1), DateTime.Today);


            modelBuilder.Entity<Client>()
                .HasData(
                new Client { Id = 1,  Surname = "Вакулин",    Name = "Сегей",      Patronymic = "Владимирович", Passport = "64 25 42462" },
                new Client { Id = 2,  Surname = "Гончарова",  Name = "Ариана",     Patronymic = "Алексеевна",   Passport = "74 22 82822" },
                new Client { Id = 3,  Surname = "Серов",      Name = "Платон",     Patronymic = "Львович",      Passport = "68 24 87462" },
                new Client { Id = 4,  Surname = "Котов",      Name = "Михаил",     Patronymic = "Павлович",     Passport = "24 28 52462" },
                new Client { Id = 5,  Surname = "Грачева",    Name = "Василиса",   Patronymic = "Алексеевна",   Passport = "66 29 82461" },
                new Client { Id = 6,  Surname = "Морозова",   Name = "Таисия",     Patronymic = "Платоновна",   Passport = "64 18 87462" },
                new Client { Id = 7,  Surname = "Семенова",   Name = "Дарина",     Patronymic = "Марковна",     Passport = "14 68 52462" },
                new Client { Id = 8,  Surname = "Лавров",     Name = "Роман",      Patronymic = "Андреевич",    Passport = "69 27 82432" },
                new Client { Id = 9,  Surname = "Смирнов",    Name = "Константин", Patronymic = "Артёмович",    Passport = "65 24 92462" },
                new Client { Id = 10, Surname = "Грибова",    Name = "Софья",      Patronymic = "Никитична",    Passport = "64 08 82461" },
                new Client { Id = 11, Surname = "Николаева",  Name = "Валерия",    Patronymic = "Максимовна",   Passport = "64 23 84462" },
                new Client { Id = 12, Surname = "Рыбаков",    Name = "Даниил",     Patronymic = "Георгиевич",   Passport = "61 27 82452" },
                new Client { Id = 13, Surname = "Островский", Name = "Александр",  Patronymic = "Михайлович",   Passport = "54 78 82432" },
                new Client { Id = 14, Surname = "Воронина",   Name = "Василиса",   Patronymic = "Кирилловна",   Passport = "63 38 62467" },
                new Client { Id = 15, Surname = "Григорьев",  Name = "Тихон",      Patronymic = "Кириллович",   Passport = "84 27 72452" }
                
                );

            modelBuilder.Entity<Country>()
                .HasData(
                new Country { Id = 1, NameCountry = "Франция",  CostVisa = 2000, CostTransportServ = 7000 },
                new Country { Id = 2, NameCountry = "Нью-Йорк", CostVisa = 4200, CostTransportServ = 4000 },
                new Country { Id = 3, NameCountry = "Австрия",  CostVisa = 3000, CostTransportServ = 2000 },
                new Country { Id = 4, NameCountry = "Египет",   CostVisa = 1500, CostTransportServ = 2000 },
                new Country { Id = 5, NameCountry = "Турция",   CostVisa = 2400, CostTransportServ = 5000 },
                new Country { Id = 6, NameCountry = "Болгария", CostVisa = 1800, CostTransportServ = 2000 },
                new Country { Id = 7, NameCountry = "Москва",   CostVisa = 2300, CostTransportServ = 3000 },
                new Country { Id = 8, NameCountry = "Италия",   CostVisa = 4200, CostTransportServ = 6000 },
                new Country { Id = 9, NameCountry = "Китай",    CostVisa = 6300, CostTransportServ = 3000 },
                new Country { Id = 10, NameCountry = "Япония",  CostVisa = 4000, CostTransportServ = 2000 }

                );


            modelBuilder.Entity<PurposeTravel>()
                .HasData(
                new PurposeTravel { Id = 1, NamePurpTravel = "лечение" },
                new PurposeTravel { Id = 2, NamePurpTravel = "туризм" },
                new PurposeTravel { Id = 3, NamePurpTravel = "гости" },
                new PurposeTravel { Id = 4, NamePurpTravel = "спорт" },
                new PurposeTravel { Id = 5, NamePurpTravel = "отдых" },
                new PurposeTravel { Id = 6, NamePurpTravel = "работа" },
                new PurposeTravel { Id = 7, NamePurpTravel = "учёба" }
                );

            modelBuilder.Entity<Route>()
                .HasData(
                new Route { Id = 1, CountryId = 1, PurposeTravelId = 1, CostDayStay = 3300 },
                new Route { Id = 2, CountryId = 3, PurposeTravelId = 5, CostDayStay = 7100 },
                new Route { Id = 3, CountryId = 5, PurposeTravelId = 6, CostDayStay = 2800 },
                new Route { Id = 4, CountryId = 7, PurposeTravelId = 1, CostDayStay = 3100 },
                new Route { Id = 5, CountryId = 8, PurposeTravelId = 3, CostDayStay = 5300 },
                new Route { Id = 6, CountryId = 10, PurposeTravelId = 1, CostDayStay = 5200 },
                new Route { Id = 7, CountryId = 1, PurposeTravelId = 7, CostDayStay = 3300 },
                new Route { Id = 8, CountryId = 2, PurposeTravelId = 1, CostDayStay = 5300 },
                new Route { Id = 9, CountryId = 4, PurposeTravelId = 2, CostDayStay = 2200 },
                new Route { Id = 10, CountryId = 7, PurposeTravelId = 1, CostDayStay = 3100 },
                new Route { Id = 11, CountryId = 2, PurposeTravelId = 7, CostDayStay = 3300 },
                new Route { Id = 12, CountryId = 10, PurposeTravelId = 1, CostDayStay = 5200 },
                new Route { Id = 13, CountryId = 9, PurposeTravelId = 5, CostDayStay = 6400 },
                new Route { Id = 14, CountryId = 7, PurposeTravelId = 2, CostDayStay = 3100 },
                new Route { Id = 15, CountryId = 2, PurposeTravelId = 4, CostDayStay = 5300 }

                );


            modelBuilder.Entity<Travel>()
                .HasData(
                new Travel { Id = 1, ClientId = 1, RouteId = 1, StartTravel = GetDate(), Duration = 6 },
                new Travel { Id = 2, ClientId = 2, RouteId = 2, StartTravel = GetDate(), Duration = 7 },
                new Travel { Id = 3, ClientId = 3, RouteId = 3, StartTravel = GetDate(), Duration = 10 },
                new Travel { Id = 4, ClientId = 4, RouteId = 4, StartTravel = GetDate(), Duration = 12 },
                new Travel { Id = 5, ClientId = 5, RouteId = 5, StartTravel = GetDate(), Duration = 14 },
                new Travel { Id = 6, ClientId = 6, RouteId = 6, StartTravel = GetDate(), Duration = 7 },
                new Travel { Id = 7, ClientId = 7, RouteId = 7, StartTravel = GetDate(), Duration = 6 },
                new Travel { Id = 8, ClientId = 8, RouteId = 8, StartTravel = GetDate(), Duration = 10 },
                new Travel { Id = 9, ClientId = 9, RouteId = 9, StartTravel = GetDate(), Duration = 12 },
                new Travel { Id = 10, ClientId = 10, RouteId = 10, StartTravel = GetDate(), Duration = 14 },
                new Travel { Id = 11, ClientId = 11, RouteId = 11, StartTravel = GetDate(), Duration = 6 },
                new Travel { Id = 12, ClientId = 12, RouteId = 12, StartTravel = GetDate(), Duration = 7 },
                new Travel { Id = 13, ClientId = 13, RouteId = 13, StartTravel = GetDate(), Duration = 10 },
                new Travel { Id = 14, ClientId = 14, RouteId = 14, StartTravel = GetDate(), Duration = 12 },
                new Travel { Id = 15, ClientId = 15, RouteId = 15, StartTravel = GetDate(), Duration = 14 }
                );

        } // OnModelCreating


    } // class ApplicationContext
}
