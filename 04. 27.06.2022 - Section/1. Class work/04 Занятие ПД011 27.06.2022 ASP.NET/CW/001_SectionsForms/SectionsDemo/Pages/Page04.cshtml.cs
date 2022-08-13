using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SectionsDemo.Models;

namespace SectionsDemo.Pages
{
    // несколько кнопок submit на форме, обработчики запросов POST на странице
    public class Page04Model : PageModel
    {
        // условные данные
        IEnumerable<Person> _people = new List<Person>
        {
            new ("Тома", 37),
            new ("Сеня", 28),
            new ("Боря", 41),
            new ("Денис", 25),
            new ("Юлия", 37)
        };
        public IEnumerable<Person> DisplayedPeople { get; private set; } = new List<Person>();


        // обработка GET-запроса, вывод страницы
        public void OnGet() {
            DisplayedPeople = _people;
        } // OnGet


        // обработчик первой кнопки submit
        public void OnPostGreaterThan(int age) {
            DisplayedPeople = _people.Where(p => p.Age > age);
        } // OnPostGreaterThan


        // обработчик второй кнопки submit
        public void OnPostLessThan(int age) {
            DisplayedPeople = _people.Where(p => p.Age < age);
        } // OnPostLessThan
    } // class Page04Model

}
