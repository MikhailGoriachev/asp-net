using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models.ViewModels.Clients;

public class SelectBySurnameViewModel
{
    // клиенты
    public IEnumerable<Client> Clients { get; set; } = new List<Client>();

    // фамилия
    [Display(Name = "Фамилия:")]
    [Required(ErrorMessage = "Укажите фамилию")]
    public string Surname { get; set; } = "";

    #region Конструкторы

    // конструктор по умолчанию
    public SelectBySurnameViewModel()
    {

    }


    // конструктор инициализирующий
    public SelectBySurnameViewModel(IEnumerable<Client> clients, string surname = "")
    {
        Clients = clients;
        Surname = surname;
    }

    #endregion
}
