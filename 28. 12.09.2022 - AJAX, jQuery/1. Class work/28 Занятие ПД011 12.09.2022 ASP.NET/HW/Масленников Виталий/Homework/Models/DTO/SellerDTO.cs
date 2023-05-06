namespace Homework.Models.DTO;

// Модель для передачи данных о продавце (Data Transfer Object)
public class SellerDTO
{
    public int Id { get; set; }

    public string Surname { get; set; } = "н/д";

    public string Name { get; set; } = "н/д";

    public string Patronymic { get; set; } = "н/д";

    public string Passport { get; set; } = "н/д";

    public double Interest { get; set; }

    public SellerDTO(Seller seller)
    {
        Id = seller.Id;
        Surname = seller.Surname;
        Name = seller.Name;
        Patronymic = seller.Patronymic;
        Passport = seller.Passport;
        Interest = seller.Interest;
    }
}