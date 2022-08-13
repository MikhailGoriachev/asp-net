using HomeWork.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HomeWork.Pages;

public class PeriodicalsReport : PageModel
{
    // результирующая коллеция запроса 4
    public IEnumerable<ResultQuery04>? ResultQuery04List { get; set; }

    // результирующая коллеция запроса 5
    public IEnumerable<ResultQuery05>? ResultQuery05List { get; set; }

    // результирующая коллеция запроса 6
    public IQueryable<ResultQuery06>? ResultQuery06List { get; set; }

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

        ResultQuery04List = Data.Categories.ToArray()
            .GroupJoin(Data.Periodicals.ToArray(),
                c => c.Id,
                p => p.CategoryId,
                (c, gr) => new { c, gr = gr.DefaultIfEmpty() })
            .Select(res => new ResultQuery04(
                res.c.Name!,
                res.gr.Average(g => g?.Price ?? 0),
                res.gr.Count(p => p != null)
                ));
    }


    // запрос 5
    public void OnGetQuery05()
    {
        Title = "Запрос 5";

        ResultQuery05List = Data.Categories.ToArray()
            .GroupJoin(Data.Periodicals.ToArray(),
                c => c.Id,
                p => p.CategoryId,
                (c, gr) => new { c, gr = gr.DefaultIfEmpty() })
            .Select(res => new ResultQuery05(
                res.c.Name!,
                res.gr.Min(g => g?.Price ?? 0),
                res.gr.Max(g => g?.Price ?? 0),
                res.gr.Count(p => p != null)
            ));
    }

    // запрос 6
    public void OnGetQuery06()
    {
        Title = "Запрос 6";

        ResultQuery06List = Data.Periodicals.AsQueryable()
            .GroupBy(p => p.Duration,
                (k, g) =>
                    new ResultQuery06(k, g.Average(g => g.Price), g.Count()));
    }

    #endregion
}

// результирующий класс для запроса 4
public record ResultQuery04(string TypeEdition, double AvgPrice, int Count);

// результирующий класс для запроса 5
public record ResultQuery05(string TypeEdition, double MinPrice, double MaxPrice, int Count);

// результирующий класс для запроса 6
public record ResultQuery06(int Duration, double AvgPrice, int Count);
