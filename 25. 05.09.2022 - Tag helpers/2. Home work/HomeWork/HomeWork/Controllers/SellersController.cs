using HomeWork.Models;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

public class SellersController : Controller
{
    // контекст базы данных
    public SalesAccountingContext DataContext { get; set; }

    // размер страницы для вывода
    public int SizePage { get; set; } = 5;


    #region Конструкторы

    // конструктор инициализирующий
    public SellersController(SalesAccountingContext dataContext) =>
        DataContext = dataContext;

    #endregion

    #region Методы действия

    // продавцы
    public IActionResult Index(int pageNumber = 1) => RedirectToAction("OrderBySellers", new
    {
        sortField = "Id",
        pageNumber = 0,
        sortState = SortStateSales.IdAsc
    });


    // сортировка продавцов
    [HttpGet("[controller]/[action]/{sortField}/{pageNumber:int}/{sortState}")]
    public async Task<IActionResult> OrderBySellersAsync(string sortField, int pageNumber, SortStateSellers sortState)
    {
        // коллекция поездок
        var items = DataContext.Sellers.AsNoTracking();

        // текущий тип сортировки
        ViewBag.SortState = sortState;

        // установка названия поля сортировки
        ViewBag.SortField = sortField;

        if (pageNumber != 0)
        {
            ViewBag.IdSort = sortState == SortStateSellers.IdAsc
                ? SortStateSellers.IdDesc
                : SortStateSellers.IdAsc;
            ViewBag.SurnameSort = sortState == SortStateSellers.SurnameAsc
                ? SortStateSellers.SurnameDesc
                : SortStateSellers.SurnameAsc;
            ViewBag.InterestSort = sortState == SortStateSellers.InterestAsc
                ? SortStateSellers.InterestDesc
                : SortStateSellers.InterestAsc;
        }
        else pageNumber++;

        // сортировка
        items = sortState switch
        {
            // по идентификатору
            SortStateSellers.IdAsc => items.OrderBy(s => s.Id),
            SortStateSellers.IdDesc => items.OrderByDescending(s => s.Id),

            // по дате начала поездки
            SortStateSellers.SurnameAsc => items.OrderBy(s => s.Surname),
            SortStateSellers.SurnameDesc => items.OrderByDescending(s => s.Surname),

            // по длительности
            SortStateSellers.InterestAsc => items.OrderBy(s => s.Interest),
            SortStateSellers.InterestDesc => items.OrderByDescending(s => s.Interest),

            _ => items.OrderBy(s => s.Id)
        };

        return View("Index", new PaginationPageViewModel<Seller>(
            await items.Skip((pageNumber - 1) * SizePage)
                .Take(SizePage)
                .ToListAsync(),
            new PageViewModel(await DataContext.Sellers.CountAsync(), pageNumber, SizePage)));
    }


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

        return RedirectToAction("Index", "Sellers");
    }

    // редактирование записи
    public async Task<IActionResult> EditSellerAsync(int id)
    {
        ViewBag.IsAdd = false;

        var seller = await DataContext.Sellers.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id)!;

        return seller != null ? View("SellerForm", seller) : RedirectToAction("Index", "Sellers");
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

        return RedirectToAction("Index", "Sellers");
    }

    #endregion
}
