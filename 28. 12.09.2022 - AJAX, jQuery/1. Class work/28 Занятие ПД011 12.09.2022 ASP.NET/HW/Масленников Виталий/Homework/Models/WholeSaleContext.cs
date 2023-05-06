using Microsoft.EntityFrameworkCore;
namespace Homework.Models;

public class WholeSaleContext : DbContext
{
    public DbSet<Unit>     Units     { get; set; } = null!;
    public DbSet<Goods>    Goods     { get; set; } = null!;
    public DbSet<Seller>   Sellers    { get; set; } = null!;
    public DbSet<Purchase> Purchases { get; set; } = null!;
    public DbSet<Sale>     Sales     { get; set; } = null!;

    public WholeSaleContext(DbContextOptions<WholeSaleContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var units = new List<Unit>
        {
            new() { Id = 1, Short = "шт", Long = "штук" },
            new() { Id = 2, Short = "пак", Long = "пакет" },
            new() { Id = 3, Short = "пал", Long = "палета" },
            new() { Id = 4, Short = "ящ", Long = "ящик" },
            new() { Id = 5, Short = "жб", Long = "жестяная банка" },
            new() { Id = 6, Short = "бут", Long = "бутылка" },
            new() { Id = 7, Short = "меш", Long = "мешок" },
            new() { Id = 8, Short = "бл", Long = "блок" },
            new() { Id = 9, Short = "кор", Long = "коробка" },
            new() { Id = 10, Short = "конт", Long = "контейнер" }
        };
        modelBuilder.Entity<Unit>().HasData(units);

        var goods = new List<Goods>
        {
            new() { Id = 1, Name = "Чехол защитный" },
            new() { Id = 2, Name = "Пиджак замшевый" },
            new() { Id = 3, Name = "Кинокамера импортная" },
            new() { Id = 4, Name = "Ручка гелевая" },
            new() { Id = 5, Name = "Маркер спиртовой" },
            new() { Id = 6, Name = "Блюдце фаянсовое" },
            new() { Id = 7, Name = "Подставка для горячего" },
            new() { Id = 8, Name = "Ботинки защитные" },
            new() { Id = 9, Name = "Зонтик автоматический" },
            new() { Id = 10, Name = "Галоши резиновые" },
            new() { Id = 11, Name = "Кружка алюминиевая" },
            new() { Id = 12, Name = "Рюкзак для ноутбука" },
            new() { Id = 13, Name = "Велосипед горный" },
            new() { Id = 14, Name = "Палатка туристическая" },
            new() { Id = 15, Name = "Лодка надувная" },
            new() { Id = 16, Name = "Ложка чайная" },
            new() { Id = 17, Name = "Шланг гофрированный" },
            new() { Id = 18, Name = "Шланг армированный" },
            new() { Id = 19, Name = "Провод черный" },
            new() { Id = 20, Name = "Реле прерыватель" },
            new() { Id = 21, Name = "Автомат защиты" }
        };
        modelBuilder.Entity<Goods>().HasData(goods);
        
        var sellers = new List<Seller>
        {
            new (){ Id =  1, Surname = "Юрковский",   Name = "Марк",      Patronymic = "Максимилианович", Passport = "1221345671", Interest =  3.5},  
			new (){ Id =  2, Surname = "Якубовская",  Name = "Диана",     Patronymic = "Павловна",	      Passport = "1221123452", Interest =  5.5},  
			new (){ Id =  3, Surname = "Шапиро",      Name = "Федор",     Patronymic = "Федорович",       Passport = "1221456123", Interest =  5.5},  
			new (){ Id =  4, Surname = "Вожжаев",     Name = "Сергей",    Patronymic = "Денисович",       Passport = "1221123122", Interest =  6.5},  
			new (){ Id =  5, Surname = "Хроменко",    Name = "Игорь",     Patronymic = "Владимирович",    Passport = "1221342121", Interest =  8.5},  
			new (){ Id =  6, Surname = "Пелых",       Name = "Марина",    Patronymic = "Ульяновна",       Passport = "1121121212", Interest = 10.5},  
			new (){ Id =  7, Surname = "Лапотникова", Name = "Тамара",    Patronymic = "Оскаровна",       Passport = "1121098181", Interest =  8.0},  
			new (){ Id =  8, Surname = "Огородников", Name = "Сергей",    Patronymic = "Иванович",        Passport = "1221091911", Interest =  5.0},  
			new (){ Id =  9, Surname = "Яйло",        Name = "Екатерина", Patronymic = "Николаевна",      Passport = "1221345675", Interest =  8.0},  
			new (){ Id = 10, Surname = "Лосева",      Name = "Инна",      Patronymic = "Степановна",      Passport = "1221187651", Interest =  8.0},  
			new (){ Id = 11, Surname = "Михайлович",  Name = "Анна",      Patronymic = "Валентиновна",    Passport = "0920000122", Interest =  3.5},  
			new (){ Id = 12, Surname = "Тарапата",    Name = "Михаил",    Patronymic = "Исаакович",       Passport = "0920001981", Interest =  3.0},  
			new (){ Id = 13, Surname = "Трубихин",    Name = "Эдуард",    Patronymic = "Михайлович",      Passport = "0921121921", Interest =  4.0},  
			new (){ Id = 14, Surname = "Чмыхало",     Name = "Олег",      Patronymic = "Тарасович",       Passport = "1220021121", Interest = 10.0},  
			new (){ Id = 15, Surname = "Князьков",    Name = "Степан",    Patronymic = "Сидорович",       Passport = "0919002165", Interest =  5.5},  
			new (){ Id = 16, Surname = "Потемкина",   Name = "Наталья",   Patronymic = "Павловна",        Passport = "0919002213", Interest = 10.0},  
			new (){ Id = 17, Surname = "Гритченко",   Name = "Степан",    Patronymic = "Романович",       Passport = "0919002129", Interest =  8.0},  
			new (){ Id = 18, Surname = "Селиванов",   Name = "Александр", Patronymic = "Михайлович",      Passport = "1118000503", Interest =  5.0},  
			new (){ Id = 19, Surname = "Царькова",    Name = "Лариса",    Patronymic = "Ильинична",       Passport = "1118000523", Interest =  4.5},
			new (){ Id = 20, Surname = "Яструб",    Name = "Владимир",    Patronymic = "Данилович",       Passport = "1421091811", Interest = 11.0},
        };
        modelBuilder.Entity<Seller>().HasData(sellers);

        var purchases = new List<Purchase>
        {
			new(){ Id = 1,  GoodsId = 1,  UnitId =  1, PurchaseDate = new DateTime(2022, 8, 1),  Price =    1700, Amount =   5}, 
			new(){ Id = 2,  GoodsId = 1,  UnitId =  1, PurchaseDate = new DateTime(2022, 8, 4),  Price =    1850, Amount =   2}, 
			new(){ Id = 3,  GoodsId = 1,  UnitId =  1, PurchaseDate = new DateTime(2022, 8, 5),  Price =    1600, Amount =  12}, 
			new(){ Id = 4,  GoodsId = 3,  UnitId =  1, PurchaseDate = new DateTime(2022, 8, 8),  Price =    8000, Amount =   6}, 
			new(){ Id = 5,  GoodsId = 4,  UnitId =  9, PurchaseDate = new DateTime(2022, 8, 11), Price =    2000, Amount =   3}, 
			new(){ Id = 6,  GoodsId = 5,  UnitId =  9, PurchaseDate = new DateTime(2022, 8, 13), Price =    2000, Amount =   3}, 
			new(){ Id = 7,  GoodsId = 6,  UnitId =  1, PurchaseDate = new DateTime(2022, 8, 16), Price =     400, Amount =  10}, 
			new(){ Id = 8,  GoodsId = 7,  UnitId =  1, PurchaseDate = new DateTime(2022, 8, 17), Price =    7900, Amount =  10}, 
			new(){ Id = 9,  GoodsId = 4,  UnitId =  9, PurchaseDate = new DateTime(2022, 8, 19), Price =    3000, Amount =   3}, 
			new(){ Id = 10, GoodsId = 5,  UnitId =  1, PurchaseDate = new DateTime(2022, 8, 21), Price =     200, Amount =  30}, 
			new(){ Id = 11, GoodsId = 3,  UnitId =  1, PurchaseDate = new DateTime(2022, 8, 22), Price =    8300, Amount =   5}, 
			new(){ Id = 12, GoodsId = 7,  UnitId =  4, PurchaseDate = new DateTime(2022, 8, 23), Price =   36000, Amount =   3}, 
			new(){ Id = 13, GoodsId = 6,  UnitId =  1, PurchaseDate = new DateTime(2022, 8, 24), Price =     380, Amount =  60}, 
			new(){ Id = 14, GoodsId = 7,  UnitId =  4, PurchaseDate = new DateTime(2022, 8, 24), Price =   22000, Amount =   2}, 
			new(){ Id = 15, GoodsId = 3,  UnitId =  9, PurchaseDate = new DateTime(2022, 8, 25), Price =   80000, Amount =   2}, 
			new(){ Id = 16, GoodsId = 4,  UnitId =  1, PurchaseDate = new DateTime(2022, 8, 25), Price =      30, Amount = 200}, 
			new(){ Id = 17, GoodsId = 7,  UnitId =  4, PurchaseDate = new DateTime(2022, 8, 25), Price =   12000, Amount =   3}, 
			new(){ Id = 18, GoodsId = 1,  UnitId =  1, PurchaseDate = new DateTime(2022, 8, 26), Price =    1300, Amount = 300}, 
			new(){ Id = 19, GoodsId = 2,  UnitId = 10, PurchaseDate = new DateTime(2022, 8, 26), Price = 1000000, Amount =   1}, 
			new(){ Id = 20, GoodsId = 11, UnitId =  1, PurchaseDate = new DateTime(2022, 8, 27), Price =     260, Amount =  20}, 
			new(){ Id = 21, GoodsId = 12, UnitId =  7, PurchaseDate = new DateTime(2022, 9, 1) , Price =   60000, Amount =   3}, 
			new(){ Id = 22, GoodsId = 11, UnitId =  1, PurchaseDate = new DateTime(2022, 9, 2) , Price =     250, Amount =  30}, 
			new(){ Id = 23, GoodsId = 12, UnitId =  1, PurchaseDate = new DateTime(2022, 9, 3) , Price =    5200, Amount =  10} 
        };
        modelBuilder.Entity<Purchase>().HasData(purchases);

        var sales = new List<Sale>
        {
			new(){ Id =  1, PurchaseId =  1, UnitId =  1, SellerId = 1, SaleDate = new DateTime(2022,5,02), Price =    1900, Amount =   2},  
			new(){ Id =  2, PurchaseId =  1, UnitId =  1, SellerId = 2, SaleDate = new DateTime(2022,5,02), Price =    1950, Amount =   3},  
			new(){ Id =  3, PurchaseId =  2, UnitId =  1, SellerId = 5, SaleDate = new DateTime(2022,5,02), Price =    1960, Amount =   2},  
			new(){ Id =  4, PurchaseId =  3, UnitId =  1, SellerId = 3, SaleDate = new DateTime(2022,5,03), Price =    1900, Amount =   6},  
			new(){ Id =  5, PurchaseId =  3, UnitId =  1, SellerId = 2, SaleDate = new DateTime(2022,5,03), Price =    1950, Amount =   5},  
			new(){ Id =  6, PurchaseId =  4, UnitId =  1, SellerId = 3, SaleDate = new DateTime(2022,5,04), Price =    9000, Amount =   3},  
			new(){ Id =  7, PurchaseId =  4, UnitId =  1, SellerId = 8, SaleDate = new DateTime(2022,5,04), Price =    9300, Amount =   2},  
			new(){ Id =  8, PurchaseId =  4, UnitId =  1, SellerId = 2, SaleDate = new DateTime(2022,5,04), Price =    9200, Amount =   1},  
			new(){ Id =  9, PurchaseId =  5, UnitId =  9, SellerId = 3, SaleDate = new DateTime(2022,5,05), Price =    2500, Amount =   2},  
			new(){ Id = 10, PurchaseId =  5, UnitId =  9, SellerId = 9, SaleDate = new DateTime(2022,5,05), Price =    2600, Amount =   1},  
			new(){ Id = 11, PurchaseId =  6, UnitId =  9, SellerId = 4, SaleDate = new DateTime(2022,5,05), Price =    2300, Amount =   1},  
			new(){ Id = 12, PurchaseId =  7, UnitId =  1, SellerId = 5, SaleDate = new DateTime(2022,5,05), Price =     500, Amount =   1},  
			new(){ Id = 13, PurchaseId =  8, UnitId =  1, SellerId = 9, SaleDate = new DateTime(2022,5,07), Price =    8300, Amount =   6},  
			new(){ Id = 14, PurchaseId =  6, UnitId =  9, SellerId = 3, SaleDate = new DateTime(2022,5,07), Price =    2500, Amount =   2},  
			new(){ Id = 15, PurchaseId =  7, UnitId =  1, SellerId = 6, SaleDate = new DateTime(2022,5,07), Price =     500, Amount =   8},  
			new(){ Id = 16, PurchaseId =  9, UnitId =  9, SellerId = 5, SaleDate = new DateTime(2022,5,09), Price =    4300, Amount =   2},  
			new(){ Id = 17, PurchaseId =  8, UnitId =  1, SellerId = 10, SaleDate = new DateTime(2022,5,09), Price =    8300, Amount =   4},  
			new(){ Id = 18, PurchaseId = 11, UnitId =  1, SellerId = 12, SaleDate = new DateTime(2022,5,09), Price =    9000, Amount =   3},  
			new(){ Id = 19, PurchaseId = 10, UnitId =  1, SellerId = 3, SaleDate = new DateTime(2022,5,10), Price =     500, Amount =  20},  
			new(){ Id = 20, PurchaseId = 11, UnitId =  1, SellerId = 4, SaleDate = new DateTime(2022,5,10), Price =    9000, Amount =   1},  
			new(){ Id = 21, PurchaseId = 10, UnitId =  1, SellerId = 13, SaleDate = new DateTime(2022,5,11), Price =     450, Amount =   8},  
			new(){ Id = 22, PurchaseId = 11, UnitId =  1, SellerId = 2, SaleDate = new DateTime(2022,5,12), Price =    9000, Amount =   1},  
			new(){ Id = 23, PurchaseId = 10, UnitId =  1, SellerId = 1, SaleDate = new DateTime(2022,5,13), Price =     510, Amount =   3},  
			new(){ Id = 24, PurchaseId = 10, UnitId =  1, SellerId = 4, SaleDate = new DateTime(2022,5,15), Price =     560, Amount =   1},  
			new(){ Id = 25, PurchaseId = 10, UnitId =  1, SellerId = 16, SaleDate = new DateTime(2022,5,16), Price =     480, Amount =   6},  
			new(){ Id = 26, PurchaseId = 15, UnitId =  9, SellerId = 5, SaleDate = new DateTime(2022,5,17), Price =  100000, Amount =   1},  
			new(){ Id = 27, PurchaseId = 12, UnitId =  4, SellerId = 3, SaleDate = new DateTime(2022,5,17), Price =   40000, Amount =   1},  
			new(){ Id = 28, PurchaseId = 13, UnitId =  1, SellerId = 16, SaleDate = new DateTime(2022,5,17), Price =     600, Amount =  20},  
			new(){ Id = 29, PurchaseId = 12, UnitId =  4, SellerId = 5, SaleDate = new DateTime(2022,5,18), Price =   43000, Amount =   1},  
			new(){ Id = 30, PurchaseId = 15, UnitId =  9, SellerId = 11, SaleDate = new DateTime(2022,5,18), Price =  110000, Amount =   1},  
			new(){ Id = 41, PurchaseId = 13, UnitId =  1, SellerId = 2, SaleDate = new DateTime(2022,5,20), Price =     580, Amount =  30},  
			new(){ Id = 42, PurchaseId = 14, UnitId =  4, SellerId = 3, SaleDate = new DateTime(2022,5,20), Price =   25500, Amount =   1},  
			new(){ Id = 43, PurchaseId = 14, UnitId =  4, SellerId = 11, SaleDate = new DateTime(2022,5,21), Price =   27000, Amount =   1},  
			new(){ Id = 44, PurchaseId = 12, UnitId =  4, SellerId = 3, SaleDate = new DateTime(2022,5,23), Price =   45000, Amount =   1},  
			new(){ Id = 45, PurchaseId = 13, UnitId =  1, SellerId = 12, SaleDate = new DateTime(2022,5,25), Price =     650, Amount =  10},  
			new(){ Id = 46, PurchaseId = 18, UnitId =  1, SellerId = 12, SaleDate = new DateTime(2022,5,27), Price =     500, Amount = 200},  
			new(){ Id = 47, PurchaseId = 19, UnitId = 10, SellerId = 15, SaleDate = new DateTime(2022,5,27), Price = 1600000, Amount =   1},  
			new(){ Id = 48, PurchaseId = 16, UnitId =  1, SellerId = 14, SaleDate = new DateTime(2022,5,27), Price =      60, Amount = 100},  
			new(){ Id = 49, PurchaseId = 17, UnitId =  4, SellerId = 2, SaleDate = new DateTime(2022,5,28), Price =   16000, Amount =   1},  
			new(){ Id = 40, PurchaseId = 18, UnitId =  1, SellerId = 5, SaleDate = new DateTime(2022,5,29), Price =     660, Amount = 100},  
			new(){ Id = 51, PurchaseId = 16, UnitId =  1, SellerId = 14, SaleDate = new DateTime(2022,5,30), Price =      60, Amount = 100},  
			new(){ Id = 52, PurchaseId = 17, UnitId =  4, SellerId = 13, SaleDate = new DateTime(2022,5,30), Price =   16000, Amount =   1},  
			new(){ Id = 53, PurchaseId = 17, UnitId =  4, SellerId = 2, SaleDate = new DateTime(2022,5,30), Price =   16800, Amount =   1},  
			new(){ Id = 54, PurchaseId = 18, UnitId =  1, SellerId = 16, SaleDate = new DateTime(2022,5,31), Price =     580, Amount = 200},  
			new(){ Id = 55, PurchaseId = 18, UnitId =  1, SellerId = 1, SaleDate = new DateTime(2022,5,31), Price =     680, Amount = 100},  
			new(){ Id = 56, PurchaseId = 21, UnitId =  7, SellerId = 16, SaleDate = new DateTime(2022,6,02), Price =   80000, Amount =   1},  
			new(){ Id = 57, PurchaseId = 20, UnitId =  1, SellerId = 5, SaleDate = new DateTime(2022,6,03), Price =     380, Amount =   2},  
			new(){ Id = 58, PurchaseId = 21, UnitId =  7, SellerId = 14, SaleDate = new DateTime(2022,6,04), Price =   82000, Amount =   1},  
			new(){ Id = 59, PurchaseId = 20, UnitId =  1, SellerId = 3, SaleDate = new DateTime(2022,6,05), Price =     360, Amount =   12}, 
			new(){ Id = 60, PurchaseId = 21, UnitId =  7, SellerId = 4, SaleDate = new DateTime(2022,6,06), Price =   80000, Amount =    1}, 
			new(){ Id = 61, PurchaseId = 20, UnitId =  1, SellerId = 3, SaleDate = new DateTime(2022,6,07), Price =     380, Amount =    6}, 
			new(){ Id = 62, PurchaseId = 23, UnitId =  1, SellerId = 18, SaleDate = new DateTime(2022,6,07), Price =    6600, Amount =    3}, 
			new(){ Id = 63, PurchaseId = 22, UnitId =  1, SellerId = 4, SaleDate = new DateTime(2022,6,08), Price =     650, Amount =   10}, 
			new(){ Id = 64, PurchaseId = 23, UnitId =  1, SellerId = 15, SaleDate = new DateTime(2022,6,08), Price =    6800, Amount =    4}, 
			new(){ Id = 65, PurchaseId = 22, UnitId =  1, SellerId = 5, SaleDate = new DateTime(2022,6,08), Price =     600, Amount =   15}, 
			new(){ Id = 66, PurchaseId = 22, UnitId =  1, SellerId = 17, SaleDate = new DateTime(2022,6,09), Price =     650, Amount =    5}, 
			new(){ Id = 67, PurchaseId = 23, UnitId =  1, SellerId = 20, SaleDate = new DateTime(2022,6,09), Price =    6600, Amount =    3} 
        };
        modelBuilder.Entity<Sale>().HasData(sales);
    }
}