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
        // подписки
        Periodical[] periodicals = {
            new Periodical { Id = 1,Index = "93749231", Name = "Комсомольская правда", TypeEdition = "газета", Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 340 },
            new Periodical { Id = 2,Index = "54365454", Name = "Metro", TypeEdition = "газета", Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 250 },
            new Periodical { Id = 3,Index = "56484624", Name = "Российская газета", TypeEdition = "газета", Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 530 },
            new Periodical { Id = 4,Index = "56484232", Name = "Москва Вечерняя", TypeEdition = "газета", Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 580 },
            new Periodical { Id = 5,Index = "69342331", Name = "Mona", TypeEdition = "каталог", Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 780 },
            new Periodical { Id = 6,Index = "77622342", Name = "Alba Moda", TypeEdition = "каталог", Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 930 },
            new Periodical { Id = 7,Index = "67832435", Name = "Peter Hahn", TypeEdition = "каталог", Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 870 },
            new Periodical { Id = 8,Index = "26326755", Name = "Wenz", TypeEdition = "каталог", Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 630 },
            new Periodical { Id = 9,Index = "13472345", Name = "Cosmopolitan", TypeEdition = "журнал", Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 1800 },
            new Periodical { Id = 10,Index = "76423464", Name = "National Geographic", TypeEdition = "журнал", Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 1450 },
            new Periodical { Id = 11,Index = "23462334", Name = "Good Housekeeping", TypeEdition = "журнал", Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 2100 },
            new Periodical { Id = 12,Index = "42556723", Name = "Glamour", TypeEdition = "журнал", Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 1730 },
            new Periodical { Id = 13,Index = "8535626 4", Name = "People", TypeEdition = "журнал", Date = Utils.GetDate(DateTime.Now.AddDays(-80), DateTime.Now), Duration = Utils.GetInt(1, 5), Price = 2500 },
        };

        modelBuilder.Entity<Periodical>().HasData(periodicals);
    }

    #endregion
}
