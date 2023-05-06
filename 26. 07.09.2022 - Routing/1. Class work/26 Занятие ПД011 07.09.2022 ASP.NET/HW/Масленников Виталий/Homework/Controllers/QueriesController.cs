using Homework.Models;
using Homework.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Homework.Controllers;

public class QueriesController : Controller
{
    private readonly WholeSaleContext _context;

    public QueriesController(WholeSaleContext context) => _context = context;

    // Выбирает информацию о продавцах с заданным значением процента комиссионных. Значение задавать параметром
    public IActionResult Query04() =>
        View(_context.Sellers.AsNoTracking());

    [HttpPost]
    public IActionResult Query04(int interest) =>
        View(_context.Sellers.Where(i => Math.Abs(i.Interest - interest) < 1E-6).AsNoTracking());

    // Выбирает информацию о зафиксированных фактах продажи для заданного параметром продавца
    public IActionResult Query05(int? sellerId)
    {
        var items = _context.Sales.AsNoTracking()
            .Include(s => s.Seller)
            .Include(s => s.Purchase)
            .ThenInclude(p => p!.Goods)
            .AsQueryable();

        ViewBag.Sellers = new SelectList(items.Select(s =>
            s.Seller).Distinct().ToList(), "Id", "ShortName", sellerId ?? 1);

        if (sellerId != null)
            items = items.Where(s => s.Seller!.Id == sellerId);

        return View(items);
    }

    // Выбирает информацию обо всех зафиксированных фактах продажи товаров (Наименование товара, Цена закупки,
    // Цена продажи, дата продажи), для которых Цена продажи оказалась в некоторых заданных границах.
    // Значения задавать параметрами
    public IActionResult Query06(int? fromPrice, int? toPrice)
    {
        var items = _context.Sales.AsNoTracking()
            .Include(r => r.Purchase)
            .ThenInclude(p => p.Goods)
            .AsQueryable();

        if (fromPrice != null && toPrice != null)
            items = items.Where(s => fromPrice <= s.Price && s.Price <= toPrice);
        return View(items);
    }

    // Вычисляет прибыль от продажи за каждый проданный товар. Включает поля Дата продажи, Наименование товара,
    // Цена закупки, Цена продажи, Количество проданных единиц, Прибыль. Сортировка по полю Наименование товара
    public IActionResult Query07()
    {
        var items = _context.Sales
            .AsNoTracking()
            .Include(r => r.Purchase)
            .ThenInclude(p => p.Goods)
            .ToList()
            .Select(s => new Query07Result(
                s.Id, s.SaleDate, s.Purchase!.Goods!.Name, s.Purchase.Price,
                s.Price, s.Amount, s.Price - s.Purchase.Price))

            .OrderBy(s => s.ProductName);
        return View(items);
    }

    // Выполняет группировку по наименованию закупленного товара. Для каждого наименования
    // вычисляет среднюю цену закупки товара, количество закупок
    public IActionResult Query08() => View(
        _context.Purchases.AsNoTracking()
            .Include(p => p.Goods)
            .GroupBy(p => new { GoodsName = p.Goods!.Name, p.Unit.Long },
                (key, group) => new Query08Result
                {
                    GoodsName = key.GoodsName,
                    Units = key.Long,
                    AvgPrice = (int)group.Average(p => p.Price),
                    Amount = group.Count()
                })
    );


    // Выполняет группировку по полю Код продавца из таблицы ПРОДАЖИ. Для каждого продавца вычисляет
    // среднее значение по полю Цена продажи единицы товара, количество продаж
    public IActionResult Query09() =>
        View(_context.Sales.AsNoTracking()
            .Include(s => s.Seller)
            .GroupBy(s => new {
                s.SellerId,
                s.Seller!.Surname,
                s.Seller.Name,
                s.Seller.Patronymic
            }, (key, group) => new Query09Result
            {
                Seller = $"{key.Surname} {key.Name[0]}.{key.Patronymic[0]}.",
                AmountSales = group.Count(),
                AvgPrice = (int)group.Average(s => s.Price)
            })
        );
    
    // Выполняет группировку по единицам измерений проданных товаров, для каждой
    // единицы измерения вычисляет сумму продаж и суммарное количество проданного товара
    public IActionResult Query10() =>
        View(_context.Units.AsNoTracking()
            .Select(u => new Query10Result(
                u,
                u.Sales != null ? u.Sales.Sum(sale => sale.Price * sale.Amount) : null,
                u.Sales != null ? u.Sales.Sum(sale => sale.Amount) : null,
                u.Sales != null ? u.Sales.Count : null
                )));

    // Для всех продавцов вывести сумму и количество продаж, минимальную и максимальную стоимости продаж
    public IActionResult Query11() =>
    View(_context.Sellers.AsNoTracking()
            .Select(s => new Query11Result {
                Seller = s,
               MinSale  = s.Sales != null ? s.Sales.Min(sale => sale.Price) : null,
               MaxSale = s.Sales != null ? s.Sales.Max(sale => sale.Price) : null,
               AmountSales = s.Sales != null ? s.Sales.Count : null,
               SumSales = s.Sales != null ? s.Sales.Sum(sale => sale.Amount * sale.Price) : null
    }));
            
}