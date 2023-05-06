using ActionResultsSamples.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActionResultsSamples.Controllers;

public class JsonController : Controller
{
    public IActionResult Index() {
        return View();
    }

    // GET /Json/ClientInfo1
    public IActionResult ClientInfo1() {
        // использование именованных типов для формирования JSON ответ
        int id = DateTime.Now.Millisecond + 1;
        var client = new Client {
            Id = id,
            Login = $"user_{id}",
            Email = $"user{id}@example.com"
        };

        // Json - Сериализует объект переданный в параметрах в JSON и возвращает клиенту ответ.
        return Json(client);
    } // ClientInfo1


    // на этот запрос реагирует клиент
    // GET /Json/ClientInfo2
    public IActionResult ClientInfo2() {
        // использование анонимных типов для формирования JSON ответа
        int id = DateTime.Now.Millisecond + 1;
        return Json(new {
            Id = id,
            Login = $"user{id}",
            Email = $"user{id}@example.com"
        });
    }
}
