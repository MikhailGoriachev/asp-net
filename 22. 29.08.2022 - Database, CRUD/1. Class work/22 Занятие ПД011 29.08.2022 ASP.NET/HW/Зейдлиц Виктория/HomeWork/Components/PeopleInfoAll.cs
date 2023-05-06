using HomeWork.Models.Task1;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

public class PeopleInfoAll : ViewComponent
{
    private readonly PeopleService _service;

    // получить связь с данными через внедрение зависимостей 
    public PeopleInfoAll(PeopleService service)
    {
        _service = service;
    } // PeopleInfoAll


    // Реализация логики компонента
    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
        // получение данных должно быть асинхронным
        List<Person> persons = await _service.GetPeoplesAsync(id);


        return View(persons);
    } // InvokeAsync

} // PeopleInfoAll

