using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    // страница редактировани€
    [IgnoreAntiforgeryToken]
    public class EditModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        [BindProperty] public Publication? Publication { get; set; }

        // создание объекта дл€ выпадающего списка
        // SelectList(коллекци€ƒанныхƒл€¬ывода, »м€—войства«начени€, »м€—войстваќтображени€)
        public SelectList Categories { get; private set; } = null!;

        // конструктор с внедрением зависимости, доступ к Ѕƒ
        public EditModel(ApplicationContext db) {
            _context = db;
        } // EditModel


        // по GET-запросу найти запись в таблице, отправить HTML-разметку
        // формы клиенту, если же запись в Ѕƒ не найдена - вернуть статусный
        // код 404
        public async Task<IActionResult> OnGetAsync(int id) {
            Publication = await _context.Publications.FindAsync(id);

            if (Publication == null) {
                return NotFound();
            } // if

            // получить список значений дл€ формировани€ выпадающего списка
            Categories = new SelectList(_context.Categories.AsNoTracking().ToList(), "Id", "CategoryName");
            return Page();
        } // OnGetAsync


        // обработка данных, полученных из формы
        public async Task<IActionResult> OnPostAsync() {
            // !!! если Id == 0 то Update(), не падает, а добавл€ет новую заись :(
            _context.Publications.Update(Publication);
            await _context.SaveChangesAsync();

            // переход на страницу отображени€ всех записей - дл€ эффекта
            // "закрыти€ формы"
            return RedirectToPage("GetAll");
        } // OnPostAsync
    } // class EditModel
}
