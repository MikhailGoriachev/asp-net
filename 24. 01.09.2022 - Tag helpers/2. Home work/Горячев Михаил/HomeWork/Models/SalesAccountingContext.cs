using Microsoft.EntityFrameworkCore;

namespace HomeWork.Models;

// контекст базы данных "Оптовый магазин. Учет продаж"
public sealed class SalesAccountingContext : DbContext
{
    // единицы измерения
    public DbSet<Unit> Units { get; set; } = default!;

    // товары
    public DbSet<Goods> GoodsList { get; set; } = default!;

    // продавцы
    public DbSet<Seller> Sellers { get; set; } = default!;

    // закупки
    public DbSet<Purchase> Purchases { get; set; } = default!;

    // продажи
    public DbSet<Sale> Sales { get; set; } = default!;


    #region Конструкторы

    // конструктор инициализирующий
    public SalesAccountingContext(DbContextOptions<SalesAccountingContext> options) : base(options) =>
        Database.EnsureCreatedAsync();

    #endregion

    #region Методы

    // инициализация таблиц
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // единицы измерения
        modelBuilder.Entity<Unit>().HasData(
            new Unit(1, "Метр", "м"),
            new Unit(2, "Квадратный метр", "м2"),
            new Unit(3, "Литр", "л"),
            new Unit(4, "Килограмм", "кг"),
            new Unit(5, "Грамм", "г"),
            new Unit(6, "Тонна метрическая", "т"),
            new Unit(7, "Центнер", "ц"),
            new Unit(8, "Штука", "шт"),
            new Unit(9, "Коробка", "кор"),
            new Unit(10, "Цистерна", "цистерн"),
            new Unit(11, "Ящик", "ящ"),
            new Unit(12, "Пакет", "пак"),
            new Unit(13, "Пачка", "пач"),
            new Unit(14, "Рулон", "рул"),
            new Unit(15, "Погонный метр", "пог. м"),
            new Unit(16, "Комплект", "комп")
        );

        // товары
        modelBuilder.Entity<Goods>().HasData(
            new Goods(1, "Чай Lipton"),
            new Goods(2, "Чай Grenfield"),
            new Goods(3, "Чай Curtis"),
            new Goods(4, "Чай Richard"),
            new Goods(5, "Мука 1 сорт"),
            new Goods(6, "Мука высший сорт"),
            new Goods(7, "Гречка"),
            new Goods(8, "Рис"),
            new Goods(9, "Тетардь Interdruk"),
            new Goods(10, "Ячмень"),
            new Goods(11, "Тетардь KiteStudio"),
            new Goods(12, "Сахар"),
            new Goods(13, "Обои Адель"),
            new Goods(14, "Обои Grandeco"),
            new Goods(15, "Обои Асти"),
            new Goods(16, "Обои Континент"),
            new Goods(17, "Обои Сафари"),
            new Goods(18, "Обои Rasch"),
            new Goods(19, "Обои Синтра"),
            new Goods(20, "Обои Oscar")
        );

        // продавцы
        modelBuilder.Entity<Seller>().HasData(
            new Seller(1, "Астафьева", "Дарья", "Алексеевна", 8),
            new Seller(2, "Дмитриева", "София", "Кирилловна", 12),
            new Seller(3, "Григорьева", "Вера", "Владиславовна", 5),
            new Seller(4, "Литвинова", "Ольга", "Ярославовна", 8),
            new Seller(5, "Козырев", "Юрий", "Семёнович", 7),
            new Seller(6, "Власов", "Михаил", "Александрович", 4),
            new Seller(7, "Григорьев", "Игорь", "Андреевич", 8),
            new Seller(8, "Колесов", "Иван", "Александрович", 6),
            new Seller(9, "Черных", "Алина", "Ильинична", 7),
            new Seller(10, "Фадеева", "София", "Богдановна", 10)
        );

        // закупки
        modelBuilder.Entity<Purchase>().HasData(
            new Purchase(1, 1, 13, 520, 120, new DateTime(2021, 11, 22)),
            new Purchase(2, 2, 13, 320, 30, new DateTime(2021, 10, 06)),
            new Purchase(3, 4, 7, 630, 4, new DateTime(2021, 11, 22)),
            new Purchase(4, 1, 13, 480, 85, new DateTime(2021, 03, 07)),
            new Purchase(5, 13, 14, 760, 20, new DateTime(2021, 11, 15)),
            new Purchase(6, 3, 13, 350, 70, new DateTime(2021, 11, 16)),
            new Purchase(7, 4, 13, 600, 30, new DateTime(2021, 11, 13)),
            new Purchase(8, 1, 13, 500, 90, new DateTime(2021, 09, 15)),
            new Purchase(9, 7, 4, 35, 350, new DateTime(2021, 03, 18)),
            new Purchase(10, 9, 8, 40, 90, new DateTime(2021, 11, 22)),
            new Purchase(11, 13, 14, 700, 40, new DateTime(2021, 10, 15)),
            new Purchase(12, 14, 14, 950, 70, new DateTime(2021, 10, 15)),
            new Purchase(13, 15, 14, 630, 20, new DateTime(2021, 09, 18)),
            new Purchase(14, 16, 14, 750, 45, new DateTime(2021, 09, 18)),
            new Purchase(15, 15, 14, 650, 40, new DateTime(2021, 11, 22)),
            new Purchase(16, 17, 14, 930, 30, new DateTime(2021, 10, 15)),
            new Purchase(17, 18, 14, 550, 35, new DateTime(2021, 09, 15)),
            new Purchase(18, 19, 14, 840, 28, new DateTime(2021, 09, 15)),
            new Purchase(19, 20, 14, 730, 35, new DateTime(2021, 09, 18)),
            new Purchase(20, 17, 14, 880, 48, new DateTime(2021, 08, 19)),
            new Purchase(21, 15, 14, 830, 52, new DateTime(2021, 08, 19)),
            new Purchase(22, 15, 14, 830, 38, new DateTime(2021, 05, 15)),
            new Purchase(23, 17, 14, 760, 15, new DateTime(2021, 05, 08)),
            new Purchase(24, 12, 7, 1200, 3, new DateTime(2021, 10, 15)),
            new Purchase(25, 15, 14, 760, 18, new DateTime(2021, 03, 28)),
            new Purchase(26, 11, 8, 49, 60, new DateTime(2021, 01, 06)),
            new Purchase(27, 4, 13, 720, 16, new DateTime(2021, 01, 06)),
            new Purchase(28, 9, 8, 70, 120, new DateTime(2021, 07, 05)),
            new Purchase(29, 11, 8, 35, 120, new DateTime(2021, 05, 09)),
            new Purchase(30, 14, 14, 870, 50, new DateTime(2021, 06, 18))
        );

        // продажи
        modelBuilder.Entity<Sale>().HasData(
            new Sale(1, new DateTime(2021, 10, 07), 4, 11, 6, 780, 14),
            new Sale(2, new DateTime(2021, 11, 16), 4, 18, 20, 890, 14),
            new Sale(3, new DateTime(2021, 06, 07), 6, 12, 12, 1200, 14),
            new Sale(4, new DateTime(2021, 10, 03), 3, 1, 16, 530, 13),
            new Sale(5, new DateTime(2021, 09, 18), 4, 18, 10, 850, 14),
            new Sale(6, new DateTime(2021, 11, 16), 3, 11, 11, 750, 14),
            new Sale(7, new DateTime(2021, 09, 18), 2, 20, 12, 900, 14),
            new Sale(8, new DateTime(2021, 06, 03), 6, 30, 6, 1000, 14),
            new Sale(9, new DateTime(2021, 10, 07), 4, 20, 12, 1080, 14),
            new Sale(10, new DateTime(2021, 06, 18), 4, 27, 20, 760, 13),
            new Sale(11, new DateTime(2021, 06, 03), 3, 27, 12, 980, 13),
            new Sale(12, new DateTime(2021, 11, 13), 7, 30, 8, 1090, 14),
            new Sale(13, new DateTime(2021, 06, 18), 3, 20, 16, 950, 14),
            new Sale(14, new DateTime(2021, 10, 07), 4, 11, 12, 1000, 14),
            new Sale(15, new DateTime(2021, 09, 18), 7, 27, 20, 990, 13),
            new Sale(16, new DateTime(2021, 09, 18), 1, 12, 12, 560, 14),
            new Sale(17, new DateTime(2021, 11, 13), 4, 12, 12, 1050, 14),
            new Sale(18, new DateTime(2021, 09, 18), 4, 20, 12, 980, 14),
            new Sale(19, new DateTime(2021, 09, 18), 2, 11, 10, 700, 14),
            new Sale(20, new DateTime(2021, 10, 07), 4, 1, 20, 560, 13)
        );
    }

    #endregion
}
