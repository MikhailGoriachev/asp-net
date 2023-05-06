using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace RouteAttributeOnly.Controllers;

public class HomeController : Controller
{
    // Атрибут маршрутизации, их модет быть несколько
    [Route("")]             // localhost::pppp
    [Route("Home/Index")]   // localhost::pppp/Home/Index
    [Route("Index")]        // localhost::pppp/Index
    public IActionResult Index() => View();


    // метод действия с кратким маршрутом
    [Route("Secret")]   // localhost:pppp/Seret
    public IActionResult Secret() => View();


    // Атрибут маршрутизации с одним или несколькими параметрами
    [Route("Add/{a}/{b}")]
    public IActionResult Add(int a, int b) => View((a, b, a + b));


    // Ограничения в атрибутах маршрутов
    [Route("{name:minlength(3)}/{age:int}")]
    public IActionResult Person(string name, int age) => View((name, age));  // кортеж - прсото для демонстрации


    // использование переменных {controller} и {action}
    // Значения по умолчанию для маршрутов
    [Route("{controller}/{action}/{name=Лариса}/{age=33}/{phone=123-456-78-90}")]
    public IActionResult DefaultValues(string name, int age, string phone) => View((name, age, phone));


} // class HomeController
