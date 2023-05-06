using HomeWork.Models.Task2;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

public class RegisteredPlanets : ViewComponent
{
    private readonly PlanetService _service;

    // получить связь с данными через внедрение зависимостей 
    public RegisteredPlanets(PlanetService service)
    {
        _service = service;
    } // RegisteredPlanets


    public async Task<IViewComponentResult> InvokeAsync()
    {
        // получение данных должно быть асинхронным
        List<Planet> planets = await _service.GetPlanetsAsync();

        // Обработка по заданию – вывод минимального,
        // среднего и максимального диаметра планет;
        // минимального, среднего и максимального орбитального периода планет
        ViewBag.Tasks = (
            planets.Min(p => p.Diameter),
            planets.Average(p => p.Diameter),
            planets.Max(p => p.Diameter),
            planets.Min(p => p.Orbital_Period),
            planets.Average(p => p.Orbital_Period),
            planets.Max(p => p.Orbital_Period));


        return View(planets);
    } // InvokeAsync

} // class RegisteredPlanets
