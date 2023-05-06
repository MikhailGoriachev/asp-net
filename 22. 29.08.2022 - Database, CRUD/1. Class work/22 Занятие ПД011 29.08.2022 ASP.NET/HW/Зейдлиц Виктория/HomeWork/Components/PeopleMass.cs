using HomeWork.Models.Task1;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

public class PeopleMass : ViewComponent
{
    private readonly PeopleService _service;

    // получить связь с данными через внедрение зависимостей 
    public PeopleMass(PeopleService service)
    {
        _service = service;
    } // PeopleMass


    // Реализация логики компонента
    public async Task<IViewComponentResult> InvokeAsync()
    {
        // получение данных должно быть асинхронным
        List<Person> persons = await _service.GetPeoplesOrderedAsync();


        return View(persons);
    } // InvokeAsync

} // class PeopleMass
