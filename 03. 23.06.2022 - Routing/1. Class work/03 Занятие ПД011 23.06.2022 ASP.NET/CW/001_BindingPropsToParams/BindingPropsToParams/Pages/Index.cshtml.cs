using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BindingPropsToParams.Pages
{
    // привязка свойств класса к парамтерам формы
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        // атрибут привязки свойства к параметру формы
        [BindProperty]
        public string Name { get; set; } = "";

        // атрибут привязки свойства к параметру формыы
        [BindProperty]
        public int Age { get; set; }

        public string Message { get; private set; } = "";


        public void OnGet() {
            Message = "Введите данные";
        }

        // Передача параметров onPost() не нужна, т.к. выполнена привязка
        public void OnPost() {
            Message = $"Имя: {Name}, возраст: {Age}";
        }
    } // class IndexModel
}
