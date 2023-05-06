using HomeWork.Models;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

public class SalesAccountingController : Controller
{
    // контекст базы данных
    public SalesAccountingContext DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public SalesAccountingController(SalesAccountingContext dataContext) =>
        DataContext = dataContext;

    // размер страницы для вывода
    public int SizePage { get; set; } = 5;

    #endregion

    #region Методы действия

    // единицы измерения
    public async Task<IActionResult> Units(int pageNumber = 1) => View(
        new PaginationPageViewModel<Unit>(await DataContext.Units
                .Skip((pageNumber - 1) * SizePage)
                .Take(SizePage)
                .AsNoTracking()
                .ToListAsync(),
            new PageViewModel(await DataContext.Units.CountAsync(), pageNumber, SizePage)
        ));


    // товары
    public async Task<IActionResult> GoodsListAsync(int pageNumber = 1) => View(
        new PaginationPageViewModel<Goods>(await DataContext.GoodsList.AsNoTracking()
                .Skip((pageNumber - 1) * SizePage)
                .Take(SizePage)
                .AsNoTracking()
                .ToListAsync(),
            new PageViewModel(await DataContext.GoodsList.CountAsync(), pageNumber, SizePage)
        ));


    // продавцы

    public async Task<IActionResult> Sellers(int pageNumber = 1) => View(
        new PaginationPageViewModel<Seller>(await DataContext.Sellers.AsNoTracking()
                .Skip((pageNumber - 1) * SizePage)
                .Take(SizePage)
                .AsNoTracking()
                .ToListAsync(),
            new PageViewModel(await DataContext.Sellers.CountAsync(), pageNumber, SizePage)
        ));


    // закупки
    public async Task<IActionResult> Purchases(int pageNumber = 1) => View(
        new PaginationPageViewModel<Purchase>(await DataContext.Purchases.AsNoTracking()
                .Skip((pageNumber - 1) * SizePage)
                .Take(SizePage)
                .AsNoTracking()
                .Include(p => p.Goods)
                .Include(p => p.Unit)
                .ToListAsync(),
            new PageViewModel(await DataContext.Purchases.CountAsync(), pageNumber, SizePage)
        ));


    // продажи
    public IActionResult Sales() => RedirectToAction("OrderBySales", new
    {
        sortField = "Id",
        pageNumber = 0,
        sortState = SortStateSales.IdAsc
    });


    // public async Task<IActionResult> Sales(int pageNumber = 1) => View(
    //     new PaginationPageViewModel<Sale>(await DataContext.Sales.AsNoTracking()
    //             .Skip((pageNumber - 1) * SizePage)
    //             .Take(SizePage)
    //             .AsNoTracking()
    //             .Include(s => s.Purchase)
    //             .ThenInclude(p => p!.Goods)
    //             .Include(p => p.Purchase!.Unit)
    //             .Include(s => s.Seller)
    //             .Include(s => s.Unit)
    //             .ToListAsync(),
    //         new PageViewModel(await DataContext.Sales.CountAsync(), pageNumber, SizePage)
    //     ));

    // сортировка продаж
    [HttpGet("[controller]/[action]/{sortField}/{pageNumber:int}/{sortState}")]
    public async Task<IActionResult> OrderBySales(string sortField, int pageNumber, SortStateSales sortState)
    {
        // коллекция поездок
        var items = DataContext.Sales.AsNoTracking()
            .AsNoTracking()
            .Include(s => s.Purchase)
            .ThenInclude(p => p!.Goods)
            .Include(p => p.Purchase!.Unit)
            .Include(s => s.Seller)
            .Include(s => s.Unit)
            .AsQueryable();

        // текущий тип сортировки
        ViewBag.SortState = sortState;

        // установка названия поля сортировки
        ViewBag.SortField = sortField;

        if (pageNumber != 0)
        {
            ViewBag.IdSort = sortState == SortStateSales.IdAsc
                ? SortStateSales.IdDesc
                : SortStateSales.IdAsc;
            ViewBag.DateSellSort = sortState == SortStateSales.DateSellAsc
                ? SortStateSales.DateSellDesc
                : SortStateSales.DateSellAsc;
            ViewBag.PriceSaleSort = sortState == SortStateSales.PriceSaleAsc
                ? SortStateSales.PriceSaleDesc
                : SortStateSales.PriceSaleAsc;
        }
        else pageNumber++;

        // сортировка
        items = sortState switch
        {
            // по дате начала поездки
            SortStateSales.IdAsc => items.OrderBy(s => s.Id),
            SortStateSales.IdDesc => items.OrderByDescending(s => s.Id),

            // по дате начала поездки
            SortStateSales.DateSellAsc => items.OrderBy(s => s.DateSell.Date),
            SortStateSales.DateSellDesc => items.OrderByDescending(s => s.DateSell.Date),

            // по длительности
            SortStateSales.PriceSaleAsc => items.OrderBy(s => s.Price),
            SortStateSales.PriceSaleDesc => items.OrderByDescending(s => s.Price),

            _ => items.OrderBy(s => s.Id)
        };

        return View("Sales", new PaginationPageViewModel<Sale>(
            await items.Skip((pageNumber - 1) * SizePage)
                .Take(SizePage)
                .ToListAsync(),
            new PageViewModel(await DataContext.Sales.CountAsync(), pageNumber, SizePage)));
    }

    #endregion
}
