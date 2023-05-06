using HomeWork.Models;
using HomeWork.Models.Queries;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeWork.Controllers;

public class QueriesController : Controller
{
    // контекст базы данных
    private WholesaleStoreContext _db;

    // В конструкторе получаем доступ к контексту базы данных
    public QueriesController(WholesaleStoreContext context) =>
        _db = context;


    // фильтр для 1, 2, 3 запроса
    public async Task<IActionResult> Filter(FilterByPurchaseViewModel? model)
    {
        var units = await _db.Units?.AsNoTracking().ToListAsync()!;
        units.Insert(0, new Unit { Id = 0, Short = "Все" });

        // список единицы измерения товара
        ViewBag.UnitsList = new SelectList(units,"Id", "Short");

        var goods = await _db.Goods?.AsNoTracking().ToListAsync()!;
        goods.Insert(0, new Good { Id = 0, NameGood = "Все" });

        // список наименования товара
        ViewBag.GoodsList = new SelectList(goods,"Id", "NameGood");


        // коллекция данных
        var items = _db.Purchases!.AsNoTracking()
            .Include(r => r.Good)
            .Include(r => r.Unit)
            .AsQueryable();

        ViewBag.GoodName = " ";
        ViewBag.UnitName = " ";
        ViewBag.Price = " ";


        // при первом GET-запросе - отдать нефильтрованные данные
        if (model == null)
            return View(new FilterByPurchaseViewModel(await items.ToListAsync(), 0, 0, null, null));

        // единица измерения товара
        if (model.UnitId != 0)
        {
            items = items.Where(i => i.UnitId == model.UnitId);
            ViewBag.UnitName = (await _db.Units!.AsQueryable()
                .FirstOrDefaultAsync(o => o.Id == model.UnitId))!.Short!;
        }

        // наименование товара
        if (model.GoodId != 0)
        {
            items = items.Where(i => i.GoodId == model.GoodId);
            ViewBag.GoodName = (await _db.Goods!.AsQueryable()
                .FirstOrDefaultAsync(c => c.Id == model.GoodId))!.NameGood!;
        }

        // минимальная цена
        if (model.MinPrice != null)
        {
            items = items.Where(i => i.PricePurchase <= model.MinPrice);
            ViewBag.MinPrice = model.MinPrice;
        }

        // максимальная цена
        if (model.MaxPrice != null)
        {
            items = items.Where(i => i.PricePurchase <= model.MaxPrice);
            ViewBag.MaxPrice = model.MaxPrice;
        }

        model.Purchases = items;

        return View(model);
    }

    // 4.Запрос с параметрами.
    // Выбирает информацию о продавцах с заданным значением процента комиссионных
    public IActionResult Query04()
    {
        ViewBag.Interest = null;

        return View(_db.Sellers.AsNoTracking());

    }

    [HttpPost]
    public IActionResult Query04(int Interest)
    {
        ViewBag.Interest = Interest;

        return View(_db.Sellers
            .Where(s => s.Interest == Interest)
            .AsNoTracking());
    }


    // 5.Запрос с параметром
    // Выбирает информацию о зафиксированных фактах продажи для
    // заданного параметром продавца
    public IActionResult Query05()
    {
        // список продавцов
        ViewBag.SellersList = new SelectList(_db.Sellers.AsNoTracking(), "Id", "Surname");

        return View(_db.Sales.AsNoTracking()
            .Include(r => r.Purchase)
            .ThenInclude(p => p.Good)
            .Include(r => r.Seller));
    } 

    [HttpPost]
    public IActionResult Query05(int sellerId)
    {
        ViewBag.SellersList = new SelectList(_db.Sellers.AsNoTracking(), "Id", "Surname");

        return View(_db.Sales.AsNoTracking()
            .Where(s => s.SellerId == sellerId)
            .Include(r => r.Purchase)
            .ThenInclude(p => p.Good)
            .Include(r => r.Seller));
    }


    // 6.Запрос с параметрами
    // Выбирает информацию обо всех зафиксированных фактах продажи товаров
    // (Наименование товара, Цена закупки, Цена продажи, дата продажи),
    // для которых Цена продажи оказалась в некоторых заданных границах.
    // Значения задавать параметрами
    public IActionResult Query06()
    {
        // диапазон цены
        ViewBag.MinPrice = null;
        ViewBag.ToPrice = null;

        return View(_db.Sales.AsNoTracking()
            .Include(r => r.Purchase)
            .ThenInclude(p => p!.Good));
    } 

    [HttpPost]
    public IActionResult Query06(int? minPrice, int? maxPrice)
    {
        ViewBag.MinPrice = minPrice;
        ViewBag.MaxPrice = maxPrice;

        var query = _db.Sales.AsNoTracking()
            .Include(r => r.Purchase)
            .ThenInclude(p => p.Good)
            .AsQueryable();

        if (minPrice != null && maxPrice != null)
            query = query.Where(s => minPrice <= s.PriceSale && s.PriceSale <= maxPrice);
        return View(query);
    }


    // 7.Запрос с вычисляемыми полями
    // 	Вычисляет прибыль от продажи за каждый проданный товар.
    // 	Включает поля Дата продажи, Наименование товара,
    // 	Цена закупки, Цена продажи, Количество проданных единиц, Прибыль.
    // 	Сортировка по полю Наименование товара
    public IActionResult Query07()
    {
        var query = _db.Sales
            .AsNoTracking()
            .Include(r => r.Purchase)
            .ThenInclude(p => p!.Good)
            .ToList()
            .Select(s => new Query07Result(
                s.DateSale,
                s.Purchase!.Good!.NameGood!,
                s.Purchase.PricePurchase,
                s.PriceSale,
                s.AmountSale,
                (s.PriceSale - s.Purchase.PricePurchase)* s.AmountSale))
            .OrderBy(s => s.ProductName);
        return View(query);
    }


    // 8.Итоговый запрос
    // Выполняет группировку по наименованию закупленного товара.
    // Для каждого наименования вычисляет среднюю цену закупки товара,
    // количество закупок
    public IActionResult Query08()
    {
        IQueryable<Query08Result> query = _db.Purchases
            .Include(t => t.Good)
            .AsNoTracking()
            .GroupBy(r => r.Good!.NameGood,
                (key, group) => new Query08Result(
                    key!,
                    group.Count(),
                    group.Average(t => t.PricePurchase)                   
                ));

        return View(query);
    }


    // 9.Итоговый запрос (нет левого соединения)
    // Выполняет группировку по полю Код продавца из таблицы ПРОДАЖИ.
    // Для каждого продавца вычисляет среднее значение
    // по полю Цена продажи единицы товара, количество продаж
    public IActionResult Query09()
    {
        IQueryable<Query09Result> query = _db.Sales
            .Include(s => s.Seller)
            .AsNoTracking()
            .GroupBy(r => new { r!.Seller!.Surname, r.Seller.NameSeller, r.Seller.Patronymic },
                (key, group) => new Query09Result(
                    $"{key.Surname} {key!.NameSeller![0]}.{key!.Patronymic![0]}.", 
                    group.Average(t => t.PriceSale),
                    group.Count()
                ));

        return View(query);
    }


    // 10.Итоговый запрос
    // Выполняет группировку по единицам измерений проданных товаров,
    // для каждой единицы измерения вычисляет сумму продаж и
    // суммарное количество проданного товара
    public IActionResult Query10()
    {
        IQueryable<Query10Result> query = _db.Sales
            .Include(s => s.Purchase)
            .ThenInclude(p => p!.Unit)
            .AsNoTracking()
            .GroupBy(s => s!.Purchase!.Unit!.Long,
                (key, group) => new Query10Result(
                    key!,
                    group.Sum(t => t.PriceSale),
                    group.Count()
                ));

        return View(query);
    }


    // 11.Итоговый запрос с левым соединением
    // Для всех продавцов вывести сумму и количество продаж,
    // минимальную и максимальную стоимости продаж
    public IActionResult Query11()
    {
        return View(_db.Sellers.AsNoTracking()
            .Select(s => new Query11Result(
                s,
                s.Sales != null ? s.Sales.Sum(s => s.PriceSale) : null,
                s.Sales != null ? s.Sales.Count : null,
                s.Sales != null ? s.Sales.Min(s => s.PriceSale) : null,
                s.Sales != null ? s.Sales.Max(s => s.PriceSale) : null
            ))
            .ToList());
    }

} // class QueriesController
