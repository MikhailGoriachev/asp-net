using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models.ViewModels.Routes;

public class SelectByRangeLengthViewModel
{
    // маршруты
    public IEnumerable<RouteViewModel> Routes { get; set; } = new List<RouteViewModel>();

    // минимальная продолжительность маршрута
    [Required(ErrorMessage = "Введите минимальную продолжительность маршрута!")]
    [Range(1, int.MaxValue, ErrorMessage = "Значение минимальной продолжительности маршрута должно быть больше 0!")]
    public int MinLength { get; set; }

    // максимальная продолжительность маршрута
    [Required(ErrorMessage = "Введите максимальную продолжительность маршрута!")]
    [Range(1, int.MaxValue, ErrorMessage = "Значение максмальной продолжительности маршрута должно быть больше 0!")]
    public int MaxLength { get; set; }

    #region Конструкторы

    // конструктор по умолчанию
    public SelectByRangeLengthViewModel()
    {

    }


    // конструктор инициализирующий
    public SelectByRangeLengthViewModel(IEnumerable<RouteViewModel> routes, int minLength = 0, int maxLength = 0)
    {
        Routes = routes;

        if (minLength > maxLength)
            throw new Exception("SelectByRangeLengthViewModel: максимальное значение не может быть меньше минимального!");

        MinLength = minLength;
        MaxLength = maxLength;
    }

    #endregion
}
