using HomeWork.Models;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

public class WholesaleStoreController : Controller
{
    // контекст базы данных
    private WholesaleStoreContext _db;

    // В конструкторе получаем доступ к контексту базы данных
    public WholesaleStoreController(WholesaleStoreContext context) =>    
        _db = context;


    // количество товаров на странице
    public int ItemPageSize => 5;

    // вывод записей таблицы "Товары"
    public async Task<IActionResult> Goods(int pageNumber = 1) =>
        View(new PaginationPageViewModel<Good>(
            await _db.Goods!.AsNoTracking()
                     .Skip((pageNumber - 1) * ItemPageSize)
                     .Take(ItemPageSize)
                     .ToListAsync(),
            new PageViewModel(await _db.Goods!.CountAsync(), pageNumber, ItemPageSize)));

    #region CRUD для таблицы "Товары"
    // добавление продукта
    public IActionResult AddGood()
    {
        ViewBag.IsAdd = true;

        return View("GoodForm", new Good());
    }

    [HttpPost]
    public async Task<IActionResult> AddGood(Good good)
    {

        _db.Goods!.Add(good);
        await _db.SaveChangesAsync();

        return RedirectToAction("Goods");
    }

    // редактирование продукта
    public async Task<IActionResult> EditGood(int id)
    {
        ViewBag.IsAdd = false;

        var good = await _db.Goods?.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id)!;

        return good != null ? View("GoodForm", good) : RedirectToAction("Goods");
    }

    [HttpPost]
    public async Task<IActionResult> EditGood(Good good)
    {
        _db.Goods!.Update(good);
        await _db.SaveChangesAsync();

        return RedirectToAction("Goods");
    }

    #endregion

    // вывод записей таблицы "Подавцы"
    public async Task<IActionResult> Sellers(int pageNumber = 1) =>
        View(new PaginationPageViewModel<Seller>(
            await _db.Sellers!.AsNoTracking()
                     .Skip((pageNumber - 1) * ItemPageSize)
                     .Take(ItemPageSize)
                     .ToListAsync(),
            new PageViewModel(await _db.Sellers!.CountAsync(), pageNumber, ItemPageSize)));


    #region CRUD для таблицы "Продавцы"
    // добавление продавца
    public IActionResult AddSeller()
    {
        ViewBag.IsAdd = true;

        return View("SellerForm", new Seller());
    }

    [HttpPost]
    public async Task<IActionResult> AddSeller(Seller seller)
    {

        _db.Sellers!.Add(seller);
        await _db.SaveChangesAsync();

        return RedirectToAction("Sellers");
    }

    // редактирование продавца
    public async Task<IActionResult> EditSeller(int id)
    {
        ViewBag.IsAdd = false;

        var seller = await _db.Sellers?.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id)!;

        return seller != null ? View("SellerForm", seller) : RedirectToAction("Sellers");
    }

    [HttpPost]
    public async Task<IActionResult> EditSeller(Seller seller)
    {
        _db.Sellers!.Update(seller);
        await _db.SaveChangesAsync();

        return RedirectToAction("Sellers");
    }

    #endregion


    // вывод записей таблицы "Закупки"
    public async Task<IActionResult> Purchases(int pageNumber = 1) =>
        View(new PaginationPageViewModel<Purchase>(
            await _db.Purchases!.AsNoTracking()
                     .Skip((pageNumber - 1) * ItemPageSize)
                     .Take(ItemPageSize)
                     .Include(r => r.Good)
                     .Include(r => r.Unit) 
                     .ToListAsync(),
            new PageViewModel(await _db.Purchases!.CountAsync(), pageNumber, ItemPageSize)));


    #region CRUD для таблицы "Закупки"
    // добавление закупки
    public IActionResult AddPurchase()
    {
        ViewBag.IsAdd = true;
        ViewBag.UnitList = new SelectList(_db.Units, "Id", "Short");
        ViewBag.GoodList = new SelectList(_db.Goods, "Id", "NameGood");


        return View("PurchaseForm", new Purchase());
    }

    [HttpPost]
    public async Task<IActionResult> AddPurchase(Purchase purchase)
    {
        ViewBag.UnitList = new SelectList(_db.Units, "Id", "Short");
        ViewBag.GoodList = new SelectList(_db.Goods, "Id", "NameGood");

        _db.Purchases!.Add(purchase);
        await _db.SaveChangesAsync();

        return RedirectToAction("Purchases");
    }

    // редактирование закупки
    public async Task<IActionResult> EditPurchase(int id)
    {
        ViewBag.IsAdd = false;

        var purchase = await _db.Purchases?.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id)!;

        ViewBag.UnitList = new SelectList(_db.Units, "Id", "Short");
        ViewBag.GoodList = new SelectList(_db.Goods, "Id", "NameGood");

        return purchase != null ? View("PurchaseForm", purchase) : RedirectToAction("Purchases");
    }

    [HttpPost]
    public async Task<IActionResult> EditPurchase(Purchase purchase)
    {
        ViewBag.UnitList = new SelectList(_db.Units, "Id", "Short");
        ViewBag.GoodList = new SelectList(_db.Goods, "Id", "NameGood");

        _db.Purchases!.Update(purchase);
        await _db.SaveChangesAsync();

        return RedirectToAction("Purchases");
    }

    #endregion

    // вывод записей таблицы "Продажи"
    public async Task<IActionResult> Sales(int pageNumber = 1) =>
        View(new PaginationPageViewModel<Sale>(
            await _db.Sales!.AsNoTracking()
                     .Skip((pageNumber - 1) * ItemPageSize)
                     .Take(ItemPageSize)
                     .Include(r => r.Purchase)
                     .ThenInclude(p => p.Good)
                     .Include(r => r.Seller)
                     .ToListAsync(),
            new PageViewModel(await _db.Sales!.CountAsync(), pageNumber, ItemPageSize)));


    #region CRUD для таблицы "Продажи"
    // добавление продажи
    public IActionResult AddSale()
    {
        ViewBag.IsAdd = true;
        ViewBag.GoodList = new SelectList(_db.Goods, "Id", "NameGood");
        ViewBag.SellerList = new SelectList(_db.Sellers, "Id", "Surname");


        return View("SalesForm", new Sale());
    }

    [HttpPost]
    public async Task<IActionResult> AddSale(Sale sale)
    {
        ViewBag.GoodList = new SelectList(_db.Goods, "Id", "NameGood");
        ViewBag.SellerList = new SelectList(_db.Sellers, "Id", "Surname");

        _db.Sales!.Add(sale);
        await _db.SaveChangesAsync();

        return RedirectToAction("Sales");
    }

    // редактирование продажи
    public async Task<IActionResult> EditSale(int id)
    {
        ViewBag.IsAdd = false;

        var sale = await _db.Sales?.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id)!;

        ViewBag.GoodList = new SelectList(_db.Goods, "Id", "NameGood");
        ViewBag.SellerList = new SelectList(_db.Sellers, "Id", "Surname");

        return sale != null ? View("SaleForm", sale) : RedirectToAction("Sales");
    }

    [HttpPost]
    public async Task<IActionResult> EditSale(Sale sale)
    {
        ViewBag.GoodList = new SelectList(_db.Goods, "Id", "NameGood");
        ViewBag.SellerList = new SelectList(_db.Sellers, "Id", "Surname");

        _db.Sales!.Update(sale);
        await _db.SaveChangesAsync();

        return RedirectToAction("Sales");
    }


    // удаление продажи
    public async Task<IActionResult> RemoveSale(int id)
    {
        _db.Entry(new Sale { Id = id }).State = EntityState.Deleted;
        await _db.SaveChangesAsync();

        return RedirectToAction("Sales");
    }

    #endregion



} // class WholesaleStoreController
