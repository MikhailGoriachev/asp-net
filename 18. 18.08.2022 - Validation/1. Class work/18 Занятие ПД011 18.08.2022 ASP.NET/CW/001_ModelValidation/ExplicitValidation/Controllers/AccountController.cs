using ExplicitValidation.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ExplicitValidation.Controllers
{
    // Явная (explicit) валидация в контроллере - удобно применять при нестандартных проверках
    public class AccountController : Controller
    {
        // GET Account/Register - вывод разметки страницы регистрации
        public IActionResult Register() {
            return View();
        }

        // Обработка данных формы - валидация в коде
        [HttpPost]
        public IActionResult Register(RegistrationBindingModel model) {
            // ModelState.AddModelError - для HTML элементов, которые привязаны к свойству,
            //                            указанному в первом параметре добавляется CSS класс
            //                            input-validation-error
            // ModelState.AddModelError - меняет состояние модели на невалидное
            // ModelState.AddModelError - добавляет текст ошибки, связанный со свойством,
            //                            указанном в первом параметре
            if (string.IsNullOrEmpty(model.FirstName)) {
                ModelState.AddModelError(nameof(model.FirstName), "Введите имя");
            }

            if (!string.IsNullOrEmpty(model.FirstName) && model.FirstName.Length > 50) {
                ModelState.AddModelError(nameof(model.FirstName), "Значение имени не может привышать 50 символов");
            }

            if (string.IsNullOrEmpty(model.LastName)) {
                ModelState.AddModelError(nameof(model.LastName), "Введите фамилию");
            }

            if (!string.IsNullOrEmpty(model.LastName) && model.LastName.Length > 50) {
                ModelState.AddModelError(nameof(model.LastName), "Значение фамилии не может привышать 50 символов");
            }

            if (string.IsNullOrEmpty(model.Email)) {
                ModelState.AddModelError(nameof(model.Email), "Введите Email");
            }

            if (!string.IsNullOrEmpty(model.Email) && !Regex.IsMatch(model.Email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")) {
                ModelState.AddModelError(nameof(model.Email), "Не правильный формат Email");
            }

            if (string.IsNullOrEmpty(model.Password)) {
                ModelState.AddModelError(nameof(model.Password), "Введите пароль");
            }

            if (string.IsNullOrEmpty(model.ConfirmPassword)) {
                ModelState.AddModelError(nameof(model.ConfirmPassword), "Введите подтверждение пароля");
            }

            if (model.Password != model.ConfirmPassword) {
                ModelState.AddModelError(nameof(model.Password), "Пароли не совпадают");
            }

            // Если модель прошла проверку и значения свойств соответствуют атрибутом установленным на модели,
            // (т.е. ни разу не вызывали ModelState.AddModelError()) то свойство ModelState.IsValid получает значение true
            if (ModelState.IsValid) {
                Debug.WriteLine($"В базу записан пользователь: {model.FirstName} {model.LastName} (email: {model.Email}, tel: {model.PhoneNumber})");
                return View("Success", model);
            }

            // если модель содержит значения противоречащие бизнес правилам - возвращаем то же самое представление с неправильными данными
            // для того, чтобы пользователь мог их поправить
            return View(model);
        } // Register
    }
}

