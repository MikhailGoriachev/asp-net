namespace EntityFrameworkInAspNetCoreMvcIntro.Models;

// пользователь
public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }


    // связанное свойство
    public int? CompanyId { get; set; }
    public Company? Company { get; set; }
} // class User


