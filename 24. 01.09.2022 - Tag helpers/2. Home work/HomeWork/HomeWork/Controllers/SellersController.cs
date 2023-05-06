using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

public class SellersController : Controller
{
    // контекст базы данных
    public SalesAccountingContext DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public SellersController(SalesAccountingContext dataContext) =>
        DataContext = dataContext;

    #endregion

    #region Методы действия


    // добавление записи
    public IActionResult AddSeller()
    {
        ViewBag.IsAdd = true;

        return View("SellerForm", new Seller());
    }

    [HttpPost]
    public async Task<IActionResult> AddSeller(Seller seller)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = true;

            return View("SellerForm", seller);
        }

        DataContext.Sellers!.Add(seller);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Sellers", "SalesAccounting");
    }

    // редактирование записи
    public async Task<IActionResult> EditSellerAsync(int id)
    {
        ViewBag.IsAdd = false;

        var seller = await DataContext.Sellers.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id)!;

        return seller != null ? View("SellerForm", seller) : RedirectToAction("Sellers", "SalesAccounting");
    }

    [HttpPost]
    public async Task<IActionResult> EditSeller(Seller seller)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = false;

            return View("SellerForm", seller);
        }

        DataContext.Sellers!.Update(seller);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Sellers", "SalesAccounting");
    }

    #endregion
}
