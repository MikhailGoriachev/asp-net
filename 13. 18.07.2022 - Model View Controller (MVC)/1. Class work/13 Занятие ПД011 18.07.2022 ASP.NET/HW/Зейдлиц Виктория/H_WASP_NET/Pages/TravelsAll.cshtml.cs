using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace H_WASP_NET.Pages
{
    [IgnoreAntiforgeryToken]

    public class TravelsAllModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // ссылка на коллекцию сущностей - таблицу из БД
        public List<Travel>? Travels { get; private set; }

        // Список цели поездки
        // ввода параметров запроса 1
        public SelectList NamePurpose{ get; private set; } = null!;

        // Список стран назначения
        // ввода параметров запроса 4
        public SelectList NameCountry{ get; private set; } = null!;


        // получение доступа к БД при помощи конструктора
        // с внедрением зависимости
        public TravelsAllModel(ApplicationContext db)
        {
            _context = db;
        } // TravelsAllModel

        // по GET-зпросу отдать клиенту разметку с выведенной
        // коллекцией записей из таблиц БД
        public void OnGet()
        {
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(r => r.Country)
                .Include(t => t.Route)
                .ThenInclude(r => r.PurposeTravel)
                .AsNoTracking()
                .ToList();

            // получить цели поездки 
            NamePurpose = new SelectList(_context.PurposeTravels.AsNoTracking().Select(c => c.NamePurpTravel));

            // получить страны
            NameCountry = new SelectList(_context.Countries.AsNoTracking().Select(c => c.NameCountry));
        } // OnGet

        // обработчик клика по кнопкам submit
        // удаление записи по идентификатору
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var travel = await _context.Travels.FindAsync(id);

            if (travel != null)
            {
                _context.Remove(travel);
                await _context.SaveChangesAsync();
            } // if

            return RedirectToPage();
        } // OnPostDeleteAsync

    } // class TravelsAllModel
}
