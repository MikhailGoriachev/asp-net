using HomeWork.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages;

public class PeriodicalsReport : PageModel
{
    // результирующая коллеция запроса 4
    public IQueryable<ResultQuery04>? ResultQuery04List { get; set; }

    // результирующая коллеция запроса 5
    public IQueryable<ResultQuery05>? ResultQuery05List { get; set; }

    // контекст базы данных
    public PeriodicalsContext Data { get; set; }

    // заголовок
    public string? Title { get; set; }

    #region Конструкторы

    // конструктор инициаилизирующий
    public PeriodicalsReport(PeriodicalsContext data)
    {
        Data = data;
    }

    #endregion

    #region Методы

    // запрос 4
    public void OnGetQuery04()
    {
        Title = "Запрос 4";

        ResultQuery04List = Data.Periodicals.AsQueryable()
            .GroupBy(p => p.TypeEdition,
                (k, g) =>
                    new ResultQuery04(k!, g.Average(i => i.Price), g.Count()));
    }


    // запрос 5
    public void OnGetQuery05()
    {
        Title = "Запрос 5";

        ResultQuery05List = Data.Periodicals.AsQueryable()
            .GroupBy(p => p.TypeEdition,
                (k, g) =>
                    new ResultQuery05(k!, g.Min(i => i.Price), g.Min(i => i.Price), g.Count()));
    }

    #endregion
}

// результирующий класс для запроса 4
public record ResultQuery04(string TypeEdition, double AvgPrice, int Count);

// результирующий класс для запроса 5
public record ResultQuery05(string TypeEdition, double MinPrice, double MaxPrice, int Count);
