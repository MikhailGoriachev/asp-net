using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Models;

// Класс Поездка
public class Visit
{
    // идентификатор
    [HiddenInput]
    public int Id { get; set; }

    // клиент
    [Display(Name = "Клиент")] [Required] public int ClientId { get; set; }
    public Client? Client { get; set; }

    // маршрут
    [Display(Name = "Маршрут")] [Required] public int RouteId { get; set; }
    public Route? Route { get; set; }

    // дата начала поездки
    [Display(Name = "Дата начала поездки")]
    [UIHint("Date")]
    public DateTime DateStart { get; set; }

    // длительность поездки
    [Display(Name = "Длительность поездки")]
    [Required(ErrorMessage = "Это поле не может быть пустым!")]
    [Range(1, int.MaxValue, ErrorMessage = "Значение поля должно быть больше 0!")]
    public int Duration { get; set; }


    // полная стоимость
    // стоимость поездки может быть вычислена как Стоимость 1 дня пребывания * Количество дней пребывания + Стоимость
    // транспортных услуг + Стоимость оформления визы. Кроме того, клиент платит налог на добавленную стоимость (НДС) в
    // размере 3% от стоимости поездки.
    public double OverCost => Route?.Country != null
        ? Route!.DailyCost * Duration + Route.Country!.CostService +
          Route.Country!.CostVisa
        : 0d;

    // полная стоимость включая НДС
    public double OverCostAndNds => OverCost * 1.03;

    #region Конструкторы

    // конструктор по умолчанию
    public Visit()
    {
    }


    // конструктор инициализирующий
    public Visit(int id, Client? client, Route? route, DateTime dateStart, int duration)
    {
        Id = id;
        Client = client;
        Route = route;
        DateStart = dateStart;
        Duration = duration;
    }


    // конструктор инициализирующий
    public Visit(int id, int clientId, int routeId, DateTime dateStart, int duration)
    {
        Id = id;
        ClientId = clientId;
        RouteId = routeId;
        DateStart = dateStart;
        Duration = duration;
    }

    #endregion
}
