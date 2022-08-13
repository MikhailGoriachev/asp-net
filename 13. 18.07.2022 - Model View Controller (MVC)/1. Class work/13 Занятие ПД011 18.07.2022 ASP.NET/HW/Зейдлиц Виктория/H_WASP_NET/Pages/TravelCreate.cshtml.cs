using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace H_WASP_NET.Pages
{
    [IgnoreAntiforgeryToken]
    public class TravelCreateModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // поле дл€ прив€зки к элементам формы
        [BindProperty] public Travel? Travel { get; set; }

        // создание объекта дл€ выпадающего списка
        public SelectList Countries { get; private set; } = null!;
        public SelectList NamePurpose { get; private set; } = null!;


        // конструктор с внедрением зависимости дл€ получени€
        // доступа к бпзе данных
        public TravelCreateModel(ApplicationContext db)
        {
            _context = db;
        } // TravelCreateModel


        // отдать клиенту разметку с формой ввода
        public void OnGet()
        {
            // получить список значений дл€ формировани€ выпадающего списка
            Countries = new SelectList(_context.Countries.AsNoTracking().ToList(), "Id", "NameCountry");

            NamePurpose = new SelectList(_context.PurposeTravels.AsNoTracking().ToList(), "Id", "NamePurpTravel");
        } // OnGet


        // обработка формы ввода с данными созданного пользовател€
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Travels.Add(Travel);
            await _context.SaveChangesAsync();

            // переходим на страницу отображени€ всех записей таблицы
            return RedirectToPage("TravelsAll");
        } // OnPostAsync

    } // class TravelCreateModel
}
