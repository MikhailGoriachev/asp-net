using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    [IgnoreAntiforgeryToken]
    public class CategoryEditModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        [BindProperty] public Category? Category { get; set; }


        // конструктор с внедрением зависимости, доступ к Ѕƒ
        public CategoryEditModel(ApplicationContext db) {
            _context = db;
        } // CategoryEditModel


        // по GET-запросу найти запись в таблице, отправить HTML-разметку
        // формы клиенту, если же запись в Ѕƒ не найдена - вернуть статусный
        // код 404
        public async Task<IActionResult> OnGetAsync(int id) {
            Category = await _context.Categories.FindAsync(id);

            if (Category == null) {
                return NotFound();
            } // if
            return Page();
        } // OnGetAsync


        // обработка данных, полученных из формы
        public async Task<IActionResult> OnPostAsync() {
            // !!! если Id == 0 то Update(), не падает, а добавл€ет новую заись :(
            _context.Categories.Update(Category);
            await _context.SaveChangesAsync();

            // переход на страницу отображени€ всех записей - дл€ эффекта
            // "закрыти€ формы"
            return RedirectToPage("GetCategoriesAll");
        } // OnPostAsync
    } // class CategoryEditModel
}
