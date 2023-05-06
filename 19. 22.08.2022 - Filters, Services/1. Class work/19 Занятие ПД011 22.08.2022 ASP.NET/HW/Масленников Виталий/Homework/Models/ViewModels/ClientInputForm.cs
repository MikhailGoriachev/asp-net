using System.ComponentModel.DataAnnotations;
using Homework.Models.TravelCompany;

namespace Homework.Models.ViewModels;

public class ClientInputForm
{
    public Client Client { get; set; } = null!;
    
    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [StringLength(28, MinimumLength = 8, ErrorMessage = "Допустимая длина пароля от 8 до 28 символов")]
    [Display(Name = "Пароль")]
    public string? RawPassword { get; set; }

    [Required(ErrorMessage = "Введите подтверждение пароля")]
    [StringLength(28, MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Compare("RawPassword", ErrorMessage = "Пароли не совпадают")]
    [Display(Name = "Подтверждение пароля")]
    public string? ConfirmRawPassword { get; set; }
    
    [DataType(DataType.Upload)]
    public IFormFile? File { get; set; }

    public string? Photo { get; set; }

}