using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // поле дл€ прив€зки к элементам формы
        [BindProperty] public Publication? Publication { get; set; }

        // создание объекта дл€ выпадающего списка
        // SelectList(коллекци€ƒанныхƒл€¬ывода, »м€—войства«начени€, »м€—войстваќтображени€)
        public SelectList Categories { get; private set; } = null!;

        // конструктор с внедрением зависимости дл€ получени€
        // доступа к бпзе данных
        public CreateModel(ApplicationContext db) {
            _context = db;
        } // CreateModel


        // отдать клиенту разметку с формой ввода
        public void OnGet() {
            // получить список значений дл€ формировани€ выпадающего списка
            Categories = new SelectList(_context.Categories.AsNoTracking().ToList(), "Id", "CategoryName");
        } // OnGet


        // обработка формы ввода с данными созданного пользовател€
        public async Task<IActionResult> OnPostAsync() {
            _context.Publications.Add(Publication);
            await _context.SaveChangesAsync();

            // после добавлени€ записи в таблицу Ѕƒ переходим 
            // на страницу отображени€ всех записей таблицы
            return RedirectToPage("GetPublicationsAll");
        } // OnPostAsync
    } // class CreateModel
}
