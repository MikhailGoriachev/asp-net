using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using HomeWork.Infrastructure;
using Utils = HomeWork.Infrastructure.Utils;

namespace HomeWork.Models;

// Контекст базы данных "Туристические агенты"
public sealed class TouristAgencyContext : DbContext
{
    // клиенты
    public DbSet<Client>? Clients { get; set; }

    // страны
    public DbSet<Country>? Countries { get; set; }

    // цели поездки
    public DbSet<Objective>? Objectives { get; set; }

    // маршруты
    public DbSet<Route>? Routes { get; set; }

    // поездки
    public DbSet<Visit>? Visits { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public TouristAgencyContext(DbContextOptions<TouristAgencyContext> options) : base(options)
    {
        // создание базы данных при её отсутствии
        Database.EnsureCreated();
    }

    #endregion

    #region Методы

    // инициализация базы данных
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // установка уникальности для поля
        modelBuilder.Entity<Client>()
            .HasIndex(c => c.Passport)
            .IsUnique();

        // клиенты
        Client[] clients =
        {
            new Client(1, "Терехова", "Ева", "Тимуровна", "001347343"),
            new Client(2, "Поляков", "Михаил", "Яромирович", "004234543"),
            new Client(3, "Балашова", "Ульяна", "Андреевна", "005431243"),
            new Client(4, "Кузьмина", "Алиса", "Захаровна", "004562346"),
            new Client(5, "Денисова", "Елизавета", "Александровна", "009884665"),
            new Client(6, "Белоусов", "Лука", "Даниилович", "002348663"),
            new Client(7, "Наумов", "Александр", "Маркович", "009572844"),
            new Client(8, "Ефимов", "Артём", "Матвеевич", "006864234"),
            new Client(9, "Новикова", "Амира", "Матвеевна", "002383632"),
            new Client(10, "Митрофанова", "Алёна", "Марковна", "001533134"),
        };

        // страны
        Country[] countries =
        {
            new Country(1, "Италия", 80_000, 25_000),
            new Country(2, "Франция", 90_000, 23_000),
            new Country(3, "Германия", 60_000, 20_000),
            new Country(4, "Великобритания", 95_000, 30_000),
            new Country(5, "Ирландия", 75_000, 18_000),
            new Country(6, "Испания", 80_000, 20_000),
            new Country(7, "Чехия", 40_000, 15_000),
            new Country(8, "Нидерланды", 60_000, 27_000),
            new Country(9, "Греция", 50_000, 20_000),
            new Country(10, "Австрия", 75_000, 28_000),
        };

        // цели поездки
        Objective[] objectives =
        {
            new Objective(1, "Туризм"),
            new Objective(2, "Гости"),
            new Objective(3, "Спорт"),
            new Objective(4, "Деловая/Бизнес"),
            new Objective(5, "Культура"),
            new Objective(6, "Работа"),
            new Objective(7, "Учеба"),
            new Objective(8, "Въездная виза")
        };

        // количество записей
        int n = 30;

        // маршруты
        Route[] routes = Enumerable.Range(1, n)
            .Select(id => new Route(id, Utils.GetInt(1, countries.Length), Utils.GetInt(1, objectives.Length),
                Utils.GetInt(5, 11) * 1000))
            .ToArray();

        n = 40;

        // поездки
        Visit[] visits = Enumerable.Range(1, n)
            .Select(id => new Visit(id, Utils.GetInt(1, clients.Length), Utils.GetInt(1, routes.Length),
                Utils.GetDate(DateTime.Now.Date.AddDays(-80), DateTime.Now.Date), Utils.GetInt(3, 15)))
            .ToArray();


        // запись данных в таблицы
        modelBuilder.Entity<Client>().HasData(clients);
        modelBuilder.Entity<Country>().HasData(countries);
        modelBuilder.Entity<Objective>().HasData(objectives);
        modelBuilder.Entity<Route>().HasData(routes);
        modelBuilder.Entity<Visit>().HasData(visits);
    }

    #endregion
}
