using System.ComponentModel.DataAnnotations;

namespace Homework.Models.TravelCompany;

// Клиент туристической фирмы
public class Client
{
    // Идентификатор
    public int Id { get; set; }
    
    // Фамилия
    [Display(Name = "Фамилия:")]
    [Required(ErrorMessage = "Введите фамилию")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    public string Surname { get; set; } = null!;
    
    // Имя 
    [Display(Name = "Имя:")]
    [Required(ErrorMessage = "Введите имя")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    public string Name { get; set; } = null!;
    
    // Отчество
    [Display(Name = "Отчество:")]
    [Required(ErrorMessage = "Введите отчество")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    public string Patronymic { get; set; } = null!;
    
    // Возраст клиента в годах
    [Display(Name = "Возраст:")]
    [Required(ErrorMessage = "Введите возраст")]
    [Range(18,65, ErrorMessage = "Введите значение от 18 до 65")] 
    public int Age { get; set; }   
    
    // Номер телефона
    [DataType(DataType.PhoneNumber)]
    [Required(ErrorMessage = "Введите номер телефона")]
    [Phone(ErrorMessage = "Не является корректным телефонным номером")]
    [Display(Name = "Номер телефона")]
    public string PhoneNumber { get; set; } = null!;

    // Адрес электронной почты
    [Required(ErrorMessage = "Введите email")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string Email { get; set; } = null!;
    
    // Пароль
    public string? Password { get; set; }

    // Признак постоянного клиента
    public bool IsRegular { get; set; }
    
    // Название файла фотографии пользователя
    public string? Photo { get; set; }
    
    
    public string ShortName => $"{Surname} {Name.First()}.{Patronymic.First()}.";
}