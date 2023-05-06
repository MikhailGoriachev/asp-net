using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAjax.Models;

// пример объекта для демонстрации работы AJAX
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }


    public override string ToString() => $"Ид: {Id,3} | Имя: {Name,-15} | Возраст, лет: {Age,3}";
} // class Person

