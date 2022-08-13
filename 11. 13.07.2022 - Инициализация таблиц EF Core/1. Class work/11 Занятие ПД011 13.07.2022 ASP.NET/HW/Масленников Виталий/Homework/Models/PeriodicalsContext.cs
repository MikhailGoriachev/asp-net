using Microsoft.EntityFrameworkCore;
using Homework.Common;

namespace Homework.Models;

// контекст отображения на базу данных
public sealed class PeriodicalsContext : DbContext
{
    public DbSet<Publication> Publications { get; set; } = null!;
    
    public PeriodicalsContext(DbContextOptions<PeriodicalsContext> options) : base(options)
    {
	    Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        IEnumerable<int> durations = new[] { 1, 3, 6, 9, 12 };
        DateTime GetDate() => Utils.RandomDate(new DateTime(2021, 1, 1), DateTime.Today);

        modelBuilder.Entity<Publication>().HasData(
            new Publication { PublicationId =  1, PubType = "газета", PubIndex = "Е13133", Title = "Известия", Price = 798, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId =  2, PubType = "газета", PubIndex = "Е27000", Title = "Моя семья",                       Price =  210, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId =  3, PubType = "газета", PubIndex = "Е19946", Title = "Пенсионерская правда",            Price =   63, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId =  4, PubType = "журнал", PubIndex = "Т79943", Title = "Кабели и провода",                Price =  312, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId =  5, PubType = "газета", PubIndex = "Е43298", Title = "Поиск",                           Price =  315, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId =  6, PubType = "газета", PubIndex = "Е50102", Title = "Правда",                          Price =  273, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId =  7, PubType = "журнал", PubIndex = "Т20859", Title = "Космонавтика и ракетостроение",   Price = 1324, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId =  8, PubType = "газета", PubIndex = "И32089", Title = "Экономика и жизнь",               Price =  105, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId =  9, PubType = "газета", PubIndex = "Э87404", Title = "Налоги. Экономика. Финансы",      Price =  145, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 10, PubType = "журнал", PubIndex = "Е71444", Title = "Клиническая медицина",            Price = 1125, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 11, PubType = "журнал", PubIndex = "Е26902", Title = "Домашний очаг",                   Price =  307, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 12, PubType = "газета", PubIndex = "Р18060", Title = "РБК",                             Price =  385, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 13, PubType = "журнал", PubIndex = "Е43133", Title = "Юный техник",                     Price =  551, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 14, PubType = "газета", PubIndex = "Э11750", Title = "Аргументы и факты",               Price =  327, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 15, PubType = "журнал", PubIndex = "Е43246", Title = "Мурзилка",                        Price =  322, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 16, PubType = "журнал", PubIndex = "Ц34147", Title = "Наука и жизнь",                   Price =  460, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 17, PubType = "газета", PubIndex = "Е29723", Title = "Завалинка",                       Price =  116, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 18, PubType = "журнал", PubIndex = "Э38908", Title = "64-шахматное обозрение",          Price =  280, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 19, PubType = "альманах", PubIndex = "А31120", Title = "Альманах пеших маршрутов",        Price = 1280, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 20, PubType = "газета", PubIndex = "Е27818", Title = "Травинка",                        Price =   75, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 21, PubType = "журнал", PubIndex = "Н73477", Title = "Морская коллекция",               Price =  516, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 22, PubType = "журнал", PubIndex = "Р70558", Title = "Моделист-конструктор",            Price =  448, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 23, PubType = "газета", PubIndex = "Е29090", Title = "СПИД-инфо",                       Price =  315, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 24, PubType = "журнал", PubIndex = "Н70002", Title = "Автоматика, связь, информатика",  Price =  438, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 25, PubType = "журнал", PubIndex = "Е38850", Title = "Развивалки",                      Price =   68, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 26, PubType = "журнал", PubIndex = "Е99555", Title = "Будь здоров!",                    Price =   75, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 27, PubType = "журнал", PubIndex = "Е43039", Title = "Веселый затейник",                Price =  145, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 28, PubType = "журнал", PubIndex = "Е43181", Title = "Черепашки-ниндзя. Играй и учись", Price =  134, StartDate = GetDate(), Duration = durations.Random() },
            new Publication { PublicationId = 29, PubType = "газета", PubIndex = "Е39803", Title = "Медицинский вестник Челябинска",  Price =  130, StartDate = GetDate(), Duration = durations.Random() }
        );

    }
}