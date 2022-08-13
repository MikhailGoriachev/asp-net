using DemoEf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoEf.Pages
{
    // представление таблицы БД с возможностью создания записи,
    // удаления и редактирования записи 
    public class GetAllModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // ссылка на коллекцию сущностей - таблицу из БД
        public List<User>? Users { get; private set; }


        // получение доступа к БД при помощи конструктора
        // с внедрением зависимости
        public GetAllModel(ApplicationContext db) {
            _context = db;
        } // GetAllModel

        // по GET-зпросу отдать клиенту разметку с выведенной
        // коллекцией записей из таблицы БД
        public void OnGet() {
            Users = _context.Users.AsNoTracking().ToList();
        } // OnGet

        // обработчик клика по кнопкам submit
        // удаление записи по идентификатору
        public async Task<IActionResult> OnPostDeleteAsync(int id) {
            var user = await _context.Users.FindAsync(id);
            if (user != null) {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            } // if

            return RedirectToPage();
        } // OnPostDeleteAsync
    } //  class GetAllModel
}
