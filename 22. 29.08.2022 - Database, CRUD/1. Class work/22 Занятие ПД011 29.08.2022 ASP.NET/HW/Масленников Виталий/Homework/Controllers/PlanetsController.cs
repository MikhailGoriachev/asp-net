using Homework.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Homework.Controllers;

public class PlanetsController : Controller
{
    private const int PlanetsToShow = 33;
    private readonly SWAPIService _swapiService;
    
    public PlanetsController(SWAPIService swapiService) => _swapiService = swapiService;
    
    public async Task<IActionResult> Index()
    {
        // Название компонента, загружаемого на странице
        ViewBag.Component = $"{nameof(Components.SwPlanets)}";
        // Параметры для компонента
        ViewBag.ComponentParameters = new { amount = PlanetsToShow };
        
        return View(new SelectList(await GetTerrainsListOfFirst(PlanetsToShow)));
    }
    
    // Отборка по типу поверхности
    public async Task<IActionResult> ByTerrain(string terrain)
    {
        ViewBag.Component = $"{nameof(Components.SwPlanetsByTerrain)}";
        ViewBag.ComponentParameters = new { amount = PlanetsToShow, terrain };
        return View("Index",new SelectList(await GetTerrainsListOfFirst(PlanetsToShow)));
    }
    
    // Выборка по указанному диапазону диаметров
    public async Task<IActionResult> ByDiameter(int from, int to)
    {
        ViewBag.Component = $"{nameof(Components.SwPlanetsByDiameter)}";
        ViewBag.ComponentParameters = new { amount = PlanetsToShow, from, to };
        return View("Index",new SelectList(await GetTerrainsListOfFirst(PlanetsToShow)));
    }
    
    // Упорядочивание по убыванию диаметров
    public async Task<IActionResult> OrderByDiameterDesc()
    {
        ViewBag.Component = $"{nameof(Components.SwPlanetsOrderByDiameterDesc)}";
        ViewBag.ComponentParameters = new { amount = PlanetsToShow };
        return View("Index",new SelectList(await GetTerrainsListOfFirst(PlanetsToShow)));
    }
    
    // Подробная информация о планете
    public IActionResult PlanetInfo(int id) => View(id);

    // Получить список уникальных значений поверхностей для количества планет
    private async Task<IEnumerable<string>> GetTerrainsListOfFirst(int amount) => 
        (await _swapiService.GetPlanetsListAsync(amount))
        .Select(p => p.Terrain.Split(", "))
        .SelectMany(x => x);
}