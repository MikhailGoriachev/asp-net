namespace UsingTagHelpers.Models;

/// <summary>
/// Продавцы	
/// *Фамилия продавца, оформившего продажу
/// *Имя продавца, оформившего продажу
/// *Отчество продавца, оформившего продажу
/// *Процент комиссионных продавца, оформившего продажу
/// </summary>
public class Seller
{
    public int Id { get; set; }

    // Имя продавца
    public string? NameSeller { get; set; }

    // Фамилия продавца
    public string? Surname { get; set; }
    // Отчество продавца
    public string? Patronymic { get; set; }
    // Процент комиссионных продавца
    public int Interest { get; set; }

    // связное свойство
    public List<Sale> Sales { get; set; } = new();

}
