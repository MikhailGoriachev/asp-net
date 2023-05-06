using Microsoft.AspNetCore.Mvc;

namespace Variables.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();


    // Параметры x и y будут инициализироваться автоматически.
    // Подробнее о работе данного механизма вы узнаете из материала о привязке модели. 
    public IActionResult Calc(int x, int y) {
        int result = x - y;
        return View(result);
    }

    // метод действия для префикса API
    public IActionResult Values(int value) =>
        View(new[] { "помидоры", "огурцы", "тыква", "картошка", "морковь" });

}
