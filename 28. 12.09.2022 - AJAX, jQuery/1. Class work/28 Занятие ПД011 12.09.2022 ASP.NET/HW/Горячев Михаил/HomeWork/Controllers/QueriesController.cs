using HomeWork.Models;
using HomeWork.Models.BindingModels;
using HomeWork.Models.QueryResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

[Route("[controller]/[action]")]
[ApiController]
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


    // 1. Выбирает из информацию о товарах, единицей измерения которых является «шт» (штуки) и цена закупки составляет
    // меньше 200 руб. Значения задавать параметрами
    [HttpGet]
    public async Task<IActionResult> Query01Async()
    {
        // элементы
        ViewBag.Items = await DataContext.Purchases.AsNoTracking()
            .Include(p => p.Goods)
            .Include(p => p.Unit)
            .AsQueryable()
            .ToListAsync();

        ViewBag.UnitName = "————";
        ViewBag.MaxPrice = "————";

        // список единиц измерения
        var units = await DataContext.Units.AsNoTracking().ToListAsync();
        units.Insert(0, new Unit { LongName = "Все" });
        ViewBag.UnitList = new SelectList(units, nameof(Unit.Id), nameof(Unit.LongName));

        return View(new Query01BindingModel(0, 0));
    }

    [HttpPost]
    public async Task<IActionResult> Query01Async([FromForm]Query01BindingModel model)
    {
        ViewBag.Items = await DataContext.Purchases.AsNoTracking()
            .Include(p => p.Goods)
            .Include(p => p.Unit)
            .AsQueryable()
            .Where(i => i.UnitId == model.UnitId && i.Price <= model.MaxPrice)
            .ToListAsync();

        ViewBag.UnitName = (await DataContext.Units.AsNoTracking().FirstAsync(g => g.Id == model.UnitId)).LongName;
        ViewBag.MaxPrice = model.MaxPrice;

        // список единиц измерения
        var units = await DataContext.Units.AsNoTracking().ToListAsync();
        units.Insert(0, new Unit { LongName = "Все" });
        ViewBag.UnitList = new SelectList(units, nameof(Unit.Id), nameof(Unit.LongName));

        return View(model);
    }


    // 2. Выбирает информацию о товарах, цена закупки которых меньше 500 руб. за единицу товара. Значения
    // задавать параметрами
    [HttpGet]
    public async Task<IActionResult> Query02Async()
    {
        // элементы
        ViewBag.Items = await DataContext.Purchases.AsNoTracking()
            .Include(p => p.Goods)
            .Include(p => p.Unit)
            .AsQueryable()
            .ToListAsync();

        ViewBag.MaxPrice = "————";

        return View(new IntBindingModel());
    }

    [HttpPost]
    public async Task<IActionResult> Query02Async([FromForm]IntBindingModel model)
    {
        ViewBag.Items = await DataContext.Purchases.AsNoTracking()
            .Include(p => p.Goods)
            .Include(p => p.Unit)
            .AsQueryable()
            .Where(i => i.Price <= model.Value)
            .ToListAsync();

        ViewBag.MaxPrice = model.Value!;

        return View(model);
    }


    // 3. Выбирает информацию о товарах с заданным наименованием (например, «чехол защитный»), для которых цена
    // закупки меньше 1800 руб. Значения задавать параметрами
    [HttpGet]
    public async Task<IActionResult> Query03Async()
    {
        // элементы
        ViewBag.Items = await DataContext.Purchases.AsNoTracking()
            .Include(p => p.Goods)
            .Include(p => p.Unit)
            .AsQueryable()
            .ToListAsync();

        ViewBag.GoodsName = "————";
        ViewBag.MaxPrice = "————";

        // список товаров
        var goods = await DataContext.GoodsList.AsNoTracking().ToListAsync();
        goods.Insert(0, new Goods { Name = "Все" });
        ViewBag.GoodsList = new SelectList(goods, nameof(Goods.Id), nameof(Goods.Name));

        return View(new Query03BindingModel(0, 0));
    }

    [HttpPost]
    public async Task<IActionResult> Query03Async([FromForm]Query03BindingModel model)
    {
        ViewBag.Items = await DataContext.Purchases.AsNoTracking()
            .Include(p => p.Goods)
            .Include(p => p.Unit)
            .AsQueryable()
            .Where(i => i.Price <= model.MaxPrice && i.GoodsId == model.GoodsId)
            .ToListAsync();

        ViewBag.GoodsName = (await DataContext.GoodsList.AsNoTracking().FirstAsync(g => g.Id == model.GoodsId))
            .Name;

        ViewBag.MaxPrice = model.MaxPrice;

        // список товаров
        var goods = await DataContext.GoodsList.AsNoTracking().ToListAsync();
        goods.Insert(0, new Goods { Name = "Все" });
        ViewBag.GoodsList = new SelectList(goods, nameof(Goods.Id), nameof(Goods.Name));

        return View(model);
    }

    // 4. Запрос с параметрами. Выбирает информацию о продавцах с заданным значением процента комиссионных.
    // Значения задавать параметрами
    [HttpGet]
    public async Task<IActionResult> Query04Async()
    {
        ViewBag.Items = await DataContext.Sellers.AsNoTracking().ToListAsync();

        return View(new IntBindingModel());
    }

    [HttpPost]
    public async Task<IActionResult> Query04Async([FromForm]IntBindingModel? model)
    {
        // данные
        var items = DataContext.Sellers.AsNoTracking();

        ViewBag.Items = await (ModelState.IsValid && model!.Value != null
            ? items.Where(i => i.Interest == model.Value)
            : items).ToListAsync();

        return View(model);
    }


    // 5. Запрос с параметрами. Выбирает информацию о зафиксированных фактах продажи для заданного параметром продавца
    [HttpGet]
    public async Task<IActionResult> Query05Async()
    {
        // данные
        ViewBag.Items = await DataContext.Sales.AsNoTracking()
            .Include(s => s.Seller)
            .Include(s => s.Unit)
            .Include(s => s.Purchase)
            .ThenInclude(p => p!.Goods)
            .ToListAsync();

        var list = await DataContext.Sellers.AsNoTracking().ToListAsync();
        list.Insert(0, new Seller { Name = "Все" });

        ViewBag.SellerList = new SelectList(
            list,
            nameof(Seller.Id),
            nameof(Seller.IdAndFullName)
        );

        ViewBag.Seller = "————";

        return View(new IntBindingModel());
    }

    [HttpPost]
    public async Task<IActionResult> Query05Async([FromForm]IntBindingModel? model)
    {
        // данные
        var items = DataContext.Sales.AsNoTracking()
            .Include(s => s.Seller)
            .Include(s => s.Unit)
            .Include(s => s.Purchase)
            .ThenInclude(p => p!.Goods);

        ViewBag.Items = await (ModelState.IsValid && model?.Value != 0
            ? items.Where(i => i.SellerId == model!.Value)
            : items).ToListAsync();

        var list = await DataContext.Sellers.AsNoTracking().ToListAsync();
        list.Insert(0, new Seller { Name = "Все" });

        ViewBag.SellerList = new SelectList(
            list,
            nameof(Seller.Id),
            nameof(Seller.IdAndFullName)
        );

        ViewBag.Seller = (model!.Value != null && model.Value != 0
            ? (await DataContext.Sellers.FirstOrDefaultAsync(s => s.Id == model.Value))?.IdAndFullName
            : "————")!;

        return View(model);
    }

    // 6. Запрос с параметрами. Выбирает информацию обо всех зафиксированных фактах продажи товаров
    // (Наименование товара, Цена закупки, Цена продажи, дата продажи), для которых Цена продажи оказалась в
    // некоторых заданных границах. Значения задавать параметрами
    [HttpGet]
    public async Task<IActionResult> Query06Async()
    {
        // данные
        ViewBag.Items = await DataContext.Sales.AsNoTracking()
            .Include(s => s.Seller)
            .Include(s => s.Unit)
            .Include(s => s.Purchase)
            .ThenInclude(p => p!.Goods)
            .ToListAsync();

        return View(new RangeBindingModel());
    }

    [HttpPost]
    public async Task<IActionResult> Query06Async([FromForm]RangeBindingModel? model)
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


    // 7. Запрос с вычисляемыми полями. Вычисляет прибыль от продажи за каждый проданный товар. Включает поля
    // Дата продажи, Наименование товара, Цена закупки, Цена продажи, Количество проданных единиц, Прибыль.
    // Сортировка по полю Наименование товара
    [HttpGet]
    public async Task<IActionResult> Query07Async() => View(
        (await DataContext.Sales.AsNoTracking()
            .Include(s => s.Purchase)
            .ThenInclude(p => p!.Goods).ToArrayAsync())
        .Select(s => new Query07Result(
            s.DateSell,
            s.Purchase!.Goods!.Name,
            s.Purchase.Price,
            s.Price, s.Amount,
            (s.Price - s.Purchase.Price) * s.Amount))
        .OrderBy(s => s.GoodsName)
        .ToList()
    );


    // 8. Итоговый запрос. Выполняет группировку по полю Наименование товара. Для каждого наименования вычисляет
    // среднюю цену закупки товара, количество закупок
    [HttpGet]
    public async Task<IActionResult> Query08Async() => View(
        await DataContext.GoodsList.AsNoTracking()
            .Include(g => g.Purchases)
            .Select(g => new Query08Result(
                g.Name,
                g.Purchases != null ? g.Purchases.Min(p => p.Price) : null,
                g.Purchases != null ? (int?)g.Purchases.Average(p => p.Price) : null,
                g.Purchases != null ? g.Purchases.Max(p => p.Price) : null,
                g.Purchases!.Count
            ))
            .ToListAsync()
    );


    // 9. Итоговый запрос. Выполняет группировку по полю Код продавца из таблицы ПРОДАЖИ. Для каждого продавца
    // вычисляет среднее значение по полю Цена продажи единицы товара, количество продаж
    [HttpGet]
    public async Task<IActionResult> Query09Async() => View(
        await DataContext.Sellers.AsNoTracking()
            .Include(s => s.Sales)
            .Where(s => s.Sales!.Count != 0)
            .Select(s => new Query09Result(
                s,
                s.Sales != null ? s.Sales.Min(sale => sale.Price) : null,
                s.Sales != null ? (int)s.Sales.Average(sale => sale.Price) : null,
                s.Sales != null ? s.Sales.Max(sale => sale.Price) : null,
                s.Sales != null ? s.Sales.Count : null
            ))
            .ToListAsync()
    );


    // 10. Итоговый запрос. Выполняет группировку по единицам измерений проданных товаров, для каждой единицы
    // измерения вычисляет сумму продаж и суммарное количество проданного товара
    [HttpGet]
    public async Task<IActionResult> Query10Async() => View(
        (await DataContext.Units.AsNoTracking()
            .Include(u => u.Sales)
            .Select(u => new Query10Result(
                u,
                u.Sales != null ? u.Sales.Sum(sale => sale.Price * sale.Amount) : (int?)null,
                u.Sales != null ? u.Sales.Count : (int?)null
            ))
            .ToArrayAsync())
            .OrderByDescending(s => s.Sum)
        .ToList()
    );


    // 11. Итоговый запрос.
    // Для всех продавцов вывести сумму и количество продаж, минимальную и максимальную стоимости продаж
    [HttpGet]
    public async Task<IActionResult> Query11Async() => View(
        await DataContext.Sellers.AsNoTracking()
            .Select(s => new Query11Result(
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
