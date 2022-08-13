using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    public class SelectModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // ссылка на коллекцию сущностей - таблицу из БД
        public IQueryable<Publication> Publications { get; private set; } = null!;

        // формулировака запроса
        public string? Task { get; private set; }


        // получение доступа к БД при помощи конструктора
        // с внедрением зависимости
        public SelectModel(ApplicationContext db) {
            _context = db;
        } // SelectModel

        public void OnGet() { }

        // Выбирает информацию об издании с заданным индексом.
        public void OnPostQuery01(string pubIndex = "464885") {
            Task = $"Выбрана информация об издании с индексом {pubIndex}";
            Publications = _context.Publications
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => p.PubIndex == pubIndex);
        } // OnPostQuery01


        // Выбирает информацию обо всех изданиях, для которых цена 1 экземпляра
        // есть значение из некоторого диапазона.
        public void OnPostQuery02(int costFrom = 300, int costTo = 1200) {
            Task = $"Выбрана информация об изданиях с ценой из диапазона {costFrom}, …, {costTo}";
            Publications = _context.Publications
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => costFrom <= p.Cost && p.Cost <= costTo);
        } // OnPostQuery02


        // Выбирает информацию об изданиях, для которых "Длительность подписки"
        // есть значение из некоторого диапазона.
        public void OnPostQuery03(int durationFrom = 6, int durationTo = 12) {
            Task = $"Выбрана информация об изданиях с длительностью подписки из диапазона {durationFrom}, …, {durationTo}";
            Publications = _context.Publications
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => durationFrom <= p.Duration && p.Duration <= durationTo);
        } // OnPostQuery03


        // Выбирает информацию об изданиях с заданным значением в поле "Вид издания"
        public void OnPostQuery04(string categoryName = "газета") {
            Task = $"Выбрана информация об изданиях с видом издания \"{categoryName}\"";
            Publications = _context.Publications
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => p.Category.CategoryName == categoryName);
        } // OnPostQuery04
    } // class QueriesModel
}
