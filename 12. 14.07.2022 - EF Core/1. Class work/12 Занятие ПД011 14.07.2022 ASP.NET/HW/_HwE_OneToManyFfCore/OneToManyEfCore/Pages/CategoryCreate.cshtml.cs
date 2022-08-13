using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    [IgnoreAntiforgeryToken]
    public class CategoryCreateModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // поле для привязки к элементам формы
        [BindProperty] public Category? Category { get; set; }


        // конструктор с внедрением зависимости для получения
        // доступа к бпзе данных
        public CategoryCreateModel(ApplicationContext db) {
            _context = db;
        } // CategoryCreateModel


        // отдать клиенту разметку с формой ввода
        public void OnGet() {} // OnGet


        // обработка формы ввода с данными созданной категории
        public async Task<IActionResult> OnPostAsync() {
            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            // после добавления записи в таблицу БД переходим 
            // на страницу отображения всех записей таблицы
            return RedirectToPage("GetCategoriesAll");
        } // OnPostAsync
    } // class CategoryCreateModel
}
