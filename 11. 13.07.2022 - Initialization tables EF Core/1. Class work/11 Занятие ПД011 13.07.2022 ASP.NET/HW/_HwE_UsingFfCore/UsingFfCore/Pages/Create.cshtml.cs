using UsingFfCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UsingFfCore.Pages
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // поле для привязки к элементам формы
        [BindProperty] public Publication? Publication { get; set; }

        // конструктор с внедрением зависимости для получения
        // доступа к бпзе данных
        public CreateModel(ApplicationContext db) {
            _context = db;
        } // CreateModel


        // отдать клиенту разметку с формой ввода
        public void OnGet() { } // OnGet


        // обработка формы ввода с данными созданного пользователя
        public async Task<IActionResult> OnPostAsync() {
            _context.Publications.Add(Publication);
            await _context.SaveChangesAsync();

            // после добавления записи в таблицу БД переходим 
            // на страницу отображения всех записей таблицы
            return RedirectToPage("GetAll");
        } // OnPostAsync
    } // class CreateModel
}
