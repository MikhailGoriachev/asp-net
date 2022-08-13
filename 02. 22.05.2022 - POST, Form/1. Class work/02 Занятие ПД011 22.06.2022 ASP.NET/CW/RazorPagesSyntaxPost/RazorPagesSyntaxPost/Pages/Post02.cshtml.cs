using EmptyProjectRazorPagesLayout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesSyntaxPost.Pages
{
    // для POST-обработки
    [IgnoreAntiforgeryToken]
    public class Post02Model : PageModel
    {
        public string Message { get; private set; } = "";

        // запрос страницы с формой
        public void OnGet() {
            Message = "Введите Ваши данные";
        }

        // обработка POST-запроса
        // public void OnPost(string name, int age) {
        public void OnPost(Person person) {
            // Message = $"Вы ввели: {name}, {age}";
            Message = $"Вы ввели: {person.Name}, {person.Age}";
        } // OnPost
    }

    public record class Person(string Name, int Age);
}
