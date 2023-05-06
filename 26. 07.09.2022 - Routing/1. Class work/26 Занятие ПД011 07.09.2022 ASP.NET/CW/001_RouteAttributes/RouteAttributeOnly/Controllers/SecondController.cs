using Microsoft.AspNetCore.Mvc;

namespace RouteAttributeOnly.Controllers;

// атрибут класса - для всех методов действия, модно
// использовать также префиксы  произвольные строки, напромер fly
[Route("{controller}")]
[Route("fly/{controller}")]
public class SecondController : Controller
{
    [Route("")]        // Second
    [Route("Index")]   // Second/Index
    public IActionResult Index() => View();

    // маршрут со значением по умолчанию, маршрут с префиксом
    [Route("{action=test}")]         // Second/Test       или fly/Second/Test
    [Route("self/{action=test}")]    // Second/self/Test  или fly/Second/self/Test
    public IActionResult Test() => View();

    // требуется уникальность маршрутов, она должна обеспечиваться
    // програмистом за счет атрибутов
    [Route("Secret")]
    public IActionResult Secret() => View();
} // class SecondController
