using DemoEf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoEf.Pages
{
    // страница редактирования
    [IgnoreAntiforgeryToken]
    public class EditModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        [BindProperty] public User? Person { get; set; }


        // конструктор с внедрением зависимости, доступ к БД
        public EditModel(ApplicationContext db) {
            _context = db;
        } // EditModel


        // по GET-запросу найти запись в таблице, отправить HTML-разметку
        // формы клиенту, если же запись в БД не найдена - вернуть статусный
        // код 404
        public async Task<IActionResult> OnGetAsync(int id) {
            Person = await _context.Users.FindAsync(id);
            return Person != null?Page():NotFound();
        } // OnGetAsync


        // обработка данных, полученных из формы
        public async Task<IActionResult> OnPostAsync() {
            // !!! если Id == 0 то Update(), не падает, а добавляет новую заись :(
            _context.Users.Update(Person);
            await _context.SaveChangesAsync();

            return RedirectToPage("GetAll");
        } // OnPostAsync
    } // class EditModel
}
