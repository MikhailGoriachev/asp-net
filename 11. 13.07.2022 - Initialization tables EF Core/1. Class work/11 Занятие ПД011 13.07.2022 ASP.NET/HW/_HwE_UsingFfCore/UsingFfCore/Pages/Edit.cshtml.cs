using UsingFfCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace UsingFfCore.Pages
{
    // страница редактирования
    [IgnoreAntiforgeryToken]
    public class EditModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        [BindProperty] public Publication? Publication { get; set; }


        // конструктор с внедрением зависимости, доступ к БД
        public EditModel(ApplicationContext db) {
            _context = db;
        } // EditModel


        // по GET-запросу найти запись в таблице, отправить HTML-разметку
        // формы клиенту, если же запись в БД не найдена - вернуть статусный
        // код 404
        public async Task<IActionResult> OnGetAsync(int id) {
            Publication = await _context.Publications.FindAsync(id);
            return Publication != null?Page():NotFound();
        } // OnGetAsync


        // обработка данных, полученных из формы
        public async Task<IActionResult> OnPostAsync() {
            // !!! если Id == 0 то Update(), не падает, а добавляет новую заись :(
            _context.Publications.Update(Publication);
            await _context.SaveChangesAsync();

            // переход на страницу отображения всех записей - для эффекта
            // "закрытия формы"
            return RedirectToPage("GetAll");
        } // OnPostAsync
    } // class EditModel
}
