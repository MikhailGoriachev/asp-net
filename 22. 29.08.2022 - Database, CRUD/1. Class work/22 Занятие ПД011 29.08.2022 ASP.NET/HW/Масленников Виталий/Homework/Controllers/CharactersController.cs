using Homework.Infrastructure;
using Homework.Models;
using Homework.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Homework.Controllers;

[ServiceFilter(typeof(ProfileAttribute))]
public class CharactersController : Controller
{
    private const int CharactersToShow = 42;
    private readonly SWAPIService _swapiService;

    public CharactersController(SWAPIService swapiService) => _swapiService = swapiService;
    
    public async Task<IActionResult> Index()
    {
        // Название компонента, загружаемого на странице
        ViewBag.Component = $"{nameof(Components.SwCharacters)}";
        // Параметры для компонента
        ViewBag.ComponentParameters = new { amount = CharactersToShow };
        
        return View(new SelectList(await GetHomeworldsListOfFirst(CharactersToShow)));
    }
    
    // GET Упорядочивание по массе
    public async Task<IActionResult> OrderByMass()
    {
        ViewBag.Component = $"{nameof(Components.SwCharactersOrderByMass)}";
        ViewBag.ComponentParameters = new { amount = CharactersToShow };
        return View("Index",new SelectList(await GetHomeworldsListOfFirst(CharactersToShow)));
    }
    
    // POST Выборка по указанному диапазону массы
    [HttpPost]
    public async Task<IActionResult> ByMass(int from, int to)
    {
        ViewBag.Component = $"{nameof(Components.SwCharactersByMass)}";
        ViewBag.ComponentParameters = new { amount = CharactersToShow, from, to };
        return View("Index",new SelectList(await GetHomeworldsListOfFirst(CharactersToShow)));
    }
    
    // Выыборка по миру
    public async Task<IActionResult> ByHomeworld(string homeworld)
    {
        ViewBag.Component = $"{nameof(Components.SwCharactersByHomeworld)}";
        ViewBag.ComponentParameters = new { amount = CharactersToShow, homeworld };
        return View("Index",new SelectList(await GetHomeworldsListOfFirst(CharactersToShow)));
    }
    
    // Подробная информация о персонаже
    public IActionResult CharacterInfo(int id) => View(id);

    // Получить список уникальных значения названий родных планет для количества персонажей
    private async Task<IEnumerable<string>> GetHomeworldsListOfFirst(int amount) =>
        (await _swapiService.GetCharactersListAsync(amount))
        !.Select(c => c.Homeworld).Distinct();
}