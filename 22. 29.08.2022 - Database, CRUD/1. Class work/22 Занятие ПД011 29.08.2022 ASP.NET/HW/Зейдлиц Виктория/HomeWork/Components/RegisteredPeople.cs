using HomeWork.Models.Task1;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

public class RegisteredPeople : ViewComponent
{
    private readonly PeopleService _service;

    // получить связь с данными через внедрение зависимостей 
    public RegisteredPeople(PeopleService service)
    {
        _service = service;
    } // RegisteredPeople


    public async Task<IViewComponentResult> InvokeAsync()
    {
        // получение данных должно быть асинхронным
        List<Person> peoples = await _service.GetPeoplesAsync();

        
        return View(peoples);
    } // InvokeAsync


} // class RegistredUsers


