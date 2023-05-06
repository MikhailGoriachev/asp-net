using HomeWork.Models.Task1;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

public class PeopleSelectMass : ViewComponent
{
    private readonly PeopleService _service;

    // получить связь с данными через внедрение зависимостей 
    public PeopleSelectMass(PeopleService service)
    {
        _service = service;
    } // PeopleSelectMass


    // Реализация логики компонента
    public async Task<IViewComponentResult> InvokeAsync()
    {
        // получение данных должно быть асинхронным
        List<Person> persons = await _service.GetPeoplesSelectedMassAsync();


        return View(persons);
    } // InvokeAsync
} // class PeopleSelectMass
