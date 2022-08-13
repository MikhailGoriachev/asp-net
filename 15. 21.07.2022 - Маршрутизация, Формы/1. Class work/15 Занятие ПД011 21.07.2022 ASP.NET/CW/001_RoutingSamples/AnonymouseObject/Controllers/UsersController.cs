using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnonymouseObject.Controllers
{
    public class UsersController : Controller
    {
        // name - получаем из явно заданной части маршрута
        // id   - получаем из неявно заданной части маршрута, анонимного объекта defaults
        public IActionResult Index(string name, int id) {
            ViewBag.Id = id;
            return View(name as object);
        }
    }
}