using UsingTagHelpers.Models;
using UsingTagHelpers.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using UsingTagHelpers.Models.Queries;

namespace UsingTagHelpers.Controllers;

public class QueriesController : Controller
{
    // контекст базы данных
    private WholesaleStoreContext _db;

    // В конструкторе получаем доступ к контексту базы данных
    public QueriesController(WholesaleStoreContext context) =>
        _db = context;


    // фильтр для 1, 2, 3 запроса
    public async Task<IActionResult> Filter(FilterByPurchaseViewModel? model) {
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
        if (model == null) {
            return View(new FilterByPurchaseViewModel(await items.ToListAsync(), 0, 0, null));
        } // if

        // фильтрация по единице измерения товара
        if (model.UnitId != 0) {
            items = items.Where(i => i.UnitId == model.UnitId);
            ViewBag.UnitName = (await _db.Units!.AsQueryable()
                .FirstOrDefaultAsync(o => o.Id == model.UnitId))!.Short!;
        } // if

        // фильтрация по наименованию товара
        if (model.GoodId != 0) {
            items = items.Where(i => i.GoodId == model.GoodId);
            ViewBag.GoodName = (await _db.Goods!.AsQueryable()
                .FirstOrDefaultAsync(c => c.Id == model.GoodId))!.NameGood!;
        } // if

        // фильтрация по цене товара
        if (model.Price != null) {
            items = items.Where(i => i.PricePurchase > model.Price);
            ViewBag.Price = model.Price;
        } // if

        // передать в модель отфильтрованный товар
        model.Purchases = items;

        return View(model);
    } // Filter


    // Запрос 4
    // Выбирает информацию о продавцах с заданным значением процента комиссионных
    // Разметка с коллекцией продавцов и формой ввода параметра запроса
    public IActionResult Query04() {
        ViewBag.Interest = null;
        return View(_db.Sellers.AsNoTracking());
    } // Query04

    // получение параметра запроса, выборка и вывод выбранных данных
    [HttpPost]
    public IActionResult Query04(int? interest) {
        // передать параметр для индикации
        ViewBag.Interest = interest;

        // если процент не ввели, вернуть все записи из таблицы,
        // вернуть выбранные записи, если процент ввели  в форму
        var items = _db.Sellers.AsNoTracking();
        if (interest != null)
            items = items.Where(s => s.Interest == interest);
        return View(items);
    } // Query04


    // Запрос 5
    // Выбирает информацию обо всех зафиксированных фактах продажи товаров
    // (Наименование товара, Цена закупки, Цена продажи, дата продажи),
    // для которых Цена продажи оказалась в некоторых заданных границах.
    // Значения задавать параметрами
    // Разметка с коллекцией фактов продажи и формой ввода параметра запроса
    public IActionResult Query05() {
        ViewBag.FromPrice = null;
        ViewBag.ToPrice = null;
        return View(_db.Sales.AsNoTracking()
            .Include(r => r.Purchase)
            .ThenInclude(p => p.Good));
    } // Query05

    // получение параметра запроса, выборка и вывод выбранных данных
    [HttpPost]
    public IActionResult Query05(int? fromPrice, int? toPrice) {
        // передать параметры для индикации
        ViewBag.FromPrice = fromPrice;
        ViewBag.ToPrice = toPrice;

        // если диапазон цены не ввели, вернуть все записи из таблицы,
        // вернуть выбранные записи, если диапазон цены ввели  в форму
        var items = _db.Sales.AsNoTracking()
            .Include(r => r.Purchase)
            .ThenInclude(p => p.Good)
            .AsQueryable();

        if (fromPrice != null && toPrice != null)
            items = items.Where(s => fromPrice <= s.PriceSale && s.PriceSale <= toPrice);
        return View(items);
    } // Query05


    // Запрос 6
    // Вычисляет прибыль от продажи за каждый проданный товар. Включает поля Дата продажи,
    // Наименование товара, Цена закупки, Цена продажи, Количество проданных единиц, Прибыль.
    // Сортировка по полю Наименование товара
    public IActionResult Query06() {
        var items = _db.Sales
            .AsNoTracking()
            .Include(r => r.Purchase)
            .ThenInclude(p => p.Good)
            // список необходим для Select() и OrderBy()
            .ToList()
            .Select(s => new Query06Result(
                s.Id, s.DateSale, s.Purchase!.Good!.NameGood!, s.Purchase.PricePurchase, 
                s.PriceSale, s.AmountSale, s.PriceSale - s.Purchase.PricePurchase))
            
            .OrderBy(s => s.ProductName);
        return View(items);
    } // Query06


    // Запрос 7
    // Выполняет группировку по полю Наименование товара. Для каждого
    // наименования вычисляет среднюю цену закупки товара, количество
    // закупок
    public IActionResult Query07()
    {
        IQueryable<Query07Result> items = null!;
        return View(items);
    } // Query07


    // Запрос 8
    // Выполняет группировку по полю Код продавца из таблицы ПРОДАЖИ.
    // Для каждого продавца вычисляет среднее значение по полю Цена
    // продажи единицы товара, количество продаж
    public IActionResult Query08() {
        IQueryable<Query07Result> items = null!;
        return View(items);
    } // Query08

} // class QueriesController
