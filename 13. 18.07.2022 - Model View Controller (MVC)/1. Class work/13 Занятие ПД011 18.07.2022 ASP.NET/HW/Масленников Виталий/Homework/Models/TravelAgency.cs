using Homework.Common;
using Microsoft.EntityFrameworkCore;

namespace Homework.Models;

public class TravelAgencyContext : DbContext
{
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Purpose> Purposes { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Route> Routes { get; set; } = null!;
    public DbSet<Travel> Travels { get; set; } = null!;

    public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
	    var clients = new List<Client>
	    {
            new (){ Id =  1, Surname = "Юрковский",   Name = "Марк",      Patronymic = "Максимилианович", Passport = "1221345671" },  
			new (){ Id =  2, Surname = "Якубовская",  Name = "Диана",     Patronymic = "Павловна",	      Passport = "1221123452" },  
			new (){ Id =  3, Surname = "Шапиро",      Name = "Федор",     Patronymic = "Федорович",       Passport = "1221456123" },  
			new (){ Id =  4, Surname = "Вожжаев",     Name = "Сергей",    Patronymic = "Денисович",       Passport = "1221123122" },  
			new (){ Id =  5, Surname = "Хроменко",    Name = "Игорь",     Patronymic = "Владимирович",    Passport = "1221342121" },  
			new (){ Id =  6, Surname = "Пелых",       Name = "Марина",    Patronymic = "Ульяновна",       Passport = "1121121212" },  
			new (){ Id =  7, Surname = "Лапотникова", Name = "Тамара",    Patronymic = "Оскаровна",       Passport = "1121098181" },  
			new (){ Id =  8, Surname = "Огородников", Name = "Сергей",    Patronymic = "Иванович",        Passport = "1221091911" },  
			new (){ Id =  9, Surname = "Яйло",        Name = "Екатерина", Patronymic = "Николаевна",      Passport = "1221345675" },  
			new (){ Id = 10, Surname = "Лосева",      Name = "Инна",      Patronymic = "Степановна",      Passport = "1221187651" },  
			new (){ Id = 11, Surname = "Михайлович",  Name = "Анна",      Patronymic = "Валентиновна",    Passport = "0920000122" },  
			new (){ Id = 12, Surname = "Тарапата",    Name = "Михаил",    Patronymic = "Исаакович",       Passport = "0920001981" },  
			new (){ Id = 13, Surname = "Трубихин",    Name = "Эдуард",    Patronymic = "Михайлович",      Passport = "0921121921" },  
			new (){ Id = 14, Surname = "Чмыхало",     Name = "Олег",      Patronymic = "Тарасович",       Passport = "1220021121" },  
			new (){ Id = 15, Surname = "Князьков",    Name = "Степан",    Patronymic = "Сидорович",       Passport = "0919002165" },  
			new (){ Id = 16, Surname = "Потемкина",   Name = "Наталья",   Patronymic = "Павловна",        Passport = "0919002213" },  
			new (){ Id = 17, Surname = "Гритченко",   Name = "Степан",    Patronymic = "Романович",       Passport = "0919002129" },  
			new (){ Id = 18, Surname = "Селиванов",   Name = "Александр", Patronymic = "Михайлович",      Passport = "1118000503" },  
			new (){ Id = 19, Surname = "Царькова",    Name = "Лариса",    Patronymic = "Ильинична",       Passport = "1118000523" }
	    };

        var purposes = new List<Purpose>
        {
	        new (){ Id = 1, Name = "Туризм" },
	        new (){ Id = 2, Name = "Гости" },
	        new (){ Id = 3, Name = "Спорт" },
	        new (){ Id = 4, Name = "Бизнес" },
	        new (){ Id = 5, Name = "Культура" },
	        new (){ Id = 6, Name = "Работа" },
	        new (){ Id = 7, Name = "Учеба" },
	        new (){ Id = 8, Name = "Лечение" }
        };

        var countries = new List<Country>
        {
	        new (){Id = 1, Name = "Австрия", DailyCost = Random.Shared.Next(10, 100) * 10, TransferCost = Random.Shared.Next(10, 50) * 100, VisaCost = Random.Shared.Next(10, 50) * 100 },
	        new (){Id = 2, Name = "Бельгия", DailyCost = Random.Shared.Next(10, 100) * 10, TransferCost = Random.Shared.Next(10, 50) * 100, VisaCost = Random.Shared.Next(10, 50) * 100 },
	        new (){Id = 3, Name = "Германия", DailyCost = Random.Shared.Next(10, 100) * 10, TransferCost = Random.Shared.Next(10, 50) * 100, VisaCost = Random.Shared.Next(10, 50) * 100 },
	        new (){Id = 4, Name = "Греция", DailyCost = Random.Shared.Next(10, 100) * 10, TransferCost = Random.Shared.Next(10, 50) * 100, VisaCost = Random.Shared.Next(10, 50) * 100 },
	        new (){Id = 5, Name = "Дания", DailyCost = Random.Shared.Next(10, 100) * 10, TransferCost = Random.Shared.Next(10, 50) * 100, VisaCost = Random.Shared.Next(10, 50) * 100 },
	        new (){Id = 6, Name = "Исландия", DailyCost = Random.Shared.Next(10, 100) * 10, TransferCost = Random.Shared.Next(10, 50) * 100, VisaCost = Random.Shared.Next(10, 50) * 100 },
	        new (){Id = 7, Name = "Испания", DailyCost = Random.Shared.Next(10, 100) * 10, TransferCost = Random.Shared.Next(10, 50) * 100, VisaCost = Random.Shared.Next(10, 50) * 100 },
	        new (){Id = 8, Name = "Италия", DailyCost = Random.Shared.Next(10, 100) * 10, TransferCost = Random.Shared.Next(10, 50) * 100, VisaCost = Random.Shared.Next(10, 50) * 100 },
	        new (){Id = 9, Name = "Латвия", DailyCost = Random.Shared.Next(10, 100) * 10, TransferCost = Random.Shared.Next(10, 50) * 100, VisaCost = Random.Shared.Next(10, 50) * 100 },
	        new (){Id = 10, Name = "Италия", DailyCost = Random.Shared.Next(10, 100) * 10, TransferCost = Random.Shared.Next(10, 50) * 100, VisaCost = Random.Shared.Next(10, 50) * 100 }
        };

        var routes = new List<Route>();
        int id = 1;
        countries.ForEach(country =>
	        purposes.ForEach(purpose => 
		        routes.Add(
			        new Route{Id = id++, CountryId = country.Id, PurposeId = purpose.Id, DailyCost = Random.Shared.Next(10,50) * 10 }
			        )));
        
        var travels = new List<Travel>();
        id = 1;
		clients.ForEach(cl =>
		{
			travels.Add(new Travel
			{
				Id = id++, ClientId = cl.Id, RouteId = routes.Select(r => r.Id).Random(), 
				StartDate = Utils.RandomDate(new DateTime(2022, 6, 1), new DateTime(2022, 8, 1)),
				Duration = Random.Shared.Next(7,31)
			});	
		});
		
		modelBuilder.Entity<Client>().HasData(clients);
		modelBuilder.Entity<Purpose>().HasData(purposes);
		modelBuilder.Entity<Country>().HasData(countries);
		modelBuilder.Entity<Route>().HasData(routes);
		modelBuilder.Entity<Travel>().HasData(travels);
    }
}