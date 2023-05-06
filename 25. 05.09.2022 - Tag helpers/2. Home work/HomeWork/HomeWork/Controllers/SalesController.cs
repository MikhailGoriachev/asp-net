using HomeWork.Models;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

public class SalesController : Controller
{
    // контекст базы данных
    public SalesAccountingContext DataContext { get; set; }

    // размер страницы для вывода
    public int SizePage { get; set; } = 5;


    #region Конструкторы

    // конструктор инициализирующий
    public SalesController(SalesAccountingContext dataContext) =>
        DataContext = dataContext;

    #endregion

    #region Методы действия

    // продажи
    public IActionResult Index() => RedirectToAction("OrderBySales", new
    {
        sortField = "Id",
        pageNumber = 0,
        sortState = SortStateSales.IdAsc
    });


    // сортировка продаж
    [HttpGet("[controller]/[action]/{sortField}/{pageNumber:int}/{sortState}")]
    public async Task<IActionResult> OrderBySalesAsync(string sortField, int pageNumber, SortStateSales sortState)
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
            // по идентификатору
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

        return View("Index", new PaginationPageViewModel<Sale>(
            await items.Skip((pageNumber - 1) * SizePage)
                .Take(SizePage)
                .ToListAsync(),
            new PageViewModel(await DataContext.Sales.CountAsync(), pageNumber, SizePage)));
    }


    // добавление записи
    public async Task<IActionResult> AddSaleAsync()
    {
        ViewBag.IsAdd = true;

        ViewBag.SellerList = new SelectList(await DataContext.Sellers.AsNoTracking().ToListAsync(),
            nameof(Seller.Id),
            nameof(Seller.IdAndFullName)
        );

        ViewBag.UnitList = new SelectList(await DataContext.Units.AsNoTracking().ToListAsync(),
            nameof(Unit.Id),
            nameof(Unit.LongName)
        );

        ViewBag.PurchaseList = new SelectList(
            await DataContext.Purchases.AsNoTracking().Include(p => p.Goods).ToListAsync(),
            nameof(Purchase.Id),
            nameof(Purchase.StringInfoPurchase)
        );


        return View("SaleForm", new Sale());
    }

    [HttpPost]
    public async Task<IActionResult> AddSale(Sale sale)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = true;

            ViewBag.SellerList = new SelectList(await DataContext.Sellers.AsNoTracking().ToListAsync(),
                nameof(Seller.Id),
                nameof(Seller.IdAndFullName)
            );

            ViewBag.UnitList = new SelectList(await DataContext.Units.AsNoTracking().ToListAsync(),
                nameof(Unit.Id),
                nameof(Unit.LongName)
            );

            ViewBag.PurchaseList = new SelectList(
                await DataContext.Purchases.AsNoTracking().Include(p => p.Goods).ToListAsync(),
                nameof(Purchase.Id),
                nameof(Purchase.StringInfoPurchase)
            );

            return View("SaleForm", sale);
        }

        DataContext.Sales!.Add(sale);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Index", "Sales");
    }

    // редактирование записи
    public async Task<IActionResult> EditSaleAsync(int id)
    {
        ViewBag.IsAdd = false;

        ViewBag.SellerList = new SelectList(await DataContext.Sellers.AsNoTracking().ToListAsync(),
            nameof(Seller.Id),
            nameof(Seller.IdAndFullName)
        );

        ViewBag.UnitList = new SelectList(await DataContext.Units.AsNoTracking().ToListAsync(),
            nameof(Unit.Id),
            nameof(Unit.LongName)
        );

        ViewBag.PurchaseList = new SelectList(
            await DataContext.Purchases.AsNoTracking().Include(p => p.Goods).ToListAsync(),
            nameof(Purchase.Id),
            nameof(Purchase.StringInfoPurchase)
        );

        var sale = await DataContext.Sales.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id)!;

        return sale != null ? View("SaleForm", sale) : RedirectToAction("Index", "Sales");
    }

    [HttpPost]
    public async Task<IActionResult> EditSale(Sale sale)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = false;

            ViewBag.SellerList = new SelectList(await DataContext.Sellers.AsNoTracking().ToListAsync(),
                nameof(Seller.Id),
                nameof(Seller.IdAndFullName)
            );

            ViewBag.UnitList = new SelectList(await DataContext.Units.AsNoTracking().ToListAsync(),
                nameof(Unit.Id),
                nameof(Unit.LongName)
            );

            ViewBag.PurchaseList = new SelectList(
                await DataContext.Purchases.AsNoTracking().Include(p => p.Goods).ToListAsync(),
                nameof(Purchase.Id),
                nameof(Purchase.StringInfoPurchase)
            );

            return View("SaleForm", sale);
        }

        DataContext.Sales!.Update(sale);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Index", "Sales");
    }


    // удаление продажи
    public async Task<IActionResult> RemoveSale(int id)
    {
        DataContext.Entry(new Sale { Id = id }).State = EntityState.Deleted;
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Index", "Sales");
    }

    #endregion
}
