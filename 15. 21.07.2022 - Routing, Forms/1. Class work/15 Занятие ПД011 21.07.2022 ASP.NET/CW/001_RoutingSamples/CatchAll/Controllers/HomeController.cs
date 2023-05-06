using Microsoft.AspNetCore.Mvc;

namespace CatchAll.Controllers;
public class HomeController : Controller
{
    public IActionResult Index() => View();
    

    // в примере данные из секции data маршрута передаются в параметр data
    // метода действия, парсинг этих данных - задача программиста :)
    // (показана в примере)
    public IActionResult Values(string data) {
        string[] splited = data.Split("/");
        int result = 0;

        foreach (string item in splited) {
            if (int.TryParse(item, out int converted)) {
                result += converted;
            } // if
        } // foreach item

        ViewBag.Data = data;
        return View(result);
    } // Values
}
