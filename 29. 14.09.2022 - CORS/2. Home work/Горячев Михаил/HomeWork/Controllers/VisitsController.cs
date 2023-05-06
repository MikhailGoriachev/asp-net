using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class VisitsController : Controller
{
    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public VisitsController(TouristAgencyContext dataContext) =>
        DataContext = dataContext;

    #endregion

    #region Методы действия

    // все клиенты
    [HttpGet]
    public async Task<IActionResult> GetAsync() =>
        Json(await DataContext.Visits!.AsNoTracking()
            .Include(v => v.Client)
            .Include(v => v.Route)
            .ThenInclude(r => r!.Country)
            .Include(v => v.Route!.Objective)
            .ToListAsync());

    // клиент
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id) =>
        Json(await DataContext.Visits!.AsNoTracking()
            .Include(v => v.Client)
            .Include(v => v.Route)
            .ThenInclude(r => r!.Country)
            .Include(v => v.Route!.Objective)
            .FirstOrDefaultAsync(v => v.Id == id));


    // добавление поездки
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromForm]Visit visit)
    {
        DataContext.Visits!.Add(visit);
        await DataContext.SaveChangesAsync();

        return NoContent();
    }


    // редактирование поездки
    [HttpPut]
    public async Task<IActionResult> PutAsync([FromForm]Visit visit)
    {
        DataContext.Visits!.Update(visit);
        await DataContext.SaveChangesAsync();

        return NoContent();
    }


    // удаление поездки
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        DataContext.Entry(new Visit { Id = id }).State = EntityState.Deleted;
        await DataContext.SaveChangesAsync();

        return NoContent();
    }

    #endregion
}
