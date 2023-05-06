using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models.ViewModels.Routes;

public class SelectByMiddlePointViewModel
{
    // маршруты
    public IEnumerable<RouteViewModel> Routes { get; set; } = new List<RouteViewModel>();

    // промежуточный пункт
    [Required]
    [Display(Name = "Промежуточный пункт:")]
    public string MiddlePoint { get; set; } = "";

    #region Конструкторы

    // конструктор по умолчанию
    public SelectByMiddlePointViewModel()
    {

    }

    // конструктор инициализирующий
    public SelectByMiddlePointViewModel(IEnumerable<RouteViewModel> routes, string middlePoint)
    {
        MiddlePoint = middlePoint;
        Routes = routes;
    }

    #endregion

}
