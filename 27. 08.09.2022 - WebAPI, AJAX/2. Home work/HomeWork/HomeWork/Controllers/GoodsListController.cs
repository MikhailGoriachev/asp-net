using HomeWork.Models;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class GoodsListController : Controller
{
    // контекст базы данных
    public SalesAccountingContext DataContext { get; set; }


    #region Конструкторы

    // конструктор инициализирующий
    public GoodsListController(SalesAccountingContext dataContext) =>
        DataContext = dataContext;

    #endregion

    #region Методы действия

    // товары
    // [HttpGet]
    // public async Task<IActionResult> GetAsync() => Json(
    //     await DataContext.GoodsList.AsNoTracking()
    //         .AsNoTracking()
    //         .ToListAsync()
    // );
    //
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Json(
            await DataContext.GoodsList.AsNoTracking()
                .AsNoTracking()
                .ToListAsync()
        );
    }


    // // добавление записи
    // public IActionResult AddGoods()
    // {
    //     ViewBag.IsAdd = true;
    //
    //     return View("GoodsForm", new Goods());
    // }
    //
    // [HttpPost]
    // public async Task<IActionResult> AddGoods(Goods goods)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         ViewBag.IsAdd = true;
    //
    //         return View("GoodsForm", goods);
    //     }
    //
    //     DataContext.GoodsList!.Add(goods);
    //     await DataContext.SaveChangesAsync();
    //
    //     return RedirectToAction("Index", "GoodsList");
    // }
    //
    // // редактирование записи
    // public async Task<IActionResult> EditGoodsAsync(int id)
    // {
    //     ViewBag.IsAdd = false;
    //
    //     var goods = await DataContext.GoodsList.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id)!;
    //
    //     return goods != null ? View("GoodsForm", goods) : RedirectToAction("Index", "GoodsList");
    // }
    //
    // [HttpPost]
    // public async Task<IActionResult> EditGoods(Goods goods)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         ViewBag.IsAdd = false;
    //
    //         return View("GoodsForm", goods);
    //     }
    //
    //     DataContext.GoodsList!.Update(goods);
    //     await DataContext.SaveChangesAsync();
    //
    //     return RedirectToAction("Index", "GoodsList");
    // }

    #endregion
}
