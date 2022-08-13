using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using RazorPages_Post.Models;
using RazorPages_Post.Infrastructure;

namespace RazorPages_Post.Pages
{
    [IgnoreAntiforgeryToken]
    public class BookModel : PageModel
    {
        public Book BookData {
            get; private set;
        }

        // по GET-запросу создавать объект класса Book с полями по умолчанию
        public void OnGet() {
            BookData = new Book();

            // формируем имя файла изображения обложки
            BookData.Cover = "/images/task01/cover" + Utils.GetRandom(1, 5) + ".jpg";
        } // OnGet
        

        // Пролучить данные книги из формы по запросу onPost, вывести
        // значения полей на страницу
        public void OnPost() {
            // для повышения читабельности - ссылка на форму
            var form = Request.Form;

            // создать объект, заполнить его свойства
            BookData = new Book  {
                Author = form["author"],
                Title = form["title"],
                Year = int.Parse(form["year"]),
                Price = int.Parse(form["price"]),
                Cover = "/images/task01/cover" + Utils.GetRandom(1, 5) + ".jpg"
            };

            // в представлении просто выводим объект
        } // OnPost
    } // class BookModel
}
