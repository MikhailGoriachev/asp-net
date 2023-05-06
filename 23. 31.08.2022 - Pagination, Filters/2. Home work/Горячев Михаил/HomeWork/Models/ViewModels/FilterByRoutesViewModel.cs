using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models.ViewModels;

// Класс Модель представления для фильтрации маршрутов
public class FilterByRoutesViewModel
{
    // маршруты
    public IEnumerable<Route> Routes { get; set; } = null!;

    // id цели поездки
    [Display(Name = "Цель поездки")]
    public int ObjectiveId { get; set; }

    // id страна маршрута
    [Display(Name = "Страна")]
    public int CountryId { get; set; }

    // максимальная стоимость
    [Display(Name = "Макс. стоимость транс. услуг")]
    public int? MaxPrice { get; set; }


    #region Конструкторы

    // конструктор по умолчанию
    public FilterByRoutesViewModel()
    {

    }

    // конструктор инициализирующий
    public FilterByRoutesViewModel(IEnumerable<Route> routes, int objectiveId, int countryId, int? maxPrice)
    {
        Routes = routes;
        ObjectiveId = objectiveId;
        CountryId = countryId;
        MaxPrice = maxPrice;
    }

    #endregion
}
