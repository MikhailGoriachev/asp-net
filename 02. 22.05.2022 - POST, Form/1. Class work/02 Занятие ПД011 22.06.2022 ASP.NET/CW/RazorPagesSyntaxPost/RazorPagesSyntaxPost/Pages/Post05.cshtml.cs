using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesSyntaxPost.Pages
{
    public class Post05Model : PageModel
    {
        public string Message { get; private set; } = "";

        // запрос страницы с формой
        public void OnGet()
        {
            Message = "Введите Ваши данные";
        }

        // обработка POST-запроса -получение данных из контекста запроса
        public void OnPost() {
            Person person = new Person(
                this.Request.Form["name"],
                int.Parse(Request.Form["age"]));

            Message = $"Вы ввели: {person.Name}, {person.Age}";
        } // OnPost
    }
}
