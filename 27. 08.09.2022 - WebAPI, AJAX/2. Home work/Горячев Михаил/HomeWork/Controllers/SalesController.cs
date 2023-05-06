using HomeWork.Models;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

[Route("[controller]/[action]")]
[ApiController]
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

    // продажи
    [HttpGet]
    public async Task<IActionResult> GetAsync() => Json(await DataContext.Sales.AsNoTracking()
        .AsNoTracking()
        .Include(s => s.Purchase)
        .ThenInclude(p => p!.Goods)
        .Include(p => p.Purchase!.Unit)
        .Include(s => s.Seller)
        .Include(s => s.Unit)
        .ToListAsync()
    );


    // получить данные записи по id
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id) =>
        Json(await DataContext.Sales.AsNoTracking()
            .Include(s => s.Purchase)
            .ThenInclude(p => p!.Goods)
            .Include(p => p.Purchase!.Unit)
            .Include(s => s.Seller)
            .Include(s => s.Unit)
            .FirstOrDefaultAsync(s => s.Id == id));


    // добавление продажи
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromForm]Sale sale)
    {
        DataContext.Sales!.Add(sale);
        await DataContext.SaveChangesAsync();

        return NoContent();
    }


    // редактирование записи
    [HttpPut]
    public async Task<IActionResult> PutAsync([FromForm]Sale sale)
    {
        DataContext.Sales!.Update(sale);
        await DataContext.SaveChangesAsync();

        return NoContent();
    }


    // удаление продажи
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        DataContext.Entry(new Sale { Id = id }).State = EntityState.Deleted;
        await DataContext.SaveChangesAsync();

        return NoContent();
    }

    #endregion
}
