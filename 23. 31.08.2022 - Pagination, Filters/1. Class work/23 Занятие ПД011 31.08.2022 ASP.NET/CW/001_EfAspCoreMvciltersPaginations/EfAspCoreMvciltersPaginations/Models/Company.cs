using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkInAspNetCoreMvcIntro.Models;

// компания, в которой работает пользователь
public class Company
{
    public int Id { get; set; }

    [Display(Name="Название компании")]
    public string? Name { get; set; }

    // связное свойство
    public List<User> Users { get; set; } = new();
}
