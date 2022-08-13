using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    // работа с таблицей КАТЕГОРИИ
    public class GetCategoriesAllModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // ссылка на коллекцию сущностей - таблицу из БД
        public List<Category>? Categories { get; private set; }

        // получение доступа к БД при помощи конструктора
        // с внедрением зависимости
        public GetCategoriesAllModel(ApplicationContext db)
        {
            _context = db;
        } // GetCategoriesAllModel

        // по GET-зпросу отдать клиенту разметку с выведенной
        // коллекцией записей из таблицы БД
        public void OnGet() {
            Categories = _context.Categories
                .AsNoTracking()
                .ToList();
        } // OnGet
    } // class GetCategoriesAllModel
}
