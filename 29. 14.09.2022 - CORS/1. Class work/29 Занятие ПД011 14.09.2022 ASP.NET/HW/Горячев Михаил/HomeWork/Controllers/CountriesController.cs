using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CountriesController : Controller
{
    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public CountriesController(TouristAgencyContext dataContext) =>
        DataContext = dataContext;

    #endregion

    #region Методы действия

    // все клиенты
    [HttpGet]
    public async Task<IActionResult> GetAsync() =>
        Json(await DataContext.Countries!.AsNoTracking().ToListAsync());

    #endregion
}
