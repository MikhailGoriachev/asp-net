using HomeWork.Models;
using HomeWork.Models.BindingModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

public class PurchasesController : Controller
{
    // контекст базы данных
    public SalesAccountingContext DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public PurchasesController(SalesAccountingContext dataContext) =>
        DataContext = dataContext;

    #endregion

    #region Методы действия

    // фильтр закупок
    public async Task<IActionResult> FilterByAsync(FilterByPurchasesBindingModel model)
    {
        // элементы
        var items = DataContext.Purchases.AsNoTracking()
            .Include(p => p.Goods)
            .Include(p => p.Unit)
            .AsQueryable();

        ViewBag.GoodsName = "————";
        ViewBag.UnitName = "————";
        ViewBag.MaxPrice = "————";
        ViewBag.MinPrice = "————";

        // товар
        if (model.GoodsId != 0)
        {
            items = items.Where(i => i.GoodsId == model.GoodsId);

            ViewBag.GoodsName = (await DataContext.GoodsList.AsNoTracking().FirstAsync(g => g.Id == model.GoodsId))
                .Name;
        }

        // единица измерения
        if (model.UnitId != 0)
        {
            items = items.Where(i => i.UnitId == model.UnitId);

            ViewBag.UnitName = (await DataContext.Units.AsNoTracking().FirstAsync(g => g.Id == model.UnitId)).LongName;
        }

        // максимальная цена
        if (model.MaxPrice != null)
        {
            items = items.Where(i => i.Price <= model.MaxPrice);

            ViewBag.MaxPrice = model.MaxPrice;
        }

        // минимальная цена
        if (model.MinPrice != null)
        {
            items = items.Where(i => i.Price <= model.MinPrice);

            ViewBag.MinPrice = model.MinPrice;
        }

        // список единиц измерения
        var units = await DataContext.Units.AsNoTracking().ToListAsync();
        units.Insert(0, new Unit { LongName = "Все" });
        ViewBag.UnitList = new SelectList(units, nameof(Unit.Id), nameof(Unit.LongName));

        // список товаров
        var goods = await DataContext.GoodsList.AsNoTracking().ToListAsync();
        goods.Insert(0, new Goods { Name = "Все" });
        ViewBag.GoodsList = new SelectList(goods, nameof(Goods.Id), nameof(Goods.Name));

        model.Items = await items.ToListAsync();

        return View(model);
    }


    // добавление записи
    public async Task<IActionResult> AddPurchaseAsync()
    {
        ViewBag.IsAdd = true;

        ViewBag.GoodsList = new SelectList(await DataContext.GoodsList.AsNoTracking().ToListAsync(),
            nameof(Goods.Id), nameof(Goods.Name));

        ViewBag.UnitList = new SelectList(await DataContext.Units.AsNoTracking().ToListAsync(), nameof(Unit.Id),
            nameof(Unit.LongName));

        return View("PurchaseForm", new Purchase());
    }

    [HttpPost]
    public async Task<IActionResult> AddPurchase(Purchase purchase)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = true;

            ViewBag.GoodsList = new SelectList(await DataContext.GoodsList.AsNoTracking().ToListAsync(),
                nameof(Goods.Id), nameof(Goods.Name));

            ViewBag.UnitList = new SelectList(await DataContext.Units.AsNoTracking().ToListAsync(), nameof(Unit.Id),
                nameof(Unit.LongName));

            return View("PurchaseForm", purchase);
        }

        DataContext.Purchases!.Add(purchase);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Purchases", "SalesAccounting");
    }

    // редактирование записи
    public async Task<IActionResult> EditPurchaseAsync(int id)
    {
        ViewBag.IsAdd = false;

        ViewBag.GoodsList = new SelectList(await DataContext.GoodsList.AsNoTracking().ToListAsync(),
            nameof(Goods.Id), nameof(Goods.Name));

        ViewBag.UnitList = new SelectList(await DataContext.Units.AsNoTracking().ToListAsync(), nameof(Unit.Id),
            nameof(Unit.LongName));

        var purchase = await DataContext.Purchases.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id)!;

        return purchase != null ? View("PurchaseForm", purchase) : RedirectToAction("Purchases", "SalesAccounting");
    }

    [HttpPost]
    public async Task<IActionResult> EditPurchaseAsync(Purchase purchase)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = false;

            ViewBag.GoodsList = new SelectList(await DataContext.GoodsList.AsNoTracking().ToListAsync(),
                nameof(Goods.Id), nameof(Goods.Name));

            ViewBag.UnitList = new SelectList(await DataContext.Units.AsNoTracking().ToListAsync(), nameof(Unit.Id),
                nameof(Unit.LongName));

            return View("PurchaseForm", purchase);
        }

        DataContext.Purchases!.Update(purchase);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Purchases", "SalesAccounting");
    }

    #endregion
}
