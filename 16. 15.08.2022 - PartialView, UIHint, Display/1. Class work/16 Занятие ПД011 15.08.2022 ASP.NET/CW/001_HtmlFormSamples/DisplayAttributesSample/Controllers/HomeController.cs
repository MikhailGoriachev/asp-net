using DisplayAttributeSample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DisplayAttributeSample.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() {
        return View();
    }

    public IActionResult Register() {
        // этот метод возвращает представление с формой, которую должен заполнить пользователь
        // вывод пустой формы..
        // return View();

        return View(new RegistrationBindingModel() {
            Login = "vasia",
            Email = "vasia@gmail.com",
            TermsAccepted = true
        });
    }

    [HttpPost]
    public IActionResult Register(RegistrationBindingModel model) {
        // этот метод получает POST запрос, валидирует модель и выполняет бизнес правило связанное с этой моделью.
        // в данном примере пропущена валидация и дальнейшее использование модели (эти темы будут рассмотрены в следующих уроках)

        Debug.WriteLine(model.Login);
        Debug.WriteLine(model.Email);
        Debug.WriteLine(model.Password);
        Debug.WriteLine(model.PasswordConfirm);
        Debug.WriteLine(model.TermsAccepted);

        return View("Success", model);
    }
}
