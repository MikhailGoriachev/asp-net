using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApiDatabase.Models;
using WebApiDatabase.Models.Queries;

namespace WebApiDatabase.Controllers;

// запросы по заданию к базе данных
[ApiController]
[Route("api/[controller]/{action}")]
public class QueriesController : Controller
{
    // контекст базы данных
    private WholesaleStoreContext _db;

    // В конструкторе получаем доступ к контексту базы данных
    public QueriesController(WholesaleStoreContext context) =>
        _db = context;


    // Запрос 1. Запрос с параметрами
    // Выбирает из информацию о товарах, единицей измерения которых является
    // «шт» (штуки) и цена закупки составляет меньше 200 руб. Значения
    // задавать параметрами
    [HttpGet("{unit}/{purchasePrice}")]
    public IActionResult Query01(string unit="шт", int purchasePrice=200) {
        // построение выборки данных
        var data = _db.Purchases!.AsNoTracking()
            .Include(r => r.Good)
            .Include(r => r.Unit)
            .Where(p => p.Unit.Short == unit && p.PricePurchase < purchasePrice)
            .AsQueryable();

        ViewBag.Title = $"Запрос 1. Выбрана информация о товарах, единицей измерения которых " +
                        $"является «{unit}» и цена закупки составляет меньше {purchasePrice} руб.";
        return View("Queries", data);
    } // Query01


    // Запрос 2. Запрос с параметрами
    // Выбирает информацию о товарах, цена закупки которых меньше 500 руб.
    // за единицу товара. Значения задавать параметрами
    [HttpGet("{purchasePrice}")]
    public IActionResult Query02(int purchasePrice = 500) {
        // построение выборки данных
        var data = _db.Purchases!.AsNoTracking()
            .Include(r => r.Good)
            .Include(r => r.Unit)
            .Where(p => p.PricePurchase < purchasePrice)
            .AsQueryable();

        ViewBag.Ttile = $"Запрос 2. Выбирана информация о товарах, цена закупки которых меньше " +
                        $"{purchasePrice} руб. за единицу товара.";
        return View("Queries", data);
    } // Query02


    // Запрос 3. Запрос с параметрами
    // Выбирает информацию о товарах с заданным наименованием (например, «чехол
    // защитный»), для которых цена закупки меньше 1800 руб. Значения задавать
    // параметрами
    [HttpGet("{goodsName}/{purchasePrice}")]
    public IActionResult Query03(string goodsName= "Кофе NESCAFE Классик растворимый", int purchasePrice = 300) {
        // построение выборки данных
        var data = _db.Purchases!.AsNoTracking()
            .Include(r => r.Good)
            .Include(r => r.Unit)
            .Where(p => p.Good.NameGood == goodsName &&  p.PricePurchase < purchasePrice)
            .AsQueryable();

        ViewBag.Title = $"Запрос 3. Выбирана информация о товаре с наименованием «{goodsName}», " +
                        $"для которых цена закупки меньше {purchasePrice} руб.";
        return View("Queries", data);
    } // Query03


    // Запрос 4
    // Выбирает информацию о продавцах с заданным значением процента комиссионных
    // Разметка с коллекцией продавцов и формой ввода параметра запроса
    [HttpGet("{interest}")]
    public IActionResult Query04(int? interest = 8) {
        var items = _db.Sellers
            .AsNoTracking()
            .Where(s => s.Interest == interest)
            .AsQueryable();
        ViewBag.Title = $"Запрос 4. Выбирана информация о продавцах с процентом комиссионных {interest}%.";
        return View(items);
    } // Query04


    // Запрос 5 Запрос с параметром
    // Выбирает информацию о зафиксированных фактах продажи для заданного
    // параметром продавца
    // получение параметра запроса, выборка и вывод выбранных данных
    [HttpGet("{sellerId}")]
    public IActionResult Query05(int sellerId = 1) {

        // вернуть выбранные записи
        var items = _db.Sales
            .AsNoTracking()
            .Include(s => s.Purchase)
            .ThenInclude(p => p.Good)
            .Include(s => s.Seller)
            .Where(s => s.SellerId == sellerId)
            .AsQueryable();

        return View(items);
    } // Query05


    // Запрос 6
    // Выбирает информацию обо всех зафиксированных фактах продажи товаров
    // (Наименование товара, Цена закупки, Цена продажи, дата продажи),
    // для которых Цена продажи оказалась в некоторых заданных границах.
    // Значения задавать параметрами
    // Разметка с коллекцией фактов продажи и формой ввода параметра запроса
    [HttpGet("{fromPrice}/{toPrice}")]
    public IActionResult Query06(int fromPrice = 50, int toPrice = 250) {
        // если диапазон цены не ввели, вернуть все записи из таблицы,
        // вернуть выбранные записи, если диапазон цены ввели  в форму
        var items = _db.Sales.AsNoTracking()
            .Include(r => r.Purchase)
            .ThenInclude(p => p.Good)
            .Where(s => fromPrice <= s.PriceSale && s.PriceSale <= toPrice)
            .AsQueryable();
        return View(items);
    } // Query06


    // Запрос 7
    // Вычисляет прибыль от продажи за каждый проданный товар. Включает поля Дата продажи,
    // Наименование товара, Цена закупки, Цена продажи, Количество проданных единиц, Прибыль.
    // Сортировка по полю Наименование товара
    [HttpGet]
    public IActionResult Query07() {
        var items = _db.Sales
            .AsNoTracking()
            .Include(r => r.Purchase)
            .ThenInclude(p => p.Good)
            // список необходим для Select() и OrderBy()
            .ToList()
            .Select(s => new Query07Result(
                s.Id, s.DateSale, s.Purchase!.Good!.NameGood!, s.Purchase.PricePurchase,
                s.PriceSale, s.AmountSale, s.PriceSale - s.Purchase.PricePurchase))

            .OrderBy(s => s.ProductName);
        return View(items);
    } // Query07


    // Запрос 8
    // Выполняет группировку по полю Наименование товара. Для каждого
    // наименования вычисляет среднюю цену закупки товара, количество
    // закупок
    public IActionResult Query08() {
        IQueryable<Query08Result> items = _db.Purchases
            .Include(p => p.Good)
            .AsNoTracking()
            .GroupBy(p => p.Good.NameGood,
                (key, group) => new Query08Result(
                    key,
                    group.Average(g => g.PricePurchase),
                    group.Count()));
        return View(items);
    } // Query08


    // Запрос 9
    // Выполняет группировку по полю Код продавца из таблицы ПРОДАЖИ.
    // Для каждого продавца вычисляет среднее значение по полю Цена
    // продажи единицы товара, количество продаж
    public IActionResult Query09() {
        IQueryable<Query09Result> items = _db.Sales
            .Include(s => s.Seller)
            .AsNoTracking()
            .GroupBy(s => new { Surname = s.Seller.Surname, Name = s.Seller.NameSeller, Patronymic = s.Seller.Patronymic },
                (key, group) => new Query09Result(
                    $"{key.Surname} {key.Name[0]}.{key.Patronymic[0]}.",
                    group.Average(g => g.PriceSale),
                    group.Count()));
        return View(items);
    } // Query09


    // Запрос 10. Итоговый запрос
    // Выполняет группировку по единицам измерений проданных товаров,
    // для каждой единицы измерения вычисляет сумму продаж и суммарное
    // количество проданного товара
    public IActionResult Query10() {
        IQueryable<Query09Result> items = _db.Sales
            .Include(s => s.Purchase)
            .ThenInclude(p => p.Good)
            .Include(s => s.Purchase)
            .ThenInclude(p => p.Unit)
            .AsNoTracking()
            .GroupBy(s => s.Purchase.Unit.Long,
                (key, group) => new Query09Result(
                    key,   // название едииницы измерения это тоже строка
                    group.Sum(g => g.PriceSale),
                    group.Count()));
        return View(items);
    } // Query10

    // Запрос 11. Итоговый запрос с левым соединением
    // Для всех продавцов вывести сумму и количество продаж, минимальную
    // и максимальную стоимости продаж

} // class QueriesController
