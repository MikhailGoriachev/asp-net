using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace H_WASP_NET.Pages
{
    [IgnoreAntiforgeryToken]
    public class ClientCreateModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // поле для привязки к элементам формы
        [BindProperty] public Client? Client { get; set; }


        // конструктор с внедрением зависимости для получения
        // доступа к бaзе данных
        public ClientCreateModel(ApplicationContext db)
        {
            _context = db;
        } // ClientCreateModel


        // отдать клиенту разметку с формой ввода
        public void OnGet() { } // OnGet


        // обработка формы ввода с данными созданного клиента
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Clients.Add(Client);
            await _context.SaveChangesAsync();


            // переходим на страницу отображения всех записей таблицы
            return RedirectToPage("ClientsAll");
        } // OnPostAsync

    } // class ClientCreateModel
}
