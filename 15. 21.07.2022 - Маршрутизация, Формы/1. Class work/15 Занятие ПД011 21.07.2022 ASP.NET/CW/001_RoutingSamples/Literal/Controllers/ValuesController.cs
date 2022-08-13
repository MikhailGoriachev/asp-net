using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Literal.Controllers;
public class ValuesController : Controller
{
    // в д.с. api - это литеральная часть маршрута
    // маршрут набираем руками в адресной строке браузера
    // /api/Values/List
    public IActionResult List() {
        string[] data = { "яблоко", "груша", "апельсин", "банан", "киви", "слива", "манго" };
        return View(data);
    }
}
