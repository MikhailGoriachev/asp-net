using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace H_WASP_NET.Pages
{
    [IgnoreAntiforgeryToken]

    public class TravelEditModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        [BindProperty] public Travel? Travel { get; set; }

        // создание объекта для выпадающего списка
        public SelectList NameCountry { get; private set; } = null!;

        public SelectList NamePurpose { get; private set; } = null!;

        // конструктор с внедрением зависимости, доступ к БД
        public TravelEditModel(ApplicationContext db)
        {
            _context = db;
        } // TravelEditModel


        // по GET-запросу найти запись в таблице
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Travel = await _context.Travels.FindAsync(id);

            // получить цели поездки 
            NamePurpose = new SelectList(_context.PurposeTravels.AsNoTracking().Select(c => c.NamePurpTravel));

            // получить страны
            NameCountry = new SelectList(_context.Countries.AsNoTracking().Select(c => c.NameCountry));
            return Page();
        } // OnGetAsync


        // обработка данных, полученных из формы
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Travels.Update(Travel);
            await _context.SaveChangesAsync();

            // переход на страницу отображения всех записей
            return RedirectToPage("TravelsAll");
        } // OnPostAsync

    } // class TravelEditModel
}
