using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithControllersClients.Models;

namespace WebApiWithControllersClients.Controllers;

// пример контроллера с реализацией RESTful API
// для возврата разметки, сформированной в представлении, 
// надо наследоваться от Controller, а не от ControllerBase

[Route("[controller]/{action}")]
[ApiController]
public class PeopleController : Controller
{
    private Person Person { get; set; } = new Person() { Id = 42, Fullname = "Романова Р.Р.", Age = 47 };

    // GET-запрос
    [HttpGet]
    public string Get() {
        // имитируем время нв получение
        Thread.Sleep(900);
        return $"<b>GET</b>: есть объект, {Person}";
    } // Get


    // GET-запрос для получения данных в формате JSON
    [HttpGet]
    public IActionResult GetPerson() {
        // имитируем время нв получение
        Thread.Sleep(900);
        return new JsonResult(Person);
    } // GetPerson


    // POST-запрос
    [HttpPost("{id}/{fullName}/{age}")]
    public string Post(int id, string fullName, int age) {
        // имитируем изменение данных
        Person.Id = id;
        Person.Fullname = fullName;
        Person.Age = age;

        // имитируем время нв изменение
        Thread.Sleep(900);

        return $"<b>POST</b>: изменен объект, {Person}";
    } // Post


    // PUT-запрос
    [HttpPut("{id}/{fullName}/{age}")]
    public string Put(int id, string fullName, int age) {
        // в духе запроса создадим новый объект
        Person = new Person { Id = id, Fullname = fullName, Age = age };

        // имитируем время нв создание
        Thread.Sleep(900);

        // в качестве квитанции отдадим объект клиенту
        return $"<b>PUT</b>: создан объект, {Person}";
    } // Put


    // DELETE-запрос
    [HttpDelete("{id}")]
    public string Delete(int id) {
        // в духе запроса имитируем удаление
        Thread.Sleep(900);

        // в качестве квитанции отдадим ид удаляемого объекта клиенту
        return $"<b>DELETE</b>: удален объект, id: {id}";
    } // Delete


    // возвращаем разметку по запросу клиента, разметку формируем в представлении
    [HttpGet]
    public IActionResult GetPersonHtml() {
        // имитация получения данных
        Thread.Sleep(800);
        Person = new Person {Id = 787, Fullname = "Иванов М.В.", Age = 45};

        return View(Person);
    } // GetPersonHtml
} // class PeopleController

