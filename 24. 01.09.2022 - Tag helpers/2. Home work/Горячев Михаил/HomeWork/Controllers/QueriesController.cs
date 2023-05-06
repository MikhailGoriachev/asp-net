using HomeWork.Models;
using HomeWork.Models.BindingModels;
using HomeWork.Models.QueryResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

public class QueriesController : Controller
{
    // контекст базы данных
    public SalesAccountingContext DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public QueriesController(SalesAccountingContext dataContext) =>
        DataContext = dataContext;

    #endregion

    #region Методы действия

    // 4. Запрос с параметрами. Выбирает информацию о продавцах с заданным значением процента комиссионных.
    // Значения задавать параметрами
    public async Task<IActionResult> Query04Async(IntBindingModel? model)
    {
        // данные
        var items = DataContext.Sellers.AsNoTracking();

        ViewBag.Items = await (ModelState.IsValid && model!.Value != null
            ? items.Where(i => i.Interest == model.Value)
            : items).ToListAsync();

        return View(model);
    }


    // 5. Запрос с параметрами. Выбирает информацию обо всех зафиксированных фактах продажи товаров
    // (Наименование товара, Цена закупки, Цена продажи, дата продажи), для которых Цена продажи оказалась в
    // некоторых заданных границах. Значения задавать параметрами
    public async Task<IActionResult> Query05Async(RangeBindingModel? model)
    {
        // данные
        var items = DataContext.Sales.AsNoTracking()
            .Include(s => s.Seller)
            .Include(s => s.Unit)
            .Include(s => s.Purchase)
            .ThenInclude(p => p!.Goods);

        ViewBag.Items = await (ModelState.IsValid && model?.Min != null && model.Max != null
            ? items.Where(i => i.Price >= model.Min && i.Price <= model.Max)
            : items).ToListAsync();

        return View(model);
    }


    // 6. Запрос с вычисляемыми полями. Вычисляет прибыль от продажи за каждый проданный товар. Включает поля
    // Дата продажи, Наименование товара, Цена закупки, Цена продажи, Количество проданных единиц, Прибыль.
    // Сортировка по полю Наименование товара
    public async Task<IActionResult> Query06Async() => View(
        (await DataContext.Sales.AsNoTracking()
            .Include(s => s.Purchase)
            .ThenInclude(p => p!.Goods).ToArrayAsync())
        .Select(s => new Query06Result(
            s.DateSell,
            s.Purchase!.Goods!.Name,
            s.Purchase.Price,
            s.Price, s.Amount,
            (s.Price - s.Purchase.Price) * s.Amount))
        .OrderBy(s => s.GoodsName)
        .ToList()
    );


    // 9. Итоговый запрос. Выполняет группировку по полю Наименование товара. Для каждого наименования вычисляет
    // среднюю цену закупки товара, количество закупок
    public async Task<IActionResult> Query09Async() => View(
        await DataContext.GoodsList.AsNoTracking()
            .Include(g => g.Purchases)
            .Select(g => new Query09Result(
                g.Name,
                g.Purchases != null ? g.Purchases.Min(p => p.Price) : null,
                g.Purchases != null ? (int?)g.Purchases.Average(p => p.Price) : null,
                g.Purchases != null ? g.Purchases.Max(p => p.Price) : null,
                g.Purchases!.Count
            ))
            .ToListAsync()
    );


    // 10. Итоговый запрос. Выполняет группировку по полю Код продавца из таблицы ПРОДАЖИ. Для каждого продавца
    // вычисляет среднее значение по полю Цена продажи единицы товара, количество продаж
    public async Task<IActionResult> Query10Async() => View(
        await DataContext.Sellers.AsNoTracking()
            .Select(s => new Query10Result(
                s,
                s.Sales != null ? s.Sales.Min(sale => sale.Price) : null,
                s.Sales != null ? (int)s.Sales.Average(sale => sale.Price) : null,
                s.Sales != null ? s.Sales.Max(sale => sale.Price) : null,
                s.Sales != null ? s.Sales.Count : null
            ))
            .ToListAsync()
    );

    #endregion
}
