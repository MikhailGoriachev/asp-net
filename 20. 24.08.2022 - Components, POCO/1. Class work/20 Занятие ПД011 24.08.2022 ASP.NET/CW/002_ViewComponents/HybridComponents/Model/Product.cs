namespace HybridComponents.Model;

// Товар для магазина
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public Product() {
        Id = 1;
        Name = "Бодиан Дианы";
        Price = 2_300;
    } // Product

    public override string ToString() => $"{Id} {Name} {Price:f2}";
}
