using HomeWork.Models;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

public class UnitsController : Controller
{
    // контекст базы данных
    public SalesAccountingContext DataContext { get; set; }

    // размер страницы для вывода
    public int SizePage { get; set; } = 5;


    #region Конструкторы

    // конструктор инициализирующий
    public UnitsController(SalesAccountingContext dataContext) =>
        DataContext = dataContext;

    #endregion

    #region Методы действия

    // единицы измерения
    public async Task<IActionResult> IndexAsync(int pageNumber = 1) => View(
        new PaginationPageViewModel<Unit>(await DataContext.Units
                .Skip((pageNumber - 1) * SizePage)
                .Take(SizePage)
                .AsNoTracking()
                .ToListAsync(),
            new PageViewModel(await DataContext.Units.CountAsync(), pageNumber, SizePage)
        ));


    // добавление записи
    public IActionResult AddUnit()
    {
        ViewBag.IsAdd = true;

        return View("UnitForm", new Unit());
    }

    [HttpPost]
    public async Task<IActionResult> AddUnit(Unit unit)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = true;

            return View("UnitForm", unit);
        }

        DataContext.Units!.Add(unit);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Index", "Units");
    }

    // редактирование записи
    public async Task<IActionResult> EditUnitAsync(int id)
    {
        ViewBag.IsAdd = false;

        var unit = await DataContext.Units.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id)!;

        return unit != null ? View("UnitForm", unit) : RedirectToAction("Index", "Units");
    }

    [HttpPost]
    public async Task<IActionResult> EditUnit(Unit unit)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = false;

            return View("UnitForm", unit);
        }

        DataContext.Units!.Update(unit);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Index", "Units");
    }

    #endregion
}
