using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    public class GroupingsQuery01Model : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // ссылка на коллекцию сущностей - результат запроса к таблице БД
        public IQueryable<Result01> Results { get; private set; }

        // формулировка запроса
        public string? Task { get; private set; }


        // получение доступа к БД при помощи конструктора
        // с внедрением зависимости
        public GroupingsQuery01Model(ApplicationContext db)
        {
            _context = db;
        } // GroupingsQuery01Model


        // Выполняет группировку по полю Вид издания. Для каждого вида
        // вычисляет среднюю цену 1 экземпляра, количество изданий в группе
        public void OnGet() {
            Task = "Группировка по полю \"Вид издания\". Средняя цена 1 экземпляра, количество изданий в группе";
            
            // заполнить коллекцию данными для отображения в разметке
            Results = _context.Publications
                .AsNoTracking()
                .GroupBy(p => p.Category.CategoryName,
                (key, group) => new Result01(
                    key, 
                    group.Average(p => p.Cost), 
                    group.Count())
                );
        } // OnGet
    }

    // Тип записи коллекции итогового запроса
    public record Result01(string Category, double AvgCost, int Amount);
}
