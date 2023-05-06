namespace ComponentContext.Model;

// репозиторий товаров, регистрируется как сервис-singleton
// внедрения зависимости DI  
public class ProductRepository
{
    public List<Product> Products { get; set; }

    public ProductRepository() {
        Products = new List<Product> {
            new Product { Id = 1, Name ="Ручка гелевая",         Price =  60.00  },
            new Product { Id = 2, Name ="Галоши резиновые",      Price = 150.25  },
            new Product { Id = 3, Name ="Пиджак замшевый",       Price = 110.10  },
            new Product { Id = 4, Name ="Кинокамера импортная",  Price = 130.10  },
            new Product { Id = 5, Name ="Зонтик автоматический", Price = 170.50  }
        };
    }
}
