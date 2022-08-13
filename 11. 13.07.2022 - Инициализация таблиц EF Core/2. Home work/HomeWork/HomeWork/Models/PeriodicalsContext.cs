using System.Collections;
using System.Data;
using Microsoft.VisualBasic.CompilerServices;
using HomeWork.Infrastructure;
using Utils = HomeWork.Infrastructure.Utils;

namespace HomeWork.Models;

using Microsoft.EntityFrameworkCore;

// Класс Контекст базы данных "Учет подписок на периодические издания"
public sealed class PeriodicalsContext : DbContext
{
    // категории (типы изданий)
    public DbSet<Category> Categories { get; set; } = null!;

    // подписки
    public DbSet<Periodical> Periodicals { get; set; } = null!;

    #region Конструкторы

    // конструктор инициаилизирующий
    public PeriodicalsContext(DbContextOptions<PeriodicalsContext> options) : base(options)
    {
        // создание базы данных при её отсутствии
        Database.EnsureCreated();
    }

    #endregion

    #region Методы

    // заполнение базы данных при создании
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // категории
        Category[] categories =
        {
            new Category { Id = 1, Name = "газета" },
            new Category { Id = 2, Name = "каталог" },
            new Category { Id = 3, Name = "журнал" },
            new Category { Id = 4, Name = "альманах" },
            new Category { Id = 5, Name = "бюллетень" }
        };

        modelBuilder.Entity<Category>().HasData(categories);

        // подписки
        var periodicals = new[] {
            new { Id = 1,Index = "56484624", Name = "Российская газета", CategoryId = 1, Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 530 },
            new { Id = 2,Index = "76423464", Name = "National Geographic", CategoryId = 3, Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 1450 },
            new { Id = 3,Index = "77622342", Name = "Alba Moda", CategoryId = 2, Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 930 },
            new { Id = 4,Index = "56484232", Name = "Москва Вечерняя", CategoryId = 1, Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 580 },
            new { Id = 5,Index = "26326755", Name = "Wenz", CategoryId = 2, Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 630 },
            new { Id = 6,Index = "93749231", Name = "Комсомольская правда", CategoryId = 1, Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 340 },
            new { Id = 7,Index = "69342331", Name = "Mona", CategoryId = 2, Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 780 },
            new { Id = 8,Index = "8535626 4", Name = "People", CategoryId = 3, Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 2500 },
            new { Id = 9,Index = "54365454", Name = "Metro", CategoryId = 1, Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 250 },
            new { Id = 10,Index = "42556723", Name = "Glamour", CategoryId = 3, Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 1730 },
            new { Id = 11,Index = "23462334", Name = "Good Housekeeping", CategoryId = 3, Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 2100 },
            new { Id = 12,Index = "67832435", Name = "Peter Hahn", CategoryId = 2, Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 870 },
            new { Id = 13,Index = "13472345", Name = "Cosmopolitan", CategoryId = 3, Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 1800 },
        };

        modelBuilder.Entity<Periodical>().HasData(periodicals);
    }

    #endregion
}
