using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models.ViewModels.Clients;

public class SelectByRangeYearsOldViewModel
{
    // клиенты
    public IEnumerable<Client> Clients { get; set; } = new List<Client>();

    // минимальный возраст
    [Required(ErrorMessage = "Введите минимальный возраст")]
    [Range(18, 65, ErrorMessage = "Минимальный возраст должен быть в диапазоне от 18 до 65 лет")]
    public int MinYears { get; set; }

    // максимальный возраст
    [Required(ErrorMessage = "Введите максимальный возраст")]
    [Range(18, 65, ErrorMessage = "Максимальный возраст должен быть в диапазоне от 18 до 65 лет")]
    public int MaxYears { get; set; }

    #region Конструкторы

    // конструктор по умолчанию
    public SelectByRangeYearsOldViewModel()
    {

    }


    // конструктор инициализирующий
    public SelectByRangeYearsOldViewModel(IEnumerable<Client> clients, int minYears = 0, int maxYears = 0)
    {
        Clients = clients;

        if (minYears > maxYears)
            throw new Exception("SelectByRangeYearsOldViewModel: максимальное значение не может быть меньше минимального!");

        MinYears = minYears;
        MaxYears = maxYears;
    }

    #endregion
}
