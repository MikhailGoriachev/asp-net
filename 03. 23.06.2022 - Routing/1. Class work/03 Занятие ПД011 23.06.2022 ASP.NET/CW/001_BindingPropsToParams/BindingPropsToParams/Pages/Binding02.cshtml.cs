using BindingPropsToParams.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BindingPropsToParams.Pages
{
    
    // Привязка к сложным объектам
    [IgnoreAntiforgeryToken]
    public class Binding02Model : PageModel
    {
        // привязка сложного типа - выполняется по свойствам этого типа
        [BindProperty]
        public Person? Admin { get; set; } = new Person("Василий", 23);

        public string Message { get; set; } = "";

        public void OnGet() {
            Message = "Введите данные";
        }

        public void OnPost() {
            Message = $"Имя: {Admin.Name}, возраст: {Admin.Age}";
        }
    }
}

