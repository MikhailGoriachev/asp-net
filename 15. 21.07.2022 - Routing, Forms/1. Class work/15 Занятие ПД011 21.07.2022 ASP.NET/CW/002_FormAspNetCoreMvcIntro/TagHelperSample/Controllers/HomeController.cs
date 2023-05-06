using TagHelperSample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TagHelperSample.Controllers;

public class HomeController : Controller
{
    // GET /Home/Index передает разметку начальной страницы
    public IActionResult Index() => View();

    // GET /Home/Register
    public IActionResult Register() {
        // этот метод возвращает представление с формой, которую должен заполнить пользователь
        // при передаче проинициированной модели - поля ввода заполняются этими значениями
        return View(new RegistrationBindingModel("default", "", "", "", true));
    }

    // POST /Home/Register
    [HttpPost]
    public IActionResult Register(RegistrationBindingModel model) {
        // этот метод получает POST запрос, валидирует модель и выполняет бизнес
        // правило связанное с этой моделью.
        // в данном примере пропущена валидация и дальнейшее использование модели
        // (эти темы будут рассмотрены в следующих уроках)

        Debug.WriteLine(model.Login);
        Debug.WriteLine(model.Email);
        Debug.WriteLine(model.Password);
        Debug.WriteLine(model.PasswordConfirm);
        Debug.WriteLine(model.TermsAccepted);

        return View("Success", model);
    } // Register
}
