using HomeWork.Models;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            return View(new FilterByPurchaseViewModel(await items.ToListAsync(), 0, 0, null));

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

        // цена
        if (model.Price != null)
        {
            items = items.Where(i => i.PricePurchase < model.Price || i.PricePurchase > model.Price);
            ViewBag.Price = model.Price;
        }

        model.Purchases = items;

        return View(model);
    }

    // Выбирает информацию о продавцах с заданным значением процента комиссионных
    public IActionResult Query04()
    {
        ViewBag.InterestList = new SelectList(_db.Sellers!.AsNoTracking(),"Id","Interest" );

        return View(_db.Sellers.AsNoTracking());

    }

    [HttpPost]
    public IActionResult Query04(int Interest)
    {
        ViewBag.InterestList = new SelectList(_db.Sellers!.AsNoTracking(), "Id", "Interest");

        return View(_db.Sellers
            .Where(s => s.Interest == Interest)
            .AsNoTracking());
    }


} // class QueriesController
