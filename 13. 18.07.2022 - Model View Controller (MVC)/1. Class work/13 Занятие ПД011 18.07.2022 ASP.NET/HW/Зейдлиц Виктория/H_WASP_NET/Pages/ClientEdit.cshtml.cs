using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace H_WASP_NET.Pages
{
    [IgnoreAntiforgeryToken]
    public class ClientEditModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        [BindProperty] public Client? Client { get; set; }


        // конструктор с внедрением зависимости, доступ к БД
        public ClientEditModel(ApplicationContext db)
        {
            _context = db;
        } // ClientEditModel


        // по GET-запросу найти запись в таблице, отправить HTML-разметку формы клиенту 
        public async Task<IActionResult> OnGetAsync(int id)
        {

           Client = await _context.Clients.FindAsync(id);

            return Page();
        } // OnGetAsync


        // обработка данных, полученных из формы
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Clients.Update(Client!);
            await _context.SaveChangesAsync();

            // переходим на страницу отображения всех записей таблицы
            return RedirectToPage("ClientsAll");
        } // OnPostAsync
    }
}
