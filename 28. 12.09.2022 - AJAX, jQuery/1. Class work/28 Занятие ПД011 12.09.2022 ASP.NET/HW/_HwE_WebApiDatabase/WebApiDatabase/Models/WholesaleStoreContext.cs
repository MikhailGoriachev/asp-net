using Microsoft.EntityFrameworkCore;
using WebApiDatabase.Models;
using WebApiDatabase.Infrastructure;


namespace WebApiDatabase.Models;

// Контекст базы данных «Оптовый магазин. Учет продаж»
public class WholesaleStoreContext : DbContext
{
    public DbSet<Unit> Units { get; set; } = null!;
    public DbSet<Good> Goods { get; set; } = null!;
    public DbSet<Seller> Sellers { get; set; } = null!;
    public DbSet<Purchase> Purchases { get; set; } = null!;
    public DbSet<Sale> Sales { get; set; } = null!;


    public WholesaleStoreContext(DbContextOptions<WholesaleStoreContext> options) : base(options)
    {
        // гарантированное удаление БД, если она существует
        // Database.EnsureDeleted();

        // создание базы данных при её отсутствии
        Database.EnsureCreated();
    }

    // инициализация данных в таблицах
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DateTime GetDate() => Utils.RandomDate(new DateTime(2022, 1, 1), DateTime.Today);

        modelBuilder.Entity<Good>()
            .HasData(
            new Good { Id =  1, NameGood = "Печенье МИЛКА" },
            new Good { Id =  2, NameGood = "Сыр БЕЛЕБЕЕВСКИЙ 45%" },
            new Good { Id =  3, NameGood = "Чай АХМАД, Эрл Грей" },
            new Good { Id =  4, NameGood = "Салат Айсберг" },
            new Good { Id =  5, NameGood = "Кофе NESCAFE Классик растворимый" },
            new Good { Id =  6, NameGood = "Крабовые палочки ВИЧИ" },
            new Good { Id =  7, NameGood = "Томаты коктейльные" },
            new Good { Id =  8, NameGood = "Морковь по-корейски" },
            new Good { Id =  9, NameGood = "Огурцы сладкие" },
            new Good { Id = 10, NameGood = "Маслины БОНДЮЭЛЬ, без косточек " },
            new Good { Id = 11, NameGood = "НАГГЕТСЫ КУРИНЫЕ Классические(Мираторг)" },
            new Good { Id = 12, NameGood = "Сосиски ПАПА МОЖЕТ, Сочные(ОМПК)" },
            new Good { Id = 13, NameGood = "Напиток газированный 7 АП" },
            new Good { Id = 14, NameGood = "Напиток газированный СОСА-СOLA без сахара" },
            new Good { Id = 15, NameGood = "Кукуруза БОНДЮЭЛЬ" },
            new Good { Id = 16, NameGood = "Ассорти рыбное ЕВРОПРОМ рубленная горбуша" },
            new Good { Id = 17, NameGood = "Печенье ЮБИЛЕЙНОЕ, Традиционное" },
            new Good { Id = 18, NameGood = "Шоколад РОССИЯ, Кофе с молоком" },
            new Good { Id = 19, NameGood = "Чипсы РУССКАЯ КАРТОШКА, Сметана-укроп" }
        );

        modelBuilder.Entity<Unit>()
            .HasData(
            new Unit { Id =1,  Short = "шт", Long ="штук"},
            new Unit { Id =2,  Short = "пак", Long ="пакет"},
            new Unit { Id =3,  Short = "пл", Long ="палет"},
            new Unit { Id =4,  Short = "ящ", Long ="ящик"},
            new Unit { Id =5,  Short = "жб", Long ="жестяная банка"},
            new Unit { Id =6,  Short = "бут", Long ="бутылка"},
            new Unit { Id =7,  Short = "меш", Long ="мешок"},
            new Unit { Id =8,  Short = "бл", Long ="блок"},
            new Unit { Id =9,  Short = "кор", Long ="коробка"}
        );

        modelBuilder.Entity<Seller>()
             .HasData(
            new Seller { Id = 1,  Surname ="Гончарова",  NameSeller = "Ариана",     Patronymic = "Алексеевна", Interest = 3},
            new Seller { Id = 2,  Surname ="Серов",      NameSeller = "Платон",     Patronymic = "Львович",    Interest = 8},
            new Seller { Id = 3,  Surname ="Котов",      NameSeller = "Михаил",     Patronymic = "Павлович",   Interest = 5},
            new Seller { Id = 4,  Surname ="Грачева",    NameSeller = "Василиса",   Patronymic = "Алексеевна", Interest = 12},
            new Seller { Id = 5,  Surname ="Морозова",   NameSeller = "Таисия",     Patronymic = "Платоновна", Interest = 4},
            new Seller { Id = 6,  Surname ="Семенова",   NameSeller = "Дарина",     Patronymic = "Марковна",   Interest = 8},
            new Seller { Id = 7,  Surname ="Лавров",     NameSeller = "Роман",      Patronymic = "Иванов",     Interest = 9},
            new Seller { Id = 8,  Surname ="Смирнов",    NameSeller = "Константин", Patronymic = "Артёмович",  Interest = 8},
            new Seller { Id = 9,  Surname ="Грибова",    NameSeller = "Софья",      Patronymic = "Никитична",  Interest = 3},
            new Seller { Id = 10, Surname ="Николаева",  NameSeller = "Валерия",    Patronymic = "Максимовна", Interest = 4},
            new Seller { Id = 11, Surname ="Рыбаков",    NameSeller = "Даниил",     Patronymic = "Георгиевич", Interest = 8},
            new Seller { Id = 12, Surname ="Островский", NameSeller = "Александр",  Patronymic = "Михайлович", Interest = 12},
            new Seller { Id = 13, Surname ="Воронина",   NameSeller = "Василиса",   Patronymic = "Кирилловна", Interest = 4},
            new Seller { Id = 14, Surname ="Григорьев",  NameSeller = "Тихон",      Patronymic = "Кириллович", Interest = 7},
            new Seller { Id = 15, Surname = "Денисова",  NameSeller = "Александра", Patronymic = "Ивановна",   Interest = 3 }
        );

        modelBuilder.Entity<Purchase>()
             .HasData(
            new Purchase { Id = 1,  GoodId = 1,  UnitId = 4, Amount = 48, PricePurchase = 127, DatePurchase = GetDate() },
            new Purchase { Id = 2,  GoodId = 12, UnitId = 9, Amount = 16, PricePurchase = 512, DatePurchase = GetDate() },
            new Purchase { Id = 3,  GoodId = 19, UnitId = 3, Amount = 24, PricePurchase = 44,  DatePurchase = GetDate() },
            new Purchase { Id = 4,  GoodId = 2,  UnitId = 9, Amount = 12, PricePurchase = 145, DatePurchase = GetDate() },
            new Purchase { Id = 5,  GoodId = 14, UnitId = 1, Amount = 48, PricePurchase = 88,  DatePurchase = GetDate() },
            new Purchase { Id = 6,  GoodId = 1,  UnitId = 4, Amount = 48, PricePurchase = 127, DatePurchase = GetDate() },
            new Purchase { Id = 7,  GoodId = 5,  UnitId = 1, Amount = 10, PricePurchase = 194, DatePurchase = GetDate() },
            new Purchase { Id = 8,  GoodId = 19, UnitId = 3, Amount = 12, PricePurchase = 44,  DatePurchase = GetDate() },
            new Purchase { Id = 9,  GoodId = 12, UnitId = 9, Amount = 12, PricePurchase = 525, DatePurchase = GetDate() },
            new Purchase { Id = 10, GoodId = 8,  UnitId = 9, Amount = 54, PricePurchase = 75,  DatePurchase = GetDate() },
            new Purchase { Id = 11, GoodId = 14, UnitId = 1, Amount = 48, PricePurchase = 88,  DatePurchase = GetDate() },
            new Purchase { Id = 12, GoodId = 11, UnitId = 4, Amount = 12, PricePurchase = 150, DatePurchase = GetDate() },
            new Purchase { Id = 13, GoodId = 17, UnitId = 4, Amount = 32, PricePurchase = 61,  DatePurchase = GetDate() },
            new Purchase { Id = 14, GoodId = 2,  UnitId = 9, Amount = 10, PricePurchase = 145, DatePurchase = GetDate() },
            new Purchase { Id = 15, GoodId = 10, UnitId = 5, Amount = 24, PricePurchase = 109, DatePurchase = GetDate() },
            new Purchase { Id = 16, GoodId = 19, UnitId = 3, Amount = 32, PricePurchase = 44,  DatePurchase = GetDate() },
            new Purchase { Id = 17, GoodId = 18, UnitId = 3, Amount = 24, PricePurchase = 64,  DatePurchase = GetDate() },
            new Purchase { Id = 18, GoodId = 12, UnitId = 9, Amount = 8,  PricePurchase = 150, DatePurchase = GetDate() },
            new Purchase { Id = 19, GoodId = 8,  UnitId = 9, Amount = 40, PricePurchase = 75,  DatePurchase = GetDate() },
            new Purchase { Id = 20, GoodId = 5,  UnitId = 1, Amount = 8,  PricePurchase = 507, DatePurchase = GetDate() }
        );

        modelBuilder.Entity<Sale>()
            .HasData(
            new Sale { Id = 1, DateSale = GetDate(), SellerId = 6, PurchaseId = 5, AmountSale = 2, PriceSale = 110 },  //UnitId = 1 },
            new Sale { Id = 2, DateSale = GetDate(), SellerId = 2, PurchaseId = 17, AmountSale = 3, PriceSale = 80 },  //UnitId = 3 },
            new Sale { Id = 3, DateSale = GetDate(), SellerId = 4, PurchaseId = 7, AmountSale = 4, PriceSale = 620 },  //UnitId = 1 },
            new Sale { Id = 4, DateSale = GetDate(), SellerId = 1, PurchaseId = 9, AmountSale = 1, PriceSale = 188 },  //UnitId = 9 },
            new Sale { Id = 5, DateSale = GetDate(), SellerId = 3, PurchaseId = 4, AmountSale = 3, PriceSale = 181 },  //UnitId = 9 },
            new Sale { Id = 6, DateSale = GetDate(), SellerId = 6, PurchaseId = 3, AmountSale = 1, PriceSale = 55 },   //UnitId =  3},
            new Sale { Id = 7, DateSale = GetDate(), SellerId = 9, PurchaseId = 16, AmountSale = 1, PriceSale = 55 },  //UnitId = 3 },
            new Sale { Id = 8, DateSale = GetDate(), SellerId = 6, PurchaseId = 9, AmountSale = 4, PriceSale = 188 },  //UnitId = 9 },
            new Sale { Id = 9, DateSale = GetDate(), SellerId = 12, PurchaseId = 6, AmountSale = 2, PriceSale = 159 }, //UnitId = 1 },
            new Sale { Id = 10, DateSale = GetDate(), SellerId = 4, PurchaseId = 17, AmountSale = 1, PriceSale = 80 }, //UnitId = 3 },
            new Sale { Id = 11, DateSale = GetDate(), SellerId = 8, PurchaseId = 1, AmountSale = 2, PriceSale = 159 }, //UnitId = 4 },
            new Sale { Id = 12, DateSale = GetDate(), SellerId = 6, PurchaseId = 18, AmountSale = 2, PriceSale = 80 }, //UnitId = 9 },
            new Sale { Id = 13, DateSale = GetDate(), SellerId = 10, PurchaseId = 5, AmountSale = 4, PriceSale = 110 }, //UnitId = 1 },
            new Sale { Id = 14, DateSale = GetDate(), SellerId = 2, PurchaseId = 7, AmountSale = 4, PriceSale = 650 },  //UnitId = 1 },
            new Sale { Id = 15, DateSale = GetDate(), SellerId = 8, PurchaseId = 11, AmountSale = 2, PriceSale = 110 }, //UnitId = 1 },
            new Sale { Id = 16, DateSale = GetDate(), SellerId = 3, PurchaseId = 20, AmountSale = 3, PriceSale = 614 }, //UnitId = 1 },
            new Sale { Id = 17, DateSale = GetDate(), SellerId = 4, PurchaseId = 12, AmountSale = 5, PriceSale = 188 } //UnitId = 4 }
        );

    } // OnModelCreating

} // class WholesaleStoreContext
