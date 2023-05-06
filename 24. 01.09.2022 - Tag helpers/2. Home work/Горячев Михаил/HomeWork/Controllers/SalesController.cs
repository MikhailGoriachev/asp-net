using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

public class SalesController : Controller
{
    // контекст базы данных
    public SalesAccountingContext DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public SalesController(SalesAccountingContext dataContext) =>
        DataContext = dataContext;

    #endregion

    #region Методы действия

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

        return RedirectToAction("Sales", "SalesAccounting");
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

        return sale != null ? View("SaleForm", sale) : RedirectToAction("Sales", "SalesAccounting");
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

        return RedirectToAction("Sales", "SalesAccounting");
    }

    #endregion
}
