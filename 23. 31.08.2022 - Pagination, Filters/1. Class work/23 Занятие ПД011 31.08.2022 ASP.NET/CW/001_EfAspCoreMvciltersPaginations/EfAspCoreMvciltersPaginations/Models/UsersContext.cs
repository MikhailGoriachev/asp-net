using Microsoft.EntityFrameworkCore;



namespace EntityFrameworkInAspNetCoreMvcIntro.Models;

public class UsersContext : DbContext
{
    // отображение на таблицы БД сущностей
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;


    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options) {

        // гарантированное удаление БД
        // Database.EnsureDeleted();

        // гарантированное создание БД
        Database.EnsureCreated();
    } // UsersContext


    // инициализация данных в таблицах
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // компании, в которых работают пользователи
        modelBuilder.Entity<Company>().HasData(
            new Company { Id = 1, Name = "Рога и копыта" },
            new Company { Id = 2, Name = "Купим все" },
            new Company { Id = 3, Name = "Купим всех" },
            new Company { Id = 4, Name = "Научим всех" },
            new Company { Id = 5, Name = "Научные исследования" },
            new Company { Id = 6, Name = "Частный Визит" },
            new Company { Id = 7, Name = "Пирожки для всей семьи" },
            new Company { Id = 8, Name = "Медицинское обследование" },
            new Company { Id = 9, Name = "Спортивные соревнования" },
            new Company { Id = 10, Name = "Работа для всех" }
        );

        // клиенты
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Юрковский Б.В.", Age = 34, CompanyId = 1 },
            new User { Id = 2, Name = "Якубовская", Age = 34, CompanyId = 1 },
            new User { Id = 3, Name = "Шапиро", Age = 18, CompanyId = 2 },
            new User { Id = 4, Name = "Вожжаев С.А.", Age = 34, CompanyId = 4 },
            new User { Id = 5, Name = "Хроменко", Age = 56, CompanyId = 1 },
            new User { Id = 6, Name = "Пелых С.М.", Age = 34, CompanyId = 3 },
            new User { Id = 7, Name = "Лапотникова К.Е.", Age = 34, CompanyId = 1 },
            new User { Id = 8, Name = "Огородников С.Т.", Age = 28, CompanyId = 1 },
            new User { Id = 9, Name = "Яйло Е.Н.", Age = 27, CompanyId = 2 },
            new User { Id = 10, Name = "Лосева  Е.Е.", Age = 33, CompanyId = 1 },
            new User { Id = 11, Name = "Михайлович Б.А.", Age = 42, CompanyId = 1 },
            new User { Id = 12, Name = "Тарапата М.П.", Age = 34, CompanyId = 10 }
        );
    } // OnModelCreating
}

