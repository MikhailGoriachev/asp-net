using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ModelValidation.Model;

namespace ModelValidation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationBindingModel model) {
            // В данном коде отсутствует проверка достоверности модели:
            // отсутствие проверки ведет к:
            // * проблемы безопасности
            // * проблемы форматирования полученных данных (например, email адрес)
            // * проблемы соответствия полученных данных бизнес правилам (например,
            //   обязательные значения, соответствия диапазонов значений и т.д.)

            Debug.WriteLine($"В базу записан пользователь: {model.FirstName} {model.LastName} (email: {model.Email}, tel: {model.PhoneNumber})");

            return View("Success", model);
        }
    }
}
