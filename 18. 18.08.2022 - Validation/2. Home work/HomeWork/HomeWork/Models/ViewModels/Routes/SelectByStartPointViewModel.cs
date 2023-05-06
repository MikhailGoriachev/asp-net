using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models.ViewModels.Routes;

public class SelectByStartPointViewModel
{
    // маршруты
    public IEnumerable<RouteViewModel> Routes { get; set; } = null!;

    // начальный пункт
    [Required]
    [Display(Name = "Начальный пункт:")]
    public string StartPoint { get; set; } = "";

    #region Конструкторы

    // конструктор по умолчанию
    public SelectByStartPointViewModel()
    {

    }

    // конструктор инициализирующий
    public SelectByStartPointViewModel(IEnumerable<RouteViewModel> routes, string startPoint)
    {
        Routes = routes;
        StartPoint = startPoint;
    }

    #endregion
}
