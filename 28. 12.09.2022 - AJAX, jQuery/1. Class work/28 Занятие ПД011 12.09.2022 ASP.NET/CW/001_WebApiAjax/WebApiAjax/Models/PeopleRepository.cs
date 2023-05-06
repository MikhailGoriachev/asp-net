using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAjax.Models;

public class PeopleRepository
{
    // коллекция персон
    private List<Person> _people;

    public PeopleRepository() {
        _people = new List<Person> {
                new Person {Id = 1, Name = "Иванова О.Л.",    Age = 34 },
                new Person {Id = 4, Name = "Федоров И.Е.",    Age = 21 },
                new Person {Id = 5, Name = "Сурин В.В.",      Age = 33 },
                new Person {Id = 8, Name = "Алексикова Б.К.", Age = 51 },
                new Person {Id = 7, Name = "Попов С.Ю.",      Age = 37 },
                new Person {Id = 6, Name = "Семихатова В.Е.", Age = 42 },
                new Person {Id = 3, Name = "Шарапова Ю.Г.",   Age = 27 },
                new Person {Id = 9, Name = "Васильева И.А.",   Age = 54 },
                new Person {Id = 2, Name = "Дедов Н.В.",      Age = 33 }
            };
    } // PeopleRepository

    // получить коллекцию объектов
    public IEnumerable<Person> GetAll() => _people;

    // получить объект по идентификатору
    public Person Get(int id) => _people.FirstOrDefault(p => p.Id == id);

    // добавить объект в репозиторий 
    public void Add(Person person) {
        if (_people.Count(p => p.Id == person.Id) == 0)
            _people.Add(person);
    } // Add

    // удаление объекта по значению этого объекта
    public void Delete(Person person) {
        if (_people.Count(p => p.Id == person.Id) != 0)
            _people.Remove(person);
    } // Delete

    // удаление объекта по его идентификатору
    public void Delete(int id) {
        int index = _people.FindIndex(p => p.Id == id);
        if (index >= 0)
            _people.RemoveAt(index);
    } // Delete


} // class PeopleRepository

