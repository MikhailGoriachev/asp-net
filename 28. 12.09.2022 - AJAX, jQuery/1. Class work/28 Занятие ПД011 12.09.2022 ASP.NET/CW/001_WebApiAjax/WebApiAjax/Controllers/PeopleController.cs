using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiAjax.Models;


namespace WebApiAjax.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PeopleController : ControllerBase
{
    // получение зарегистрированного сервиса через внедрение зависимости
    private readonly PeopleRepository _repository;
    public PeopleController(PeopleRepository repository) {
        _repository = repository;
    }

    // GET: api/<ValuesController>
    [HttpGet]
    public IEnumerable<Person> Get() => _repository.GetAll();

    // GET api/People?id=1
    [HttpGet]
    public Person GetId(int id) => _repository.Get(id);

    // POST api/<ValuesController>
    [HttpPost]
    public Person Post([FromBody] Person person) {
        person.Age++;
        _repository.Add(person);

        return person;
    } // Post


    // Запрос POST для метода load() AJAX-запроса (jQuery)
    [HttpPost]
    public Person Post1([FromForm] int id, [FromForm] string name, [FromForm] int age) {
        var person = new Person { Id = id, Age = age, Name = name };
        _repository.Add(person);

        return person;
    } // Post1


    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public Person Put(int id, [FromForm] string name, [FromForm] int age) {
        var person = new Person { Id = id, Age = age, Name = name };
        _repository.Add(person);

        return person;
    } // Put


    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id) {
        _repository.Delete(id);
    } // Delete
} // class PeopleController

