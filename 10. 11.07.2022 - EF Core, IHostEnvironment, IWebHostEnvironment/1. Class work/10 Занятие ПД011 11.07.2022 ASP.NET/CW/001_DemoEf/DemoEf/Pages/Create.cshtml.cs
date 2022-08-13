using DemoEf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoEf.Pages
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // поле для привязки к элементам формы
        [BindProperty] public User? Person { get; set; }

        // конструктор с внедрением зависимости для получения
        // доступа к бпзе данных
        public CreateModel(ApplicationContext db) {
            _context = db;
        } // CreateModel


        // отдать клиенту разметку с формой ввода
        public void OnGet() { } // OnGet


        // обработка формы ввода с данными созданного пользователя
        public async Task<IActionResult> OnPostAsync() {
            _context.Users.Add(Person);
            await _context.SaveChangesAsync();

            // после добавления записи в таблицу БД переходим 
            // на страницу отображения всех записей таблицы
            return RedirectToPage("GetAll");
        } // OnPostAsync
    } // class CreateModel
}
