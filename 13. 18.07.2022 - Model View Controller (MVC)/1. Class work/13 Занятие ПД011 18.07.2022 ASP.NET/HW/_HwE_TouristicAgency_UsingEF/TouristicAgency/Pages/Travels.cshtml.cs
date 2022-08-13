using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TouristicAgency.Models;

namespace TouristicAgency.Pages
{
    // работа с таблицей ПОЕЗДКИ
    public class TravelsModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // ссылка на коллекцию сущностей - таблицу из БД
        public List<Travel>? Travels { get; private set; }

        // получение доступа к БД при помощи конструктора
        // с внедрением зависимости
        public TravelsModel(ApplicationContext db) {
            _context = db;
        } // TravelsModel


        // ответ на GET-запрос, вернем клиенту разметку с коллекцией данных о поездках
        public void OnGet() {
            // получить данные по коллекции поездок, требуется включить все зависимые сущности
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(t => t.Country)
                .Include(t => t.Route.Purpose)
                .AsNoTracking()
                .ToList();
        } // OnGet
    } // class TravelsModel
}
