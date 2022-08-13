using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    // представление таблицы БД с возможностью создания записи,
    // удаления и редактирования записи 
    public class GetAllModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // ссылка на коллекцию сущностей - таблицу из БД
        public List<Publication>? Publications { get; private set; }


        // получение доступа к БД при помощи конструктора
        // с внедрением зависимости
        public GetAllModel(ApplicationContext db) {
            _context = db;
        } // GetAllModel

        // по GET-зпросу отдать клиенту разметку с выведенной
        // коллекцией записей из таблицы БД
        public void OnGet() {
            Publications = _context.Publications
                .Include(c => c.Category)
                .AsNoTracking()
                .ToList();
        } // OnGet

        // обработчик клика по кнопкам submit
        // удаление записи по идентификатору
        public async Task<IActionResult> OnPostDeleteAsync(int id) {
            var publication = await _context.Publications.FindAsync(id);

            if (publication != null) {
                _context.Publications.Remove(publication);
                await _context.SaveChangesAsync();
            } // if

            return RedirectToPage();
        } // OnPostDeleteAsync
    } //  class GetAllModel
}
