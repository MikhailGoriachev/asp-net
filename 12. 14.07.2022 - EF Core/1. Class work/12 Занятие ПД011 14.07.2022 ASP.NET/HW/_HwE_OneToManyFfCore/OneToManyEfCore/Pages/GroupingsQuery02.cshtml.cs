using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    public class GroupingsQuery02Model : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // ссылка на коллекцию сущностей - таблицу из БД
        public IQueryable<Result02> Results { get; private set; }

        // формулировка запроса
        public string? Task { get; private set; }


        // получение доступа к БД при помощи конструктора
        // с внедрением зависимости
        public GroupingsQuery02Model(ApplicationContext db) {
            _context = db;
        } // GroupingsQuery02Model


        // Для каждого вида вычисляет максимальную и минимальную
        // цену 1 экземпляра, количество изданий в группе
        public void OnGet() {
            Task = "Группировка по полю \"Вид издания\". Максимальная и минимальная цена 1 экземпляра, количество изданий в группе";

            Results = _context.Publications
                .AsNoTracking()
                .GroupBy(p => p.Category.CategoryName, 
                    (key, group) => new Result02(
                        key,
                        group.Min(p => p.Cost),
                        group.Max(p => p.Cost),
                        group.Count())
            );
        } // OnGet
    } // class GroupingsQuery02Model

    // Тип записи коллекции итогового запроса
    public record Result02(string Category, int MinCost, int MaxCost, int Amount);
}
