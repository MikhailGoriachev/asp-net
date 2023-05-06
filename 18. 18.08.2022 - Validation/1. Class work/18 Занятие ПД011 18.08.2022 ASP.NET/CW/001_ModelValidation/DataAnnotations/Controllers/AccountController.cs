using DataAnnotations.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataAnnotations.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register() {
            return View();
        } // Register

        // резкое упрощение кода - за счет атрибутов валидации (метаданные) 
        [HttpPost]
        public IActionResult Register(RegistrationBindingModel model) {
            if (ModelState.IsValid) {
                Debug.WriteLine($"В базу записан пользователь: {model.FirstName} {model.LastName} (email: {model.Email}, tel: {model.PhoneNumber})");
                return View("Success", model);
            } // if

            // если модель содержит значения противоречащие бизнес правилам - возвращаем то же самое представление с неправильными данными
            // для того, чтобы пользователь мог их поправить
            return View(model);
        } // Register
    }
}

