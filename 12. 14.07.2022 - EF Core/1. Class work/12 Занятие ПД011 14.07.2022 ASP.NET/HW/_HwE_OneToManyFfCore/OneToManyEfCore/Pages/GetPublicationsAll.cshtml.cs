
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    // представление таблицы БД с возможностью создания записи,
    // удаления и редактирования записи 
    public class GetPublicationsAllModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // ссылка на коллекцию сущностей - таблицу из БД
        public List<Publication>? Publications { get; private set; }

        // Список индексов подписных изданий - для отображения в форме
        // ввода параметров запроса 1
        public SelectList PubIndeces { get; private set; } = null!;

        // Список видов подписных изданий - для отображения в форме
        // ввода параметров запроса 4
        public SelectList CategoryNames { get; private set; } = null!;

        // получение доступа к БД при помощи конструктора
        // с внедрением зависимости
        public GetPublicationsAllModel(ApplicationContext db) {
            _context = db;
        } // GetPublicationsAllModel

        // по GET-зпросу отдать клиенту разметку с выведенной
        // коллекцией записей из таблиц БД
        public void OnGet() {
            Publications = _context.Publications
                .Include(c => c.Category)
                .AsNoTracking()
                .ToList();
			
			// получить индексы подписных изданий 
			// (предполагаем, что поиск ведем только по тем индексам, 
			// что есть в таблице)
			PubIndeces = new SelectList(Publications.Select(p => p.PubIndex).Distinct());

            // получить список названий видоа изданий в списке выбора 
            CategoryNames = new SelectList(_context.Categories.AsNoTracking().Select(c => c.CategoryName));
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
    } //  class GetPublicationsAllModel
}
