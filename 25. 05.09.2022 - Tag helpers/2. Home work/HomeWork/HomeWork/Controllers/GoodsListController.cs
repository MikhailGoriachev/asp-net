using HomeWork.Models;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

public class GoodsListController : Controller
{
    // контекст базы данных
    public SalesAccountingContext DataContext { get; set; }

    // размер страницы для вывода
    public int SizePage { get; set; } = 5;


    #region Конструкторы

    // конструктор инициализирующий
    public GoodsListController(SalesAccountingContext dataContext) =>
        DataContext = dataContext;

    #endregion

    #region Методы действия

    // товары
    public async Task<IActionResult> IndexAsync(int pageNumber = 1) => View(
        new PaginationPageViewModel<Goods>(await DataContext.GoodsList.AsNoTracking()
                .Skip((pageNumber - 1) * SizePage)
                .Take(SizePage)
                .AsNoTracking()
                .ToListAsync(),
            new PageViewModel(await DataContext.GoodsList.CountAsync(), pageNumber, SizePage)
        ));


    // добавление записи
    public IActionResult AddGoods()
    {
        ViewBag.IsAdd = true;

        return View("GoodsForm", new Goods());
    }

    [HttpPost]
    public async Task<IActionResult> AddGoods(Goods goods)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = true;

            return View("GoodsForm", goods);
        }

        DataContext.GoodsList!.Add(goods);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Index", "GoodsList");
    }

    // редактирование записи
    public async Task<IActionResult> EditGoodsAsync(int id)
    {
        ViewBag.IsAdd = false;

        var goods = await DataContext.GoodsList.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id)!;

        return goods != null ? View("GoodsForm", goods) : RedirectToAction("Index", "GoodsList");
    }

    [HttpPost]
    public async Task<IActionResult> EditGoods(Goods goods)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = false;

            return View("GoodsForm", goods);
        }

        DataContext.GoodsList!.Update(goods);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Index", "GoodsList");
    }

    #endregion
}
