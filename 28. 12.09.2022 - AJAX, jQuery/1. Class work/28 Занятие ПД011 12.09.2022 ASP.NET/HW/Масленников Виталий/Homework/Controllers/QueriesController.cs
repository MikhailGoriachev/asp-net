using Homework.Models;
using Homework.Models.DTO;
using Homework.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework.Controllers;

[ApiController]
[Route("api/")]
public class QueriesController : ControllerBase
{
    private readonly WholeSaleContext _context;

    public QueriesController(WholeSaleContext context) => _context = context;
    
    // GET: api/query1
    [HttpGet("query1/{units}/{maxPriceExcl:int}")]
    public IActionResult Query01(string units, int maxPriceExcl) =>
        new JsonResult(_context.Purchases.AsNoTracking()
            .Include(p => p.Goods)
            .Include(p => p.Unit)
            .Where(p => p.Unit!.Long.Equals(units) && p.Price < maxPriceExcl)
            .Select(v => new PurchaseDTO(v)));

    // GET: api/query2
    [HttpGet("query2/{maxPriceExcl:int}")]
    public IActionResult Query02(int maxPriceExcl) =>
        new JsonResult(_context.Purchases.AsNoTracking()
            .Include(p => p.Unit)
            .Include(p => p.Goods)
            .Where(p => p.Price < maxPriceExcl)
            .Select(v => new PurchaseDTO(v)));

    // GET: api/query3/Рюкзак/750
    [HttpGet("query3/{name}/{maxPriceExcl:int}")]
    public IActionResult Query03(string name, int maxPriceExcl) =>
        new JsonResult(_context.Purchases.AsNoTracking()
            .Include(p => p.Unit)
            .Include(p => p.Goods)
            .Where(p => p.Goods!.Name.Equals(name) && p.Price < maxPriceExcl)
            .Select(v => new PurchaseDTO(v)));

    // GET: api/query4/8
    [HttpGet("query4/{interest:int}")]
    public IActionResult Query04(int interest) =>
        new JsonResult(_context.Sellers.AsNoTracking()
            .Where(i => Math.Abs(i.Interest - interest) < 1E-6)
            .Select(v => new SellerDTO(v)));
    
    
    // GET: api/query5/3
    [HttpGet("query5/{sellerId:int?}")]
    public IActionResult Query05(int? sellerId)
    {
        var items = _context.Sales.AsNoTracking()
            .Include(s => s.Seller)
            .Include(s => s.Unit)
            .Include(s => s.Purchase)
            .ThenInclude(p => p!.Goods)
            .AsQueryable();

        if (sellerId != null)
            items = items.Where(s => s.Seller!.Id == sellerId);

        return new JsonResult(items.Select(v => new SaleDTO(v)));
    }
    
    // GET: api/query6/120/500
    [HttpGet("query6/{fromPrice:int?}/{toPrice:int?}")]
    public IActionResult Query06(int? fromPrice, int? toPrice)
    {
        var items = _context.Sales.AsNoTracking()
            .Include(s => s.Seller)
            .Include(s => s.Unit)
            .Include(s => s.Purchase)
            .ThenInclude(p => p!.Goods)
            .AsQueryable();

        if (fromPrice != null && toPrice != null)
            items = items.Where(s => fromPrice <= s.Price && s.Price <= toPrice);
        return new JsonResult(items.Select(v => new SaleDTO(v)));
    }
    
    // Вычисляет прибыль от продажи за каждый проданный товар. Включает поля Дата продажи, Наименование товара,
    // Цена закупки, Цена продажи, Количество проданных единиц, Прибыль. Сортировка по полю Наименование товара
    // GET: api/query7
    [HttpGet("query7/")]
    public IActionResult Query07()
    {
        var items = _context.Sales
            .AsNoTracking()
            .Include(r => r.Purchase)
            .ThenInclude(p => p!.Goods)
            .ToList()
            .Select(s => new Query07Result(
                s.Id, s.SaleDate, s.Purchase!.Goods!.Name, s.Purchase.Price,
                s.Price, s.Amount, (s.Price - s.Purchase.Price) * s.Amount))

            .OrderBy(s => s.Goods);
        return new JsonResult(items);
    }
    
    // Выполняет группировку по наименованию закупленного товара. Для каждого наименования
    // вычисляет среднюю цену закупки товара, количество закупок
    // GET: api/query8
    [HttpGet("query8/")]
    public IActionResult Query08() => 
        new JsonResult(_context.Purchases.AsNoTracking()
            .Include(p => p.Goods)
            .GroupBy(p => new { p.Goods!.Name, p.Unit!.Long },
                (key, group) => new Query08Result
                {
                    Goods = key.Name,
                    Units = key.Long,
                    AvgPrice = (int)group.Average(p => p.Price),
                    Amount = group.Count()
                })
    );
    
    // Выполняет группировку по полю Код продавца из таблицы ПРОДАЖИ. Для каждого продавца вычисляет
    // среднее значение по полю Цена продажи единицы товара, количество продаж
    // GET: api/query9
    [HttpGet("query9/")]
    public IActionResult Query09() =>
        new JsonResult(_context.Sales.AsNoTracking()
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
    // GET: api/query10
    [HttpGet("query10/")]
    public IActionResult Query10() =>
        new JsonResult(_context.Units.AsNoTracking()
            .Select(u => new Query10Result(
                u.Short,
                u.Sales != null ? u.Sales.Sum(sale => sale.Price * sale.Amount) : null,
                u.Sales != null ? u.Sales.Sum(sale => sale.Amount) : null,
                u.Sales != null ? u.Sales.Count : null
            )));
    
    // Для всех продавцов вывести сумму и количество продаж, минимальную и максимальную стоимости продаж
    // GET: api/query11
    [HttpGet("query11/")]
    public IActionResult Query11()=>
        new JsonResult(_context.Sellers.AsNoTracking()
            .Select(s => new Query11Result {
                Seller = s.ShortName,
                MinSale  = s.Sales != null ? s.Sales.Min(sale => sale.Price) : null,
                MaxSale = s.Sales != null ? s.Sales.Max(sale => sale.Price) : null,
                AmountSales = s.Sales != null ? s.Sales.Count : null,
                SumSales = s.Sales != null ? s.Sales.Sum(sale => sale.Amount * sale.Price) : null
            }));
}