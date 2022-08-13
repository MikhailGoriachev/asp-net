using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    public class GroupingsQuery03Model : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // ссылка на коллекцию сущностей - таблицу из БД
        public IQueryable<Result03> Results { get; private set; }

        // формулировка запроса
        public string? Task { get; private set; }


        // получение доступа к БД при помощи конструктора
        // с внедрением зависимости
        public GroupingsQuery03Model(ApplicationContext db)
        {
            _context = db;
        } // GroupingsQuery03Model


        // Выполняет группировку по полю Длительность подписки.
        // Для каждого срока вычисляет среднюю цену 1 экземпляра
        public void OnGet()
        {
            Task = "Группировка по полю \"Длительность подписки\". Средняя цена 1 экземпляра, количество изданий в группе";

            Results = _context.Publications
                .AsNoTracking()
                .GroupBy(p => p.Duration,
                    (key, group) => new Result03(
                        key,
                        group.Average(p => p.Cost),
                        group.Count())
                );
        } // OnGet
    } // class GroupingsQuery02Model

    // Тип записи коллекции итогового запроса
    public record Result03(int Duration, double AvgCost, int Amount);
}