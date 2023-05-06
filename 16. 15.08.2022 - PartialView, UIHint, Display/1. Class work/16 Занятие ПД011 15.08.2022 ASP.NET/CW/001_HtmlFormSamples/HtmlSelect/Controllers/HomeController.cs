using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HtmlSelect.Controllers;

public class HomeController : Controller
{
    // GET /Home/Index
    public IActionResult Index() {
        string[] source = {
            "Яблоки", "Груши", "Вишня", "Черешня", "Крыжовник", 
            "Смородина", "Малина"
        };

        // список для выбора, значение для начального отображения
        SelectList selectList = new SelectList(source, source[2]);
        ViewBag.SelectItems = selectList;

        return View();
    } // Index


    // POST  /Home/Index
    [HttpPost]
    public IActionResult Index(string selectedItem) {
        Debug.WriteLine("Selected - " + selectedItem);

        return View("Result", selectedItem);
    }
}
